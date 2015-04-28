using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour 
{

    Animator animator;
    NavMeshAgent agent;
    Transform player;
    Vector3 place;
    bool target;
    public bool chasing;
    int firsttime;
    public Transform barrel;
    public ParticleSystem explotion;
    AudioSource music;
    public AudioClip[] soundEffects;
    ParticleSystem explot;
    PlayerMovement playerhealth;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerhealth = player.GetComponent<PlayerMovement>();
        target = false;
        chasing = false;
        firsttime = 1;
        Wacht();
    }


    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (agent.remainingDistance < 0.8)
        {
            if (chasing == false)
            {
                if (firsttime == 1)
                {
                    animator.SetFloat("speed", 0f);
                    StartCoroutine(Waitforit(6f));
                    firsttime++;
                }
            }


        }
        
        if (target == false)
        {
            Wacht();
        }

    }

    void Wacht()
    {
        target = true;
        chasing = false;
        place = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        agent.SetDestination(place);
        // music.SendMessage("Normal");
        animator.SetFloat("speed", 0.15f);
        animator.SetBool("raise", false);
    }

    void Chase()
    {
        target = true;
        chasing = true;
        //player.SendMessage("Chasing");
        agent.SetDestination(player.position);
        animator.SetFloat("speed", Mathf.Min(agent.remainingDistance * 0.9f, 0.9F));
        if (!animator.GetBool("raise"))
        {
            animator.SetBool("raise", true);
        }

    }

    IEnumerator Waitforit(float waittime)
    {
        target = true;
        //Debug.Log("esperando");
        yield return new WaitForSeconds(waittime);
        target = false;
        firsttime = 1;
        //Debug.Log("termino de esperar");
        Wacht();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (playerhealth.currentHealth > 0)
            {
                playerhealth.TakeDamage(10);
            }
            
            Debug.Log("Chocaste con enemigo1");
            
        }
    }

}
