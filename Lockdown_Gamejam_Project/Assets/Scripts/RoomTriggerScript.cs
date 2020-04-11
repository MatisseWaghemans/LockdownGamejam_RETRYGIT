using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerScript : MonoBehaviour
{
    Collider2D[] _colliders;
    EnemyController[] _enemies;
    CivillianController[] _passengers;
    public List<EnemyController> _hitEnemies = new List<EnemyController>();
    public List<CivillianController> _hitPassengers = new List<CivillianController>();
    bool _allEnemiesHit, _allPassengersHit;
    public bool NextRoom;

    // Start is called before the first frame update
    void Start()
    {
        _colliders = GetComponents<Collider2D>();
        foreach(Collider2D col in _colliders)
        {
            col.enabled =false;
        }
        _enemies =FindObjectsOfType<EnemyController>();
        _passengers = FindObjectsOfType<CivillianController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(EnemyController enemy in _enemies)
        {
            if(!enemy._isHit)
            {
                return;               
            }
            else _allEnemiesHit = true;
        }

        foreach(CivillianController passenger in _passengers)
        {
            if(!passenger.IsHit)
            {
                return;
            }
            else _allPassengersHit = true;
        }
        if(_allEnemiesHit&&_allPassengersHit)
        {
            GoToNextRoom();
        }
    }
    void GoToNextRoom()
    {
        NextRoom = true;
    }
}
