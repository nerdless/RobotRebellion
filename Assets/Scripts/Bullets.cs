using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour 
{
    public float lifetime;

    void OnEnable()
    {
        
        Invoke("Destroy", lifetime); //Deberia ser en oncollision enter.
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter()
    {
        Destroy();
    }
}
