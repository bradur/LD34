// Date   : 13.12.2015 19:15
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;

    [SerializeField]
    private Text txtOldScore;

    [SerializeField]
    private Animator animator;

    private int score = 0;
    private int targetScore = 0;

    private float speed = 0;

    [SerializeField]
    private float duration = 0.2f;

    private bool isUpdating = false;

    private Queue scoreQueue = new Queue();

    public void AddToScore(int score)
    {
        if (score > 0) {
            scoreQueue.Enqueue(score);
        }
    }

    public int GetScore()
    {
        return this.targetScore;
    }

    public void SetRecord(int recordScore)
    {
        txtOldScore.text = recordScore + "";
    }

    public void ResetScore()
    {
        score = 0;
        scoreQueue.Clear();
        isUpdating = false;
        speed = 0;
        targetScore = 0;
        txtComponent.text = "";
    }

    void Update()
    {
        if (isUpdating)
        {
            score = (int)Mathf.Lerp(score, targetScore, speed);
            if (speed < 1)
            {
                speed += Time.deltaTime / duration;
            }
            else
            {
                speed = 0;
                isUpdating = false;
                score = targetScore;
            }
            txtComponent.text = score + "";
        }
        else
        {
            if (scoreQueue.Count > 0)
            {
                animator.SetTrigger("UpdateScore");
                isUpdating = true;
                this.targetScore += (int)scoreQueue.Dequeue();
            }
        }
    }
}
