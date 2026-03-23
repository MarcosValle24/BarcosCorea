using UnityEditor;
using UnityEngine;

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
