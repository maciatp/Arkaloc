using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Controller_Script : MonoBehaviour
{
    [SerializeField] bool isUnbreakable = false;
    public bool IsUnbreakable
    {
        get { return isUnbreakable; }
        set { isUnbreakable = value; }
    }

    [SerializeField] bool mustAddPoints = true;
    [SerializeField] int maxBlockHealth = 4;
    [SerializeField] int blockHealth;
    public int BlockHealth
    {
        get { return blockHealth; }
        set { blockHealth = value; }
    }

    [SerializeField] float percentChanceOfPowerUp = 20;   //put the number in % of chance that you want!
    [SerializeField] int pointsDestroy = 100;
    [SerializeField] int pointsTouched = 20;

    
    [SerializeField] Material block1Material;
    [SerializeField] Material block2Material;
    [SerializeField] Material block3Material;
    [SerializeField] Material block4Material;

    [SerializeField] GameObject blockDestroyedParticles_GO;

   
    [SerializeField] MeshRenderer block_MR;

    [SerializeField] AudioClip blockHithealthRemaining2;
    [SerializeField] AudioClip blockHithealthRemaining1;
    
    [SerializeField] AudioSource block_AudioSource;


    BlockManager_Script _blockManager_Script;

    PowerUpsManager_Script _powerUpsManager;

    Ball_Controller_Script _ballController;

    ScoreManager_Script _scoreManager;



    // Start is called before the first frame update
    void Start()
    {
        //block_RB = gameObject.GetComponent<Rigidbody>();
        block_MR = gameObject.GetComponent<MeshRenderer>();


        _blockManager_Script = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager_Script>();

        _powerUpsManager = GameObject.FindGameObjectWithTag("PowerUpsManager").GetComponent<PowerUpsManager_Script>();

       // _ballController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>().Ball.GetComponent<Ball_Controller_Script>();
       

        if(mustAddPoints)
        {

        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        }


        block_AudioSource = GetComponent<AudioSource>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")// && !IsUnbreakable)
        {


            if (!IsUnbreakable && collision.collider.GetComponentInParent<Ball_Controller_Script>().IsFireBall)
            {

                DestroyBlock();
                return;
            }

            else if (collision.collider.GetComponentInParent<Ball_Controller_Script>().IsUselessBall && !IsUnbreakable)
            {

                AddPointsWhenTouched(pointsTouched, collision.GetContact(0).point);
                return;
            }

            if (!IsUnbreakable) //NORMAL BALL
            {
                BlockDamaged(collision.collider.GetComponentInParent<Ball_Controller_Script>().DamageToBlock, collision.GetContact(0).point);

            }
        }
    }

    void BlockDamaged(int damage, Vector3 spawnPosition)
    {
        blockHealth -= damage;
        if(mustAddPoints)
        {
            AddPointsWhenTouched(pointsTouched, spawnPosition);

        }

        CheckBlockColor();
        CheckAndPlayBlockSound();
    }

    public void AddHealthToBlock(int healthToAdd)
    {
        if (blockHealth < maxBlockHealth)
        {
            blockHealth += healthToAdd;
            CheckBlockColor();
        }
    }

    public void CheckBlockColor()
    {
        switch (blockHealth)
        {
            case 0: DestroyBlock() ;//DESTROY BLOCK;
                break;
            case 1:
                block_MR.material = block1Material;
                

                break;
            case 2:
                block_MR.material = block2Material;
              
               
                break;
            case 3:
                block_MR.material = block3Material;
               
                break;
            case 4:
                block_MR.material = block4Material;
                break;

            default:
                break;
        }
    }


    public void CheckAndPlayBlockSound()
    {
        switch (blockHealth)
        {
            case 0:
               // BLOCK MANAGER SE ENCARGA DEL SONIDO CUANDO MUERE
            case 1:
               
                block_AudioSource.clip = blockHithealthRemaining1;
                block_AudioSource.Play();

                break;
            case 2:
                
                block_AudioSource.clip = blockHithealthRemaining2;
                block_AudioSource.Play();

                break;
            case 3:
               
                block_AudioSource.clip = blockHithealthRemaining2;
                block_AudioSource.Play();
                
                break;
            case 4:
                block_AudioSource.clip = blockHithealthRemaining2;
                block_AudioSource.Play();

                break;

            default:
                break;
        }
    }

    void PlaySound(AudioClip soundToPlay)
    {
        block_AudioSource.clip = soundToPlay;
        block_AudioSource.Play();
    }

    public void AddPointsWhenTouched(int scoreWillAdd, Vector3 spawnPosition)
    {
        if(mustAddPoints)
        {
            _scoreManager.AddScore(scoreWillAdd);
            _scoreManager._scoreCanvas.InstantiatePointsToAdd(scoreWillAdd, spawnPosition, 4); //SET TEXT SIZE TO 4
        }
    }

    void CheckToSpawnPowerUp()
    {
        float r = Random.Range(0, 100);
        //print(r + " r" + "Algorithm" + (100 - (100 - percentChanceOfPowerUp)));
        if(r < (100 - (100 - percentChanceOfPowerUp))) // if r <= (100- (100-20)); r <= 20
        {
            _powerUpsManager.SpawnPowerUp(transform.position);
        }
    }


    void DestroyBlock()
    {
        CheckToSpawnPowerUp();
        //ADD SCORE
        if(mustAddPoints) // -> así en la intro se puede jugar sin romper la escena (mustAddPoints en cada block de esa escena está desactivado)
        {
            _scoreManager._scoreCanvas.InstantiatePointsToAdd(pointsDestroy, transform.position, 8f);
            _scoreManager.AddScore(pointsDestroy);
            _blockManager_Script.RemoveBlockFromList(this); 

        }
        else
        {
            //DESTROY BLOCK WITHOUT ADDING SCORE
            _blockManager_Script.PlayDestroyedBrickSound();
        }
       GameObject blockDestroyedParticles = Instantiate(blockDestroyedParticles_GO, transform.position, blockDestroyedParticles_GO.transform.rotation, null);
        blockDestroyedParticles.transform.localScale = transform.localScale;

        //BLOCK DESTROYED AUDIO ACTIVATED FROM BLOCKMANAGER

        Destroy(gameObject);
    }

   
}
