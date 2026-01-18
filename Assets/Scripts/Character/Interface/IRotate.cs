using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Character.Interface
{
    public interface IRotate
    {
        UniTask RotateCharacter(Vector3 mousePosition);
    }
}