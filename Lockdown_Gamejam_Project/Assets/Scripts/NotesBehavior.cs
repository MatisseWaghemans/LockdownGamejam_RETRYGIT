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
        transform.localScale += new Vector3(0,0.02f,0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Hit();
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag =="Civillian")
        {
            other.gameObject.GetComponent<CivillianController>().Hit();
            other.collider.isTrigger = true;
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
