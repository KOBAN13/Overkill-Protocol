using CharactersStats.Interface;
using CharactersStats.Stats;
using UnityEngine;

namespace CharactersStats.Impl
{
    public abstract class StatConfig : ScriptableObject, IStatConfig
    {
        public abstract ECharacterStat StatType { get; }
    }
}
