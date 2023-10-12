using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvas_Script : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI highScore_Text;
   // [SerializeField] TMPro.TextMeshProUGUI highestPlayer_Text;
    [SerializeField] TMPro.TextMeshProUGUI currentScore_Text;
    [SerializeField] Color newHighScoreColor;

    [SerializeField] ScoreManager_Script _scoreManager;
    [SerializeField] SaveManager_Script _saveManager;

    [SerializeField] GameObject pointsToAdd_GO;

    // Start is called before the first frame update
    void Start()
    {
        highScore_Text = gameObject.transform.Find("HighScore_Label").GetComponent<TMPro.TextMeshProUGUI>();
       // highestPlayer_Text = gameObject.transform.Find("HighestPlayer_Label").GetComponent<TMPro.TextMeshProUGUI>();
        currentScore_Text = gameObject.transform.Find("CurrentScore_Label").GetComponent<TMPro.TextMeshProUGUI>();

        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        _saveManager = GameObject.Find("GameManager").GetComponent<SaveManager_Script>();

        //currentScore_Text.text = _scoreManager.GetSavedCurrentScore().ToString("#000000"); 
        currentScore_Text.text = _saveManager.GetSavedCurrentScore().ToString("#000000");

        //CHECK SCORE AT LOADING and paint it yellow if > highscore
        if (_saveManager.GetSavedCurrentScore() > _saveManager.GetHighScore())
        {
            PaintScore();
        }

       // UpdateHighScore(_scoreManager.GetHighScore());
        UpdateHighScore(_saveManager.GetHighScore());



        
    }

   

    public void UpdateScore(int score)
    {
        currentScore_Text.text = score.ToString("#000000");

    }

    public void PaintScore()
    {
        if(currentScore_Text.color != newHighScoreColor)
        {

            currentScore_Text.color = newHighScoreColor;
        }
    }


    public void UpdateHighScore(int newHighScore)
    {
        //highScore_Text.text =  string.Format(newHighScore.ToString(), "{0:###.000}");
        highScore_Text.text =  newHighScore.ToString("000###");
    }


    public void InstantiatePointsToAdd(int pointsToAdd, Vector3 spawnPosition, float textSize)
    {
        GameObject textPoints = Instantiate(pointsToAdd_GO, spawnPosition, pointsToAdd_GO.transform.rotation, null);

        textPoints.GetComponent<UI_PointsAdded_Script>().SetPointsToAddText(pointsToAdd, spawnPosition, textSize);

        textPoints.transform.SetParent(GameObject.Find("ScoreCanvas").gameObject.transform);

    }
}
