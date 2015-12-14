// Date   : 12.12.2015 12:26
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum ButtonDirection
{
    Left,
    Right
}

public class RotationButton : MonoBehaviour {

    [SerializeField]
    private Color colorDown;
    private Color colorUp;
    [SerializeField]
    private Image imgComponent;
    [SerializeField]
    private CharacterMovement characterMovement;

    [SerializeField]
    private ButtonDirection direction;

    [SerializeField]
    private RectTransform androidPosition;

    private string directionString;

    void Start()
    {
        #if UNITY_ANDROID
            GetComponent<RectTransform>().position = androidPosition.position;
        #endif
        colorUp = imgComponent.color;
        if (direction == ButtonDirection.Left)
        {
            directionString = "left";
        }
        else if (direction == ButtonDirection.Right)
        {
            directionString = "right";
        }
    }

    public void ButtonDown()
    {
        characterMovement.ButtonDown(directionString);
        imgComponent.color = colorDown;
    }

    public void ButtonUp()
    {
        characterMovement.ButtonUp(directionString);
        imgComponent.color = colorUp;
    }
}
