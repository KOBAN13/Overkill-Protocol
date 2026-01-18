using UnityEngine;

namespace Character.Config
{
    public interface IPlayerParameters
    {
        float MaxDistance { get; }
        float RotationSpeed { get; }
        LayerMask CollisionLayerMask { get; }
    }
}