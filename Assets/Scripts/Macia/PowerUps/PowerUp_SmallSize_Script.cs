using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_SmallSize_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;
    [SerializeField] float decreaseRatio = 1.75f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInParent<Player_Controller_Script>().ActivateSmallSize(powerUpDuration, decreaseRatio);

       
        }
    }
}
