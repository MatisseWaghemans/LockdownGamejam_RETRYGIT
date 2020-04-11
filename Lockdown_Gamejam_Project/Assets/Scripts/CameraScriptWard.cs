using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Generator _generator;
    [SerializeField] private float  LerpValue;
    Vector3 position;

    private void FixedUpdate()
  {
      transform.position = Vector3.Lerp(transform.position, position, LerpValue);
  }
  public void NextRoom()
  {
    position = _generator._roomPositionList[_generator.CurrentRoom];
  }
}
 