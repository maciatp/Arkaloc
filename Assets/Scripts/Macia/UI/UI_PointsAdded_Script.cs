using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointsAdded_Script : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] TMPro.TextMeshPro pointsText;
    [SerializeField] GameManager_Script _gameManager;

    private void Awake()
    {
        pointsText = transform.Find("PointsAdded_Text").GetComponent<TMPro.TextMeshPro>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();
    }

    private void Update()
    {
        if(!_gameManager.IsGamePaused)
        {
            transform.Translate(new Vector3(0, speed, 0));

        }
    }

    public void SetPointsToAddText(int pointsAdded, Vector3 spawnPosition, float textSize)
    {
        pointsText.fontSize = textSize;

        transform.position = new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z);

        pointsText.text = pointsAdded.ToString();
    }

    public void DestroyPointsAddedText()
    {
        Destroy(gameObject);
    }
}
