using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadText_Script : MonoBehaviour
{
    float characterSpawnEvery = 0.2f;
    int visibleCharacters = 0;
    int totalCharacters = 0;
   
    public TMP_Animated animatedText;

    //public AudioSource audioSource; //writing sound
    


    private void Awake()
    {
        animatedText = gameObject.GetComponent<TMP_Animated>();
        totalCharacters = animatedText.text.Length;
       // audioSource = gameObject.GetComponent<AudioSource>();
       

        gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

        ReadDialogue();

        //animatedText.onEmotionChange.AddListener((newEmotion) => EmotionChanger(newEmotion));
        //animatedText.onAction.AddListener((action) => SetAction(action));
        animatedText.onDialogueFinish.AddListener(() => FinishDialogue());


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadDialogue()
    {

        string textToRead = animatedText.text;
        animatedText.ReadText(textToRead);
        //audioSource.enabled = true;

    }
    public void FinishDialogue()
    {
       // audioSource.enabled = false;

        
        
    }
}
