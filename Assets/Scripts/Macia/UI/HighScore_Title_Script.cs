using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore_Title_Script : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI highScoreTitle_Text;

    private void Start()
    {
        highScoreTitle_Text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        //highScoreTitle_Text.text = GameObject.Find("UI").transform.Find("ScoreCanvas").GetComponent<ScoreCanvas_Script>().UpdateHighScore(GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().GetHighScore().to);

        //UpdateHighScoreText(GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().GetHighScore());
        UpdateHighScoreText(GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager_Script>().GetHighScore());
    }

    public void UpdateHighScoreText(int highScore)
    {
        highScoreTitle_Text.text = highScore.ToString("000###"); 
    }

}
//GameObject.FindGameObjectWithTag("UI").transform.Find("ScoreCanvas").GetComponent<ScoreCanvas_Script>().UpdateHighScore(GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().GetHighScore())