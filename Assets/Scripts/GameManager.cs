using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameResult
{
    Win,
    Lose,
    Pause,
    Playing,
}
public enum Mode
{
    FreeTime,
    TimeMode,
    Menus
}
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;

    public Mode gameMode;
    public GameResult gameResult;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public UnityEvent <Mode> OnGameModeChanged;
    public UnityEvent <GameResult> OnGameResultChanged;

    public void StartGameMode(Mode mode)
    {
        gameMode = mode;
        gameResult = GameResult.Playing;

        OnGameModeChanged?.Invoke(gameMode); 
        OnGameResultChanged?.Invoke(gameResult);
    }
    public void SetResult(GameResult result)
    {
        gameResult = result;
        OnGameResultChanged?.Invoke(gameResult);    
    }
    public void GameOver()
    {
        gameResult = GameResult.Lose;
    }
    public void Pause(bool isPaused) 
    {
        if (isPaused)
        {
            SetResult(GameResult.Pause);
        }
        else
        {
            SetResult(GameResult.Playing);
        }
    }
    public void OpenFreeGameMode()
    {
        StartGameMode(Mode.FreeTime);
        SceneManager.LoadScene("FreeMode");
    }
    public void OpenTimeGameMode()
    {
        StartGameMode(Mode.TimeMode);
        SceneManager.LoadScene("TimeMode");
    }
    public void OpenMainMenu()
    {
        //StartGameMode(Mode.Menus);
        SceneManager.LoadScene("Menu");
    }
    public void MenuChoserFreeGameMode()
    {
        gameMode = Mode.FreeTime;
        SceneManager.LoadScene("Menu");
    }
    public void MenuChoserTimeGameMode()
    {
        gameMode = Mode.TimeMode;
        SceneManager.LoadScene("Menu");
    }
}
