using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Button_Script : MonoBehaviour
{
    public void PlayClicked()
    {
       // gameObject.SetActive(false); //TESTING
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().StartGame();
    }
}
