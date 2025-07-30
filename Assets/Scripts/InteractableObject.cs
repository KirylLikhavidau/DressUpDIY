using UnityEngine;

[System.Serializable]
public abstract class InteractableObject : MonoBehaviour 
{
    [SerializeField] protected Hand Hand;
    [SerializeField] private GameObject _parent;

    private Vector2 _defaultPosition;

    public Vector2 DefaultPosition => _defaultPosition;

    private void Awake()
    {
        _defaultPosition = (transform as RectTransform).anchoredPosition;
    }

    protected virtual void OnEnable()
    {
        Hand.ResetObjectPosition += ResetPosition;
    }

    protected virtual void OnDisable()
    {
        Hand.ResetObjectPosition -= ResetPosition;
    }

    private void ResetPosition()
    {
        transform.SetParent(_parent.transform);
        (transform as RectTransform).anchoredPosition = _defaultPosition;
    }
}
