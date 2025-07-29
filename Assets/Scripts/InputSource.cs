using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputSource : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Hand _hand;
    [SerializeField] private float _dampingSpeed = 0.05f;

    public bool IsHoldingObject => _isHoldingObject;

    public event Action<InteractableObject> ObjectPressed;
    public event Action ObjectDraggedOnFace;

    private PlayerInput _playerInput;
    private RectTransform _draggingObject;
    private Vector3 velocity = Vector3.zero;
    private bool _isHoldingObject = false;
    private bool _isApplyingObject = false;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _draggingObject = transform as RectTransform;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Gameplay.TouchPress.performed += OnTouchPerformed;

        _hand.PickedUpObject += () => _isHoldingObject = true;
        _hand.ApplyingObject += () => _isApplyingObject = true;
        _hand.ObjectApplied += (obj) => _isHoldingObject = false;
        _hand.ObjectApplied += (obj) => _isApplyingObject = false;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Gameplay.TouchPress.performed -= OnTouchPerformed;

        _hand.PickedUpObject -= () => _isHoldingObject = true;
        _hand.ApplyingObject -= () => _isApplyingObject = true;
        _hand.ObjectApplied -= (obj) => _isHoldingObject = false;
        _hand.ObjectApplied -= (obj) => _isApplyingObject = false;
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        if (!_isApplyingObject)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(_playerInput.Gameplay.TouchPosition.ReadValue<Vector2>());

            if (!_isHoldingObject)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.up, 0.1f);
                if (!!hit)
                {
                    if (hit.collider.gameObject.TryGetComponent(out InteractableObject obj))
                    {
                        ObjectPressed?.Invoke(obj);
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isHoldingObject)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingObject, eventData.position,
                eventData.pressEventCamera, out var globalMousePosition))
            {
                _draggingObject.position = Vector3.SmoothDamp(_draggingObject.position, globalMousePosition, ref velocity, _dampingSpeed);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isApplyingObject && _isHoldingObject)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.up, 0.1f);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out Face face))
                {
                    ObjectDraggedOnFace?.Invoke();
                }
            }
        }
    }
}
