using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotaEnemy : MonoBehaviour 
{

    public Transform cannon1;
    public Transform cannon2;
    public Transform cannon3;
    public Transform cannon4;
    Rigidbody bullet;
    public Rigidbody proyectile;
    bool destroyed;
    public float offset;
    public float bulletforce;
    public float period;


	void Start () 
    {
       // bullets = new List<Rigidbody>();
       // for (int i = 0; i < bulletsAmount; i++)
       // {
        //    Rigidbody obj = Instantiate(proyectile) as Rigidbody;
         //   obj.gameObject.SetActive(false);
         //   bullets.Add(obj);

        //}


        StartCoroutine(Shoot());
	}


    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(period);

            bullet = Instantiate(proyectile, cannon1.position + cannon1.up * offset, Quaternion.identity) as Rigidbody;
            bullet.AddForce(cannon1.up * bulletforce);
            bullet = Instantiate(proyectile, cannon2.position + cannon2.up * offset, Quaternion.identity) as Rigidbody;
            bullet.AddForce(cannon2.up * bulletforce);
            bullet = Instantiate(proyectile, cannon3.position + cannon3.up * offset, Quaternion.identity) as Rigidbody;
            bullet.AddForce(cannon3.up * bulletforce);
            bullet = Instantiate(proyectile, cannon4.position + cannon4.up * offset, Quaternion.identity) as Rigidbody;
            bullet.AddForce(cannon4.up * bulletforce);




            //for (int i = 0; i < bullets.Count; i++)
            //{
             //   if (!bullets[i].gameObject.activeInHierarchy)
              //  {
                //    bullets[i].transform.position = cannon1.position + cannon1.up * offset;
                 //   bullets[i].AddForce(cannon1.up * 600);
                  //  bullets[i].gameObject.SetActive(true);
                   // break;
              //  }
            //}
            //for (int i = 0; i < bullets.Count; i++)
            //{
              //  if (!bullets[i].gameObject.activeInHierarchy)
              //  {
              //      bullets[i].transform.position = cannon2.position + cannon2.up * offset;
              //      bullets[i].AddForce(cannon2.up * 60);
              //      bullets[i].gameObject.SetActive(true);
              //      break;
              //  }
            //}
                //bullet = Instantiate(proyectile, cannon2.position + cannon2.up * offset, Quaternion.identity) as GameObject;
                //bullet.rigidbody.AddForce(cannon2.up * 60);
                //bullet = Instantiate(proyectile, cannon3.position + cannon3.up * offset, Quaternion.identity) as GameObject;
                //bullet.rigidbody.AddForce(cannon3.up * 60);
                //bullet = Instantiate(proyectile, cannon4.position + cannon4.up * offset, Quaternion.identity) as GameObject;
                //bullet.rigidbody.AddForce(cannon4.up * 60);

                
                Debug.Log("disparo");
        }

    }
// todo esto dentro del while
	
}
