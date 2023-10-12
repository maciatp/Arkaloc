using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Menu_Button_Script : MonoBehaviour
{
    public void ClickedQUIT()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().QuitGame();
    }
}
