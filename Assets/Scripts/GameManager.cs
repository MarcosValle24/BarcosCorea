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
    [SerializeField] private FreeGameMode freeGameMode;
    [SerializeField] private TimeGameMode timeGameMode;
    [SerializeField] private float maxTime;

    public Mode gameMode;
    public bool hasFish;
    public UnityEvent StartFreeTimeGame;
    public UnityEvent StartTimeGame;
    public UnityEvent FinishGame;

    public bool isPlaying = false;

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

    public void BeginPlay()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        isPlaying = false;
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
    public void FreeGameMode()
    {
        gameMode = Mode.FreeTime;
        StartFreeTimeGame?.Invoke();
        BeginPlay();
    }
    public void TimeGameMode()
    {
        gameMode = Mode.TimeMode;
        StartTimeGame?.Invoke();
        BeginPlay();
    }
}
