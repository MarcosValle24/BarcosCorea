using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField]private List<GameObject> Fish = new List<GameObject>();
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
    }
    void OnDisable()
    {
        player.OnStopped.RemoveListener(HandlePlayerStopped);
        GameManager.instance.StartFreeTimeGame.RemoveListener(RemoveTimeUI);
    }
    void RemoveTimeUI()
    {
        //UIHandler.instance.timerText.gameObject.SetActive(false);
    }
    void HandlePlayerStopped()
    {
        DockManager.instance.ActivateRandomDock(); 
        player.RestartPosition();
    }
}
