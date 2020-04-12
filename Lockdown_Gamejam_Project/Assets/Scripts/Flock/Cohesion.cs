using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class Cohesion : Behaviour
{
    
    public override Vector2 CalculateMove(Agent agent, List<Transform> context, Flock flock, Transform playerTransform)
    {
        Vector2 cohesionMove = Vector2.zero;
        if (agent == null)
        {
            return cohesionMove;
        }
     //  if (context.Count == 0)
     //      return Vector2.zero;
       
     //  foreach(Transform item in context)
     //  {
     //      cohesionMove += (Vector2) item.position;
     //  }
     //
     //  cohesionMove /= context.Count;

        cohesionMove += (Vector2)playerTransform.position;

     //   cohesionMove /= 2;

        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }

}
