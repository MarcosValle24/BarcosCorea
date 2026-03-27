using UnityEngine;

public class TimeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIHandlerTimeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private float maxTime;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues();
        player.OnStopped.AddListener(HandlePlayerStopped);
        GameManager.instance.gameResult = GameResult.Playing;
        GameManager.instance.gameMode = Mode.TimeMode;
        GameManager.instance.isPlaying = true;
        dockManager.ActivateRandomDock();
    }
    void SetValues()
    {
        timer = maxTime;
    }
    void HandlePlayerStopped()
    {
        uiHandler.ShowPanel(GameResult.Win);
        GameManager.instance.gameResult = GameResult.Win;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameResult != GameResult.Playing || !GameManager.instance.isPlaying) return;

        if (GameManager.instance.gameMode == Mode.TimeMode && !player.arrived)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            uiHandler.UpdateTimer(minutes.ToString("00") + ":" + seconds.ToString("00"));

            if (timer <= 0)
            {
                uiHandler.ShowPanel(GameResult.Lose);
                GameManager.instance.gameResult = GameResult.Lose;
            }
        }
        
    }
    public void QuitToMainMenu()
    {
        GameManager.instance.OpenMainMenu();
    }
    public void RestartGame()
    {
        SetValues();
        player.RestartPosition();
        dockManager.DeactivateDocks();
        dockManager.ActivateRandomDock();
        uiHandler.RemoveAllPanels();
        GameManager.instance.gameResult = GameResult.Playing;
    }
}
