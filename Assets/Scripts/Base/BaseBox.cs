using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseBox : MonoBehaviour
{    
	public static Stack<BaseBox> StackBox = new Stack<BaseBox> ();
	public static BaseBox currentBaseBox{
		get{
			//return StackBox.Count > 0 ? StackBox.Peek() : null;
			return null;

		}
	}  
	public delegate void BoxCallbackDelegate();
	public BoxCallbackDelegate OnCloseBox;	   
	[SerializeField]
	protected RectTransform mainPanel;

	[HideInInspector] public BackObj backObj;
	private bool isClose;

    [SerializeField] protected bool isAnim = true;

	public UnityAction actionCloseBase;
    public UnityAction actionBoxAppearDone;

    public bool isStack = true;

	protected virtual void Awake()
	{
		var canvas = this.GetComponent<Canvas>();
		if (canvas != null)
		{
			canvas.worldCamera = Camera.main;
			canvas.sortingLayerID = SortingLayer.NameToID("GUI");
		}
	}
	
	public virtual void OnClickCloseButton()
	{
		CloseCurrentBox ();
	}

	protected virtual void OnStart()
	{
		if(backObj == null)
			backObj = this.GetComponent<BackObj>();
        if (backObj != null)
            backObj.actionDoOff = ActionDoOff;
	}

	protected virtual void ActionDoOff()
	{
	}

	protected virtual void DoAppear()
	{
        if (isAnim)
        {
            if (mainPanel != null)
            {
                mainPanel.localScale = Vector3.zero;
                mainPanel.DOScale(1, 0.5f).SetUpdate(true).SetEase(Ease.OutBack).OnComplete(()=> { if (actionBoxAppearDone != null) actionBoxAppearDone(); });
            }
        }
	}

	protected virtual void OnEnable()
	{
        if (!isStack)
            return;
		if (currentBaseBox != null && currentBaseBox != this) {			
			currentBaseBox.Hide ();
		}
		StackBox.Push (this);
//		FunctionHelper.ShowDebug ("StackBox.Push ("+ this.name +");");
		
		DoAppear ();
        OnStart();
        isClose = false;

	}

	protected virtual void OnDisable()
	{
        if (!isStack)
            return;
        if (actionCloseBase != null)
		{
			actionCloseBase();
			actionCloseBase = null;
		}
	}

	public virtual void Show()
	{
		gameObject.SetActive(true);       
	}		  

	protected void DestroyBox()
	{
		if (OnCloseBox != null)
			OnCloseBox ();
		if (currentBaseBox != null) {
//			FunctionHelper.ShowDebug ("StackBox.Pop ("+ currentBaseBox.name +");");
			StackBox.Pop ();
		}
		Destroy(gameObject);
	}

	protected virtual void Hide()
	{
		//gameObject.SetActive(false);
	}

	public virtual bool CloseCurrentBox()
	{
        if (backObj == null)
            return false;

		if (!backObj.isDoOffed)
		{
			backObj.DoOff();
			backObj.isDoOffed = true;
			return false;
		}
		return false;
	}

	public virtual void Close()
	{
        if (backObj == null)
            return;

        backObj.DoOff();
	}
}
