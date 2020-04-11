using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Generator _generator;
    [SerializeField] private float  LerpValue;


    private void FixedUpdate()
  {
    Vector3 position = _generator._roomPositionList[_generator.CurrentRoom];
      transform.position = Vector3.Lerp(transform.position, position, LerpValue);
  }
}
 