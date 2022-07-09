using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamagable
{
    [Header("Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rendBody;
    [SerializeField] Renderer rendHead;
    [SerializeField] Renderer rendFace;
    [SerializeField] Material enemycolor;

    [Header("------------------------------")]

    [Header("Enemy Attributes")]
    [SerializeField] int HP;
    [SerializeField] int viewAngle;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int roamRadius;
    // Start is called before the first frame update


    [Header("------------------------------")]

    [Header("Enemy Attributes")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;

    Color colorOrig;

    bool canShoot;
    bool playerInRange;
    Vector3 playerDir;
    Vector3 startingPos;
    float StoppingDisOrig;


    void Start()
    {
        //gamemanager.instance.playerScript.takeDamage(1);
        startingPos = transform.position;
        StoppingDisOrig= agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gamemanager.instance.player.transform.position - transform.position;
        if (playerInRange)
        {
            
            agent.SetDestination(gamemanager.instance.player.transform.position);

            canSeePlayer();
            facePlayer();
        }
        else if (agent.remainingDistance < 0.1f)
            roam();
            //agent.SetDestination(transform.position);
    }


    void roam()
    {
        agent.stoppingDistance = 0;
        Vector3 randomDir = Random.insideUnitSphere * roamRadius;
        randomDir += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, roamRadius, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);

    }

    void facePlayer()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //transform.LookAt(gamemanager.instance.player.transform.position);
            playerDir.y = 0;
            var rotation = Quaternion.LookRotation(playerDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
        }
    }

    void canSeePlayer()
    {
        float angle = Vector3.Angle(playerDir, transform.forward);
        //Debug.Log(angle);


        RaycastHit hit;

        if (Physics.Raycast(transform.position, playerDir, out hit))
        {
            Debug.DrawRay(transform.position, playerDir);
            if (hit.collider.CompareTag("Player") && canShoot && angle <= viewAngle)
                StartCoroutine(shoot());
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            canShoot = true;
            agent.stoppingDistance = StoppingDisOrig;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }
    }
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        playerInRange = true;
        StartCoroutine(flashColor());
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator flashColor()
    {
        //rend.material.color = Color.red;
        //coloration.color = Color.red;
        rendBody.material.color = rendHead.material.color = rendFace.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rendBody.material.color = rendHead.material.color = rendFace.material.color = enemycolor.color;
        //coloration.color = colorOrig;
        //rend.material.color = colorOrig;
    }


    IEnumerator shoot()
    {
        canShoot = false;

        Instantiate(bullet, transform.position, bullet.transform.rotation);

        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }
}
