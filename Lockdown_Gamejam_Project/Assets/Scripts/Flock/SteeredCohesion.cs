using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesion : Behaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(Agent agent, List<Transform> context, Flock flock, Transform playerTransform)
    {
       // if (context.Count == 0)
       //     return Vector2.zero;
        Vector2 cohesionMove = Vector2.zero;
     //   foreach (Transform item in context)
     //   {
     //       cohesionMove += (Vector2)item.position;
     //   }
     //
     //   cohesionMove /= context.Count;

        cohesionMove += (Vector2)playerTransform.position;

       // cohesionMove /= 2;

        cohesionMove -= (Vector2)agent.transform.position;

        cohesionMove = Vector2.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime/100f);

        return cohesionMove;
    }

}
