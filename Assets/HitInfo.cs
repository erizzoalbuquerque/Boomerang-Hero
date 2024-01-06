using UnityEngine;


public class HitInfo
{
    public Vector3 Direction;
    public float Instensity;

    public HitInfo(Vector3 direction, float intensity = 1f)
    {
        this.Direction = direction;
        this.Instensity = intensity;
    }
}
