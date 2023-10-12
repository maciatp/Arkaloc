using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCanvas_Script : MonoBehaviour
{

    [SerializeField] GameObject pause_Menu;
    [SerializeField] GameObject pause_Button;

    [SerializeField] GameObject sureScreen_ToMenu;
    [SerializeField] GameObject sureScreen_QUIT;

    [SerializeField] Image topLive;
    [SerializeField] Image centerLive;
    [SerializeField] Image bottomLive;

    [SerializeField]public GameObject countDownText;
    
    [SerializeField] AudioClip ballLost_AudioClip;
    [SerializeField] AudioClip oneLifeRemain;
    [SerializeField] AudioSource audioSource;

    [SerializeField] public UI_PowerUp_Script _ui_PowerUp_Script;

    private void Awake()
    {
        //COUNTDOWN
        countDownText = transform.Find("CountDown_Timer").gameObject;

        //PAUSE MENU
        pause_Menu = transform.Find("Pause_Menu").gameObject;
        pause_Button = transform.Find("Pause_Button").gameObject;

        //SURE SCREEN
        sureScreen_ToMenu = transform.Find("Pause_Menu").transform.Find("SureScreen_ToMenu").gameObject;
        sureScreen_QUIT = transform.Find("Pause_Menu").transform.Find("SureScreen_Quit").gameObject;


        //LIVES
        topLive = transform.Find("Lives").transform.GetChild(0).GetComponent<Image>();
        centerLive = transform.Find("Lives").transform.GetChild(1).GetComponent<Image>();
        bottomLive = transform.Find("Lives").transform.GetChild(2).GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();

        

        _ui_PowerUp_Script = transform.Find("PowerUp").GetComponent<UI_PowerUp_Script>();

        DeactivatePauseButton();
    }

   

    public void ActivateCountDown(int timeToCount)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().IsCountDown = true;
        countDownText.SetActive(true);
        countDownText.GetComponent<CountDown_Script>().SetCountDown(timeToCount);
    }

    public void DeactivateCountDown()
    {
        countDownText.GetComponent<CountDown_Script>().DeactivateCountDownTimer();

    }

    public void ActivatePauseMenu()
    {
        
        pause_Menu.SetActive(true);
        pause_Button.SetActive(false);
        
    }
    public void DeactivatePauseMenu()
    {
       
        pause_Menu.SetActive(false);
        pause_Button.SetActive(true);
       

    }

    public void DeactivatePauseButton()
    {
        pause_Button.SetActive(false);
    }

    public void ActivatePauseButton()
    {
        pause_Button.SetActive(true);
    }
    public void ActivateSureScreen_ToMenu()
    {
        sureScreen_ToMenu.SetActive(true);
    }
    public void DeactivateSureScreen_ToMenu()
    {
        sureScreen_ToMenu.SetActive(false);
    }

    public void ActivateSureScreen_QUIT()
    {
        sureScreen_QUIT.SetActive(true);
    }

    public void DeactivateSureScreen_QUIT()
    {
        sureScreen_QUIT.SetActive(false);
    }

    public void SetLivesImages()
    {
        switch (GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player_Controller_Script>().CurrentLives)
        {
            case 3:
                topLive.enabled = true;
                centerLive.enabled = true;
                bottomLive.enabled = true;
                
                break;
            case 2:
                topLive.enabled = false;
                centerLive.enabled = true;
                bottomLive.enabled = true;
                PlayLostBallSound();
                break;
            case 1:
                topLive.enabled = false;
                centerLive.enabled = false;
                bottomLive.enabled = true;
                PlayLostBallSound();
                PlayOneLifeRemSound(audioSource.clip.length);

                break;
            case 0:
                topLive.enabled = false;
                centerLive.enabled = false;
                bottomLive.enabled = false;
                PlayLostBallSound();
                break;
        }

        
        
    }

    public void PlayLostBallSound()
    {
        audioSource.clip = ballLost_AudioClip;
        audioSource.Play();
    }

     void PlayOneLifeRemSound(float timeToWait)
    {
        StartCoroutine(PlayOneLifeRemCoroutine(timeToWait));
    }

    IEnumerator PlayOneLifeRemCoroutine(float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        audioSource.clip = oneLifeRemain;
        audioSource.Play();
    }

}
