using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom_Cage_Script : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider bottomCollider;

    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        bottomCollider = gameObject.GetComponent<BoxCollider>();
    }

    public void BottomCageVisible()
    {
        meshRenderer.enabled = true;
        bottomCollider.isTrigger = false;
    }

    public void BottomCageDeathly()
    {
        meshRenderer.enabled = false;
        bottomCollider.isTrigger = true;
    }


    private void OnTriggerExit(Collider other)
    {
       
        if((other.tag == "Ball") && (other.gameObject.transform.position.y <= (transform.position.y) - bottomCollider.bounds.extents.y))
        {
           
            other.GetComponentInParent<Ball_Controller_Script>().DeleteBall();
           
        }
    }
}
