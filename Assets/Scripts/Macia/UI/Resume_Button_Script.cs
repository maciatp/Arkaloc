using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume_Button_Script : MonoBehaviour
{
   public void ResumeClicked()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ResumeGame();
    }
}
