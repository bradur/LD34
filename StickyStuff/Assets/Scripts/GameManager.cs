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

    [SerializeField]
    private BackgroundZoom backgroundZoom;

    [SerializeField]
    private bool TESTING = false;

    [SerializeField]
    private ScoreDisplay scoreDisplay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicPlayer.StartPlaying();
            if (!TESTING)
            {
                OpenLevel(currentLevel);
            }
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
        if(newScore > record){
            PlayerPrefs.SetInt("HighScore_" + currentLevel.id, newScore);
            PlayerPrefs.Save();
            scoreDisplay.SetRecord(newScore);
        }

        popupManager.ClearPool();
        popupManager.ShowPopup("New High Score! " + newScore, Vector3.zero, PopupType.Lingering);
    }

    public void OpenNextLevel()
    {
        currentLevel.Kill();
        if (nextLevel == null)
        {
            // UI show stuff heer
            Debug.Log("The End");
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
        popupManager.ShowPopup("Retry? Press R", Vector3.zero, PopupType.Stationary);
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
    }
}
