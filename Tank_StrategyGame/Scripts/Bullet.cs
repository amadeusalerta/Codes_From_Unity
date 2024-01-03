using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public float lifeTime;
     public float damage;
     public float penetrations;
     public List<string> targetTags;
    public SphereCollider col;
    public Transform trail;

    public GameObject impactPrefab;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger)
            return;

        if (collision.CompareTag("Wall"))
        {
            if(trail!=null)trail.SetParent(null);
            Destroy(gameObject);
        }
        else
        {
            if (targetTags.Contains(collision.tag))
            {
                // Damage part
                Unit unit = collision.GetComponent<Unit>();

                if (unit == null)
                {
                    Destroy(gameObject);
                    return;
                }
                unit.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
    }
}