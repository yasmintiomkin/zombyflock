using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    // context=list of other agents or obstables in scene
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);

    /// <summary>
    /// Move flock center to a new location. Use this method in order to implement flock as enemy following the player
    /// </summary>
    /// <param name="newLocation"></param>
    public virtual void MoveFlockCenter(Vector3 newLocation)
    {
        // override is optional
    }
}
