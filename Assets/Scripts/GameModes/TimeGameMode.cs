using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TimeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIHandlerTimeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private float maxTime;
    [SerializeField] private bool playerArrived;

    [SerializeField] private CanvasGroup fadeOut;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEvents.OnStopped.AddListener(PlayerArrived);
        playerEvents.OnCrashed.AddListener(OnPlayerCrashed);
        GameManager.instance.OnGameResultChanged.AddListener(OnGameResultChanged);
        ResetValues();
    }
    void ResetValues()
    {
        //StopAllCoroutines();
        StartCoroutine(FadeIn());
    }
    void PlayerArrived()
    {
        playerArrived = true;
        GameManager.instance.SetResult(GameResult.Win);
        playerMovement.Stop();
        QuitToMainMenu();

    }
    void Lose()
    {
        GameManager.instance.SetResult(GameResult.Lose);
        playerMovement.Stop();
        QuitToMainMenu();
    }
    void Update()
    {
        if (GameManager.instance.gameResult != GameResult.Playing)
            return;

        if (playerArrived == true)
            return;

        Clock();    
    }
    public void QuitToMainMenu()
    {
        StartCoroutine(FadeToQuitMainMenu());
    }
    public void RestartGame()
    {
        StartCoroutine(FadeOut());
    }
    void OnGameResultChanged(GameResult result)
    {
        uiHandler.ShowPanel(result);
    }
    private void Clock()
    {
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        uiHandler.UpdateTimer(minutes.ToString("00") + ":" + seconds.ToString("00"));

        if (timer <= 0)
        {
            uiHandler.ShowPanel(GameResult.Lose);
            GameManager.instance.SetResult(GameResult.Lose);
            QuitToMainMenu();
        }
    }
    private void OnPlayerCrashed()
    {
        StartCoroutine(RecoverFromCrash());
    }
    IEnumerator RecoverFromCrash()
    {
        yield return new WaitForSeconds(.3f);

        playerMovement.RecoverFromCrash();
    }
    IEnumerator FadeIn()
    {
        timer = maxTime;
        playerArrived = true;
        playerMovement.RestartPosition();
        uiHandler.RemoveAllPanels();
        yield return fader.DOFade(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        playerArrived = false;
        dockManager.DeactivateDocks();
        dockManager.ActivateRandomDock();
        GameManager.instance.SetResult(GameResult.Playing);
    }

    IEnumerator FadeOut()
    {
        yield return fader.DOFade(1f, 0f).SetEase(Ease.InOutQuad).WaitForCompletion();
        ResetValues();
    }
    IEnumerator FadeToQuitMainMenu()
    {
        yield return new WaitForSeconds(3);
        yield return fadeOut.DOFade(1,1).WaitForCompletion();
        GameManager.instance.OpenMainMenu();

    }
}
