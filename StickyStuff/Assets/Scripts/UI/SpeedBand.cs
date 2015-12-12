// Date   : 12.12.2015 16:03
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedBand : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Gradient speedGradient;
    [SerializeField]
    private Image imgComponent;
    [SerializeField]
    private RectTransform rtRectMask;

    [SerializeField]
    private float maxSpeed;

    private Vector2 varyingSize;

    void Start () {
        varyingSize = rtRectMask.sizeDelta;
    }

    public void UpdateSpeedBand(float speed)
    {
        varyingSize.x = speed / maxSpeed * 100f;
        rtRectMask.sizeDelta = varyingSize;
        txtComponent.text = (Mathf.Round(speed * 1000.0000f) / 1000.0000f) + "";
        imgComponent.color = speedGradient.Evaluate(varyingSize.x / 100f);
    }
}
