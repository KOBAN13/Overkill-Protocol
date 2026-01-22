using UnityEngine;

namespace Localization.Configs
{
    [CreateAssetMenu(fileName = nameof(UpgradeWindowViewTexts), menuName = nameof(UpgradeWindowViewTexts))]
    public class UpgradeWindowViewTexts : ScriptableObject, IUpgradeWindowViewTexts
    {
        [field: Header("Header")]
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string PointsLabel { get; private set; }

        [field: Header("Stats Labels")]
        [field: SerializeField] public string HealthLabel { get; private set; }
        [field: SerializeField] public string SpeedLabel { get; private set; }
        [field: SerializeField] public string DamageLabel { get; private set; }

        [field: Header("Buttons")]
        [field: SerializeField] public string ApplyButton { get; private set; }
        [field: SerializeField] public string OpenUpdateWindowButton { get; private set; }
    }
}
