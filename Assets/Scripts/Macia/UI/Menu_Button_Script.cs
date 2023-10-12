using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Button_Script : MonoBehaviour
{
    public void MenuButtonClicked()
    {

        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ReturnToMenu();

        GameObject.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>().ActivateSureScreen_ToMenu();
    }
}
