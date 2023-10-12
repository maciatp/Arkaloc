using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sure_Buttons_Script : MonoBehaviour
{
   public void ClickedYES_ToMenu()
    {
        //GO TO MENU
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ReturnToMenu();
    }

    public void ClickedYES_Quit()
    {
        //quit game
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().QuitGame();
    }
    public void ClickedNO_ToMenu()
    {
        //Close SURE SCREEN TO MENU
        GameObject.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>().DeactivateSureScreen_ToMenu();

    }

    public void ClickedNO_ToQuit()
    {
        //CLOSE SURE SCREEN QUIT
        GameObject.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>().DeactivateSureScreen_QUIT();
    }



    public void ClickedYES_ClearHighScore()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager_Script>().ClearPlayerPrefs();
        GameObject.Find("TitleCanvas").transform.Find("HighScore_Label").GetComponent<HighScore_Title_Script>().UpdateHighScoreText(GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveManager_Script>().GetHighScore());
        GameObject.Find("SureScreen_ClearHighScore").gameObject.SetActive(false);
    }

    public void ClickedNO_ClearHighScore()
    {
        GameObject.Find("SureScreen_ClearHighScore").SetActive(false);
    }
}
