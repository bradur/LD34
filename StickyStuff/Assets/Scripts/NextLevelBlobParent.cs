// Date   : 12.12.2015 15:38
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class NextLevelBlobParent : MonoBehaviour {

    public void OpenNextLevel()
    {
        GameManager.instance.OpenNextLevel();
    }
}
