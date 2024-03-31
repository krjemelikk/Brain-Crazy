using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class EarthLv84Obj : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    private List<GameObject> poolHits;
    [SerializeField] private Transform parrentHit;
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Tag.METEORITE))
        {
            this.PostEvent(EventID.METEORITE_COLLISION_EARTH);
            var hit = GetEffectHit();
            hit.gameObject.SetActive(true);
            hit.transform.position = this.transform.position;
            this.gameObject.SetActive(false);

        }
    }

    public GameObject GetEffectHit()
    {
        if (poolHits == null)
            poolHits = new List<GameObject>();

        for (int i = 0; i < poolHits.Count; i++)
        {
            if (!poolHits[i].activeSelf)
                return poolHits[i];
        }

        GameObject hit = Instantiate(hitEffectPrefab, parrentHit.transform);
        poolHits.Add(hit);
        return hit;
    }

}
