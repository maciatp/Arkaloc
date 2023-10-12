using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_UselessBall_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;

    [SerializeField] float powerFactor = 5; // 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ActivateUselessBall(powerUpDuration);

        }
    }
}
