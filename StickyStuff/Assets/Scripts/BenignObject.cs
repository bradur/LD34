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

    private Transform target;

    private bool attached = false;

    [HideInInspector]
    public int objectLevel = 0;

    private bool appearing = false;
    private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private float speed = 1f;
    private float factor = 4f;
    private float timerBeforeAppearing = 0.5f;


    void Start()
    {
        target = GameManager.instance.GetCharacter().transform;
        transform.localScale = Vector3.zero;
    }

    void OnBecameVisible(){
        appearing = true;
        timerBeforeAppearing = Random.Range(0f, 1f);
    }

    void Update () {
        if (appearing)
        {
            if (timerBeforeAppearing <= 0) { 
                transform.localScale = Vector3.Lerp(
                    transform.localScale,
                    targetScale,
                    speed * factor * Time.deltaTime
                );
                if ((transform.localScale - targetScale).magnitude < 0.1f)
                {
                    appearing = false;
                    GameManager.instance.soundPlayer.PlaySound(SoundType.Blop);
                }
            }
            else
            {
                timerBeforeAppearing -= Time.deltaTime;
            }
        }
        else
        {
            rigidBody.AddForce((target.position - transform.position).normalized * acceleration);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached && collision.gameObject.tag == "Player")
        {
            attached = true;
            if (collision.gameObject.GetComponent<BenignObject>() != null)
            {
                objectLevel = collision.gameObject.GetComponent<BenignObject>().objectLevel + 1;
            }
            GameManager.instance.popupManager.ShowPopup((objectLevel + 1) + "", transform.position);
            GameManager.instance.soundPlayer.PlayLeveledSound(SoundType.Collision, objectLevel);
            CharacterMovement character = GameManager.instance.GetCharacter().GetComponent<CharacterMovement>();
            if (transform.parent.childCount == 1)
            {
                //GameManager.instance.SpawnNextLevelBlob();
                GameManager.instance.WaitForFinish();
            }
            transform.parent = character.GetStickyContainer();
            rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
            gameObject.tag = "Player";
        }
    }
}
