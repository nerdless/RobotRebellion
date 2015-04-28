using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

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
    public int score;

    public float timelimit;
    public float timeleft;
    bool finished;
    public Slider timeSlider;

    public int startHealth = 50;
    public int currentHealth;
    bool paining;
    public Slider healthSlider;
    public AudioClip deathClip;
    public float paintinTime;
    AudioSource playerAudio;
    public Color finalColor;

    Transform playerbody;
    public Vector3 spawnpoint;
    Quaternion initialrotation;
    Ray downRay;
    public Vector3 downOffset;
    RaycastHit hit;
    public float downDistance;
    public float floordistance;
    int layerMask;
    float direction;
    bool dead;
    AudioSource music;
    bool paused;

    public GameObject pauseCanvas;
    public GameObject winCanvas;


    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerAudio = GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("music").audio;
        height = capsuleCollider.height;
        jumpState1 = Animator.StringToHash("Base Layer.jumpA");
        jumpState2 = Animator.StringToHash("Base Layer.jumpC");
        jumpState3 = Animator.StringToHash("Base Layer.jumpB");
        playerbody = GameObject.FindGameObjectWithTag("body").transform;
        playerbody.renderer.sharedMaterial.color = Color.white;
        jumping = false;
        tiempo = Time.time;
        tiempo1 = Time.time;
        score = 0;
        finished = false;
        currentHealth = startHealth;
        paining = false;
        initialrotation = transform.rotation;
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        dead = false;
        paused = false;
        pauseCanvas.SetActive(false);
        winCanvas.SetActive(false);

    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        timeleft = timelimit - (Time.time - tiempo);
        if (timeleft < 0 & !dead & !finished)
        {
            TakeDamage(currentHealth);
        }
        if (!finished)
            speed = Mathf.Lerp(0, 2, (Time.time - tiempo1) / smooth);
        else
            speed = Mathf.Lerp(2, 0, (Time.time - tiempo1) / smooth); // si no entre con el maximo hay bug.
        animator.SetFloat("speed", speed);
        animator.speed = speed;
        direction = Input.GetAxis("Horizontal");
        animator.SetFloat("direction", direction);
        if (!dead & grounded & !jumping)
        {

            transform.rigidbody.velocity = -transform.right * speed * 4f;
            transform.Rotate(Vector3.up * direction * 2f);
        }

        if (Input.GetKey(KeyCode.S))
            tiempo1 = Time.time - smooth / 2;
        if (Input.GetKey(KeyCode.O))
            Debug.Log(transform.rigidbody.velocity.magnitude);
        //poner animacion de caida B con un reaycast hacia abajo
        // para corregir que se para despues de un salto se puede utilizar el mismo raycast metiendo en caidaC con un hit mas cerca
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {

                jumping = true;
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

            capsuleCollider.height = animator.GetFloat("heightCollider");
            capsuleCollider.center = transform.rigidbody.centerOfMass;
        }
        else
            capsuleCollider.height = height;
        downRay = new Ray(transform.position + downOffset, -transform.up);
        if (Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask)) //http://docs.unity3d.com/Manual/Layers.html
        {
            //Debug.Log("estas chocando");
            if (hit.transform.tag == "platform")
            {
                if (hit.distance > floordistance)
                {
                    //Debug.Log("estas volando");
                    grounded = false;
                }
            }
        }
        else
        {
            Debug.Log("ya valiste");
            grounded = false;
        }

        //Debug.DrawRay(transform.position + downOffset, -transform.up * downDistance, Color.green);

        if (paining)
        { playerbody.renderer.sharedMaterial.color = Color.red;
        Debug.Log("holas bolas");
        }
        else
            playerbody.renderer.sharedMaterial.color = Color.Lerp(Color.red, finalColor, paintinTime * Time.deltaTime);
        paining = false;

        timeSlider.value = timeleft;

        if (transform.position.y < -40 & !dead)
        {
            Reload();
            dead = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        paused=!paused;

        if (paused)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }

        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
               
    }

    public void TakeDamage(int damageAmount)
    {
        paining = true;
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if (currentHealth <= 0 & !dead)
        {
            animator.SetBool("die", true);
            
            dead = true;
            //playerAudio.clip = deathClip;
            music.SendMessage("Restarding");
            StartCoroutine(Reload());
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy1")
        {
            TakeDamage(10);
            Debug.Log("Chocaste con enemigo1");
            tiempo1 = Time.time;
        }
        if (collision.transform.tag == "platform")
        {
            //Debug.Log("grounded");
            grounded = true;
            animator.SetBool("ground", true);
            jumping = false;
        }
        if (collision.transform.tag == "bullet")
        {

            TakeDamage(5);
            Debug.Log("Chocaste con enemigo1");
        }
        if (collision.transform.tag == "lava")
        {

            TakeDamage(currentHealth);
            Debug.Log("Chocaste con lava");
        }
    }

    void OnCollisionStay(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {

    }
    
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        transform.rigidbody.velocity = Vector3.zero;
        //transform.position = spawnpoint;
        //transform.rotation = initialrotation;
        //animator.SetBool("die", false);
        //jumping = false;
        //tiempo = Time.time;
        //score = 0;
        //finished = false;
        //currentHealth = startHealth;
        //dead = false;
        //playerbody.renderer.sharedMaterial.color = Color.white;
        //healthSlider.value = currentHealth;
        //UpdateScore.score = score;
        Application.LoadLevel(1);

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "points")
        {
            score += 5;
            UpdateScore.score = score;
            Debug.Log("score = " + score);
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Meta")
        {
            score += Mathf.FloorToInt(timeleft * 100);
            Debug.Log("score = " + score);
            finished = true;
            winCanvas.SetActive(true);
            tiempo1 = Time.time;

        }
        if (collision.transform.tag == "life")
        {
            currentHealth += 10;
            healthSlider.value = currentHealth;
            Debug.Log("tomaste vida ");
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "time")
        {
            tiempo += 10;
            
            Debug.Log("tomaste tiempo ");
            Destroy(collision.gameObject);
        }
    }


}
