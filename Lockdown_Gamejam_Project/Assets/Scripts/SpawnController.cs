using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _passengerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i<25;i++)
        {
            Vector3 pos = Random.insideUnitCircle*5;
            Instantiate(_passengerPrefab,pos,transform.rotation);
        }
        for(int i =0;i<5;i++)
        {
            Vector3 pos = Random.insideUnitCircle*5;
            Instantiate(_enemyPrefab,pos,transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
