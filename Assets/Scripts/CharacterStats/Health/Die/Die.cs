using UnityEngine;

namespace Enemy.Health.Die
{
    public class Die<T> where T : MonoBehaviour
    {
        public void Died(T objectDie)
        {
            objectDie.gameObject.SetActive(false);
        }
    }
}