using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Controller_Script : MonoBehaviour
{
    [SerializeField]const int initialLives = 3;
    MasterControls masterControls;


    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector3 moveDirection;
    public Vector3 MoveDirection
    {
        get { return moveDirection; }
    }
    [SerializeField] float speed;
    [SerializeField] int currentLives;
    [SerializeField] bool isInverted = false;
    
    [SerializeField] Material invertedMaterial;
    [SerializeField] ParticleSystem invertedParticles;

    [SerializeField] Vector3 originalPlayerSize;
    [SerializeField] Material playerOriginalMaterial;

    public int InitialLives
    {
        get { return initialLives; }
       
    }
    public int CurrentLives
    {
        get { return currentLives; }
        set { currentLives = value; }
    }

    public bool IsInverted
    {
        get { return isInverted; }
       
    }



    [SerializeField] Rigidbody player_RB;
    [SerializeField] MeshRenderer player_MeshRenderer;
    [SerializeField] BoxCollider player_Collider;

    [SerializeField] GameManager_Script _gameManager;

    [SerializeField] Transform ball_Launcher;
    public Transform Ball_Launcher
    {
        get { return ball_Launcher; }
        
    }


    private void Awake()
    {
        masterControls = new MasterControls();
        player_RB = gameObject.GetComponent<Rigidbody>();
        player_Collider = gameObject.GetComponentInChildren<BoxCollider>();

        ball_Launcher = transform.Find("Ball_Launcher").transform;

        player_MeshRenderer = transform.GetComponentInChildren<MeshRenderer>();

        invertedParticles = transform.Find("Inverted_Particles").GetComponent<ParticleSystem>();

       
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Script>();

        originalPlayerSize = transform.localScale;
        playerOriginalMaterial = player_MeshRenderer.material;
    }

  
    // Start is called before the first frame update
    void Start()
    {
        DeactivateInvertedParticles();
    }

    // Update is called once per frame
    void Update()
    {
       

        if(!_gameManager.IsGamePaused)
        {

            if (!isInverted)
            {
                moveDirection = new Vector3(moveInput.x * speed * Time.unscaledDeltaTime, moveInput.y * speed * Time.unscaledDeltaTime, transform.position.z);
                MovePlayer();
            }
            else
            {
                moveDirection = new Vector3(-moveInput.x * speed * Time.unscaledDeltaTime, moveInput.y * speed * Time.unscaledDeltaTime, transform.position.z);
                MovePlayer();
            }
        }
        
    }


    


    //CONTROLS

    void OnMove(InputValue joystickValue)
    {
        moveInput = joystickValue.Get<Vector2>();
    }

    void OnPause(InputValue buttonValue)
    {
        if (!_gameManager.IsCountDown)
        {
            if (_gameManager.IsGamePaused)
            {
                _gameManager.ResumeGame();
            }
            else
            {
                _gameManager.PauseGame();

            }
        }
        

    }

    void OnShoot(InputValue buttonValue)
    {
        //START GAME only at Title Screen
        if(SceneManager.GetActiveScene().name.Contains("Title"))
        {
            _gameManager.StartGame();
        }
        else
        {
            if (_gameManager.IsCountDown)
            {
                _gameManager._gameplayCanvas_Script.DeactivateCountDown();
            }
        }
        
    }

    //METHODS
    void MovePlayer()
    {
        transform.position = new Vector3(Mathf.Clamp((transform.position.x + moveDirection.x), -_gameManager.PlayAreaExtents.x + player_Collider.size.x, _gameManager.PlayAreaExtents.x - player_Collider.size.x), transform.position.y, transform.position.z);
       
        
    }

    //public void SetBall()
    //{
    //    //_ballController = _gameManager.ballsInScene[0];
    //    _ballController = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball_Controller_Script>();
    //}

    public void SetLives()
    {
        CurrentLives = InitialLives;
    }



    public void DepleteOneLife()
    {
        
        CurrentLives--;
        _gameManager._gameplayCanvas_Script.SetLivesImages();

        if(CurrentLives > 0)
        {

            //WAIT FOR NEW BALL
            //_gameManager.NewBall();
            StartCoroutine(_gameManager.NewBall());
        }
        else
        {
            //GAME OVER
            _gameManager.GameOver();
        }

            
        
    }



    //POWER UPS


    public void ActivateSmallSize(float powerUpDuration, float decreaseRatio)
    {
        StartCoroutine(ActivateSmallSizeCoroutine(powerUpDuration, decreaseRatio));
    }

    IEnumerator ActivateSmallSizeCoroutine(float powerUpDuration, float decreaseRatio)
    {
        //ACTIVATE UI RING
        _gameManager._gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);
        //SMALL SIZE
        gameObject.transform.localScale = new Vector3(originalPlayerSize.x / decreaseRatio, transform.localScale.y, transform.localScale.z);

        player_MeshRenderer.material = invertedMaterial;
       
        yield return new WaitForSeconds(powerUpDuration);

        //NORMAL SIZE
        gameObject.transform.localScale = originalPlayerSize;

        player_MeshRenderer.material = playerOriginalMaterial;
    }
   
    public void ActivateInvertedControls(float powerUpDuration)
    {
        StartCoroutine(ActivateInvertedControlsCoroutine(powerUpDuration));
    }

    IEnumerator ActivateInvertedControlsCoroutine(float powerUpDuration)
    {
        //ACTIVATE UI RING
        _gameManager._gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);
        //INVERTED CONTROLS
        isInverted = true;

        
        //CHANGE PLAYER COLOR / MATERIAL
        player_MeshRenderer.material = invertedMaterial;

        ActivateInvertedParticles();


        yield return new WaitForSeconds(powerUpDuration);
        
        //NORMAL CONTROLS
        isInverted = false;

        //CHANGE PLAYER COLOR / MATERIAL
        player_MeshRenderer.material = playerOriginalMaterial;
        DeactivateInvertedParticles();

    }


    //FX

    void ActivateInvertedParticles()
    {
        invertedParticles.gameObject.SetActive(true);
    }

    void DeactivateInvertedParticles()
    {
        invertedParticles.gameObject.SetActive(false);
    }






}
