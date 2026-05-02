using System.Collections;
using UnityEngine;

public class TimeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIHandlerTimeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private GameModeFader fader;
    [SerializeField] private float maxTime;
    [SerializeField] private bool playerArrived;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetValues();
        playerEvents.OnStopped.AddListener(PlayerArrived);
        playerEvents.OnCrashed.AddListener(OnPlayerCrashed);
        GameManager.instance.OnGameResultChanged.AddListener(OnGameResultChanged);
    }
    void ResetValues()
    {
        playerMovement.RestartPosition();
        fader.FadeOut();
        timer = maxTime;
        uiHandler.RemoveAllPanels();
        dockManager.DeactivateDocks();
        dockManager.ActivateRandomDock();
        playerArrived = false;
        GameManager.instance.SetResult(GameResult.Playing);
    }
    void PlayerArrived()
    {
        playerArrived = true;
        GameManager.instance.SetResult(GameResult.Win);
        fader.FadeIn();

    }
    void Lose()
    {
        GameManager.instance.SetResult(GameResult.Lose);
        fader.FadeIn();
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
        GameManager.instance.OpenMainMenu();
    }
    public void RestartGame()
    {
        ResetValues();
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
}
