using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameplayWindowView : MonoBehaviour
    {
        [field: SerializeField] public Button OpenUpgradeWindow { get; private set; } 
        [field: SerializeField] public TMP_Text PlayerHealth { get; private set; }

        public void UpdatePlayerHealth(float health) => PlayerHealth.text = health.ToString("0");
    }
}
