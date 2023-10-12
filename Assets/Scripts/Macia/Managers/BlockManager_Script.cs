using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockManager_Script : MonoBehaviour
{

    [SerializeField] GameObject[] blockList;
    [SerializeField] List<Block_Controller_Script> blocksToDestroy = new List<Block_Controller_Script>();
    

    [SerializeField] GameManager_Script _gameManager;

    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {



        blockList = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blockList)
        {
            if (!block.name.Contains("Block_4_Unbreakable"))
            {
                blocksToDestroy.Add(block.GetComponent<Block_Controller_Script>());

            }
        }

        audioSource = GetComponent<AudioSource>();

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();
        
    }


    public void FireBallActivated()
    {
        foreach (GameObject block in blockList)
        {
            if(block.gameObject.name.Contains("Block_4_Unbreakable"))
            {
                block.GetComponent<Block_Controller_Script>().IsUnbreakable = false;

            }
        }


    }

    public void FireBallDeactivated()
    {
        foreach(GameObject block in blockList)
        {
            if(block.gameObject.name.Contains("Block_4_Unbreakable"))
            {
                block.GetComponent<Block_Controller_Script>().IsUnbreakable = true;

            }

        }
    }


    public void RemoveBlockFromList(Block_Controller_Script block)
    {
        //LIST
        blocksToDestroy.Remove(block);

        //BLOCK DESTROYED SOUND
        PlayDestroyedBrickSound();

        
        if(blocksToDestroy.Count == 0)
        {

            //LEVEL COMPLETE
           _gameManager.LevelCompletedCheck();

            
        }

    }


    //SOUND
    public void PlayDestroyedBrickSound()
    {
        audioSource.Play();
    }


    //POWER UP
    public void AddOneLifeToAllBlocks(int healthToAdd)
    {
        foreach(Block_Controller_Script block in blocksToDestroy)
        {
           block.AddHealthToBlock(healthToAdd);

        }
    }

}
