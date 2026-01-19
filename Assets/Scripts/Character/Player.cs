using System;
using Character.Interface;
using Input.Interface;
using R3;
using StrategyInstaller;
using UnityEngine;
using Weapon.Interface;
using Zenject;

namespace Character
{
    public class Player : IInitializable, IDisposable, ISetWeapon, ITickable, IStrategyClient<IInputSystem>
    {
        private readonly IMovable _movable;
        private readonly IRotate _rotate;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private IWeapon _weapon;
        private CharacterStats.Stats.CharacterStats _characterStats;
        private IInputSystem _input;
        private bool _isFire;

        public Player(IMovable movable, IRotate rotate, CharacterStats.Stats.CharacterStats characterStats)
        {
            _movable = movable;
            _rotate = rotate;
            _characterStats = characterStats;
            
            
        }
        
        public void SetStrategy(IInputSystem strategy)
        {
            _input = strategy;
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
            _isFire = true;
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
            _movable.Move(_input.MoveInput, 10f);
        }
    }
}