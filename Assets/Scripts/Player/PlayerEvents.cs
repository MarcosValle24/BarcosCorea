using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public UnityEvent OnArrivedWithFish;
    public UnityEvent OnCrashed;
    public UnityEvent OnFishRecolected;
    public UnityEvent OnStopped;
    public UnityEvent OnResetPos;
    public void ArrivedWithFish() => OnArrivedWithFish?.Invoke();
    public void Crashed() => OnCrashed?.Invoke();
    public void FishRecolected() => OnFishRecolected?.Invoke();
    public void PlayerStopped() => OnStopped?.Invoke();
    public void ResetPlayerPosition() => OnResetPos?.Invoke();
}
