using Animancer;
using Animations.Character;
using CharacterInput;
using Controllers;
using Data;
using Service;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [Header("Animation Configs")]
        [SerializeField] private MoveAnimationLibrary _library;
        [SerializeField] private AttackAnimationConfig _attackConfig;
        public override void InstallBindings()
        {
            //Inputs
            Container.BindInterfacesAndSelfTo<MoveInputActions>().AsSingle();
            Container.BindInterfacesAndSelfTo<LookInputAction>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackInputAction>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSelectorAction>().AsSingle();
            
            //Common
            Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
            
            //Animations
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AnimancerComponent>().FromComponentInHierarchy().AsSingle();
            Container.BindInstance(_library);
            Container.Bind<MoveAnimatorController>().AsSingle().WithArguments(_library.GetByState(MovementState.Walk,WeaponType.None));
            Container.Bind<AttackAnimatorController>().AsSingle().WithArguments(_attackConfig);
            Container.BindInterfacesAndSelfTo<MoveAnimationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackAnimationService>().AsSingle();

            
            //Controllers
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
           // Container.BindInterfacesTo<PlayerLookController>().AsSingle().WithArguments(gameObject.transform);
           
        }
    }
}