using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hand : MonoBehaviour
{
    [SerializeField] private InputSource _dragSource;
    [SerializeField] private Animator _handAnimator;

    public event Action PickedUpObject;
    public event Action ApplyingObject;
    public event Action<InteractableObject> ObjectApplied;
    public event Action ResetObjectPosition;

    private InteractableObject _pickedObject;
    private bool _isReturningObject;
    private Vector3 _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    private void OnEnable()
    {
        _dragSource.ObjectPressed += (obj) => PickUpObject(obj);
        _dragSource.ObjectDraggedOnFace += ApplyObjectToFace;
    }

    private void OnDisable()
    {
        _dragSource.ObjectPressed -= (obj) => PickUpObject(obj);
        _dragSource.ObjectDraggedOnFace -= ApplyObjectToFace;
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isReturningObject)
        {
            if (collision.TryGetComponent(out InteractableObject obj))
            {
                await Task.Delay(200);
                obj.transform.SetParent(transform);
            }
        }
    }

    private async void PickUpObject(InteractableObject obj)
    {
        _handAnimator.Play("PickUpCream");

        await Task.Delay(1600);//Поменять

        _pickedObject = obj;

        _handAnimator.enabled = false;
        PickedUpObject.Invoke();
    }

    private async void ApplyObjectToFace()
    {
        _handAnimator.enabled = true;

        ApplyingObject.Invoke();

        if (_pickedObject is Cream)
            _handAnimator.Play("ApplyCreamToFace");

        await Task.Delay(1600);//поменять

        ObjectApplied.Invoke(_pickedObject);

        ReturnHandToStart();
    }

    private async void ReturnHandToStart()
    {
        _isReturningObject = true;

        if (_pickedObject is Cream)
            _handAnimator.Play("ReturnCream");

        await Task.Delay(1600);

        ResetObjectPosition.Invoke();

        if (_pickedObject is Cream)
            _handAnimator.Play("ReturnCreamHandToDefault");

        await Task.Delay(1600);

        _isReturningObject = false;
    }
}
