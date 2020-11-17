using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights; // behavior weight
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // handle data mismatch
       if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        // setup move
        Vector3 move = Vector3.zero;

        // iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector3.zero) // if some move is being performed
            {
                // does the move exceed the weight?
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    // limit the move to weight which is max move length
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }

    public override void MoveFlockCenter(Vector3 newLocation)
    {
        for (int i = 0; i < behaviors.Length; i++)
        {
            behaviors[i].MoveFlockCenter(newLocation);
        }
    }
}
