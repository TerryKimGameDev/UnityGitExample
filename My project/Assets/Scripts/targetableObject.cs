using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetableObject : MonoBehaviour, ITarget
{

    bool canMove = false;
    Vector3 trn;
    // Start is called before the first frame update
    void Start()
    {
        trn = transform.position;
        trn.y += 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            gamemanager.instance.player.SetActive(false);
            gamemanager.instance.player.transform.position = trn;            
            gamemanager.instance.player.SetActive(true);
            canMove = false;
        }

        //playerDir = gamemanager.instance.player.transform.position - transform.position;
        //if (playerInRange)
        //{

        //    agent.SetDestination(gamemanager.instance.player.transform.position);

        //    canSeePlayer();
        //    facePlayer();
        //}
        //else if (agent.remainingDistance < 0.1f)
        //    roam();
        ////agent.SetDestination(transform.position);
    }

    public void Launch()
    {
        canMove = true;
    }
}
