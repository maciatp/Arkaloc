using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighScoreAtEnd_Script : MonoBehaviour
{
   public void ShowHighScoreIfBroken()
    {
        if(GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().IsHighScoreBeaten)
        {
            //SHOW UI HIGHSCORE CONGRATS
            transform.Find("Containts").Find("HighScoreCongrats_Text").gameObject.SetActive(true);
        }
    }
}
