using System.Collections;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    
    public void ChangeScene(bool time)
    {
      StartCoroutine(OpenGameMode(time));
       
    }
 

    IEnumerator OpenGameMode(bool time)
    {
        if (time)
        {
            transitionAnimator.SetTrigger("Fade");
            yield return new WaitForSeconds(2f);
            GameManager.instance.OpenTimeGameMode();
        }
        else
        {
            transitionAnimator.SetTrigger("Fade");
            yield return new WaitForSeconds(2f);
            GameManager.instance.OpenFreeGameMode();
            
        }
    }
    
}
