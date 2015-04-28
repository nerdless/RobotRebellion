using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Animator animator;
    int jumpState1;
    int jumpState2;
    int jumpState3;
    AnimatorStateInfo currentState;
    CapsuleCollider capsuleCollider;
    float height;
    public float reloadTime;
    float speed;
    public float smooth;
    float tiempo;
    float tiempo1;
    bool jumping;
    float animatortmpspeed;
    public float jumpforce;
    bool grounded;
    int score;
    public float timelimit;
    float timeleft;
    bool finished;
    int lifepoints;
    Transform playerbody;
    public Vector3 spawnpoint;
    Quaternion initialrotation;
    Ray downRay;
    public Vector3 downOffset;
    RaycastHit hit;
    public float downDistance;
    public float floordistance;
    int layerMask;

    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        height = capsuleCollider.height;
        jumpState1 = Animator.StringToHash("Base Layer.jumpA");
        jumpState2 = Animator.StringToHash("Base Layer.jumpC");
        jumpState3 = Animator.StringToHash("Base Layer.jumpB");
        playerbody = GameObject.FindGameObjectWithTag("body").transform;
        jumping = false;
        tiempo = Time.time;
        tiempo1 = Time.time;
        score = 0;
        finished = false;
        lifepoints = 3;
        playerbody.renderer.sharedMaterial.color = Color.white;
        initialrotation = transform.rotation;
        layerMask = 1 << 8;
        layerMask = ~layerMask;
                
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        timeleft= timelimit -(Time.time-tiempo);
        if (timeleft < 0)
        {
            animator.SetBool("die", true);
            StartCoroutine(Reload());
        }
        if (!finished)
            speed = Mathf.Lerp(0, 2, (Time.time - tiempo1) / smooth);
        else
            speed = Mathf.Lerp(2, 0, (Time.time - tiempo1) / smooth); // si no entre con el maximo hay bug.
        animator.SetFloat("speed", speed);
        animator.speed = speed;
        animator.SetFloat("direction", Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.O))
            animator.SetBool("raise", true);
        if (Input.GetKeyUp(KeyCode.O))
            animator.SetBool("raise", false);
        if (Input.GetKey(KeyCode.S))
            tiempo1 = Time.time - smooth / 2;
        //poner animacion de caida B con un reaycast hacia abajo
        // para corregir que se para despues de un salto se puede utilizar el mismo raycast metiendo en caidaC con un hit mas cerca
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                //animator.SetBool("jump", true);
                jumping = true;
                //grounded = false;
                animator.applyRootMotion = false;
                //transform.rigidbody.velocity = -transform.right * 5.5f * animator.speed;
                transform.rigidbody.AddForce(Vector3.up * jumpforce);
            }
        }

        //animator.SetBool("jump", Input.GetKeyDown(KeyCode.Space));
        if (!grounded)
        {
            animator.SetBool("ground", false);
            if (jumping)
            {
                animator.SetBool("jump", true);
                jumping = false;
            }
        }
        if (currentState.nameHash == jumpState1 || currentState.nameHash == jumpState2 || currentState.nameHash == jumpState3)
        {
            animator.speed = 1;
            animator.SetBool("jump", false);
            //if (jumping == false)
            //{
              //  jumping = true;
                //transform.rigidbody.AddForce(Vector3.up * jumpforce);
            //}
            //animator.applyRootMotion = false;
            capsuleCollider.height = animator.GetFloat("heightCollider");
            capsuleCollider.center = transform.rigidbody.centerOfMass;
        }
        else
            capsuleCollider.height = height;
        downRay = new Ray(transform.position + downOffset, -transform.up);
        if (Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask)) //http://docs.unity3d.com/Manual/Layers.html
        {
            Debug.Log("estas chocando");
            if (hit.transform.tag == "platform")
            {
                if (hit.distance > floordistance)
                {
                    Debug.Log("estas volando");
                    grounded = false;
                }
            }
        }
        else
        {
            Debug.Log("ya valiste");
            grounded = false;
        }

        Debug.DrawRay(transform.position + downOffset, -transform.up * downDistance, Color.green);
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy1")
        {
            lifepoints -= 1;
            Debug.Log("Chocaste con enemigo1");
            tiempo = Time.time;
            //StartCoroutine(Reload());
            StartCoroutine(Paining());


        }
        if (collision.transform.tag == "platform")
        {
            Debug.Log("grounded");
            grounded = true;
            animator.SetBool("ground", true);
            animator.applyRootMotion = true;
            jumping = false;
        }
        if (collision.transform.tag == "tree")
        {
            
            Debug.Log("Chocaste con arbol");
            tiempo = Time.time;
        }
       
    }

    void OnCollisionStay(Collision collision)
    {
        
    }

    void OnCollisionExit(Collision collision)
    {
                
    }
    IEnumerator Paining()
    {
        Color cuerpo = Color.red;
        int i = 0;
        while (i < 5)
        {
            playerbody.renderer.sharedMaterial.color = cuerpo;
            if (cuerpo == Color.red)
                cuerpo = Color.white;
            else
                cuerpo = Color.red;
            i++;
            yield return new WaitForSeconds(0.2f);
        }
        playerbody.renderer.sharedMaterial.color = Color.white;
        
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        transform.position = spawnpoint;
        transform.rotation = initialrotation;
        animator.SetBool("die", false);
        jumping = false;
        tiempo = Time.time;
        score = 0;
        finished = false;
        lifepoints = 3;
        playerbody.renderer.sharedMaterial.color = Color.white;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "points")
        {
            score += 5;
            Debug.Log("score = " + score);
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Meta")
        {
            score += Mathf.FloorToInt(timeleft * 100);
            Debug.Log("score = " + score);
            finished = true;
            tiempo = Time.time;            
        }
    }
}
