using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    // associate agent with it's flock
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity) // Vector 2 for 2D version
    {
        // turn the agent to face the destination direction
        //transform.up = velocity; // 2D version
        transform.forward = velocity;

        // move the agent to the destination location
        //transform.position += (Vector3)velocity * Time.deltaTime; // 2D version, adds Z=0
        transform.position += velocity * Time.deltaTime; // insure constant movement
    }
}
