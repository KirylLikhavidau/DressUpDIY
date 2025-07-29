using UnityEngine;
using UnityEngine.UI;

public class Face : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private Image _mouth;
    [SerializeField] private Image _shadow;
    [SerializeField] private Image _blush;
    [SerializeField] private Image _acne;

    private void OnEnable()
    {
        _hand.ObjectApplied += (obj) => ChangeFacePart(obj);
    }

    private void ChangeFacePart(InteractableObject obj)
    {
        if (obj is Cream)
            _acne.enabled = false;
        else if (obj is Brush)
        {
            if (!(obj as Brush).IsForShadows)
            {
                _blush.enabled = true;
                _blush.sprite = (obj as Brush).CurrentSprite;
            }
            else
            {
                _shadow.enabled = true;
                _shadow.sprite = (obj as Brush).CurrentSprite;
            }
        }
        else if (obj is Pomade)
        {
            _mouth.enabled = true;
            _mouth.sprite = (obj as Pomade).PomadeColor;
        }
    }
}
