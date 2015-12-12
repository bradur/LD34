// Date   : 12.12.2015 13:42
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class NextLevelBlobSprite : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    private bool hasCollided = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("EnterCenter");
            hasCollided = true;
        }
    }
}
