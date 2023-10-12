using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_FastBall_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;
    [Header("PowerFactor must go between 0-10.  5 recommended")]
    [SerializeField] float powerFactor = 5;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ActivateFastBall(powerUpDuration, powerFactor);

            Destroy(gameObject);
        }
    }
}
