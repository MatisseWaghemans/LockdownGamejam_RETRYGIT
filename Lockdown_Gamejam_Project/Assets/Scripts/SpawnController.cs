using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private GameObject[] _passengerPrefab;
    private Generator _generator;
    // Start is called before the first frame update
    public void CallPeople()
    {
        _generator =FindObjectOfType<Generator>();
        for(int i =0;i<_generator.CurrentRoom+1;i++)
        {
            Vector3 pos = transform.parent.position +new Vector3((Random.insideUnitCircle*7).x,(Random.insideUnitCircle*7).y,0);
            Instantiate(_passengerPrefab[Random.Range(0,2)],pos,transform.rotation);
        }
        for(int i =0;i<_generator.CurrentRoom+1;i++)
        {
            Vector3 pos = transform.parent.position +new Vector3((Random.insideUnitCircle*7).x,(Random.insideUnitCircle*7).y,0);
            Instantiate(_enemyPrefab[Random.Range(0,2)],pos,transform.rotation);
        }
            FindObjectOfType<RoomTriggerScript>().CheckPeople();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
