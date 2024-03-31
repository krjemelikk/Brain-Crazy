using UnityEngine;

public class Level_183_RedLight : MonoBehaviour
{
    private DragUI dragUI;
    private Level_183 level;

    public void Init(Level_183 level)
    {
        this.level = level;
        dragUI = this.GetComponent<DragUI>();
    }

    public void OnBeginDrag()
    {
        this.transform.parent = level.Map;
    }

    public void OnEndDrag()
    {
        if(Vector2.Distance(this.transform.position, level.posRedLight.position) <= 0.2f)
        {
            this.transform.position = level.posRedLight.position;
            level.isRedLight = true;
            dragUI.isCanActive = false;
            level.dog.typeDrag = TypeDrag.Y_axis;
        }
    }
}
