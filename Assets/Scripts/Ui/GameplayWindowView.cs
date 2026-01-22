using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameplayWindowView : MonoBehaviour
    {
        [field: SerializeField] private Button _openUpgradeWindow;
        [field: SerializeField] private TMP_Text _playerHealth;
    }
}