using UnityEngine;

namespace Character.Interface
{
    public interface IMovable
    {
        void Move(Vector2 input, float speed);
    }
}