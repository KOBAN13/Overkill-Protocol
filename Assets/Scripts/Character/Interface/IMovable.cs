using CharactersStats.Interface;
using UnityEngine;

namespace Character.Interface
{
    public interface IMovable
    {
        void Move(Vector2 input, ISpeedStat speedStat);
    }
}