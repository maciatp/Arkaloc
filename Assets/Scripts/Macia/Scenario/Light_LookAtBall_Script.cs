using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_LookAtBall_Script : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position, Vector3.up);
    }
}
