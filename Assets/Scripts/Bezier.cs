using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
       Vector3 point01 = Vector3.Lerp(p0, p1, t);
       Vector3 point12 = Vector3.Lerp(p1, p2, t);
       Vector3 point23 = Vector3.Lerp(p2, p3, t);
       
       Vector3 point012 = Vector3.Lerp(point01, point12, t);
       Vector3 point123 = Vector3.Lerp(point12, point23, t);
       
       return Vector3.Lerp(point012, point123, t);
    }
}
