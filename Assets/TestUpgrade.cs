using Character;
using CharactersStats.Interface;
using CharacterStats.Interface;
using CharacterStats.Stats;
using UnityEngine;
using Zenject;

public class TestUpgrade : MonoBehaviour
{
    [Inject] private Player _player;
    
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Апдейт здоровья");
            _player._upgradeStats.AddUpgradePoints();
            var upgradeTrue = _player._upgradeStats.UpgradeStat<IHealthStat>(ECharacterStat.Health, 3);
            
            Debug.Log("Upgrade is norm: " + upgradeTrue);
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Апдейт дамага");
            _player._upgradeStats.AddUpgradePoints();
            var upgradeTrue = _player._upgradeStats.UpgradeStat<IDamageStat>(ECharacterStat.Damage, 3);
            
            Debug.Log("Upgrade is norm: " + upgradeTrue);
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Апдейт скорости");
            _player._upgradeStats.AddUpgradePoints();
            var upgradeTrue = _player._upgradeStats.UpgradeStat<ISpeedStat>(ECharacterStat.Speed, 3);
            
            Debug.Log("Upgrade is norm: " + upgradeTrue);
        }
    }
}
