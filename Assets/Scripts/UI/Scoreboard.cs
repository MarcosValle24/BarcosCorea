using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class ScoreEntry
{
    public string name;
    public int score;
}
[System.Serializable]
public class ScoreboardList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}
public class Scoreboard : MonoBehaviour
{
    private string filePath;
    public ScoreboardList scoreboard = new ScoreboardList();
    public TextMeshProUGUI scoreBoardText;

    [Header("Tests")]
    public string testName;
    public int testScore;
    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        LoadScoreboard();
    }
    public void AddScore(string playerName, int score)
    {
        ScoreEntry entry = new ScoreEntry();
        entry.name = playerName;
        entry.score = score;

        scoreboard.scores.Add(entry);
        SortScoreBoard();
        ShowScoreBoard();
        SaveScoreboard();
    }
    void SaveScoreboard()
    {
        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(filePath, json);
    }
    void LoadScoreboard()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreboard = JsonUtility.FromJson<ScoreboardList>(json);
            ShowScoreBoard();
        }
        else
        {
            scoreboard = new ScoreboardList();
            ShowScoreBoard();
        }

    }
    void SortScoreBoard()
    {
        scoreboard.scores = scoreboard.scores.OrderByDescending(s => s.score).ToList();
    }
    void ShowScoreBoard()
    {
        scoreBoardText.text = "";
        for (int i = 0; i < scoreboard.scores.Count; i++)
        {
            ScoreEntry entry = scoreboard.scores[i];
            scoreBoardText.text += $"{i + 1}- {entry.name}   {entry.score}\n";
        }
    }
    public void EareseScoreBoard()
    {
        scoreboard.scores.Clear();
        SortScoreBoard();
        ShowScoreBoard();
        SaveScoreboard();
    }
}

[CustomEditor(typeof(Scoreboard))]
public class ScoreboardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Scoreboard sb = (Scoreboard)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("TestAdd Score"))
        {
            sb.AddScore(sb.testName, sb.testScore);
        }

        if (GUILayout.Button("Borrar Scoreboard"))
        {
            sb.EareseScoreBoard();
        }
    }
}