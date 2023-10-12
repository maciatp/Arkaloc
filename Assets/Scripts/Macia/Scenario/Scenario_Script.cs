using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario_Script : MonoBehaviour
{
    [SerializeField] MeshRenderer leftWall_Renderer;
    [SerializeField] MeshRenderer rightWall_Renderer;
    [SerializeField] Material wallsOriginalMaterial;
    [SerializeField] Material wallsTeleportMaterial;

    [SerializeField] Sprite[] backgrounds;



    


    private void Start()
    {
        leftWall_Renderer = transform.Find("Left_Wall").GetComponent<MeshRenderer>();
        rightWall_Renderer = transform.Find("Right_Wall").GetComponent<MeshRenderer>();

        wallsOriginalMaterial = leftWall_Renderer.material;


        //TITLE SCENE ALWAYS HAS THE SAME SPRITE
        if(!SceneManager.GetActiveScene().name.Contains("Title"))
        {

            //SET RANDOM BACKGROUND FOR OTHER SCENES
            int random = Random.Range(0, backgrounds.Length);
            transform.Find("BackWall").GetComponent<SpriteRenderer>().sprite = backgrounds[random];
        }

    }


    public void ActivateTeleportingWallsMaterials()
    {
        leftWall_Renderer.material = wallsTeleportMaterial;
        rightWall_Renderer.material = wallsTeleportMaterial;
    }

    public void DeactivateTeleportingWallsMaterials()
    {
        leftWall_Renderer.material = wallsOriginalMaterial;
        rightWall_Renderer.material = wallsOriginalMaterial;
    }



}
