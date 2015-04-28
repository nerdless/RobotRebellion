using UnityEngine;
using System.Collections;

public class turn : MonoBehaviour {

    bool grounded;
    //para el freno
    public float tmpDrag;
    public float initialDrag;
    //para doblar
    public float turnforce;
	//para saltar
	public float jumpForce;
	//variables de estado
	bool died;
	bool jumping;

	float speed;
	Vector3 position;

	int score;
	int lifepoints;

	Animator animatorcontroller;


    
	void Start () 
    {
		lifepoints = 3;
		score = 0;
		position = transform.position;
		animatorcontroller = transform.GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Implemetation of break
        if (Input.GetKey(KeyCode.S) & grounded)
            transform.rigidbody.drag = tmpDrag;
        else
            transform.rigidbody.drag = initialDrag;
        //Implementation of turning
        if (grounded)
        {
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(-Vector3.forward * turnforce);
			if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
                transform.Translate(Vector3.forward * turnforce);
			if (Input.GetKeyDown(KeyCode.Space))
				transform.rigidbody.AddForce(Vector3.up * jumpForce);
        }
       
		
		


	}

    void FixedUpdate()
    {
        //Calculo de speed
        speed = (transform.position.magnitude - position.magnitude);
        //Debug.Log("position1 " + position.magnitude);
        position = transform.position;
        //Update animator
        //Debug.Log("position2 " + position.magnitude);
        animatorcontroller.SetFloat("speed", speed);
        Debug.Log("speed " + speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "mountain")
        {
            grounded = true;
        }
		if (collision.transform.tag == "tree")
		{
			lifepoints -=1;
			collision.transform.tag = "wall";
			Debug.Log("lifepoints = "+ lifepoints);
		}
			
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == "mountain")
        {
            grounded = false;
            Debug.Log("volando");
        }
    }

	void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag == "points")
		{
			score += 5;
			Debug.Log("score = "+score);
			Destroy(collision.gameObject);
		}
	}

   
}
