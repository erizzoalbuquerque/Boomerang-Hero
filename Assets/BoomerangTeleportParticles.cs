using UnityEngine;

public class BoomerangTeleportParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public void Play(Vector3 particleOriginWorldPosition)
    {
        ParticleSystem.ShapeModule shape = _particleSystem.shape;
        shape.position = this.transform.InverseTransformPoint(particleOriginWorldPosition);

        _particleSystem.Play();
    }
}
