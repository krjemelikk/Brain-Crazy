using System.Collections.Generic;
using UnityEngine;

public class MeteoriteObj : MonoBehaviour
{
    private bool IsMove;
    private Vector3 directionMove;
    [SerializeField] private float speedMove;

    [SerializeField] private GameObject hitEffectPrefab;
    private List<GameObject> poolHits;
    [SerializeField] private Transform parrentHit;

    public void SetMove(TypePosSpawn typePosSpawn, Vector3 posSpawn, Transform parrentHit)
    {
        this.parrentHit = parrentHit;
        IsMove = true;
        this.transform.position = posSpawn;
        switch (typePosSpawn)
        {
            case TypePosSpawn.Top:
                //directionMove = Helper.GetDirectionFromAngle_2(Vector2.down, Random.Range(225, 315), posSpawn);
                break;
            case TypePosSpawn.Botton:
                directionMove = Helper.GetDirectionFromAngle_2(Vector2.down, Random.Range(135, 225), posSpawn);
                break;
            case TypePosSpawn.Left:
                directionMove = Helper.GetDirectionFromAngle_2(Vector2.down, Random.Range(45, 135), posSpawn);
                break;
            case TypePosSpawn.Right:
                directionMove = Helper.GetDirectionFromAngle_2(Vector2.down, Random.Range(225, 315), posSpawn);
                break;
        }

        StartCoroutine(Helper.StartAction(() => { this.gameObject.SetActive(false); }, 1f));
    }

    public void Update()
    {
        if (IsMove)
        {
            this.transform.Translate(directionMove * speedMove * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Tag.EARTH_TAG) || collision.gameObject.tag.Equals(Tag.SHIELD_TAG))
        {
            IsMove = false;
            var hit = GetEffectHit();
            hit.gameObject.SetActive(true);
            hit.transform.position = this.transform.position;
            Attacked();
        }
    }

    private void Attacked()
    {
        this.gameObject.SetActive(false);
        StopAllCoroutines();
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
