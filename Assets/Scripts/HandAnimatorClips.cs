using UnityEngine;

[System.Serializable]
public class HandAnimatorClips : MonoBehaviour
{
    [Header("Cream Clips")]
    [SerializeField] private AnimationClip _pickUpCreamClip;
    [SerializeField] private AnimationClip _returnCreamClip;
    [SerializeField] private AnimationClip _returnCreamHandClip;
    [Header("Brush Clips")]
    [SerializeField] private AnimationClip[] _pickUpBrushClips;
    [SerializeField] private AnimationClip _returnBrushClip;
    [SerializeField] private AnimationClip _returnBrushHandClip;
    [Header("Pomade Clips")]
    [SerializeField] private AnimationClip[] _pickUpPomadeClips;
    [SerializeField] private AnimationClip[] _returnPomadeClips;
    [SerializeField] private AnimationClip[] _returnPomadeHandClips;
    [Header("Common Clips")]
    [SerializeField] private AnimationClip _applyObjectClip;

    public AnimationClip PickUpCreamClip => _pickUpCreamClip;
    public AnimationClip ReturnCreamClip => _returnCreamClip;
    public AnimationClip ReturnCreamHandClip => _returnCreamHandClip;
    public AnimationClip[] PickUpBrushClips => _pickUpBrushClips;
    public AnimationClip ReturnBrushClip => _returnBrushClip;
    public AnimationClip ReturnBrushHandClip => _returnBrushHandClip;
    public AnimationClip[] PickUpPomadeClips => _pickUpPomadeClips;
    public AnimationClip[] ReturnPomadeClips => _returnPomadeClips;
    public AnimationClip[] ReturnPomadeHandClips => _returnPomadeHandClips;

    public AnimationClip ApplyObjectClip => _applyObjectClip;
}