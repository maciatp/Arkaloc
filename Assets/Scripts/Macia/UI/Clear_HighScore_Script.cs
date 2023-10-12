using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_HighScore_Script : MonoBehaviour
{
    public void ClickedClearHighScore()
    {
        transform.parent.Find("SureScreen_ClearHighScore").gameObject.SetActive(true);
    }
}
