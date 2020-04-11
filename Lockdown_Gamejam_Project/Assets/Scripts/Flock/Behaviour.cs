using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(Agent agent, List<Transform> context, Flock flock, Transform playerTransform);
}
