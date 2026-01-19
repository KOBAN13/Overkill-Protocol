using UnityEngine;

namespace CharacterStats.Health.Die
{
    public interface IDie<in T> where T : MonoBehaviour
    {
        void Died(T objectDie);
    }
}
