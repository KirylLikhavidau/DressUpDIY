using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlushBrush : InteractableObject
{
    [SerializeField] private List<BlushSection> _blushSections;

    public Sprite CurrentSprite => _currentSprite;

    private Sprite _currentSprite;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var section in _blushSections)
        {
            section.Clicked += (sprite, index) => 
            {
                _currentSprite = sprite;
                DisableAllSections();
            };
        }

        Hand.ResetObjectPosition += EnableAllSections;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var section in _blushSections)
        {
            section.Clicked -= (sprite, index) =>
            {
                _currentSprite = sprite;
                DisableAllSections();
            };
        }

        Hand.ResetObjectPosition -= EnableAllSections;
    }

    private void DisableAllSections()
    {
        foreach (var section in _blushSections)
        {
            section.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private void EnableAllSections()
    {
        foreach (var section in _blushSections)
        {
            section.gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
