using CharactersStats.Stats;

namespace Ui.Data
{
    public readonly struct UpgradePointData
    {
        public readonly int UpgradePoints;
        public readonly ECharacterStat Type;

        public UpgradePointData(int upgradePoints, ECharacterStat type)
        {
            UpgradePoints = upgradePoints;
            Type = type;
        }
    }
}