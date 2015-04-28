using UnityEngine;
using System.Collections;

public class Camera1 : MonoBehaviour 
{

    Transform player;
    public Vector3 offset;
    public float platformheight;
    public float smooth;
    float heightoffset = 2.2f;
    float deepoffset = -2.6f;
    Vector3 rotationoffset = new Vector3(11, 0, 0);

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + player.forward * deepoffset + new Vector3(0, heightoffset, 0);
        transform.rotation = Quaternion.Euler( rotationoffset);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        else
            transform.position = new Vector3(player.position.x + offset.x, Mathf.Lerp(transform.position.y, player.position.y + offset.y, Time.deltaTime * smooth), player.position.z + offset.z);
   
	}
}
