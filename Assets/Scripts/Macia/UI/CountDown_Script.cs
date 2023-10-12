using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown_Script : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI countDownText;
    [SerializeField] float totalTimeCounter;
    [SerializeField] Animator animator;

    [SerializeField] AudioClip countDown_Sound;
    [SerializeField] AudioSource countDown_AudioSource;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        countDownText = transform.Find("CountDown_Text").GetComponent<TMPro.TextMeshProUGUI>();
        countDown_AudioSource = GetComponent<AudioSource>();
    
    }

   

    public void SetCountDown(float timeWillLast)
    {
        totalTimeCounter = timeWillLast;

        countDownText.text = timeWillLast.ToString();
        PlayBeepSound();
    }


    //LLamadas desde Anim events
    public void SubstractToTextTimer()
    {
        totalTimeCounter--;

        if (totalTimeCounter > 0)
        {
            animator.Play("Countdown_Loop");
            countDownText.text = totalTimeCounter.ToString();
            PlayBeepSound();
        }
        else
        {
            if(Lean.Localization.LeanLocalization.CurrentLanguage == "English")
            {
                countDownText.text = "GO!";

            }
            else if(Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish")
            {
                countDownText.text = "YA!";
            }
            animator.Play("Countdown_FadeOut");
            PlayLastBeepSound();
        }

    }

    private void PlayBeepSound()
    {
        countDown_AudioSource.clip = countDown_Sound;
        countDown_AudioSource.pitch = 1;
        countDown_AudioSource.Play();
    }

    void PlayLastBeepSound()
    {

        countDown_AudioSource.clip = countDown_Sound;
        countDown_AudioSource.pitch = countDown_AudioSource.pitch * 1.5f;
        countDown_AudioSource.Play();
    }

    public void DeactivateCountDownTimer()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().IsCountDown = false;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().ballsInScene[0].LaunchBall();
        GameObject.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>().ActivatePauseButton();
        gameObject.SetActive(false);
    }
}
