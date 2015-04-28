using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour 
{

    public Transform player;
    public Vector3 spawnpoint;
    GameObject jugador;

	void Start () 
    {
        jugador = Instantiate(player, spawnpoint, player.rotation) as GameObject;
	}

    
}
