using System;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    public event Action<HitInfo> GotHit;

    public void Hit(HitInfo hitInfo)
    {
        print("Receive Damage Hit");
        if (GotHit != null)
            GotHit.Invoke(hitInfo);
    }
}
