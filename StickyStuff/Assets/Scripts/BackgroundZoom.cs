// Date   : 13.12.2015 14:16
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class BackgroundZoom : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    public void StartZoom()
    {
        animator.SetTrigger("Zoom");
    }

    public void Finished()
    {
        GameManager.instance.BackgroundZoomFinished();
    }

}
