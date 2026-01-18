using Character.Interface;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerComponents : MonoBehaviour, ITarget
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform PlayerTransform { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        
        public float TargetDirectionY { get; set; }
        
        public Vector3 GetTarget()
        {
            return transform.position;
        }
    }
}