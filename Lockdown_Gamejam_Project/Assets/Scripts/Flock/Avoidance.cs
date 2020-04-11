using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class Avoidance : Behaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(Agent agent, List<Transform> context, Flock flock, Transform playerTransform)
    {
        Vector2 avoidanceMove = Vector2.zero;

        int nAvoid = 0;
        float calc = Vector2.SqrMagnitude(playerTransform.position - agent.transform.position);
        if (calc < flock.SquareAvoidanceRadius)
        {
            nAvoid++;
            avoidanceMove += (Vector2)(agent.transform.position - playerTransform.position);
        }


        if (context.Count == 0)
            return avoidanceMove;

        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (nAvoid > 0)
            avoidanceMove /= nAvoid;
        avoidanceMove = Vector2.SmoothDamp(agent.transform.forward, avoidanceMove, ref currentVelocity, agentSmoothTime / 100f);

        return avoidanceMove;
    }

}
