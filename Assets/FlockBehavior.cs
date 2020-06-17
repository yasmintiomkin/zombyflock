using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    // context=list of other agents or obstables in scene
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
