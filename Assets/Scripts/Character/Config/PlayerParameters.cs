using UnityEngine;

namespace Character.Config
{
    [CreateAssetMenu(fileName = nameof(PlayerParameters), menuName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [field: SerializeField] public  float MaxDistance { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public LayerMask CollisionLayerMask { get; private set; }
    }
}