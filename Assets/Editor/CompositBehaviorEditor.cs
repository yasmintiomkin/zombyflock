using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//[CustomEditor(typeof(CompositeBehavior))]
public class CompositBehaviorEditor : Editor
{  
    public override void OnInspectorGUI()
    {
        // inspector initial setup
        CompositeBehavior cb = (CompositeBehavior)target;

        Rect r = EditorGUILayout.BeginHorizontal(); // create a rect wherever the 'cursor' of the Editor is
        r.height = EditorGUIUtility.singleLineHeight; // baseline 'cursor'
        r.y = -30f; 

        // check for behaviors
        if (cb.behaviors == null || cb.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("no behaviors in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            r = EditorGUILayout.BeginHorizontal(); // move 'cursor' below the warning
            r.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {
            r.x = 30f; // margin on left to field name
            r.width = EditorGUIUtility.currentViewWidth - 95f; // currenviewwidth is the current editor width
            EditorGUI.LabelField(r, "Behaviors");
            r.x = EditorGUIUtility.currentViewWidth - 65f; // up to margin from right
            r.width = 60f; // 5 pixels padding from right
            EditorGUI.LabelField(r, "Weights");
            r.y += EditorGUIUtility.singleLineHeight * 1.2f;

            EditorGUI.BeginChangeCheck(); // if the value of a behavior or weight is changed during the loop, refresh it (after the loop ends)
            for (int i = 0; i < cb.behaviors.Length; i++)
            {
                r.x = 5f;
                r.width = 20f;
                EditorGUI.LabelField(r, i.ToString());
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                cb.behaviors[i] = (FlockBehavior)EditorGUI.ObjectField(r, cb.behaviors[i], typeof(FlockBehavior), false); // falst=scriptable objects only, not objects from the scene
                r.x = EditorGUIUtility.currentViewWidth - 65f;
                cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
                r.y += EditorGUIUtility.singleLineHeight * 1.1f;
            }
            if (EditorGUI.EndChangeCheck())
            {
                // something has changed so make the UI redraw
                EditorUtility.SetDirty(cb);
            }
        }

        EditorGUILayout.EndHorizontal();
       
        r.x = 5f;
        r.width = EditorGUIUtility.currentViewWidth - 10f;
        r.y += EditorGUIUtility.singleLineHeight * 0.5f;
        if (GUI.Button(r, "Add Behavior"))
        {
            AddBehavior(cb);
            // let unity know a change has happened
            EditorUtility.SetDirty(cb);
        }

        r.y += EditorGUIUtility.singleLineHeight * 1.5f;
        if (cb.behaviors != null && cb.behaviors.Length > 0)
        {
            if (GUI.Button(r, "Remove Behavior"))
            {
                RemoveBehavior(cb);
                // let unity know a change has happened
                EditorUtility.SetDirty(cb);
            }
        }
    }

    void AddBehavior(CompositeBehavior cb)
    {
        int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;
        FlockBehavior[] newBehaviros = new FlockBehavior[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviros[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviros;
        cb.weights = newWeights;
    }

    void RemoveBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors.Length;

        if (oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }

        FlockBehavior[] newBehaviros = new FlockBehavior[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviros[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
       
        cb.behaviors = newBehaviros;
        cb.weights = newWeights;
    }
}
