﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Agent agentPrefab;
    public List<Agent> agents = new List<Agent>();
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

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
        squareMaxSpeed = MaxSpeed * MaxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

     //  for (int i = 0; i < startingCount; i++)
     //  {
     //      Agent newAgent = Instantiate(
     //          agentPrefab,
     //          UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
     //          Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
     //          transform
     //          );
     //      newAgent.name = "Agent " + i;
     //      agents.Add(newAgent);
     //  }


    }

    public void CreateBoy(Vector3 position, Sprite sprite)
    {
        Agent newAgent = Instantiate(
            agentPrefab,
            position,
            Quaternion.Euler(0,0,0),
            transform
            );
        newAgent.name = "Agent " + agents.Count;
        newAgent.transform.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        agents.Add(newAgent);
        CheckAgents();
        foreach(Agent agent in agents)
        {
            if(agent==null)
            {
                agents.Remove(agent);
            }

        }
    }
    public void CheckAgents()
    {
        agents.Clear();
        agents.AddRange(FindObjectsOfType<Agent>());
        agents.TrimExcess();
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<PlayerMovement>()==null)
        {
            return;
        }
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        foreach(Agent agent in agents)
        {
            if (agent == null)
            {
                return;
            }
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behaviour.CalculateMove(agent, context, this, playerTransform);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * MaxSpeed;
            }

            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(Agent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = GetComponentsInChildren<Collider2D>();

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
