using CharacterStats.Interface;
using CharacterStats.Stats;
using UnityEngine;

namespace CharacterStats.Impl
{
    public abstract class StatConfig : ScriptableObject, IStatConfig
    {
        public abstract ECharacterStat StatType { get; }
    }
}
