using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField]private List<GameObject> Fish = new List<GameObject>();
    [SerializeField] private DockManagerFreeMode dockManager;
    void Start()
    {
        player.OnStopped.AddListener(HandlePlayerStopped);
    }
    void HandlePlayerStopped()
    {
        dockManager.ActivateRandomDock(); 
        player.RestartPosition();
    }
}
