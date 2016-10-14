using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private enum Selection
    {
        FirstHandle, SecondHandle, ThirdHandle
    }

    // style settings
    private const float handleSize = 0.04f;
    private Color curveColor = Color.green;
    private Color handlePointColor = Color.white;
    private Color handleTangentColor = new Color(0f, 0.2f, 0f);

    // other variables
    private BezierCurve curve;
    private Transform curveTransform;
    private Quaternion curveRotation;
    private const int lineSteps = 20;
    private BezierHandle selectedHandle;
    private Selection selection;

    private Tool lastTool;

    void OnEnable()
    {
        lastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void OnDisable()
    {
        Tools.current = lastTool;
    }

    void OnSceneGUI() 
    {

        // init references
        curve = target as BezierCurve;
        curveTransform = curve.transform;
        curveRotation = Tools.pivotRotation == PivotRotation.Local ?
            curveTransform.rotation : Quaternion.identity;

        // hide the default translate/rotation/scaling tool if set to
        if (curve.hideDefaultHandle)
            Tools.current = Tool.None;
        else if (Tools.current == Tool.None)
            Tools.current = lastTool;

        DrawCurve();

        // draw the handles
        foreach (BezierHandle handle in curve.handles)
        {
            DrawBezierHandle(handle);
        }
    }
    
    private void DrawBezierHandle(BezierHandle handle)
    {
        // get world position of handle
        Vector3 worldPos = curveTransform.TransformPoint(handle.position);

        // if selected draw a dragable handle and register changes
        if (selectedHandle == handle && selection == Selection.SecondHandle)
        {
            DrawDragableHandleGizmo(worldPos, delegate(Vector3 value)
            {
                handle.position = value;
                curve.CalculatePoints();
            });
        }

        DrawTangent(handle);

        // draw a simple handle that can be selected
        DrawSelectableHandleGizmo(worldPos, handlePointColor, delegate()
        {
            selectedHandle = handle;
            selection = Selection.SecondHandle;
        });
    }

    private void DrawTangent(BezierHandle handle)
    {
        // get positions in world space
        Vector3 handlePos = curveTransform.TransformPoint(handle.position);
        
        // draw first part of tangent
        Vector3 endPoint = curveTransform.TransformPoint(handle.position + handle.tangent);
        // if first handle is selected
        if (selectedHandle == handle && selection == Selection.FirstHandle)
        {
            DrawDragableHandleGizmo(endPoint, delegate (Vector3 value)
            {
                handle.tangent = value - handle.position;
                curve.CalculatePoints();
            });
        }
        Handles.color = handleTangentColor;
        Handles.DrawLine(handlePos, endPoint);
        DrawSelectableHandleGizmo(endPoint, handleTangentColor, delegate()
        {
            selectedHandle = handle;
            selection = Selection.FirstHandle;
        });

        // draw second part of tangent (mirrored)
        endPoint = curveTransform.TransformPoint(handle.position - handle.tangent);
        // if second tangent is selected
        if (selectedHandle == handle && selection == Selection.ThirdHandle)
        {
            DrawDragableHandleGizmo(endPoint, delegate (Vector3 value)
            {
                handle.tangent = -(value - handle.position);
                curve.CalculatePoints();
            });
        }
        Handles.DrawLine(handlePos, endPoint);
        DrawSelectableHandleGizmo(endPoint, handleTangentColor, delegate ()
        {
            selectedHandle = handle;
            selection = Selection.ThirdHandle;
        });
    }

    private void DrawSelectableHandleGizmo(Vector3 worldPos, Color color, Action action)
    {
        // calculate size of gizmo
        Handles.color = color;
        float size = HandleUtility.GetHandleSize(worldPos) * handleSize;

        if (Handles.Button(worldPos, curveRotation, size, size * 2, Handles.DotCap))
        {
            action.Invoke();
        }
    }

    private void DrawDragableHandleGizmo(Vector3 worldPos, Action<Vector3> action)
    {
        // register changes in position
        EditorGUI.BeginChangeCheck();
        worldPos = Handles.DoPositionHandle(worldPos, curveRotation);
        // check if a change was made
        if (EditorGUI.EndChangeCheck())
        {
            // make the change undoable
            Undo.RecordObject(curve, "Move handle");
            // tell unity a change was made
            EditorUtility.SetDirty(curve);
            // trigger logic
            action.Invoke(curveTransform.InverseTransformPoint(worldPos));
        }
    }

    // draws the curve
    private void DrawCurve()
    {
        Handles.color = curveColor;
        Vector3[] points = curve.GetPoints();
        Vector3 p0 = curveTransform.TransformPoint(points[0]);
        Vector3 p1;
        for (int i = 1; i < points.Length; i++)
        {
            p1 = curveTransform.TransformPoint(points[i]);
            Handles.DrawLine(p0, p1);
            p0 = p1;
        }
    }
}