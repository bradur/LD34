// Date   : 13.12.2015 09:34
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;

    [SerializeField]
    private Animator animator;

    private PopupManager popupManager;

    private Vector3 scale = new Vector3(0.2f, 0.2f, 1f);
    private Vector3 newPosition = Vector3.zero;

    public void Init(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }

    public void Clear()
    {
        txtComponent.text = "";
        transform.position = Vector3.zero;
        animator.SetTrigger("Finish");
        popupManager.DestroyPopup(this);
        gameObject.SetActive(false);
    }

    public void StartStationaryIdle()
    {
        animator.SetTrigger("ShowStationaryIdle");
    }

    public void Show(string message, Vector3 position, Transform parent, PopupType popupType)
    {
        transform.parent = parent;
        newPosition = position;
        newPosition.z = 0f;
        transform.position = newPosition;
        transform.localScale = scale;
        txtComponent.text = message;
        if (popupType == PopupType.Short)
        {
            animator.SetTrigger("Show");
        }
        else if(popupType == PopupType.Lingering)
        {
            animator.SetTrigger("ShowLingering");
        }
        else if (popupType == PopupType.Stationary)
        {
            animator.SetTrigger("ShowStationary");
        }
        else if (popupType == PopupType.Big)
        {
            txtComponent.fontSize = 150;
            animator.SetTrigger("ShowStationary");
        }
        
    }

}
