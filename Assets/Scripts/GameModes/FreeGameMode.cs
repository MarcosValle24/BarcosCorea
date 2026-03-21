using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private int fishRecolected;
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
    }
    void SetValues()
    {
        fishRecolected = 0;
    }
    void HandlePlayerStopped()
    {
        dockManager.ActivateRandomDock(); 
        player.RestartPosition();
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
