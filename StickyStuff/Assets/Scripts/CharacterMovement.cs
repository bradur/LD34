// Date   : 12.12.2015 10:35
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour {

    [SerializeField]
    [Range(0f, 1f)]
    private float minimumVelocity = 1;

    [SerializeField]
    [Range(0f, 10f)]
    private float speed = 1;
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private ForceMode forceMode;

    private bool leftButtonDown = false;
    private bool rightButtonDown = false;

    public void ButtonDown(string button)
    {
        if (button == "left")
        {
            rightButtonDown = false;
            leftButtonDown = true;
        }
        else if (button == "right")
        {
            leftButtonDown = false;
            rightButtonDown = true;
        }
    }

    public void ButtonUp(string button) {
        if (button == "left")
        {
            leftButtonDown = false;
        }
        else if (button == "right")
        {
            rightButtonDown = false;
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonUp("left");
            ButtonUp("right");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonDown("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonDown("right");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ButtonUp("left");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ButtonUp("right");
        }
        if (leftButtonDown)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, 0f, speed), forceMode);
        }
        else if (rightButtonDown)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, 0f, -speed), forceMode);
        }
        else if (Mathf.Abs(rigidBody.angularVelocity.z) <= minimumVelocity)
        {
            rigidBody.angularVelocity = Vector3.zero;
        }
        //Debug.Log("[RIGHT: " + (rightButtonDown ? "<color=green>ON</color>" : "<color=red>OFF</color>") + "] [LEFT: " + (leftButtonDown ? "<color=green>ON</color>" : "<color=red>OFF</color>") + "] [VEL: " + rigidBody.angularVelocity.z + "]");
    }
}
