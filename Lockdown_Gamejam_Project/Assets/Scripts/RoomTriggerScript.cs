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
    // Start is called before the first frame update
    void Start()
    {
        _colliders = GetComponents<Collider2D>();
        foreach(Collider2D col in _colliders)
        {
            col.enabled =false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(EnemyController enemy in _enemies)
        {
            if(enemy._isHit)
            {
                _hitEnemies.Add(enemy);
            }
        }
        // foreach(CivillianController passenger in _passengers)
        // {
        //     if(passenger._isHit)
        //     {
        //         _hitPassengers.Add(passenger);
        //     }
        // }
        if(_enemies.Length==_hitEnemies.Count && _passengers.Length == _hitPassengers.Count)
        {
        foreach(Collider2D col in _colliders)
        {
            col.enabled =true;
        }
        }
    }
}
