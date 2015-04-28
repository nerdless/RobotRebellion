using UnityEngine;
using System.Collections;

public class GravityChanger : MonoBehaviour 
{
    public float angle;
    public float Gforce;
	
	void Start () 
    {
        Physics.gravity = new Vector3(Gforce * Mathf.Sin(angle * Mathf.PI / 180), -Gforce * Mathf.Cos(angle * Mathf.PI / 180), 0);
	}
}
