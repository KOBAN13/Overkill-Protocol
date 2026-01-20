using Helper;
using UnityEngine;

namespace CharacterStats.Die
{
    public class Die : IDie
    {
        private readonly MonoBehaviour _owner;

        public Die(MonoBehaviour owner)
        {
            _owner = Preconditions.CheckNotNull(owner);
        }

        public void Died()
        {
            _owner.gameObject.SetActive(false);
        }
    }
}
