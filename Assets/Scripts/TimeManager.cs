using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour 
{

    float tiempo;
    public float timeleft;
    bool dead;
    public float timelimit;
    Animator animator;
    GameObject loader;
    Respawn reload;

    
	// Use this for initialization
	void Start () {
        tiempo = Time.time;
        dead = false;
        animator = GetComponent<Animator>();
        loader = GameObject.FindGameObjectWithTag("loader");
        reload = loader.GetComponent<Respawn>();
	}
	
	// Update is called once per frame
	void Update () {
        timeleft = timelimit - (Time.time - tiempo);
        if (timeleft < 0)
        {
            animator.SetBool("die", true);
            dead = true;
            //reload.Reload();
            //StartCoroutine(Reload());
        }
	}
}
