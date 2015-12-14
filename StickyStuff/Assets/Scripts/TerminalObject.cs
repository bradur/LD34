// Date   : 13.12.2015 14:51
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class TerminalObject : MonoBehaviour {

    [SerializeField]
    [Range(0f, 10f)]
    private float acceleration;

    [SerializeField]
    private bool objectMoves = false;

    [SerializeField]
    private Transform objectTarget;

    private float speed = 1f;
    private float factor = 4f;

    [SerializeField]
    private Rigidbody2D rigidBody;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.soundPlayer.PlaySound(SoundType.Hurt);
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BenignObject>() != null)
            {
                collision.gameObject.GetComponent<BenignObject>().Kill();
            }
        }
    }

    void Update()
    {
        if (objectMoves)
        {
            rigidBody.AddForce((objectTarget.position - transform.position).normalized * acceleration);
            if ((objectTarget.position - transform.position).magnitude < 0.1f)
            {
                objectMoves = false;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
