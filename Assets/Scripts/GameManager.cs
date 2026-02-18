using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static  GameManager instance;
    
    [SerializeField]private List<GameObject> docks;
    [SerializeField] private float maxTime;

    
    private bool isPlaying = false;
    private float timer = 0;
    private GameObject currentDock;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeginPlay();
    }

    public void BeginPlay()
    {
        currentDock = docks[Random.Range(0, docks.Count)]; 
        currentDock.SetActive(true);
        isPlaying = true;
        timer = maxTime;
    }

    public void GameOver()
    {
        isPlaying = false;
        foreach(GameObject dock in docks)
        { 
            dock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        { 
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            
            UIHandler.instance.UpdateTimer(minutes.ToString("00")+ ":"+ seconds.ToString("00"));
            
            if(timer <=0)
            {
                GameOver();
            }
        }
    }
}
