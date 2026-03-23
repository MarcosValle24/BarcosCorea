using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIHandlerFreeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private FishManager fishManager;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] public int fishRecolected;
    [SerializeField] public string playerName;
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
        player.OnCrashed.AddListener(Lose);
        GameManager.instance.isPlaying = true;
        GameManager.instance.gameResult = GameResult.Playing;
        GameManager.instance.gameMode = Mode.FreeTime;
        scoreboard.OnEnterScore.AddListener(uiHandler.ShowScorePanel);
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
    }
    private void OnDisable()
    {
        //player.OnStopped.RemoveListener(HandlePlayerStopped);
    }
    void SetValues()
    {
        fishRecolected = 0;
    }
    void HandlePlayerStopped()
    {
        ArrivedWithFish();
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
        GameManager.instance.isPlaying = true;
    }
    void SpawnFish()
    {
        fishManager.SpawnFish();
    }
    void ArrivedWithFish()
    {
        Debug.Log("Arrived");
        SpawnFish();
        dockManager.ActivateRandomDock();
        fishRecolected++;
        player.ResetAfterArrival();
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
    }
    void Lose()
    {
        uiHandler.EnterScorePanel();
        uiHandler.ShowFishRecolected(fishRecolected.ToString());
        GameManager.instance.gameResult = GameResult.Lose;
        GameManager.instance.isPlaying = false;

    }

}
