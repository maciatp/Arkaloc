using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_ClosedCage_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ActivateClosedPlayArea(powerUpDuration);

           
        }
    }
}
