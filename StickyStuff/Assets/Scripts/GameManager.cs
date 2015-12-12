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

    private GameObject nextLevelBlob;

    [SerializeField]
    private SpeedBand speedBand;

    [SerializeField]
    private Level currentLevel;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private GameObject GameContainer;

    private Level nextLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicPlayer.StartPlaying();
            currentLevel = Instantiate(currentLevel);
            currentLevel.transform.parent = GameContainer.transform;
            currentLevel.transform.localPosition = Vector3.zero;
            nextLevel = currentLevel.GetNextLevel();
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

    public void OpenNextLevel()
    {
        currentLevel.Kill();
        if (nextLevel == null)
        {
            // UI show stuff heer
            Debug.Log("The End");
        }
        else {
            character.GetComponent<CharacterMovement>().EmptySticky();
            currentLevel = Instantiate(nextLevel);
            currentLevel.transform.parent = GameContainer.transform;
            currentLevel.transform.localPosition = Vector3.zero;
            nextLevel = currentLevel.GetNextLevel();
        }
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
