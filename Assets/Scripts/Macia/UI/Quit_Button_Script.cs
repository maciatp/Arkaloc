using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Button_Script : MonoBehaviour
{
    public void QuitGameClicked()
    {
        GameObject.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>().ActivateSureScreen_QUIT();

        // GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().QuitGame();
    }
}
