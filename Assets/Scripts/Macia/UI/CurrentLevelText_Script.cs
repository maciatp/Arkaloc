using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelText_Script : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI currentLevelText;


    private void Start()
    {
        currentLevelText = GetComponent<TMPro.TextMeshProUGUI>();

        if(Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
        
            currentLevelText.text = "Level \n" + SceneManager.GetActiveScene().buildIndex + "/3";

        }
        else if(Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish")
        {
            currentLevelText.text = "Nivel \n" + SceneManager.GetActiveScene().buildIndex + "/3";
        }
    }
}
