using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Score_Script : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    private void Start()
    {
        text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        SetFinalScore();
    }

    public void SetFinalScore()
    {
       
        text.text = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().CurrentScore.ToString();

        
    }
}
