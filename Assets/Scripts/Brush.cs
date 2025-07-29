using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Brush : InteractableObject
{
    [SerializeField] private List<ColorSection> _colorSections;
    [SerializeField] private bool _isForShadows = false;

    public Sprite CurrentSprite => _currentSprite;
    public bool IsForShadows => _isForShadows;

    private Sprite _currentSprite;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var section in _colorSections)
        {
            section.Clicked += (sprite, index) => 
            {
                _currentSprite = sprite;
            };
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var section in _colorSections)
        {
            section.Clicked -= (sprite, index) =>
            {
                _currentSprite = sprite;
            };
        }
    }
}
