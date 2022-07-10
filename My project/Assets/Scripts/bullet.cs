using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] int destroyTime;
    [SerializeField] GameObject hitEffect;



    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (gamemanager.instance.player.transform.position - transform.position).normalized * speed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamagable>() != null)
        {
            IDamagable isDamageable = other.GetComponent<IDamagable>();
            isDamageable.takeDamage(damage);
        }

        Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

        Destroy(gameObject);


    }

}
