using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path 
{
    [SerializeField, HideInInspector]
    List<Vector2> points;
    [SerializeField, HideInInspector]
    bool isClosed;
    [SerializeField, HideInInspector]
    bool autoSetControlPoints;

    public Path(Vector2 centerPos) 
    {
        points = new List<Vector2> 
        {
            centerPos + Vector2.left,
            centerPos + (Vector2.left + Vector2.up)* 0.5f,
            centerPos + (Vector2.right + Vector2.down) * 0.5f,
            centerPos + Vector2.right
        };
    }

    public Vector2 this[int index] { get {return points[index]; } }

    public bool AutoSetControlPoints 
    { 
        get 
        {
            return autoSetControlPoints;
        } 
        set 
        { 
            if (autoSetControlPoints != value) 
            {
                autoSetControlPoints = value;
                if (autoSetControlPoints)
                {
                    AutoSetAllControlPoints();
                }
            } 
        } 
    }

    public int NumPoints { get {return points.Count; } }

    public int NumSegments { get { return points.Count / 3; } }

    public void AddSegment(Vector2 anchorPos) 
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);

        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public Vector2[] GetPointsInSegment(int index)
    {
        return new Vector2[]
        {
            points[index * 3],
            points[index * 3 + 1],
            points[index * 3 + 2],
            points[LoopIndex(index * 3 + 3)]
        };
    }

    public void MovePoint(int index, Vector2 newPos)
    {
        Vector2 deltaMove = newPos - points[index];
        if (index % 3 == 0 || !autoSetControlPoints)
        {
            points[index] = newPos;

            if (autoSetControlPoints)
            {
                AutoSetAllAffectedControlPoints(index);
            }
            else
            {
                if (index % 3 == 0)
                {
                    if (index + 1 < points.Count || isClosed)
                    {
                        points[LoopIndex(index + 1)] += deltaMove;
                    }
                    if (index - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(index - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (index + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? index + 2 : index - 2;
                    int anchorIndex = (nextPointIsAnchor) ? index + 1 : index - 1;

                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float dist = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 dir = (points[LoopIndex(anchorIndex)] - newPos).normalized;
                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + dir * dist;
                    }
                }
            }
        }
    }

    public void ToggleClosed()
    {
        isClosed = !isClosed;

        if (isClosed)
        {
            points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
            points.Add(points[0] * 2 - points[1]);

            if (autoSetControlPoints)
            {
                AutoSetAnchorControlPoints(0);
                AutoSetAnchorControlPoints(points.Count - 3);
            }
        }
        else
        {
            points.RemoveRange(points.Count - 2, 2);

            if (autoSetControlPoints)
            {
                AutoSetStartAndEndControls();
            }
        }
    }

    void AutoSetAllAffectedControlPoints(int updatedAnchorIndex)
    {
        for (int i = updatedAnchorIndex - 3; i <= updatedAnchorIndex + 3; i += 3)
        {
            if (i >= 0 && i < points.Count || isClosed)
            {
                AutoSetAnchorControlPoints(LoopIndex(i));
            }
        }

        AutoSetStartAndEndControls();
    }

    void AutoSetAllControlPoints()
    {
        for (int i = 0; i < points.Count; i += 3)
        {
            AutoSetAnchorControlPoints(i);
        }
        AutoSetStartAndEndControls();
    }

    void AutoSetAnchorControlPoints(int anchorIndex)
    {
        Vector2 anchorPos = points[anchorIndex];
        Vector2 dir = Vector2.zero;
        float[] neighborDistances = new float[2];

        if (anchorIndex - 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighborDistances[0] = offset.magnitude;
        }

        if (anchorIndex + 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighborDistances[1] = -offset.magnitude;
        }

        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;
            if (controlIndex >= 0 && controlIndex < points.Count || isClosed)
            {
                points[LoopIndex(controlIndex)] = anchorPos + dir * neighborDistances[i] * .5f;
            }
        }
    }

    void AutoSetStartAndEndControls()
    {
        if(!isClosed)
        {
            points[1] = (points[0] + points[2] * 0.5f);
            points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;
        }
    }

    int LoopIndex(int index)
    {
        return (index + points.Count) % points.Count;
    }
}
