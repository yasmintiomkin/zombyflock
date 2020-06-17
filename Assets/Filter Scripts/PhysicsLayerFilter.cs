using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            // check if object is in the same layer
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                // our item is on the same layer as our mask is check against
                // the obstacles are in a different layer so they will be filtered out
                filtered.Add(item); 
            }
        }

        return filtered;
    }
}
