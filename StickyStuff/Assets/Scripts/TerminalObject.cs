// Date   : 13.12.2015 14:51
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class TerminalObject : MonoBehaviour {

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
}
