// Date   : 12.12.2015 10:28
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField]
    private RotationButton rotationButtonLeft;
    [SerializeField]
    private RotationButton rotationButtonRight;

    public SoundPlayer soundPlayer;
    public MusicPlayer musicPlayer;
    public PopupManager popupManager;

    private GameObject nextLevelBlob;

    [SerializeField]
    private SpeedBand speedBand;

    [SerializeField]
    private Level currentLevel;

    [SerializeField]
    private GameObject character;

    private Level currentLevelPrefab;

    [SerializeField]
    private GameObject GameContainer;

    private Level nextLevel;
    private Level level1;

    [SerializeField]
    private BackgroundZoom backgroundZoom;

    [SerializeField]
    private bool TESTING = false;

    [SerializeField]
    private ScoreDisplay scoreDisplay;

    private bool waitingForExitConfirmation = false;
    private bool waitingForPlayAgainConfirmation = false;
    private bool waitForStartConfirmation = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicPlayer.StartPlaying();
            if (!TESTING)
            {
                level1 = currentLevel;
                OpenLevel(currentLevel);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        waitForStartConfirmation = true;
        Time.timeScale = 0f;
        popupManager.ShowPopup("bradur presents", new Vector3(-1f, 3f, 0f), PopupType.Stationary);
        popupManager.ShowPopup("Sticky Stuff", new Vector3(0f, 2f, 0f), PopupType.Big);
        popupManager.ShowPopup("A Ludum Dare project", new Vector3(0f, 1.5f, 0f), PopupType.Stationary);
        #if UNITY_STANDALONE || UNITY_EDITOR
            popupManager.ShowPopup("Press any key to start", new Vector3(0f, -1.5f, 0f), PopupType.Stationary);
        #elif UNITY_ANDROID
            popupManager.ShowPopup("Tap to start", new Vector3(0f, -1.5f, 0f), PopupType.Stationary);
        #endif
    }

    public void UpdateScore(int score)
    {
        scoreDisplay.AddToScore(score);
    }

    public GameObject GetCharacter(){
        return this.character;
    }

    public void NextLevelBlobFound()
    {
        StopAllCoroutines();
        int record = PlayerPrefs.GetInt("HighScore_" + currentLevel.id);
        int newScore = scoreDisplay.GetScore();
        popupManager.ClearPool();
        if(newScore > record){
            PlayerPrefs.SetInt("HighScore_" + currentLevel.id, newScore);
            PlayerPrefs.Save();
            scoreDisplay.SetRecord(newScore);
            popupManager.ShowPopup("New High Score! " + newScore, Vector3.zero, PopupType.Lingering);
        }
        GetCharacter().GetComponent<CharacterMovement>().ReleaseMaxSpeed();

        
        
    }

    public void OpenNextLevel()
    {
        currentLevel.Kill();
        if (nextLevel == null)
        {
            // UI show stuff heer
            #if UNITY_STANDALONE || UNITY_EDITOR
                popupManager.ShowPopup("THE END\nThanks for playing!\nPlay Again (Y) Quit (N)", Vector3.zero, PopupType.Stationary);
            #elif UNITY_ANDROID
                popupManager.ShowPopup("THE END! Thanks for playing!\nTap here to play again", Vector3.zero, PopupType.Stationary);
            #endif
            waitingForPlayAgainConfirmation = true;
        }
        else {
            backgroundZoom.StartZoom();
        }
    }

    private void OpenLevel(Level level){
        StopAllCoroutines();
        scoreDisplay.ResetScore();
        int record = PlayerPrefs.GetInt("HighScore_" + level.id);
        scoreDisplay.SetRecord(record);
        character.GetComponent<CharacterMovement>().EmptySticky();
        currentLevelPrefab = level;
        GetCharacter().GetComponent<CharacterMovement>().ResetMaxSpeed();
        currentLevel = Instantiate(level);
        currentLevel.transform.parent = GameContainer.transform;
        currentLevel.transform.localPosition = Vector3.zero;
        nextLevel = currentLevel.GetNextLevel();
    }

    public void BackgroundZoomFinished()
    {
        popupManager.ClearPool();
        OpenLevel(nextLevel);
    }

    public void RestartLevelFromButton()
    {
        if (waitingForPlayAgainConfirmation)
        {
            waitingForPlayAgainConfirmation = false;
            popupManager.ClearPool();
            OpenLevel(level1);
        }
        else
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        popupManager.ClearPool();
        currentLevel.Kill();
        OpenLevel(currentLevelPrefab);
    }

    public void UpdateSpeedBand(float speed)
    {
        speedBand.UpdateSpeedBand(speed);
    }

    public void ButtonDown(RotationButton rotationButton)
    {
        rotationButton.ButtonDown();
    }

    public void ButtonUp(RotationButton rotationButton)
    {
        rotationButton.ButtonUp();
    }

    public void SpawnNextLevelBlob()
    {
        currentLevel.GetNextLevelBlob().gameObject.SetActive(true);
    }

    public void WaitForFinish()
    {
        StartCoroutine(WaitOrRetry());
    }

    IEnumerator WaitOrRetry()
    {
        yield return new WaitForSeconds(5);
        #if UNITY_STANDALONE || UNITY_EDITOR
            popupManager.ShowPopup("Retry? Press R", Vector3.zero, PopupType.Stationary);
        #elif UNITY_ANDROID
            popupManager.ShowPopup("Retry? Tap here", Vector3.zero, PopupType.Stationary);
        #endif
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonUp(rotationButtonLeft);
            ButtonUp(rotationButtonRight);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonDown(rotationButtonLeft);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonDown(rotationButtonRight);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ButtonUp(rotationButtonLeft);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ButtonUp(rotationButtonRight);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape) && !waitingForExitConfirmation && !waitingForPlayAgainConfirmation)
        {
            popupManager.ShowPopup("Really want to quit?\n Yes(Y) No(N)", Vector3.zero, PopupType.Stationary);
            waitingForExitConfirmation = true;
            Time.timeScale = 0f;
        }
#endif
        if (waitingForExitConfirmation)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Time.timeScale = 1f;
                waitingForExitConfirmation = false;
                popupManager.ClearPool();
            }
        }
        else if (waitingForPlayAgainConfirmation)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                waitingForPlayAgainConfirmation = false;
                popupManager.ClearPool();
                OpenLevel(level1);
            }
        }
        else if (waitForStartConfirmation)
        {
            if (Input.anyKey || Input.touchCount > 0)
            {
                waitForStartConfirmation = false;
                popupManager.ClearPool();
                #if UNITY_STANDALONE || UNITY_EDITOR
                                popupManager.ShowPopup("Left and right arrow keys to turn", new Vector3(0f, -2f, 0f), PopupType.Lingering);
                                popupManager.ShowPopup("Esc to quit", new Vector3(0f, 2f, 0f), PopupType.Lingering);
                #endif
                Time.timeScale = 1f;
            }
        }
    }
}
