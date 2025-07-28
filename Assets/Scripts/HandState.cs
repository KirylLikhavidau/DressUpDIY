using UnityEngine;

public abstract class HandState : MonoBehaviour
{
    protected Hand Hand;

    public HandState(Hand hand)
    {
        Hand = hand;
    }

    public abstract void DoAction();
}
