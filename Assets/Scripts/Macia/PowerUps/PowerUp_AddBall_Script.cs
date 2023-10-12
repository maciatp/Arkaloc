using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_AddBall_Script : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().AddExtraBallToScene();

          
        }
    }
}
