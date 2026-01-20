using CharactersStats.Interface;
using CharacterStats.Interface;
using UnityEngine;

namespace Character.Interface
{
    public interface IMovable
    {
        void Move(Vector2 input, ISpeedStat speedStat);
    }
}