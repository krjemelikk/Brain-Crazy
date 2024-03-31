using UnityEngine;

public class WeightObject : MonoBehaviour
{
    [SerializeField] private float weight;
    public float Weight
    {
        get { return weight; }
    }

    private RectTransform m_rectTransform;
    public RectTransform rectTransform
    {
        get { if (m_rectTransform == null) m_rectTransform = this.gameObject.GetComponent<RectTransform>(); return m_rectTransform; }
    }
}

