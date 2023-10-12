using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_BulletTime_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;
    [Header("PowerFactor must go between 1-2.  1.2f recommended")]
    [SerializeField] float powerFactor = 4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ActivateBulletTime(powerUpDuration, powerFactor);

            
        }
    }
}
