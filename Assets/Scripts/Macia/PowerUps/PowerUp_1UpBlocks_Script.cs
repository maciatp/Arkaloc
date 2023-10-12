using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_1UpBlocks_Script : MonoBehaviour
{
    [SerializeField] int healthToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager_Script>().AddOneLifeToAllBlocks(healthToAdd);
        }
    }
}
