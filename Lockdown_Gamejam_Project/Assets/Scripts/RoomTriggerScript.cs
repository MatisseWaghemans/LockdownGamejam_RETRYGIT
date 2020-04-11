using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerScript : MonoBehaviour
{
    [SerializeField]private List<Collider2D> _colliders = new List<Collider2D>();
    public List<EnemyController> _enemies= new List<EnemyController>();
    public List<CivillianController> _passengers = new List<CivillianController>();
    bool _allEnemiesHit, _allPassengersHit;
    public bool NextRoom;
    [SerializeField] private Generator _generator;

    // Start is called before the first frame update
    public void CheckPeople()
    {
        foreach(Collider2D col in _colliders)
        {
            col.enabled =false;
        }
        _enemies.AddRange(FindObjectsOfType<EnemyController>());
        _passengers.AddRange(FindObjectsOfType<CivillianController>());
    }

    // Update is called once per frame
    void Update()
    {
        if(!NextRoom)
        {
        foreach(EnemyController enemy in _enemies)
        {
            if(enemy ==null)
            _enemies.Remove(enemy);
            if(!enemy._isHit)
            {
                return;               
            }
            else _allEnemiesHit = true;
        }
        if(_allEnemiesHit)
        {
            _generator.CurrentRoom++;
            GoToNextRoom();
            NextRoom = true;
        }
        }

    }
    void GoToNextRoom()
    {

        GetComponentInParent<CameraScriptWard>().NextRoom();
        if(GetComponentInParent<CameraScriptWard>().Direction.x>0)
        {
            _colliders[1].enabled=true;
        }
        if(GetComponentInParent<CameraScriptWard>().Direction.x<0)
        {
            _colliders[3].enabled=true;
        }
        if(GetComponentInParent<CameraScriptWard>().Direction.y<0)
        {
            _colliders[0].enabled=true;
        }
        if(GetComponentInParent<CameraScriptWard>().Direction.y>0)
        {
            _colliders[2].enabled=true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Collider2D col in _colliders)
            {
                col.enabled =false;
            }
            GetComponentInParent<CameraScriptWard>().MoveToNextRoom();
        }
    }
}
