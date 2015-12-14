// Date   : 13.12.2015 14:16
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundZoom : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private bool isAnimating = false;

    [SerializeField]
    private List<Color> targetColors = new List<Color>();
    private Color targetColor;

    private Color originalColor;

    private float speed;

    [SerializeField]
    private float duration = 2f;

    [SerializeField]
    private SpriteRenderer sprite;

    public void StartZoom()
    {
        animator.SetTrigger("Zoom");
        isAnimating = true;
        targetColor = targetColors[Random.Range(0, targetColors.Count)];
        //originalColor = sprite.color;
    }

    public void Finished()
    {
        GameManager.instance.BackgroundZoomFinished();
        isAnimating = false;
        speed = 0f;
        //sprite.color = originalColor;
    }

    void Update()
    {
        if(isAnimating){
            sprite.color = Color.Lerp(sprite.color, targetColor, speed);
            if(speed < 1f){
                speed += Time.deltaTime / duration;
            } else {
                isAnimating = false;
            }
        }
        
    }

}
