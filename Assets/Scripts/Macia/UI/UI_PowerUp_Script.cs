using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_PowerUp_Script : MonoBehaviour
{
    [SerializeField] Image ringImage;
    [SerializeField] GameObject powerUp_Text;
    [SerializeField] TMPro.TextMeshProUGUI powerUpTime_Text;
    [SerializeField] float timeCounter;
    [SerializeField] float timeActive;


    [SerializeField] Color goodColor;
    [SerializeField] Color mediumColor;
    [SerializeField] Color lowColor;
    [SerializeField] Color criticalColor;
    [SerializeField] GameManager_Script _gameManager;

    private void Start()
    {
        ringImage = transform.Find("PowerUp_Sprite").GetComponent<Image>();
        powerUp_Text = transform.Find("PowerUp_Text").gameObject;
        powerUpTime_Text = transform.Find("PowerUpTime_Text").GetComponent<TMPro.TextMeshProUGUI>();

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();
    }


    private void Update()
    {
        
        //IF timeCounter >= 0, show PowerUpTimer 
        if(timeCounter >= 0 && !_gameManager.IsGamePaused)
        {
            powerUpTime_Text.text = timeCounter.ToString("#0.0");
            timeCounter -= Time.unscaledDeltaTime;
            ringImage.fillAmount = (timeCounter / timeActive);

            //SET COLORS
            if(ringImage.fillAmount > 0.5f && ringImage.color != goodColor)
            {
                ringImage.color = goodColor;
            }
            else if(ringImage.fillAmount <= 0.5f && ringImage.fillAmount > 0.35f)
            {
                ringImage.color = mediumColor;
            }
            else if (ringImage.fillAmount <= 0.35f && ringImage.fillAmount > 0.15f)
            {
                ringImage.color = lowColor;
            }
            else if (ringImage.fillAmount <= 0.15f)
            {
                ringImage.color = criticalColor;
            }

        }
        //DEACTIVATE IF timeCounter <= 0
        else if(timeCounter <= 0 && ringImage.enabled)
        {
            DeactivateRingAndText();
        }


    }

    public void SetDefaultRing()
    {
        ringImage.fillAmount = 0;
        powerUp_Text.SetActive(false);
        powerUpTime_Text.enabled = false;
    }
    public void ActivateRing(float powerUpDuration)
    {
        powerUpTime_Text.enabled = true;
        ringImage.enabled = true;
        ringImage.fillAmount = 100;
        timeActive = powerUpDuration;
        timeCounter = powerUpDuration;
        powerUp_Text.SetActive(true);
    }

    public void DeactivateRingAndText()
    {
        ringImage.enabled = false;
        powerUp_Text.SetActive(false);
        powerUpTime_Text.enabled = false;
    }

}
