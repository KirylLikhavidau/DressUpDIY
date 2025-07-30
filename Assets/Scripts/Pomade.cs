using UnityEngine;

[System.Serializable]
public class Pomade : InteractableObject 
{
    [SerializeField] private Sprite _pomadeColor;
    [SerializeField] private int _pomadeColorIndex;

    public Sprite PomadeColor => _pomadeColor;
    public int PomadeColorIndex => _pomadeColorIndex;
}
