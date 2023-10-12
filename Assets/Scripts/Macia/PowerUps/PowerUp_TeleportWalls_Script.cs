using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_TeleportWalls_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ActivateTeleportingTime(powerUpDuration);
        }
    }
}
