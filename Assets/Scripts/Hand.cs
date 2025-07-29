using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hand : MonoBehaviour
{
    [SerializeField] private InputSource _inputSource;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private HandAnimatorClips _clips;
    [SerializeField] private List<ColorSection> _blushSections;
    [SerializeField] private List<ColorSection> _shadowSections;
    [SerializeField] private Brush _blushBrush;
    [SerializeField] private Brush _eyeBrush;

    public event Action ObjectReturned;
    public event Action PickedUpObject;
    public event Action ApplyingObject;
    public event Action<InteractableObject> ObjectApplied;
    public event Action ResetObjectPosition;

    private int _colorIndex;
    private InteractableObject _pickedObject;
    private int _animationTime;
    private bool _isReturningObject;
    private Vector3 _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    private void OnEnable()
    {
        _inputSource.ObjectPressed += (obj) => PickUpObject(obj);
        _inputSource.ObjectDraggedOnFace += ApplyObjectToFace;

        foreach (var section in _blushSections)
        {
            section.Clicked += (sprite, index) => 
            {
                _colorIndex = index;
                PickUpObject(_blushBrush);
            };
        }

        foreach (var section in _shadowSections)
        {
            section.Clicked += (sprite, index) =>
            {
                _colorIndex = index;
                PickUpObject(_eyeBrush);
            };
        }
    }

    private void OnDisable()
    {
        _inputSource.ObjectPressed -= (obj) => PickUpObject(obj);
        _inputSource.ObjectDraggedOnFace -= ApplyObjectToFace;

        foreach (var section in _blushSections)
        {
            section.Clicked -= (sprite, index) =>
            {
                _colorIndex = index;
                PickUpObject(_blushBrush);
            };
        }

        foreach (var section in _shadowSections)
        {
            section.Clicked -= (sprite, index) =>
            {
                _colorIndex = index;
                PickUpObject(_eyeBrush);
            };
        }
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isReturningObject && !_inputSource.IsHoldingObject)
        {
            if (collision.TryGetComponent(out InteractableObject obj))
            {
                if(obj is Cream)
                    await Task.Delay(200);
                else if (obj is Brush)
                    await Task.Delay(400);

                obj.transform.SetParent(transform);
            }
        }
    }

    private async void PickUpObject(InteractableObject obj)
    {
        if (obj is Cream)
        {
            PlayAnimation(_clips.PickUpCreamClip);
        }
        else if (obj is Brush)
        {
            PlayAnimation(_clips.PickUpBrushClips[_colorIndex]);
        }
        else if (obj is Pomade)
        {
            _colorIndex = (obj as Pomade).PomadeColorIndex;
            PlayAnimation(_clips.PickUpPomadeClips[_colorIndex]);
        }

        await Task.Delay(_animationTime);

        _pickedObject = obj;

        _handAnimator.enabled = false;
        PickedUpObject.Invoke();
    }

    private async void ApplyObjectToFace()
    {
        _handAnimator.enabled = true;

        ApplyingObject.Invoke();

        PlayAnimation(_clips.ApplyObjectClip);

        await Task.Delay(_animationTime);

        ObjectApplied.Invoke(_pickedObject);

        ReturnHandToStart();
    }

    private async void ReturnHandToStart()
    {
        _isReturningObject = true;

        if (_pickedObject is Cream)
            PlayAnimation(_clips.ReturnCreamClip);
        else if (_pickedObject is Brush)
            PlayAnimation(_clips.ReturnBrushClip);
        else if (_pickedObject is Pomade)
            PlayAnimation(_clips.ReturnPomadeClips[_colorIndex]);

        await Task.Delay(_animationTime);

        ResetObjectPosition.Invoke();

        if (_pickedObject is Cream)
            PlayAnimation(_clips.ReturnCreamHandClip);
        else if (_pickedObject is Brush)
            PlayAnimation(_clips.ReturnBrushHandClip);
        else if (_pickedObject is Pomade)
            PlayAnimation(_clips.ReturnPomadeHandClips[_colorIndex]);

        await Task.Delay(_animationTime);

        _isReturningObject = false;
        ObjectReturned?.Invoke();
    }

    private void PlayAnimation(AnimationClip clip)
    {
        _handAnimator.Play(clip.name);
        _animationTime = Convert.ToInt32(clip.length * 1000);
    }
}
