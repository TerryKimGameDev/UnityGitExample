using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class explosives : MonoBehaviour, Explode
{
    [Range(5, 1000)] [SerializeField] int damage;
    [Range(3, 10)] [SerializeField] int radius;
    [SerializeField] GameObject effect;
    //[SerializeField] GameObject barrel;

    // Start is called before the first frame update
    public void explode()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }

        Collider[] damagableEntities = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider entity in damagableEntities)
        {
            if (entity.GetComponent<IDamagable>() != null /*&& entity != barrel*/)
            {
                entity.GetComponent<IDamagable>().takeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
