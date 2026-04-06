using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIHandlerFreeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private FishManager fishManager;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] public int fishRecolected;
    [SerializeField] public string playerName;
    void Start()
    {
        playerEvents.OnStopped.AddListener(ArrivedWithFish);
        playerEvents.OnCrashed.AddListener(Lose);
        playerEvents.OnFishRecolected.AddListener(RecolectedFish);
        scoreboard.OnEnterScore.AddListener(uiHandler.ShowScorePanel);
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
        SpawnFish();
    }
    private void OnDisable()
    {

    }
    void SetValues()
    {
        fishRecolected = 0;
    }
    public void QuitToMainMenu()
    {
        GameManager.instance.OpenMainMenu();
    }
    public void RestartGame()
    {
        SetValues();
        playerMovement.RestartPosition();
        dockManager.DeactivateDocks();
        uiHandler.RemoveAllPanels();
        SpawnFish();
        GameManager.instance.gameResult = GameResult.Playing;
    }
    void SpawnFish()
    {
        fishManager.SpawnFish();
    }
    void RemoveFishes()
    {
        fishManager.DestroyAllFishes();
    }
    void ArrivedWithFish()
    {
        Debug.Log("Arrived");
        SpawnFish();
        fishRecolected++;
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
        StopCoroutine(DeactivateDockAfterPlayingFX());
        StartCoroutine(DeactivateDockAfterPlayingFX());
    }
    void Lose()
    {
        RemoveFishes();
        uiHandler.EnterScorePanel();
        uiHandler.ShowFishRecolected(fishRecolected.ToString());
        GameManager.instance.SetResult(GameResult.Lose);
    }
    void RecolectedFish()
    {
        dockManager.ActivateRandomDock();
    }
    IEnumerator DeactivateDockAfterPlayingFX()
    {
        yield return new WaitForSeconds(1f);
        dockManager.DeactivateDocks();
    }
}
