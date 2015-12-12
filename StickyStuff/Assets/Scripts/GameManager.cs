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

    [SerializeField]
    private SoundPlayer soundPlayer;
    [SerializeField]
    private MusicPlayer musicPlayer;

    [SerializeField]
    private GameObject nextLevelBlob;

    [SerializeField]
    private SpeedBand speedBand;

    [SerializeField]
    private Level currentLevel;

    [SerializeField]
    private GameObject character;

    private Level nextLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicPlayer.StartPlaying();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetCharacter(){
        return this.character;
    }

    void Start()
    {
        nextLevel = currentLevel.GetNextLevel();
    }

    public void OpenNextLevel()
    {
        Debug.Log("Yes! Next level!");
        Debug.Log(currentLevel);
        Debug.Log(nextLevel);
        currentLevel.gameObject.SetActive(false);
        if (nextLevel == null)
        {
            // UI show stuff heer
            Debug.Log("The End");
        }
        else {
            currentLevel = nextLevel;
            character.GetComponent<CharacterMovement>().EmptySticky();
            currentLevel.gameObject.SetActive(true);
            nextLevel = currentLevel.GetNextLevel();
        }
    }

    public void PlaySound(SoundType soundType){
        soundPlayer.PlaySound(soundType);
    }

    public void PlayLeveledSound(SoundType soundType, int soundLevel)
    {
        soundPlayer.PlayLeveledSound(soundType, soundLevel);
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
    }
}
