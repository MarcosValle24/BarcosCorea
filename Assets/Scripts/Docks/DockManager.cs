using System.Collections.Generic;
using UnityEngine;

public class DockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> docks;
    private GameObject currentDock;

    private void Start()
    {
        DeactivateDocks();
        ActivateRandomDock();
    }
    public void DeactivateDocks()
    {
        foreach (GameObject dock in docks)
        {
            dock.SetActive(false);
        }
        currentDock = null;
    }
    public void ActivateRandomDock()
    {
        DeactivateDocks();
        if (currentDock != null)
            currentDock.SetActive(false);

        currentDock = docks[Random.Range(0, docks.Count)];
        currentDock.SetActive(true);
    }
}
