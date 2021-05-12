using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveInfoToDB : MonoBehaviour
{
    string saveScoreLocation = "https://gtetonline.com/Abhi-data/SaveScore.php";

    string stdName, stdEmail;

    [SerializeField]
    int maxScore;

    [SerializeField]
    string appName;
    int stdScore;

    string learningStatus;
    private void Start()
    {
        stdName = PlayerPrefs.GetString("playerName");
        stdEmail = PlayerPrefs.GetString("playerEmail");
    }
    public void Result(int score)
    {
        float finalScore = (score * 100) / maxScore;

        if (finalScore >= 90)
        {
            learningStatus = "Expert";
        }
        else if (finalScore >= 60)
        {
            learningStatus = "Intermediate";
        }
        else
        {
            learningStatus = "Beginner";
        }
    }
    public void SaveInfo()
    {
        stdScore = GetComponent<QuizManager>().score;
        Result(stdScore);
        PlayerPrefs.SetInt("score", stdScore);
        PlayerPrefs.SetString("status", learningStatus);

        StartCoroutine(ScoreToDB());
    }    

    IEnumerator ScoreToDB()
    {
        WWWForm form = new WWWForm();

        form.AddField("stdName", stdName);
        form.AddField("stdEmail", stdEmail);
        form.AddField("appName", appName);
        form.AddField("score", learningStatus);

        UnityWebRequest www = UnityWebRequest.Post(saveScoreLocation, form);

        yield return www.SendWebRequest();
    }    
}
