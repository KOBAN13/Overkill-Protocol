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
        [field: SerializeField] public Button CloseWindow { get; private set; }
        
        [field: Header("Texts")]
        [field: SerializeField] public TMP_Text CountUpgradeHealth { get; private set; }
        [field: SerializeField] public TMP_Text CountUpgradeSpeed { get; private set; }
        [field: SerializeField] public TMP_Text CountUpgradeDamage { get; private set; }
        [field: SerializeField] public TMP_Text CountUpdatePoints { get; private set; }

        [field: Header("LocalizationText")] 
        [field: SerializeField] private TMP_Text Title;
        [field: SerializeField] private TMP_Text PointsLabel;
        [field: SerializeField] private TMP_Text HealthLabel;
        [field: SerializeField] private TMP_Text SpeedLabel;
        [field: SerializeField] private TMP_Text DamageLabel;
        [field: SerializeField] private TMP_Text ApplyButtonText;
        
        [field: Header("UI Components")]
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
        
        public void UpdateTitleText(string title) => Title.text = title;
        public void UpdatePointsLabel(string label) => PointsLabel.text = label;
        public void UpdateHealthLabel(string label) => HealthLabel.text = label;
        public void UpdateSpeedLabel(string label) => SpeedLabel.text = label;
        public void UpdateDamageLabel(string label) => DamageLabel.text = label;
        public void UpdateApplyButtonText(string text) => ApplyButtonText.text = text;
        
        public void SetCountUpgradePoint(int points) => CountUpdatePoints.text = points.ToString(); 
        
        public void SetHealthUpdatePoint(int point) => CountUpgradeHealth.text = point.ToString();
        public void SetSpeedUpdatePoint(int point) => CountUpgradeSpeed.text = point.ToString();
        public void SetDamageUpdatePoint(int point) => CountUpgradeDamage.text = point.ToString();
    }
}