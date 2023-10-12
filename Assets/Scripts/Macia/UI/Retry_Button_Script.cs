using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry_Button_Script : MonoBehaviour
{
    public void RetryClicked()
    {
        //RETRY GAME
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().StartGame();
    }
}
