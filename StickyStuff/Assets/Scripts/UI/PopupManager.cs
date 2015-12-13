// Date   : 13.12.2015 09:34
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum PopupType {
    Short,
    Lingering,
    Stationary
}

public class PopupManager : MonoBehaviour {

    [SerializeField]
    private Popup basicPopup;

    [SerializeField]
    private Dialog basicDialog;

    void Start()
    {
        InitPopupPool(10, basicPopup);
    }

    public void ShowPopup(string message)
    {
        ShowPopup(message, Vector3.zero);
    }

    public void ShowPopup(string message, Vector3 position)
    {
        ShowPopup(message, position, PopupType.Short);
    }

    public void ShowPopup(string message, Vector3 position, PopupType popupType)
    {
        Popup popup = GetPopup();
        popup.gameObject.SetActive(true);
        popup.Show(message, position, transform, popupType);
    }

    private List<Popup> popupList;
    private int popupIndex = 0;

    private void InitPopupPool(int size, Popup popupPrefab)
    {
        popupList = new List<Popup>();
        for (int i = 0; i < size; i++)
        {
            Popup popup = Instantiate(popupPrefab);
            popup.Init(this);
            popupList.Add(popup);
        }
    }

    public Popup GetPopup()
    {
        if (popupList.Count > 0)
        {
            Popup popup = popupList[0];
            popupList.RemoveAt(0);
            return popup;
        }
        return null;
    }

    public void DestroyPopup(Popup popup)
    {
        popupList.Add(popup);
    }

    public void ClearPool()
    {
        for (int i = popupList.Count - 1; i < 0; i--)
        {
            popupList[i].Clear();
        }
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Popup>() != null)
            {
                child.GetComponent<Popup>().Clear();
            }
        }
    }
}
