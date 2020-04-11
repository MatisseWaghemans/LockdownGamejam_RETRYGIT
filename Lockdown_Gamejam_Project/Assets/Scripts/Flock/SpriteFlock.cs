using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlock : MonoBehaviour
{
    public bool isEnabled = false;
    private void Start()
    {

     //   GetComponent<Animator>().SetFloat("Offset", Random.Range(0, 1f));
    }
    void Update()
    {
        if(isEnabled)
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
