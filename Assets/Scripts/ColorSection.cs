using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorSection : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Button _sectionButton;
    [SerializeField] private int _sectionIndex;

    public event Action<Sprite, int> Clicked;

    private void OnEnable()
    {
        _sectionButton.onClick.AddListener(() => Clicked.Invoke(_sprite, _sectionIndex));
    }

    private void OnDisable()
    {
        _sectionButton.onClick.RemoveListener(() => Clicked.Invoke(_sprite, _sectionIndex));
    }
}
