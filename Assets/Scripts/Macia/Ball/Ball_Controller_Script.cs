using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller_Script : MonoBehaviour
{
                     Vector2 ballOriginalSpeed;
    [SerializeField] Vector2 ballSpeed = new Vector2(6, -6);
    [SerializeField] float velocityToChangeWhenPlayerHits = 0.3f; //IF THE PLAYER IS MOVING WHEN TOUCHED, change this speed to ballSpeed

                     Material ballOriginalMaterial;
    [SerializeField] Material ballFastMaterial;
    [SerializeField] Material ballFireMaterial;
    [SerializeField] Material ballUselessMaterial;
                     Material trailOriginalMaterial;
  

    [SerializeField] AudioClip ballLaunch_audioClip;
    
    [SerializeField] AudioClip ballHitWall_audioClip;

    [SerializeField] AudioSource ball_AudioSource;

    [SerializeField] bool isGoingRight = false;
    
    public bool IsGoingRight
    {
        get { return isGoingRight; }
        set { isGoingRight = value; }
    }

    [SerializeField] int damageToBlocks = 1;
    public int DamageToBlock
    {
        get { return damageToBlocks; }
        set { damageToBlocks = value; }
    }

    [SerializeField] bool isFireBall = false;
   
    public bool IsFireBall
    {
        get { return isFireBall; }
        set { isFireBall = value; }
    }

    [SerializeField] bool isUselessBall = false;
    public bool IsUselessBall
    {
        get { return isUselessBall; }
        set { isUselessBall = value; }
    }
    [SerializeField] bool isFastBall = false;
    public bool IsFastBall
    {
        get { return isFastBall; }
        set { isFastBall = value; }
    }

    [SerializeField] SphereCollider ball_Collider;
    [SerializeField] MeshRenderer ball_MeshRenderer;
    [SerializeField] TrailRenderer trail;
    [SerializeField] ParticleSystem hit_particles;
    [SerializeField] ParticleSystem fireBallparticles;
    [SerializeField] public ParticleSystem bulletTimeParticles;

    [SerializeField] Player_Controller_Script _playerController;

    [SerializeField] GameManager_Script _gameManager;
    


    private void Awake()
    {
        ball_Collider = gameObject.GetComponentInChildren<SphereCollider>();
        ball_MeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

        ballOriginalMaterial = ball_MeshRenderer.material;
        ballOriginalSpeed = ballSpeed;

        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        hit_particles = transform.Find("Particles").GetComponent<ParticleSystem>();
        fireBallparticles = transform.Find("FireBall_Particles").GetComponent<ParticleSystem>();
        bulletTimeParticles = transform.Find("BulletTime_Particles").GetComponent<ParticleSystem>();

        trailOriginalMaterial = trail.material;
        //trailOriginalTime = trail.time;

        ball_AudioSource = GetComponent<AudioSource>();

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player_Controller_Script>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();

    }

    private void Start()
    {
        fireBallparticles.gameObject.SetActive(false);
        //ACTIVATE BULLET TIME PARTICLES IF SPAWNED AFTER ACTIVATING BULLET TIME FROM GAME MANAGER
        if (_gameManager.IsBulletTime && !bulletTimeParticles.gameObject.activeSelf)
        {
            ActivateBulletTimeParticles();
        }
    }

    private void Update()
    {
        MoveBall(ballSpeed.x, ballSpeed.y);

        if(ballSpeed.x > 0) // && !isGoingRight)
        {
            IsGoingRight = true;
        }
        else if(ballSpeed.x < 0)
        {
            IsGoingRight = false;
        }
        else
        {
            IsGoingRight = false;
        }


       
    }
    

    //METHODS

    void MoveBall(float speedX, float speedY)
    {
      

       transform.position = new Vector3(Mathf.Clamp((transform.position.x + speedX * Time.deltaTime), -_gameManager.PlayAreaExtents.x + ball_Collider.bounds.extents.x, +_gameManager.PlayAreaExtents.x - ball_Collider.bounds.extents.x), Mathf.Clamp((transform.position.y - speedY * Time.deltaTime), -_gameManager.PlayAreaExtents.y -2, +_gameManager.PlayAreaExtents.y - ball_Collider.bounds.extents.y), transform.position.z);

        
    }

    //WHEN ONLY ONE COMPONENT CHANGES
    void ChangeBallSpeed(float ballSpeedSwitch, int changeDirection)  // ballSpeedSwitch -> 1, 2  //  changeDirection -> -1 to reverse ballspeed.x/y
    {
        
        switch(ballSpeedSwitch) // 1 change X , 2 change Y
        {
            case 1:
                ballSpeed.x *= changeDirection;
                break;
            case 2:
                ballSpeed.y *= changeDirection;
                break;
        }    
    }
    //WHEN THE TWO COMPONENTS CHANGE
    void ChangeBallSpeed(int changeDirection)
    {
        ballSpeed.x *= changeDirection;
        ballSpeed.y *= changeDirection;
    }


    //MOVE BALL TO PLAYER
    public void MoveToPlayerAtStart()
    {
        DeactivateTrail();
        ballSpeed = Vector2.zero;
        ball_Collider.enabled = false;
        transform.position = _playerController.Ball_Launcher.position;
        transform.SetParent(_playerController.gameObject.transform);

    }

    public void LaunchBall()
    {
        ball_AudioSource.clip = ballLaunch_audioClip;
        ball_AudioSource.Play();
        ActivateTrail();
        transform.SetParent(null);
        ball_Collider.enabled = true;
        ballSpeed = ballOriginalSpeed;
    }
   
    public void DeleteBall()
    {
        _gameManager.DeleteBallController(this); //DELETE BALL_CONTROLLER FROM BallsInScene List
        _gameManager._gameplayCanvas_Script.PlayLostBallSound();
       
        trail.gameObject.transform.SetParent(null);
        trail.autodestruct = true;

       
        Destroy(gameObject);
    }




    //COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.collider.tag == "Player")
        {
            hit_particles.Play();
            PlayBallHitScenarioSound();

            if (IsGoingRight)
            {
                //LEFT Third
                //SHOOT LEFT
                if (transform.position.x < (collision.collider.transform.position.x - (collision.collider.gameObject.transform.localScale.magnitude / 2 - collision.collider.transform.localScale.magnitude / 3)))
                {
                    //Y bola da en tercio izqu. de la pala, cambia veloc. vertical y horiz
                    
                   //CHANGE BOTH VEL
                    ChangeBallSpeed( -1);
                    
;

                }
                
                //RIGHT THIRD
                else
                {

                    
                   //CHANGE Y VEL
                    ChangeBallSpeed( 2,-1);
                }

                //SPEED CHANGE
                if (collision.collider.gameObject.GetComponentInParent<Player_Controller_Script>().MoveDirection.x < 0)
                {
                    //add some speed X?
                    ballSpeed = new Vector2(ballSpeed.x - velocityToChangeWhenPlayerHits, ballSpeed.y + velocityToChangeWhenPlayerHits);
                }
                else if (collision.collider.gameObject.GetComponentInParent<Player_Controller_Script>().MoveDirection.x > 0)
                {
                    //substract some speed X?
                    ballSpeed = new Vector2(ballSpeed.x + velocityToChangeWhenPlayerHits, ballSpeed.y - velocityToChangeWhenPlayerHits);
                }


            }
            else //IS GOING LEFT
            {

                //RIGHT Third
                //SHOOT RIGHT
                if (transform.position.x > (collision.collider.transform.position.x + (collision.collider.gameObject.transform.localScale.magnitude / 2 - collision.collider.transform.localScale.magnitude / 3)))
                {
                    //Y bola da en tercio izqu. de la pala, cambia veloc. vertical y horiz
                    
                   //CHANGE BOTH VEL
                    ChangeBallSpeed(-1);


                }
                //LEFT THIRD
                else
                {
                    
                   //CHANGE Y VEL
                    ChangeBallSpeed(2, -1);

                }


                //SPEED CHANGE
                if (collision.collider.gameObject.GetComponentInParent<Player_Controller_Script>().MoveDirection.x < 0)
                {
                    //substract some speed X? -> IS GOING LEFT, + speed must be less
                    ballSpeed = new Vector2(ballSpeed.x + velocityToChangeWhenPlayerHits, ballSpeed.y - velocityToChangeWhenPlayerHits);
                }
                else if (collision.collider.gameObject.GetComponentInParent<Player_Controller_Script>().MoveDirection.x > 0)
                {
                    //add some speed X? -> IS GOING LEFT, - speed must be more
                    ballSpeed = new Vector2(ballSpeed.x - velocityToChangeWhenPlayerHits, ballSpeed.y + velocityToChangeWhenPlayerHits);
                }
            }


           

        }
        if (collision.collider.tag == "Wall")
        {
            //AUDIO HIT WALL
            PlayBallHitScenarioSound();
            hit_particles.Play();

            if(!_gameManager.IsTeleportTime)
            {
               
                //CHANGE X VELOCITY
                ChangeBallSpeed(1, -1);

            }
            else
            {
               
                
                //TELEPORT TO OTHER SIDE
                transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
                
            }
           
        }
        if (collision.collider.tag == "Ceiling")
        {
            //AUDIO HIT WALL
            PlayBallHitScenarioSound();
            hit_particles.Play();

            //CHANGE Y VELOCITY.
            ChangeBallSpeed(2, -1);
        }


        if (collision.collider.tag == "Block")
        {
            
            if (!IsFireBall || collision.collider.gameObject.GetComponent<Block_Controller_Script>().IsUnbreakable)
            {
                
                if((collision.GetContact(0).normal.x < 0) && (transform.position.x < collision.collider.transform.position.x))
                {
                   //LEFT SIDE COLLIDED
                   //CHANGE X VELOCITY
                    ChangeBallSpeed(1, -1);
                }
                else if ((collision.GetContact(0).normal.x > 0) && (transform.position.x > collision.collider.transform.position.x))
                {
                   //RIGHT SIDE COLLIDED
                   //CHANGE X VELOCITY
                    ChangeBallSpeed(1, -1);

                   
                }
                else if ((collision.GetContact(0).normal.y > 0) && (transform.position.y > collision.collider.transform.position.y))
                {
                   //TOP SIDE COLLIDED
                   //CHANGE Y VELOCITY
                   ChangeBallSpeed(2, -1);

                }
                else if ((collision.GetContact(0).normal.y < 0) && (transform.position.y < collision.collider.transform.position.y))
                {
                   //BOTTOM SIDE COLLIDED
                   //CHANGE Y VELOCITY
                    ChangeBallSpeed(2, -1);

                }

                // IF UNBREAKABLE BLOCK //  USELESS BALL  SOUNDS

                if (collision.collider.GetComponent<Block_Controller_Script>().IsUnbreakable)
                {
                    //PLAY BALL HIT WALL SOUND
                    PlayBallHitScenarioSound();
                }
                else
                {
                    if (IsUselessBall)
                    {
                        //AUDIO HIT BLOCK WEAK
                        PlayBallHitScenarioSound();

                    }
                    
                   //AUDIO HIT BLOCK NORMAL -> se encarga Block_Controller().CheckHealthSound() (Dentro de DamageBlock())
                    
                }
            }


            hit_particles.Play();
        }
    }

    




    //POWER UPS

    public void ActivateFireBall(float powerUpDuration)
    {
        StartCoroutine(ActivateFireBallCouroutine(powerUpDuration));
    }

    public IEnumerator ActivateFireBallCouroutine(float powerUpDuration)
    {
        if(IsUselessBall)
        {
            IsUselessBall = false;
        }

        IsFireBall = true;
        //ACTIVATE FX,sound etc
        fireBallparticles.gameObject.SetActive(true);


        trail.material = ballFireMaterial;

        ball_MeshRenderer.material = ballFireMaterial;


        yield return new WaitForSeconds(powerUpDuration);



        //DEACTIVATE FX,sound etc
        IsFireBall = false;
        fireBallparticles.gameObject.SetActive(false);
        ball_MeshRenderer.material = ballOriginalMaterial;

        trail.material = trailOriginalMaterial;
    }

    public void ActivateUselessBall(float powerUpDuration)
    {
        StartCoroutine(ActivateUselessBallCouroutine(powerUpDuration));
    }

    public IEnumerator ActivateUselessBallCouroutine(float powerUpDuration)
    {
        if(IsFireBall)
        {
            IsFireBall = false;
        }

        //ACTIVATE FX,sound etc
        IsUselessBall = true;
        hit_particles.gameObject.SetActive(false);
        

        ball_MeshRenderer.material = ballUselessMaterial;
        trail.material = ballUselessMaterial;

        yield return new WaitForSeconds(powerUpDuration);

        //DEACTIVATE FX,sound etc
        isUselessBall = false;
        hit_particles.gameObject.SetActive(true);

        ball_MeshRenderer.material = ballOriginalMaterial;
        trail.material = trailOriginalMaterial;
    }

    public void ActivateFastBall(float powerUpDuration, float powerFactor)
    {
        StartCoroutine(ActivateFastBallCouroutine(powerUpDuration, powerFactor));
    }

    public IEnumerator ActivateFastBallCouroutine(float powerUpDuration, float powerFactor)
    {

        //ACTIVATE FX,sound etc
        if(!IsFastBall)
        {

        IsFastBall = true;
        ballSpeed = new Vector2(ballSpeed.x * (1 + (powerFactor / 10)), ballSpeed.y * (1 + (powerFactor / 10)));
        }

        ball_MeshRenderer.material = ballFastMaterial;
        trail.material = ballFastMaterial;


        yield return new WaitForSeconds(powerUpDuration);

        if(IsFastBall)
        {
            IsFastBall = false;
            ballSpeed = new Vector2(ballSpeed.x / (1 + (powerFactor / 10)), ballSpeed.y / (1 + (powerFactor / 10)));

        }
        //DEACTIVATE FX,sound etc

        ball_MeshRenderer.material = ballOriginalMaterial;
        trail.material = trailOriginalMaterial;


        
    }


    //FX

    void ActivateTrail()
    {
        trail.gameObject.SetActive(true);
        
    }
    void DeactivateTrail()
    {
        trail.gameObject.SetActive(false);
        
    }

    public void ActivateBulletTimeParticles()
    {
        bulletTimeParticles.gameObject.SetActive(true);
    }

    public void DeactivateBulletTimeParticles()
    {
        bulletTimeParticles.gameObject.SetActive(false);
    }

   //SOUND

    private void PlayBallHitScenarioSound()
    {
        ball_AudioSource.clip = ballHitWall_audioClip;
        ball_AudioSource.Play();
    }
}
