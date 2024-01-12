using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static Vector2 RotateTowards(Vector2 current, Vector2 target, float maxRadiansDelta)
    {
        float angle = Vector2.SignedAngle(current, target);

        // If vectors are opposite from each other favor clockwise movement.
        if (angle == 180f)
        {
            angle = -180;
        }

        float rotateAngle = Mathf.Clamp(angle, -maxRadiansDelta * Mathf.Rad2Deg, maxRadiansDelta * Mathf.Rad2Deg);

        Vector2 newDirection = Quaternion.AngleAxis(rotateAngle, Vector3.forward) * current;

        return newDirection.normalized * target.magnitude;
    }
}
