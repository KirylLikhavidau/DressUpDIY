using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemActivitySwitcher : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private InputSource _inputSource;
    [SerializeField] private List<Button> _bookButtons;
    [SerializeField] private List<Button> _colorSections;
    [SerializeField] private List<BoxCollider2D> _pomades;
    [SerializeField] private List<BoxCollider2D> _brushes;
    [SerializeField] private BoxCollider2D _cream;

    private Button _disabledBookButton;

    private void OnEnable()
    {
        _inputSource.ObjectPressed += (obj) =>
        {
            foreach (var pomade in _pomades)
            {
               pomade.enabled = false;
            }

            obj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        };
        _hand.PickedUpObject += DisableAllStuff;
        _hand.ObjectReturned += EnableAllStuff;
    }

    private void OnDisable()
    {
        _inputSource.ObjectPressed -= (obj) =>
        {
            foreach (var pomade in _pomades)
            {
                pomade.enabled = false;
            }

            obj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        };
        _hand.PickedUpObject -= DisableAllStuff;
        _hand.ObjectReturned -= EnableAllStuff;
    }

    private void DisableAllStuff()
    {
        foreach (var button in _bookButtons)
            if (button.interactable == false)
                _disabledBookButton = button;
            else
                button.interactable = false;

        foreach (var section in _colorSections)
            section.interactable = false;

        foreach (var pomade in _pomades)
            pomade.enabled = false;

        foreach (var brush in _brushes)
            brush.enabled = false;

        _cream.enabled = false;
    }

    private void EnableAllStuff()
    {
        foreach (var button in _bookButtons)
            button.interactable = true;

        _disabledBookButton.interactable = false;

        foreach (var section in _colorSections)
            section.interactable = true;

        foreach (var pomade in _pomades)
            pomade.enabled = true;

        foreach (var brush in _brushes)
            brush.enabled = true;

        _cream.enabled = true;
    }
}