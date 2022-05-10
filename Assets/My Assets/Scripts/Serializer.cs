using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    // Inner class that can be serialized 
    [System.Serializable]
    public class HighScores
    {
        [System.Serializable]
        public class HighScoreEntry
        {
            public string playerName;
            public int playerHighScore;

            public HighScoreEntry()
            {
                playerName = "";
                playerHighScore = 0;
            }

            public HighScoreEntry(string name, int score)
            {
                playerName = name;
                playerHighScore = score;
            }
        }

        public List<HighScoreEntry> scoreList;
    }

    public static Serializer Instance;
    public HighScores highScores;
    public HighScores.HighScoreEntry myHighScore;

    public string CurrentPlayerName
    {
        get; set;
    }
    public int CurrentPlayerScore
    {
        get; set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // loads player's highscore that originates from previous game sessions
    private void LoadMyHighScore()
    {
        string path = Application.persistentDataPath + "/" + CurrentPlayerName + ".json";
        LoadJsonFromFile(ref myHighScore, path);
        CurrentPlayerScore = myHighScore.playerHighScore;

        //no file yet
        if (string.IsNullOrEmpty(myHighScore.playerName))
        {
            myHighScore.playerName = CurrentPlayerName;  // replace the empty string with the player name that was entered recently. Score is already initialized with 0
        }
    }

    // loads highscores that originates from previous game sessions
    private void LoadHighScoreEntries()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        LoadJsonFromFile(ref highScores, path);
    }

    // loads all highscore entry that originates from previous game sessions
    public void LoadHighScores()
    {
        LoadMyHighScore();
        LoadHighScoreEntries();
    }

    // can be used to update the player's highscore 
    private void UpdateMyHighScore()
    {
        if (myHighScore.playerHighScore < CurrentPlayerScore)
        {
            myHighScore.playerHighScore = CurrentPlayerScore;
        }
    }

    // keeps the highscore board up-to-date
    private void UpdateHighScores()
    {
        int index = 0;       

        for (int i = 0; i < highScores.scoreList.Count; i++)
        {           
            if (highScores.scoreList[i].playerHighScore > CurrentPlayerScore)
            {
                index++;
            }
        }
        highScores.scoreList.Insert(index, new HighScores.HighScoreEntry(myHighScore.playerName, CurrentPlayerScore));
    }

    // Keeps the list storing top 10
    private void CutScoreList()
    {
        if (highScores.scoreList.Count > 10)
        {
            highScores.scoreList.RemoveAt(10);
        }
    }

    // updates and saves highscores to files
    public void SaveHighScores()
    {
        UpdateMyHighScore();
        WriteJsonToFile<HighScores.HighScoreEntry>(myHighScore,CurrentPlayerName);
        UpdateHighScores();
        CutScoreList();
        WriteJsonToFile<HighScores>(highScores, "highscores");
    }

    // generic method that converts an object to json then writes the newly created json to a file
    private void WriteJsonToFile<T>(T writable, string fileName)
    {
        string json = JsonUtility.ToJson(writable);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", json);
    }

    // generic method that loads a Json to a string then converts the newly created json string to an object
    private void LoadJsonFromFile<T>(ref T readable, string path)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            readable = JsonUtility.FromJson<T>(json);
        }
    }
}
