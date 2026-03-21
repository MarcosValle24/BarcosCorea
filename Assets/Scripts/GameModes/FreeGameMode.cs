using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private FishManager fishManager;
    [SerializeField] private int fishRecolected;
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
        player.OnFishArrived.AddListener(ArrivedWithFish);
        GameManager.instance.isPlaying = true;
        GameManager.instance.gameResult = GameResult.Playing;
        GameManager.instance.gameMode = Mode.FreeTime;
    }
    private void OnDisable()
    {
        player.OnFishArrived.RemoveListener(ArrivedWithFish);
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
        SpawnFish();
        dockManager.ActivateRandomDock();
        fishRecolected++;
    }

}
