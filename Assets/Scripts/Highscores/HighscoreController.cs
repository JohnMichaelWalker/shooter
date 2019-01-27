using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class HighscoreController : MonoBehaviour
{
    public Text namesText;
    public Text valuesText;


    private GameObject userInput;

    private static string SCORES_URL = "localhost:8080/scores/";

    // Start is called before the first frame update
    void Start()
    {
        userInput = transform.Find("UserInput").gameObject;

        namesText.text =  "hello";
        valuesText.text = " there!";

         StartCoroutine(GetAndSetHighScores(SCORES_URL));
    }

    IEnumerator GetAndSetHighScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                Highscores highscores = JsonUtility.FromJson<Highscores>(webRequest.downloadHandler.text);
                namesText.text = buildNameString(highscores.scores);
                valuesText.text = buildValueString(highscores.scores);
            }
        }
    }


    IEnumerator PostScore(string uri, string json)
    {

        var request = new UnityWebRequest (uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error);
        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
            userInput.SetActive(false);
            StartCoroutine(GetAndSetHighScores(SCORES_URL));
        }
    }


    public void SaveHighScore(string user) {
        Debug.Log("saving " + user);
        Debug.Log("with score " + ScoreKeeper.score);

        Score score = new Score(user, ScoreKeeper.score);

        Debug.Log(JsonUtility.ToJson(score));
        StartCoroutine(PostScore(SCORES_URL, JsonUtility.ToJson(score)));
    }



    private string buildNameString(List<Score> scores)
    {
        List<string> names = new List<string>();

        foreach (Score score in scores) 
        {
            names.Add(score.name + " - ");
        }    

        return System.String.Join("\n", names.ToArray());   
    }


    private string buildValueString(List<Score> scores)
    {
        List<string> values = new List<string>();

        foreach (Score score in scores) 
        {
            values.Add(score.value.ToString());
        }    

        return System.String.Join("\n", values.ToArray());   
    }

}
