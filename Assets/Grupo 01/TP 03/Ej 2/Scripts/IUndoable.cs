using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUndoable
{
    void Undo();
    string Description { get; }
}

public struct TransformSnapshot
{
    public Transform Target;
    public Vector3 LocalPosition;
    public Quaternion LocalRotation;
    public Vector3 LocalScale;

    public static TransformSnapshot Capture(Transform t)
    {
        return new TransformSnapshot
        {
            Target = t,
            LocalPosition = t.localPosition,
            LocalRotation = t.localRotation,
            LocalScale = t.localScale
        };
    }

    public void Apply()
    {
        if (Target == null)
        {
            return;
        }

        Target.localPosition = LocalPosition;
        Target.localRotation = LocalRotation;
        Target.localScale = LocalScale;
    }
}

public class TransformUndoAction : IUndoable
{
    private readonly TransformSnapshot _snapshot;
    public string Description { get; }

    public TransformUndoAction(TransformSnapshot snapshot, string description = "Transform change")
    {
        _snapshot = snapshot;
        Description = description;
    }

    public void Undo()
    {
        _snapshot.Apply();
    }
}