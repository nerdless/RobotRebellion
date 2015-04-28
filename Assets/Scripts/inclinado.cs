using UnityEngine;
using System.Collections;

public class inclinado : MonoBehaviour {

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.name == "mountain")
        {
            Debug.Log("estasChocando"+ collisionInfo.contacts[0].normal);
            transform.up = collisionInfo.contacts[0].normal;

        }
    }
}

