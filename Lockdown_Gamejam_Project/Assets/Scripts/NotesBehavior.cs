using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesBehavior : MonoBehaviour
{
    [SerializeField] private GameObject[] _notes = new GameObject[3];
    [SerializeField] private Sprite[] _sprites = new Sprite[4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<3;i++)
        {
            _notes[i].transform.localPosition = Random.insideUnitCircle*0.5f;
            _notes[i].GetComponent<SpriteRenderer>().sprite =_sprites[Random.Range(0,4)];
        }
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
        transform.localScale += new Vector3(0,0.02f,0);
    }
        void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Hit();
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag =="Civillian")
        {
            // if(other.transform.parent.CompareTag("Player"))
            if(other.transform.parent!=null)
            {
                return;
            }
            else{    
            other.gameObject.GetComponent<CivillianController>().Hit();
            other.isTrigger = true;
            Destroy(this.gameObject);
            }
        }
        if(other.gameObject.tag =="Prop")
        {
            Destroy(this.gameObject);
        }
    }

}
