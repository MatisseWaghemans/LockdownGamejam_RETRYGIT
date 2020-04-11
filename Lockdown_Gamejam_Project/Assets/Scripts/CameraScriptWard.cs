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

    private void FixedUpdate()
  {
    if(_move)
      transform.position = Vector3.Lerp(transform.position, position, LerpValue);
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
    
    _move = true;
  }
}
 