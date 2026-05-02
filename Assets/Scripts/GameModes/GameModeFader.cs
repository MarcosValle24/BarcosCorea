using DG.Tweening;
using UnityEngine;

public class GameModeFader : MonoBehaviour
{
    // [SerializeField] private Animator transitionAnimator;
    [SerializeField] private CanvasGroup faderPanel;
    [SerializeField] private float fadeDuration = 1f;

    public void FadeIn()
    {
        faderPanel.alpha = 0f;
        faderPanel.DOFade(1f, fadeDuration);
    }
    public void FadeOut()
    {
        faderPanel.alpha = 1f;
        faderPanel.DOFade(0f, fadeDuration);
    }
}