using UnityEditor;
using UnityEngine;

public static class PlainMath
{
    const float size = 0.25f;

    public static void NextGizmosColor()
    {
        Gizmos.color = Color.HSVToRGB(Random.value, 1, 1);
    }

    public static float AngleFromDirection(Vector2 dir)
    {
        //Get the angle by using some Acos on x, flipping the rotation when y is lower then 0.
        dir = dir.normalized;
        var angle = Mathf.Acos(dir.x) * Mathf.Rad2Deg;
        return dir.y > 0 ? angle : 360 - angle;
    }

    public static float AngleBetween(Vector2 A, Vector2 B)
    {
        A -= B;
        float angle = Mathf.Atan2(A.y, A.x);
        return angle * Mathf.Rad2Deg;
    }

    public static Vector2 ClosestOnLine(Vector2 origin, Vector2 direction, Vector2 point)
    {
        direction.Normalize();
        Vector2 lhs = point - origin;

        Gizmos.color = Color.black;

        float dotP = Vector2.Dot(lhs, direction);
        return origin + direction * dotP;
    }
    public static Vector3 CalculateClosestToRinPlaneABwD(Vector3 A, Vector3 B, Vector3 respectPoint, Vector3 targetPoint)
    {
        // this method, will return the closest point on the plane, to the respect point
        // while it will give as a point, that has the same langth, as respectPoint, from the center of the plain

        // create a plain, while the Z axis is the  B-A vector,
        // move the plain so respectPoint, is a point on the plane.
        Plane plane = new Plane(B - A, respectPoint);
        // get the center of the plane, A.k.a, the point at witch respectPoint is closest to the RespectPoint (in world space).
        // a.k.a 90 degree angle between respect point and the Line AB (infinite line)
        Vector3 center = plane.ClosestPointOnPlane(A);
        // get the closest point on plane to target
        Vector3 closest = plane.ClosestPointOnPlane(targetPoint);
        // find the closest point, to target.
        // this point has the same distance to A, and to B from respectPoint, and yet the closest one to targetPoint
        Vector3 dir = (closest - center).normalized;
        dir *= (respectPoint - center).magnitude;

        return dir + center;
    }
    public static Vector3 GetClosestWithRespectVisualized(Vector3 A, Vector3 B, Vector3 R, Vector3 Target)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(A, size);
        Gizmos.DrawSphere(B, size);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(R, size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Target, size);

        Plane plane = new Plane(A - B, R);
        Vector3 center = plane.ClosestPointOnPlane(A);

        Handles.DrawLine(B, A);

        Random.InitState(10);
        NextGizmosColor();
        Gizmos.DrawSphere(center, size);

        return Vector3.zero;
    }
    public static Vector3 GetClosestWithRespectVisualized(Vector3 A, Vector3 B, Vector3 respectPoint, Vector3 targetPoint, Vector3 Zero)
    {
        // this method, will return the closest point on the plane, to the respect point
        // while it will give as a point, that has the same langth, as respectPoint, from the center of the plain

        // create a plain, while the Z axis is the  B-A vector,
        // move the plain so respectPoint, is a point on the plane.
        Plane plane = new Plane(B - A, respectPoint);
        // get the center of the plane, A.k.a, the point at witch respectPoint is closest to the RespectPoint (in world space).
        // a.k.a 90 degree angle between respect point and the Line AB (infinite line)
        Vector3 center = plane.ClosestPointOnPlane(A);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(Zero + center, 0.3f);
        Handles.DrawLine(B + Zero, A + Zero);
        Handles.DrawLine(center + Zero, respectPoint + Zero);
        // get the closest point on plane to target
        Vector3 closest = plane.ClosestPointOnPlane(targetPoint);

        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(closest + Zero, 0.1f);
        Handles.DrawLine(closest + Zero, targetPoint + Zero);
        Handles.DrawLine(closest + Zero, center + Zero);

        // find the closest point, to target.
        // this point has the same distance to A, and to B from respectPoint, and yet the closest one to targetPoint
        Vector3 dir = (closest - center).normalized;
        dir *= (respectPoint - center).magnitude;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(dir + Zero + center, 0.1f);
        Handles.DrawLine(dir + Zero + center, center + Zero);
        Handles.DrawLine(dir + Zero + center, respectPoint + Zero);

        return dir + center;
    }
}