using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public static class Mates
{
    public static int Suma(int num1, int num2)
    {
        return num1 + num2;
    }
}
public class GameManager_Script : MonoBehaviour
{
    //public static GameManager_Script _GameManager;

    public const int gameFinishAtLevel = 3;
    public const int startCountDownTimer = 3;


    //  [SerializeField] GameObject player_GO;
    [SerializeField] GameObject ball_GO;
    Vector3 playAreaExtents;
    [SerializeField] int currentLevel = 0;

    [SerializeField] float timeBetweenLevels = 5;
    [SerializeField] int delayForNewBallInSeconds = 2;

    [SerializeField] bool isBulletTime = false;
    [SerializeField] bool isTeleportingTime = false;
    

    [SerializeField] bool isGamePaused = false;
    [SerializeField] bool isLevelCompleted = false;
    [SerializeField] bool isCountDown = false;
    [SerializeField] bool isGameCompleted = false;

    public List<Ball_Controller_Script> ballsInScene= new List<Ball_Controller_Script>();




    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public int GetFinishLevel
    {
        get { return gameFinishAtLevel; }
    }

    public bool IsGamePaused
    {
        get { return isGamePaused; }
        set { isGamePaused = value; }
    }

    public Vector3 PlayAreaExtents
    {
        get { return playAreaExtents; }
        set { playAreaExtents = value; }
    }

    public bool IsLevelCompleted
    {
        get { return isLevelCompleted; }
        set { isLevelCompleted = value; }
    }
    public bool IsCountDown
    {
        get { return isCountDown; }
        set { isCountDown = value; }
    }
    public bool IsGameCompleted
    {
        get { return isGameCompleted; }
        set { isGameCompleted = value; }
    }

    public bool IsBulletTime
    {
        get { return isBulletTime; }
        set { isBulletTime = value; }
    }

    public bool IsTeleportTime
    {
        get { return isTeleportingTime; }
        set { isTeleportingTime = value; }
    }

    public GameObject Ball_GO
    {
        get { return ball_GO; }
        
    }

    [SerializeField] Player_Controller_Script _playerController;

    [SerializeField] Bottom_Cage_Script bottom_Cage;

    [SerializeField] UI_Script _ui_Script;
    public GamePlayCanvas_Script _gameplayCanvas_Script;

    
    [SerializeField] SaveManager_Script _saveManager;

    [SerializeField] AudioMixer masterAudioMixer;

    private void Awake()
    {


        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player_Controller_Script>();
        ballsInScene = new List<Ball_Controller_Script>();
        ballsInScene.Add(GameObject.FindGameObjectWithTag("Ball").GetComponentInParent<Ball_Controller_Script>());


        _ui_Script = GameObject.Find("UI").GetComponent<UI_Script>();

        _gameplayCanvas_Script = GameObject.Find("UI").transform.Find("GamePlayCanvas").GetComponent<GamePlayCanvas_Script>();


        
        _saveManager = gameObject.GetComponent<SaveManager_Script>();

        bottom_Cage = GameObject.Find("Scenario").transform.Find("Bottom").GetComponent<Bottom_Cage_Script>();

        
        playAreaExtents = GameObject.Find("PlayArea").GetComponent<BoxCollider>().bounds.extents;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        //EJEMPLO Clase estática
        Mates.Suma(3, 4);

        //SetGame composes Game View
        SetGame();


    }



    public void DeleteBallController(Ball_Controller_Script ballController_ToDelete)
    {

        ballsInScene.Remove(ballController_ToDelete);

        //WHEN ARE NO MORE BALLS IN SCENE
        if (ballsInScene.Count == 0)
        {
            _gameplayCanvas_Script._ui_PowerUp_Script.DeactivateRingAndText();
            //CHECK PLAYER's LIVES
            _playerController.DepleteOneLife();


        }

       
    }


    public IEnumerator NewBall()
    {


        //BEGIN COUNTDOWN -> send time to wait during countdown
        _gameplayCanvas_Script.ActivateCountDown(delayForNewBallInSeconds);
        _gameplayCanvas_Script.DeactivatePauseButton(); //-> Deactivate Pause Button to avoid breaking the game


        //Add Ball To Scene 
        GameObject new_ball = Instantiate(Ball_GO, _playerController.Ball_Launcher.position, ball_GO.transform.rotation, _playerController.Ball_Launcher);

        ballsInScene.Add(new_ball.GetComponent<Ball_Controller_Script>());


        new_ball.GetComponent<Ball_Controller_Script>().MoveToPlayerAtStart();
        
        yield return new WaitForSeconds(delayForNewBallInSeconds);
        //new_ball.GetComponent<Ball_Controller_Script>().LaunchBall(); -> launched when countdown ends 


    }





    public void LevelCompletedCheck()
    {
        
        //SAVE SCORE BETWEEN LEVELS
        _saveManager.SaveCurrentScore();

        if (CurrentLevel < GetFinishLevel)
        {

            //LEVEL COMPLETED
            StartCoroutine(LevelCompletedCoroutine());
        
        }
        else
        {
            //GAME COMPLETED
            GameCompleted();
            
        }
               
        _saveManager.SaveCurrentPlayerLives();
    }

   

    public IEnumerator LevelCompletedCoroutine()
    {

        //SCENES 0 and 3 must not activate levelCompleted 
        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != 3)
        {
            SlowMo();
            //ACTIVATE UI CONGRATULATIONS
            _ui_Script.ActivateLevelCompleted();
            IsLevelCompleted = true;

            //WAIT
            yield return new WaitForSecondsRealtime(timeBetweenLevels);
            DeactivateSlowMoPhysics();
            //LOAD NEXT SCENE
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }


    public void GameOver()
    {
        //GAME OVER SCREEN
        _ui_Script.ActivateGameOver();
    }

    public void GameCompleted()
    {
        //GAME COMPLETED
        TimeScaleFreeze();
        IsGameCompleted = true;
        //SHOW GAME COMPLETED UI
       
        _ui_Script.ActivateGameCompleted();
        
    }


    public void StartGame()
    {

        DeactivateSlowMoPhysics();
        _saveManager.SetPlayerLivesAtBegining();
        _saveManager.ResetCurrentScore();
        // Load Level1
        SceneManager.LoadScene("Level1_Scene");


    }

    void SetGame()
    {
        //SET LEVEL NUMBER
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;

       
        //launches at every scene start to compose Game View
        //NORMAL LEVELS
        if (SceneManager.GetActiveScene().name.Contains("Level")) 
        {
            _ui_Script.ActivateGameView();
           
            _saveManager.GetSavedCurrentScore();

            //SECURE CAGE ALWAYS ACTIVE IN TESTING SCENE
            if (!SceneManager.GetActiveScene().name.Contains("Testing"))
            {
                bottom_Cage.BottomCageDeathly();

            }

           
            _saveManager.GetSavedCurrentLives();

            ActivateCountDown();

        }
        //TITLE_SCENE
        else if (SceneManager.GetActiveScene().name.Contains("Title"))
        {

            _ui_Script.ActivateTitleView();

           
            _saveManager.ResetCurrentScore();

            bottom_Cage.BottomCageVisible();

           
            _saveManager.SetPlayerLivesAtBegining();
        }



    }



    public void PauseGame()
    {
        //AVOIDING PAUSING DURING COUNTDOWN, level/game completed, and at Title Screen
        if((!_gameplayCanvas_Script.countDownText.activeSelf || !IsLevelCompleted || !IsGameCompleted) && !SceneManager.GetActiveScene().name.Contains("Title"))
        {

            IsGamePaused = true;

            // ACTIVATE PAUSE UI
            _gameplayCanvas_Script.ActivatePauseMenu();
            
            //FREEZE MOVEMENT TIMESCALE 0
            TimeScaleFreeze();
        }

    }

    public void ResumeGame()
    {
        IsGamePaused = false;

        //DEACTIVATE PAUSE UI
        //ACTIVATE PAUSE BUTTON
        _gameplayCanvas_Script.DeactivatePauseMenu();


        //UNFREEZE TIMESCALE
        DeactivateSlowMoPhysics();

    }

    //TIMEScale MANAGEMENT
    void ActivateSlowMoPhysics(float powerFactor)
    {
        masterAudioMixer.SetFloat("masterPitch", 0.5f);
        //ACTIVATE BULLET TIME
        Time.timeScale = 1 / (powerFactor * 5);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    void DeactivateSlowMoPhysics()
    {
        masterAudioMixer.SetFloat("masterPitch", 1f);
        Time.timeScale = 1; //ORIGINAL TIMESCALE ALWAYS 1
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void SlowMo() //FOR MENUS
    {
        Time.timeScale = 0.03f;
    }
    public void TimeScaleFreeze() // FOR PAUSE
    {
        Time.timeScale = 0;
    }

    

    public void ReturnToMenu()
    {
        //LOAD MENU SCENE
        DeactivateSlowMoPhysics();

        SceneManager.LoadScene("Title_Scene");
    }


    public void QuitGame()
    {
        DeactivateSlowMoPhysics();


        _saveManager.ResetCurrentScore();


        //print("GAME QUIT");
        Application.Quit();
    }


    public void ActivateCountDown()
    {
        //MOVE BALL TO PLAYER only in levels. Title Scene has its own ball moving by its own
        ballsInScene[0].MoveToPlayerAtStart();

        //BEGIN COUNTDOWN -> send time to wait during countdown
        _gameplayCanvas_Script.ActivateCountDown(startCountDownTimer);
       
        //BALL LAUNCHED WHEN COUNTDOWN ENDS (See ActivateCountDown); 
    }


   


    //POWER_UPS

    public void AddExtraBallToScene()
    {
        //INSTANTIATE extra ball
        GameObject ball_extra = (GameObject)Instantiate(Ball_GO, _playerController.Ball_Launcher.position, Ball_GO.transform.rotation, null);

        //ADD BALL TO ballsList
        ballsInScene.Add(ball_extra.gameObject.GetComponent<Ball_Controller_Script>());

        //Launch extra ball
        ball_extra.gameObject.GetComponent<Ball_Controller_Script>().LaunchBall();
    }

    public void ActivateFireBall(float powerUpDuration)
    {
      
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);

        //ACTIVATE POWER UP ON EACH BALL
        foreach (Ball_Controller_Script ball in ballsInScene)
        {
            ball.ActivateFireBall(powerUpDuration);
        }
    }

   
   

    public void ActivateUselessBall(float powerUpDuration)
    {
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);
        foreach (Ball_Controller_Script ball in ballsInScene)
        {
            ball.ActivateUselessBall(powerUpDuration);
        }
    }

    
    public void ActivateBulletTime(float powerUpDuration, float powerFactor)
    {
        StartCoroutine(ActivateBulletTimeCoroutine(powerUpDuration, powerFactor));
    }

    IEnumerator ActivateBulletTimeCoroutine(float powerUpDuration, float powerFactor)
    {
        IsBulletTime = true;
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);

        //ACTIVATE BULLET TIME PARTICLES
        foreach (Ball_Controller_Script ball in ballsInScene)
        {
            //ball.bulletTimeParticles.main.duration = powerUpDuration;
            ball.ActivateBulletTimeParticles();

        }

        ActivateSlowMoPhysics(powerFactor);

        yield return new WaitForSecondsRealtime(powerUpDuration);

        IsBulletTime = false;
        //DEACTIVATE BULLET TIME 
        DeactivateSlowMoPhysics();
        //DEACTIVATE BULLET TIME PARTICLES
        foreach (Ball_Controller_Script ball in ballsInScene)
        {
            ball.DeactivateBulletTimeParticles();
        }

    }

    


    public void ActivateClosedPlayArea(float powerUpDuration)
    {
        StartCoroutine(ActivateClosedPlayAreaCoroutine(powerUpDuration));
    }

    IEnumerator ActivateClosedPlayAreaCoroutine(float powerUpDuration)
    {
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);
        //CLOSED PLAY AREA -> Activate BOTTOM Gameobject
        bottom_Cage.BottomCageVisible();

        yield return new WaitForSeconds(powerUpDuration);

        //BOTTOM CAGE MUST BE ALWAYS ACTIVE IN Testing_Scene
        if(!SceneManager.GetActiveScene().name.Contains("Testing"))
        {

            //OPEN PLAY AREA -> DEACTIVATE BOTTOM Gameobject
            bottom_Cage.BottomCageDeathly();
        }
    }



    public void ActivateFastBall(float powerUpDuration, float powerFactor)
    {
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);
        foreach (Ball_Controller_Script ball in ballsInScene)
        {
            ball.ActivateFastBall(powerUpDuration, powerFactor);
        }
    }

    public void ActivateTeleportingTime(float powerUpDuration)
    {
        StartCoroutine(ActivateTeleportingTimeCoroutine(powerUpDuration));
    }

    public IEnumerator ActivateTeleportingTimeCoroutine(float powerUpDuration)
    {
        IsTeleportTime = true;
        //ACTIVATE UI RING
        _gameplayCanvas_Script._ui_PowerUp_Script.ActivateRing(powerUpDuration);

        GameObject.Find("Scenario").GetComponent<Scenario_Script>().ActivateTeleportingWallsMaterials();


        yield return new WaitForSeconds(powerUpDuration);

        IsTeleportTime = false;

        GameObject.Find("Scenario").GetComponent<Scenario_Script>().DeactivateTeleportingWallsMaterials();
    }

  

}
