using System.Threading.Tasks;
using UnityEngine;

public class Cream : InteractableObject
{
    [SerializeField] private Hand _hand;

    private Canvas _parentCanvas;
    private Vector2 _defaultPosition;
    
    public Vector2 DefaultPosition => _defaultPosition;

    private void Awake()
    {
        _parentCanvas = gameObject.GetComponentInParent<Canvas>();
        _defaultPosition = (transform as RectTransform).anchoredPosition;
    }

    private void OnEnable()
    {
        _hand.ResetObjectPosition += ResetPosition;
    }

    private void OnDisable()
    {
        _hand.ResetObjectPosition -= ResetPosition;
    }

    private void ResetPosition()
    {
        transform.SetParent(_parentCanvas.transform);
        (transform as RectTransform).anchoredPosition = _defaultPosition;
    }
}
