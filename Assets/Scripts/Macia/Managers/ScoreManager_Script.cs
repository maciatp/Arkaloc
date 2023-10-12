using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager_Script : MonoBehaviour
{
    [SerializeField] int currentScore = 0;
    [SerializeField] int defaultInitialScore = 35000; //When there is no data of previous plays

    [SerializeField] bool isHighScoreBeaten = false;
    public ScoreCanvas_Script _scoreCanvas;

    public SaveManager_Script _saveManager;



    public int DefaultInitialScore
    {
        get { return defaultInitialScore; }
        set { defaultInitialScore = value; }
    }
    public int CurrentScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }
    public bool IsHighScoreBeaten
    {
        get { return isHighScoreBeaten; }
        set { isHighScoreBeaten = value; }
    }



    //[SerializeField] int highScore;


    private void Awake()
    {
        
        _scoreCanvas = GameObject.Find("UI").transform.Find("ScoreCanvas").GetComponent<ScoreCanvas_Script>();
        _saveManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager_Script>();
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        CurrentScore = _saveManager.GetSavedCurrentScore();
        
    }


    public void AddScore(int scoreToAdd)
    {

        CurrentScore += scoreToAdd;
            _scoreCanvas.UpdateScore(CurrentScore);
        

        if(currentScore > PlayerPrefs.GetInt("HighScore", defaultInitialScore))
        {

            _saveManager.SetHighScore(CurrentScore);
            _scoreCanvas.UpdateHighScore(_saveManager.GetHighScore());

            //ACTIVATE HIGH SCORE MARK
            IsHighScoreBeaten = true;
            
            _scoreCanvas.PaintScore();

            _saveManager.SetHighScoreBroken();
        }
    }

}
