// Date   : 12.12.2015 11:59
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BenignObject : MonoBehaviour {

    [SerializeField]
    [Range(0f, 10f)]
    private float acceleration;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Transform target;

    private bool attached = false;

    public void Init(Transform target)
    {
        this.target = target;
    }

    void Update () {
        rigidBody.AddForce((target.position - transform.position).normalized * acceleration);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached && collision.gameObject.tag == "Player")
        {
            attached = true;
            GameManager.instance.PlaySound(SoundType.Collision);
            if (transform.parent.childCount == 1)
            {
                GameManager.instance.SpawnNextLevelBlob();
            }
            transform.parent = collision.gameObject.transform;
            rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
            gameObject.tag = "Player";
        }
    }
}