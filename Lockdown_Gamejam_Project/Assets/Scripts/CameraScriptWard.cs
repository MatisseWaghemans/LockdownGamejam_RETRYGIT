using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float  LerpValue;


    private void FixedUpdate()
  {
    Player = FindObjectOfType<PlayerMovement>().transform;
      transform.position = Vector3.Lerp(transform.position,
          new Vector3(Player.position.x, Player.position.y, transform.position.z), LerpValue);
  }
}
