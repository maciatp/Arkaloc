using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Controller_Script : MonoBehaviour
{

    [SerializeField] float fallingSpeed = 1;
    [SerializeField] int scoreWillAdd = 100;
    [SerializeField] GameManager_Script _gameManager;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] AudioClip powerUpSound; //SOUND TO PLAY WHEN PICKED UP

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.transform.Find("fbx").Find("Capsule").GetComponent<MeshRenderer>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_gameManager.IsGamePaused && !_gameManager.IsLevelCompleted && !_gameManager.IsGameCompleted)
        {
            Fall();

        }
        
    }

    
    void Fall()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed * Time.unscaledDeltaTime, transform.position.z);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().AddScore(scoreWillAdd);
            GameObject.Find("ScoreCanvas").GetComponent<ScoreCanvas_Script>().InstantiatePointsToAdd(scoreWillAdd, transform.position, 8); //TEXT SIZE TO 8
            GameObject.FindGameObjectWithTag("PowerUpsManager").GetComponent<PowerUpsManager_Script>().PlayPowerUpSound(powerUpSound);

            Destroy(gameObject);
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ceiling")
        {
            Destroy(gameObject);
        }
    }
}
