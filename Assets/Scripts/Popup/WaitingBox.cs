using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//  ----------------------------------------------
//  Author:     CuongCT <caothecuong91@gmail.com> 
//  Copyright (c) 2016 OneSoft JSC
// ----------------------------------------------
public class WaitingBox : MonoBehaviour
{
    public System.Action<Hashtable> action;
    [SerializeField]
    Image icon, background;

    private static WaitingBox instance;

    public static WaitingBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load<WaitingBox>(PathPrefabs.WAITING_BOX), GameController.Instance.HomeScene.tfPopupParent);
            // Configure popup
        }
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ShowWaiting()
    {
        gameObject.SetActive(true);
        GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);
        icon.color = new Color(1, 1, 1, 1);
        background.color = new Color(1, 1, 1, 0.5f);
    }

    public void ShowWaitingWithoutIcon()
    {
        ShowWaiting();
        icon.color = new Color(1, 1, 1, 0);
        background.color = new Color(1, 1, 1, 0);
    }

    public void HideWaiting()
    {
        gameObject.SetActive(false);
        // FunctionHelper.ShowDebug("stop all Waitingbix");
        StopAllCoroutines();
    }

    public void ShowWaiting(float time)
    {// Show va Hide, ko lam gi ca
        action = null;
        ShowWaiting();
        StartCoroutine(TimeOut(time));
    }

    public void ShowWaiting(float time, System.Action<Hashtable> action)
    {// Show va Hide, ko lam gi ca
        ShowWaiting();
        this.action = action;
        //FunctionHelper.ShowDebug("stop all waitingbox");
        StopAllCoroutines();
        StartCoroutine(TimeOut(time));
    }

    private IEnumerator TimeOut(float time)
    {
        yield return new WaitForSeconds(time);
        //FunctionHelper.ShowDebug ("TimeOut");
        gameObject.SetActive(false);
        if (action != null)
        {
            action(new Hashtable());
        }
    }
}
