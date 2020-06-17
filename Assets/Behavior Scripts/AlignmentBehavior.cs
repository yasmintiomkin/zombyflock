using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    // move in same direction as flock (average of neightbors)
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, maintain current alighnment
        if (context.Count == 0)
        {
            return agent.transform.forward; // .up in 2D
        }

        // add all positions together and average
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.forward; // if 2d then (Vector2)item.forward
        }
        alignmentMove /= context.Count; // global position

         return alignmentMove;
    }
}
