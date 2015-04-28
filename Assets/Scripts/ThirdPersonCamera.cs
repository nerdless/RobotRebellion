using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
    public float smooth;
    Transform lookAt;
	
	void Start () 
    {
        lookAt = GameObject.FindGameObjectWithTag("lookat").transform;
	}
	

	void Update ()  
    {
        if (lookAt == null)
            lookAt = GameObject.FindGameObjectWithTag("lookat").transform;
        else
        {
            transform.position = Vector3.Lerp(transform.position,
                                                lookAt.position,
                                                Time.deltaTime * smooth);

            transform.forward = Vector3.Lerp(transform.forward, lookAt.forward, Time.deltaTime * smooth);
        }
	}
}
