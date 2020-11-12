using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius; // t=0 => we're right at the center, t=1 => we're at the radius, t>1 => beyond radius
        
        if (t < 0.9)
        {
            return Vector3.zero; // we're ok, don't change location
        }

        return centerOffset * t * t; // pull back inside
    }
}
