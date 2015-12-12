// Date   : 12.12.2015 20:39
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    [SerializeField]
    private NextLevelBlobParent nextLevelBlob;

    [SerializeField]
    private Level nextLevel;

    public NextLevelBlobParent GetNextLevelBlob()
    {
        return this.nextLevelBlob;
    }

    public Level GetNextLevel()
    {
        return this.nextLevel;
    }

}
