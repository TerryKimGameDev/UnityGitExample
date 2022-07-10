using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosives : MonoBehaviour, IDamagable
{

    [Range(5, 1000)] [SerializeField] int damage;
    [SerializeField] GameObject explode;

    // Start is called before the first frame update

    bool inRange;
    Collider other;
    IDamagable isDamageable;
    void Awake()
    {
        
    }

    void Start()
    {

    }


    public void takeDamage(int dmg)
    {   
        Instantiate(explode, transform.position, explode.transform.rotation);
        if (inRange)
        {
            isDamageable.takeDamage(damage);
        }

        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        inRange = true;
        isDamageable = other.GetComponent<IDamagable>();

    }


    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

}
