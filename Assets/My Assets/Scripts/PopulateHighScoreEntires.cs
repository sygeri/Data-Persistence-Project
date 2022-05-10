using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateHighScoreEntires : MonoBehaviour
{
    private Serializer serializer;
    [SerializeField] private GameObject leaderBoard;

    // Start is called before the first frame update
    void Start()
    {
        serializer = GameObject.Find("Serializer").GetComponent<Serializer>();
        if (serializer != null)
        {
            for (int i = 0; i < Serializer.Instance.highScores.scoreList.Count; i++)
            {
                leaderBoard.transform.GetChild(i+1).FindDeepChild("Player Name").GetComponent<TextMeshProUGUI>().SetText(Serializer.Instance.highScores.scoreList[i].playerName);
                leaderBoard.transform.GetChild(i+1).FindDeepChild("Score").GetComponent<TextMeshProUGUI>().SetText(Serializer.Instance.highScores.scoreList[i].playerHighScore.ToString());
            }
        }
    }

}
