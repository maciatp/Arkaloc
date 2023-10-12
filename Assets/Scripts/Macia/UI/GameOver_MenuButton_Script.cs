using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_MenuButton_Script : MonoBehaviour
{
    public void ReturnToMenuClicked()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ReturnToMenu();
    }
}
