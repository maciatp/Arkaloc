using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguageButton_Script : MonoBehaviour
{
    public void ClickedFlag(string language) // English // Spanish
    {
        Lean.Localization.LeanLocalization.CurrentLanguage = language;
       
    }
}
