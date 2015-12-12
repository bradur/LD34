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

    [SerializeField]
    private Transform stickyContainer;

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
        GameManager.instance.soundPlayer.PlaySound(SoundType.Turn);
    }

    public Transform GetStickyContainer()
    {
        return stickyContainer;
    }

    public void EmptySticky()
    {
        foreach (Transform child in stickyContainer)
        {
            Destroy(child.gameObject);
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
        GameManager.instance.soundPlayer.StopLoopedSound();
    }

    void Update () {
        float angularVelocity = Mathf.Abs(rigidBody.angularVelocity.z);
        if (leftButtonDown)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, 0f, speed), forceMode);
        }
        else if (rightButtonDown)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, 0f, -speed), forceMode);
        }
        else if (angularVelocity <= minimumVelocity){
            rigidBody.angularVelocity = Vector3.zero;
        }
        GameManager.instance.UpdateSpeedBand(angularVelocity);
        //Debug.Log("[RIGHT: " + (rightButtonDown ? "<color=green>ON</color>" : "<color=red>OFF</color>") + "] [LEFT: " + (leftButtonDown ? "<color=green>ON</color>" : "<color=red>OFF</color>") + "] [VEL: " + rigidBody.angularVelocity.z + "]");
    }
}
