using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager_Script : MonoBehaviour
{
    [SerializeField] GameManager_Script _gameManager;
    [SerializeField] Player_Controller_Script _playerController;
    [SerializeField] ScoreManager_Script _scoreManager;
  
    void Awake()
    {
        _gameManager = gameObject.GetComponent<GameManager_Script>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player_Controller_Script>();
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();

        
    }


    //PLAYER LIVES
    public void GetSavedCurrentLives()
    {
       _playerController.CurrentLives = PlayerPrefs.GetInt("CurrentSavedLives", 1);
        _gameManager._gameplayCanvas_Script.SetLivesImages();
    }
    public void SetPlayerLivesAtBegining()
    {
       _playerController.SetLives();
        PlayerPrefs.SetInt("CurrentSavedLives", _playerController.InitialLives);
    }

    public void SaveCurrentPlayerLives()
    {
        PlayerPrefs.SetInt("CurrentSavedLives", _playerController.CurrentLives);
      
    }

    public void CheckIfHighScoreHasBeenEverBroken()
    {
        if(PlayerPrefs.GetInt("HighScoreBroken") == 1)
        {
            //ACTIVATE XALOC PLAYER
            GameObject.Find("TitleCanvas").transform.Find("HighScore_Label").transform.Find("HighestPlayer_Text").gameObject.SetActive(true);
        }
        else
        {
            //DEACTIVATE XALOC PLAYER
            GameObject.Find("TitleCanvas").transform.Find("HighScore_Label").transform.Find("HighestPlayer_Text").gameObject.SetActive(false);
        }
    }

    //SCORE

    public void SaveCurrentScore() //CALL WHEN LEVEL COMPLETED
    {
        PlayerPrefs.SetInt("CurrentScore", _scoreManager.CurrentScore);
    }

    public int GetSavedCurrentScore()
    {
        return PlayerPrefs.GetInt("CurrentScore", 000000);
    }



    public void ResetCurrentScore()
    {
        PlayerPrefs.SetInt("CurrentScore", 000000);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", _scoreManager.DefaultInitialScore);

    }

    public void SetHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
        

    }

    public void SetHighScoreBroken()
    {
        PlayerPrefs.SetInt("HighScoreBroken", 1); // 1 = YES, 0 = NO
        
    }


    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        SetHighScore(_scoreManager.DefaultInitialScore);
        CheckIfHighScoreHasBeenEverBroken();
        
    }

}
