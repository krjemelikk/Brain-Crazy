using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NGUI Enhance item example
/// </summary>
public class MyNGUIEnhanceItem : EnhanceItem
{
    private RawImage mTexture;

    protected override void OnAwake()
    {
        this.mTexture = GetComponent<RawImage>();
        mTexture.GetComponent<Button>().onClick.AddListener(()=>OnClickNGUIItem(this.gameObject));
    }

    private void OnClickNGUIItem(GameObject obj)
    {
        this.OnClickEnhanceItem();
    }

    // Item is centered
    public override void SetSelectState(bool isCenter)
    {
        if (mTexture == null)
            mTexture = this.GetComponent<RawImage>();
        if (mTexture != null)
            mTexture.color = isCenter ? Color.white : Color.gray;
    }

    protected override void OnClickEnhanceItem()
    {
        // item was clicked
        base.OnClickEnhanceItem();
    }
}
