using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private CanvasGroup faderPanel;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        faderPanel.alpha = 0f;
        faderPanel.DOFade(1f, fadeDuration);
    }
    public void ChangeScene()
    {
      StartCoroutine(OpenGameMode());
       
    }
 

    IEnumerator OpenGameMode()
    {
        yield return faderPanel.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad).WaitForCompletion();

        if (GameManager.instance.gameMode == Mode.TimeMode)
        {
            //transitionAnimator.SetTrigger("Fade");
            //yield return new WaitForSeconds(2f);
            GameManager.instance.OpenTimeGameMode();
        }
        else
        {
            //transitionAnimator.SetTrigger("Fade");
            //yield return new WaitForSeconds(2f);
            GameManager.instance.OpenFreeGameMode();
            
        }
        faderPanel.blocksRaycasts = true;
    }
    
}
