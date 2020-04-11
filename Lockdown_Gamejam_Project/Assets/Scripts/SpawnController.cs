﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _passengerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i<15;i++)
        {
            Vector3 pos = transform.parent.position +new Vector3((Random.insideUnitCircle*10).x,(Random.insideUnitCircle*10).y,0);
            Instantiate(_passengerPrefab,pos,transform.rotation);
        }
        for(int i =0;i<2;i++)
        {
            Vector3 pos = transform.parent.position +new Vector3((Random.insideUnitCircle*10).x,(Random.insideUnitCircle*10).y,0);
            Instantiate(_enemyPrefab,pos,transform.rotation);
        }
            FindObjectOfType<RoomTriggerScript>().CheckPeople();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
