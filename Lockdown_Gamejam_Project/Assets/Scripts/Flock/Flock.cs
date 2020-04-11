using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Agent agentPrefab;
    List<Agent> agents = new List<Agent>();
    public Behaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 2f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float MaxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 20f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    [SerializeField] Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        avoidanceRadiusMultiplier = avoidanceRadiusMultiplier / 10f;
        squareMaxSpeed = MaxSpeed * MaxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Agent newAgent = Instantiate(
                agentPrefab,
                UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Agent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behaviour.CalculateMove(agent, context, this, playerTransform);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * MaxSpeed;
            }

            agent.Move(move);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private List<Transform> GetNearbyObjects(Agent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        foreach (Collider2D c in contextColliders)
        {

            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
