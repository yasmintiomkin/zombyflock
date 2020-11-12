using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250; // number of agents initialized on start
    const float AgentDensity = 0.08f; // density withing flock

    [Range(1f, 100f)]
    public float driveFactor = 10f; // agent speed

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    // Start is called before the first frame update
    void Start()
    {
        //squareMaxSpeed = maxSpeed * maxSpeed;
        //squareNeighborRadius = neighborRadius * neighborRadius;
        //squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        CalcSquares();

        for (int i = 0; i < startingCount; i++)
        {
            //Vector2 insideUnitCircle = Random.insideUnitCircle;
            //Vector3 insideUnitCircleXZ = new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y);
            Vector3 insideUnitCircleXZ = Random.insideUnitSphere;
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                insideUnitCircleXZ * startingCount * AgentDensity, // position
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), // rotation
                transform // parent
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this); // associate agent with it's flock
            agents.Add(newAgent);
        }
    }

    void CalcSquares()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        CalcSquares();  // recalc on update since value might have changed

        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //debugNearbyObjects(agent, context);

            // calculate move based on nearby agents
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                // if we've exceeded max speed, set to max speed
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);

        }
    }

    void debugNearbyObjects(FlockAgent agent, List<Transform> context)
    {
        Renderer r = agent.GetComponentInChildren<Renderer>();
        r.material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
        //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                // if it's not our agent, add it to the list of objects colliding with our agent
                context.Add(c.transform);
            }
        }

        return context;
    }
}
