using System;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerStateType
{
    Moving,
    Paused,
    Stopping,
    Rotating,
    Crashed,
}
public class PlayerState : MonoBehaviour
{
    public PlayerStateType currentState {  get; private set; }
    public UnityEvent <PlayerStateType> OnPlayerStateChanged;
    public bool HasFish { get; private set; }

    public void SetState(PlayerStateType newState)
    {
        if(currentState == newState) return;

        currentState = newState;
        OnPlayerStateChanged?.Invoke(currentState);
    }
    public bool IsPlayerState(PlayerStateType currentType)
    {
        return currentState == currentType;
    }
    public void ResetState()
    {
        SetState(PlayerStateType.Moving);
        HasFish = false;
    }
}
