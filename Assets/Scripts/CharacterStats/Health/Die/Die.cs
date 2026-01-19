using UnityEngine;

namespace CharacterStats.Health.Die
{
    public class Die<T> : IDie<T> where T : MonoBehaviour
    {
        public void Died(T objectDie)
        {
            objectDie.gameObject.SetActive(false);
        }
    }
}
