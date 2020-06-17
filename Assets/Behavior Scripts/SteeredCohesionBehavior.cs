using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    // similar to CohesionBehavior. Smooth jitter when upon direction change

    Vector3 currentVelocity;
    // how long does it take the agent to get from it's current state to it's calculated state
    public float agentSmoothTime = 0.5f;

    // find middle point between our neighbors and go there
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        // add all positions together and average
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position; // if 2d then (Vector2)item.position
        }
        cohesionMove /= context.Count; // global position

        // create offset from agent position
        cohesionMove -= agent.transform.position; // cast to Vector2 if 2d

        // SmoothDamp - Gradually changes a vector towards a desired goal over time.
        // The vector is smoothed by some spring-damper like function, which will never overshoot.The most common use is for smoothing a follow camera.
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);


        return cohesionMove;
    }
}