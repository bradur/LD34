// Date   : 12.12.2015 15:38
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class NextLevelBlobParent : MonoBehaviour {

    private bool moving = false;
    private Vector3 targetPosition;

    [SerializeField]
    [Range(0f, 10f)]
    private float speed = 1f;
    [SerializeField]
    [Range(0f, 10f)]
    private float factor = 3f;

    [SerializeField]
    private Animator animator;

    private Vector3 newPosition;

    public void OpenNextLevel()
    {
        GameManager.instance.OpenNextLevel();
    }

    public void EnterCenter()
    {
        moving = true;
        targetPosition = GameManager.instance.GetCharacter().transform.position;
    }

    void Update()
    {
        if(moving){

            if ((transform.position.x - targetPosition.x) < 0.1f && (transform.position.y - targetPosition.y) < 0.1f)
            {
                transform.position = targetPosition;
                moving = false;
                GameManager.instance.PlaySound(SoundType.Finish);
                animator.SetTrigger("BounceCenter");
            }
            newPosition = Vector3.Lerp(
                transform.position,
                targetPosition,
                speed * factor * Time.deltaTime
            );
            newPosition.z = 0f;
            transform.localPosition = newPosition;
        }
    }
}
