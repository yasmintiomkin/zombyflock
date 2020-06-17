﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>(); // if this is not a FlockAgent this will return null
            if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock) // this is an agent and it's in the same flock
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
