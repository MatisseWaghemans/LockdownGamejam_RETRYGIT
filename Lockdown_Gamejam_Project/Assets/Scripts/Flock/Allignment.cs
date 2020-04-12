using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Allignment")]
public class Allignment : Behaviour
{

    public override Vector2 CalculateMove(Agent agent, List<Transform> context, Flock flock, Transform playerTransform)
    {
        Vector2 alignmentMove = Vector2.zero;
        if (agent == null)
        {
            return alignmentMove;
        }
        if (context.Count == 0)
            return agent.transform.forward;
        
        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.forward;
        }

        alignmentMove /= context.Count;

            
        return alignmentMove;
    }

}
