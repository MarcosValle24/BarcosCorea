using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameResult
{
    Win,
    Lose,
    Pause
}
public enum Mode
{
    FreeTime,
    TimeMode
}
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;
    
    [SerializeField]private List<GameObject> docks;
    [SerializeField] private float maxTime;
    Mode gameMode;
    public UnityEvent startGame;

    public bool isPlaying = false;
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
        //BeginPlay();
    }

    public void BeginPlay()
    {
        currentDock = docks[Random.Range(0, docks.Count)]; 
        currentDock.SetActive(true);
        isPlaying = true;
        timer = maxTime;
        startGame.Invoke();
    }

    public void GameOver()
    {
        isPlaying = false;
        foreach(GameObject dock in docks)
        { 
            dock.SetActive(false);
        }
        UIHandler.instance.ShowPanel(GameResult.Lose);
    }
    public void Pause(bool isPaused) 
    {
        if (isPaused)
        {
            isPlaying = false;
            UIHandler.instance.ShowPanel(GameResult.Pause);
        }
        else
        { 
            isPlaying = true;
            UIHandler.instance.RemoveAllPanels();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            switch (gameMode)
            {
                case Mode.FreeTime:
                    break;
                case Mode.TimeMode:
                    timer -= Time.deltaTime;
                    int minutes = Mathf.FloorToInt(timer / 60f);
                    int seconds = Mathf.FloorToInt(timer % 60f);

                    UIHandler.instance.UpdateTimer(minutes.ToString("00") + ":" + seconds.ToString("00"));

                    if (timer <= 0)
                    {
                        GameOver();
                    }
                    break;
            }
        }
    }
    public void FreeGameMode()
    {
        gameMode = Mode.FreeTime;
        BeginPlay();
    }
    public void TimeGameMode()
    {
        gameMode = Mode.TimeMode;
        BeginPlay();
    }
}
