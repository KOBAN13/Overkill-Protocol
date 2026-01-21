using System;
using Character.Interface;
using CharactersStats.Interface;
using CharactersStats.Stats;
using CharacterStats.Interface;
using CharacterStats.Stats;
using Input.Interface;
using R3;
using StrategyInstaller;
using Weapon.Interface;
using Zenject;

namespace Character
{
    public class Player : IInitializable, IDisposable, ISetWeapon, ITickable, IStrategyClient<IInputSystem>
    {
        private readonly IMovable _movable;
        private readonly IRotate _rotate;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public readonly IUpgradeStats _upgradeStats;
        private readonly StatsCollection _characterStats;
        
        private IWeapon _weapon;
        private IInputSystem _input;
        private bool _isFire;

        public Player(IMovable movable, IRotate rotate, StatsCollection characterStats, IUpgradeStats upgradeStats, IWeapon weapon)
        {
            _movable = movable;
            _rotate = rotate;
            _characterStats = characterStats;
            _upgradeStats = upgradeStats;
            
            SetWeapon(weapon);
        }
        
        public void SetStrategy(IInputSystem strategy)
        {
            _input = strategy;
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
            _isFire = true;
            _weapon.SetDamageStat(_characterStats.GetStat<IDamageStat>(ECharacterStat.Damage));
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
            _isFire = false;
        }

        public void Initialize()
        {
            _input.OnClick
                .Subscribe(async _ =>
                {
                    var vector = _input.PositionInMouseClick;
                    
                    await _rotate.RotateCharacter(vector);
                    
                    if(!_isFire) 
                        return;
                    
                    _weapon.Fire();
                })
                .AddTo(_compositeDisposable);
            
        }

        public void Tick()
        {
            _movable.Move(_input.MoveInput, _characterStats.GetStat<ISpeedStat>(ECharacterStat.Speed));
        }
    }
}
