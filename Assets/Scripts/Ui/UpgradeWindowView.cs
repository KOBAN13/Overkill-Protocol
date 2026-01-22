using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class UpgradeWindowView : MonoBehaviour
    {
        [field: Header("Buttons")]
        [field: SerializeField] public Button Apply { get; private set; }
        [field: SerializeField] public Button UpgradeHealth { get; private set; }
        [field: SerializeField] public Button UpgradeSpeed { get; private set; }
        [field: SerializeField] public Button UpgradeDamage { get; private set; }
        [field: SerializeField] public Button CloseWindow{ get; private set; }
        
        [field: Header("Texts")]
        [field: SerializeField] public TMP_Text CountUpgradeHealth { get; private set; }
        [field: SerializeField] public TMP_Text CountUpgradeSpeed { get; private set; }
        [field: SerializeField] public TMP_Text CountUpgradeDamage { get; private set; }
    }
}