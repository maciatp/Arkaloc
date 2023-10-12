using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager_Script : MonoBehaviour
{

    [SerializeField] float coolDownTime = 1;
    [SerializeField] bool canSpawnPowerUp = true;


    public List<GameObject> powerUps = new List<GameObject>();


    [SerializeField] AudioClip powerUpClip;
    [SerializeField] AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void SpawnPowerUp(Vector3 spawnPosition)
    {
        //SPAWN COOLDOWN
        if(canSpawnPowerUp)
        {

            int i = Random.Range(0, powerUps.Count);

            GameObject powerUp = Instantiate(powerUps[i], spawnPosition, powerUps[i].transform.rotation, null);

            StartCoroutine(PowerUpSpawnCooldownRoutine());

        }


    }

    IEnumerator PowerUpSpawnCooldownRoutine()
    {
        canSpawnPowerUp = false;
        yield return new WaitForSeconds(coolDownTime);

        canSpawnPowerUp = true;
    }


    public void PlayPowerUpSound(AudioClip clipToPlay)
    {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
