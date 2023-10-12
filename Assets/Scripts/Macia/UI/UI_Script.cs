using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{

    [SerializeField] GameObject gamePlayCanvas;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject levelCompletedCanvas;
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject gameCompletedCanvas;



    private void Awake()
    {

        gamePlayCanvas          = gameObject.transform.Find("GamePlayCanvas").gameObject;
        scoreCanvas             = gameObject.transform.Find("ScoreCanvas").gameObject;
        levelCompletedCanvas    = gameObject.transform.Find("LevelCompletedCanvas").gameObject;
        titleCanvas             = gameObject.transform.Find("TitleCanvas").gameObject;
        gameOverCanvas          = gameObject.transform.Find("GameOverCanvas").gameObject;
        gameCompletedCanvas     = gameObject.transform.Find("GameCompletedCanvas").gameObject;



    }



    public void ActivateGameView()
    {
        gamePlayCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        levelCompletedCanvas.SetActive(false);
        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameCompletedCanvas.SetActive(false);
        
        
    }

    public void ActivateLevelCompleted()
    {
        levelCompletedCanvas.SetActive(true);
    }

    public void ActivateTitleView()
    {
        gamePlayCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        levelCompletedCanvas.SetActive(false);
        titleCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameCompletedCanvas.SetActive(false);

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager_Script>().CheckIfHighScoreHasBeenEverBroken();
               
    }

    public void ActivateGameOver()
    {
        gamePlayCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        levelCompletedCanvas.SetActive(false);
        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameCompletedCanvas.SetActive(false);
    }

    public void ActivateGameCompleted()
    {
        gamePlayCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        levelCompletedCanvas.SetActive(false);
        titleCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameCompletedCanvas.SetActive(true);
    }
}
