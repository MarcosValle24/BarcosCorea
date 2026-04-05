using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public UnityEvent OnArrivedNoFish;
    public UnityEvent OnArrivedWithFish;
    public UnityEvent OnCrashed;
    public UnityEvent OnFishRecolected;
    
    public void ArrivedWithFish() => OnArrivedWithFish?.Invoke();
    public void ArrivedNoFish() => OnArrivedNoFish?.Invoke();
    public void Crashed() => OnCrashed?.Invoke();
    public void FishRecolected() => OnFishRecolected?.Invoke();
}
