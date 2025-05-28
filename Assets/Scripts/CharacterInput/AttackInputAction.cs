using System;
using R3;
using UnityEngine.InputSystem;
using Zenject;
namespace CharacterInput
{
    public sealed class AttackInputAction: IInitializable, IDisposable, IAttackInputHandler
    {
        public Observable<bool> AttackStream => _attackSubject.DistinctUntilChanged();
        
        private readonly Subject<bool> _attackSubject = new();
        
        private readonly InputAction _attack;

        public AttackInputAction()
        {
            _attack = new InputAction("Shoot", InputActionType.Button);
            _attack.AddBinding("<Mouse>/leftButton");
            _attack.AddBinding("<Gamepad>/rightTrigger");

            _attack.performed += _ => _attackSubject.OnNext(true);
            _attack.canceled  += _ => _attackSubject.OnNext(false);
        }

         void IInitializable.Initialize() => _attack.Enable();

         void IDisposable.Dispose()
        {
            _attack.Disable();
            _attack.Dispose();
            _attackSubject.Dispose();
        }
    }
}