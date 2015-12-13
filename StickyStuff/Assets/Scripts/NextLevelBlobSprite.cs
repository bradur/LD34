// Date   : 12.12.2015 13:42
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class NextLevelBlobSprite : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    private bool hasCollided = false;
    private int soundLevel = 0;

    [SerializeField]
    private Transform messagePosition;

    [SerializeField]
    private string message = "Can you reach me?";

    public void SaySomething()
    {
        GameManager.instance.popupManager.ShowPopup(message, messagePosition.position, PopupType.Lingering);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BenignObject>() != null)
            {
                soundLevel = collision.gameObject.GetComponent<BenignObject>().objectLevel + 1;
            }
            
            GameManager.instance.soundPlayer.PlayLeveledSound(SoundType.Collision, soundLevel);
            animator.SetTrigger("EnterCenter");
            hasCollided = true;
            GameManager.instance.NextLevelBlobFound();
        }
    }

}
