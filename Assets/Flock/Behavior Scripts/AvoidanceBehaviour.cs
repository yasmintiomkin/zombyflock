using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehavior
{
    // avoid colliding with neightbors
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        // add all positions together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0; // number of neighbors to avoid
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius) 
            {
                nAvoid++;
                // move away from neighbor in order to avoid collision
                avoidanceMove += agent.transform.position - item.position; // if 2d then (Vector2)item.position
            }
        }
        
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
