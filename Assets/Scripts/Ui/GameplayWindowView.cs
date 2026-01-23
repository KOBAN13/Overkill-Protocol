using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameplayWindowView : MonoBehaviour
    {
        [field: Header("Buttons")]
        [field: SerializeField] public Button OpenUpgradeWindow { get; private set; } 
        
        [field: Header("Texts")]
        [field: SerializeField] public TMP_Text PlayerHealth { get; private set; }
        
        [field: Header("LocalizationText")] 
        [field: SerializeField] public TMP_Text HealthTitle { get; private set; }
        [field: SerializeField] public TMP_Text OpenUpgradeWindowText { get; private set; }
        
        [field: Header("UI Components")]
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
        
        public void UpdatePlayerHealth(float health) => PlayerHealth.text = health.ToString("0");
        public void UpdateHealthTitle(string title) => HealthTitle.text = title;
        public void UpdateTextOpenUpgradeWindow(string label) => OpenUpgradeWindowText.text = label;
    }
}
