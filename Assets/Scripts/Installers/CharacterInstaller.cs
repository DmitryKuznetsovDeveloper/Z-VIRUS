using Animancer;
using Animations.Character;
using CharacterInput;
using Core;
using Data;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CharacterInstaller : MonoInstaller
    {
        [Header("Animation Configs")]
        [SerializeField] private MoveAnimationConfig _moveConfig;
        public override void InstallBindings()
        {
            //Inputs
            Container.BindInterfacesAndSelfTo<MoveInputActions>().AsSingle();
            Container.BindInterfacesAndSelfTo<LookInputAction>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackInputAction>().AsSingle();
            
            //Common
            Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
            
            //Animations
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AnimancerComponent>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MoveAnimatorController>().AsSingle().WithArguments(_moveConfig);
            
            //Controllers
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
           // Container.BindInterfacesTo<PlayerLookController>().AsSingle().WithArguments(gameObject.transform);
            
        }
    }
}