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
    private void FixedUpdate()
  {
    if(_move)
    {
      transform.position = Vector3.Lerp(transform.position, position, LerpValue);
      _timer +=Time.deltaTime;
      if(_timer>3)
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
    position = _generator._roomPositionList[_generator.CurrentRoom];
    Direction = transform.position-position;
    }
  }
  public void MoveToNextRoom()
  {
    GetComponentInChildren<RoomTriggerScript>()._enemies.Clear();
    GetComponentInChildren<RoomTriggerScript>()._passengers.Clear();
    _move = true;
  }
}
 