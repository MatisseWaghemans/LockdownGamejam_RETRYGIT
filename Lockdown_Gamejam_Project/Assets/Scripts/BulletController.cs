using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.WorldToViewportPoint(transform.position).x>1||Camera.main.WorldToViewportPoint(transform.position).x<0)
        {
            Destroy(gameObject);
        }
        if(Camera.main.WorldToViewportPoint(transform.position).y>1||Camera.main.WorldToViewportPoint(transform.position).y<0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag =="Civillian")
        {
            collider.GetComponent<CivillianController>().HitBullet();
            Destroy(gameObject);
        }
        if(collider.tag =="Prop")
        {
            Destroy(gameObject);
        }
        if(collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
