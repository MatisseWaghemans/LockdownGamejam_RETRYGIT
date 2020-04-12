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
    public static int VoteFrequency = 2;

    // Start is called before the first frame update
    public void CheckPeople()
    {
        foreach(Collider2D col in _colliders)
        {
            col.enabled =false;
        }
        _enemies.AddRange(FindObjectsOfType<EnemyController>());
        _passengers.AddRange(FindObjectsOfType<CivillianController>());
        foreach(CivillianController civil in _passengers)
        {
            civil.BeginPos = _generator._roomPositionList[_generator.CurrentRoom];
        }
    }

    private void Start()
    {
        CheckPeople();
    }

    // Update is called once per frame
    void Update()
    {
        if(!NextRoom)
        {
        foreach(EnemyController enemy in _enemies)
        {
            if(!enemy._isHit)
            {
                return;               
            }
        }
        _allEnemiesHit = true;
        if (_allEnemiesHit && _generator.CurrentRoom+1 != _generator._roomList.Count)
        {

            _generator.CurrentRoom++;
                if (_generator.CurrentRoom % VoteFrequency == 0)
                {
                    _generator._roomList[_generator.CurrentRoom].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                GoToNextRoom();
            NextRoom = true;
        }
        }

    }
    void GoToNextRoom()
    {
        CameraScriptWard cam =GetComponentInParent<CameraScriptWard>();
        cam.NextRoom();
        if(cam.Direction.x>0)
        {
            _colliders[1].enabled=true;
        }
        else if(cam.Direction.x<0)
        {
            _colliders[3].enabled=true;
        }
        else if(cam.Direction.y<0)
        {
            _colliders[0].enabled=true;
        }
        else if(cam.Direction.y>0)
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
