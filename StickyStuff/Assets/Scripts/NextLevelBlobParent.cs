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

    [SerializeField]
    private NextLevelBlobSprite nextLevelBlobSprite;

    public void OpenNextLevel()
    {
        GameManager.instance.OpenNextLevel();
    }

    public void EnterCenter()
    {
        moving = true;
        targetPosition = GameManager.instance.GetCharacter().transform.position;
    }

    public void PlayAppearingSound()
    {
        GameManager.instance.soundPlayer.PlaySound(SoundType.ObjectComesIntoView);
    }

    public void SaySomething()
    {
        nextLevelBlobSprite.SaySomething();
    }

    void Update()
    {
        if(moving){

            if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f && Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f)
            {
                transform.position = targetPosition;
                moving = false;
                GameManager.instance.soundPlayer.PlaySound(SoundType.Finish);
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
