using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Generator _generator;
    [SerializeField] private float  LerpValue;
    Vector3 position;
    public Vector3 Direction;
    bool _move;
    int _previousRoom;
    float _timer;
    void Start()
    {
      _generator = FindObjectOfType<Generator>();
      _generator._roomList[_generator.CurrentRoom].GetComponentInChildren<SpawnController>().CallPeople();
    }
    private void FixedUpdate()
  {

    if(_move)
    {
      transform.position = Vector3.Lerp(transform.position, position, LerpValue);
      _timer +=Time.deltaTime;
      if(_timer>1)
      {
        _move = false;
        _timer =0;
        GetComponentInChildren<RoomTriggerScript>().NextRoom = false;
      }
    }
  }
  public void NextRoom()
  {
    if(!_move)
    {
      position = new Vector3(_generator._roomPositionList[_generator.CurrentRoom].x,_generator._roomPositionList[_generator.CurrentRoom].y,-2);
      Direction = transform.position-position;
    }
  }
  public void MoveToNextRoom()
  {
    foreach(EnemyController enemy in GetComponentInChildren<RoomTriggerScript>()._enemies)
    {
      Destroy(enemy);
    }
    GetComponentInChildren<RoomTriggerScript>()._enemies.Clear();
    GetComponentInChildren<RoomTriggerScript>()._passengers.Clear();
    _generator._roomList[_generator.CurrentRoom].GetComponentInChildren<SpawnController>().CallPeople();
    _move = true;
  }
}
 