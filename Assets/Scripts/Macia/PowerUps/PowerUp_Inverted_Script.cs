using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Inverted_Script : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!other.gameObject.GetComponentInParent<Player_Controller_Script>().IsInverted)
            {
                other.GetComponentInParent<Player_Controller_Script>().ActivateInvertedControls(powerUpDuration);

            }


        }
    }
}
