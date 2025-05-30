using Animancer;
using CharacterInput;
using Controllers;
using Data;
using FSM;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [Header("Animation Configs")]
        [SerializeField] private CharacterAnimationConfigProvider _animationConfig;

        [Header("Weapon Animation Collection")]
        [SerializeField] private WeaponAnimationCollection _weaponAnimations;

        [Header("Melee Combo Config")]
        [SerializeField] private MeleeComboAnimationSet _meleeCombo;

        public override void InstallBindings()
        {
            // Inputs
            Container.BindInterfacesAndSelfTo<MoveInputActions>().AsSingle();
            Container.BindInterfacesAndSelfTo<LookInputAction>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackInputAction>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSelectorAction>().AsSingle();

            // Common
            Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AnimancerComponent>().FromComponentInHierarchy().AsSingle();

            // Animation Configs
            Container.Bind<WeaponAnimationCollection>().FromInstance(_weaponAnimations).AsSingle();
            Container.Bind<MeleeComboAnimationSet>().FromInstance(_meleeCombo).AsSingle();

            // State Machine
            Container.BindInterfacesTo<CharacterAnimationStateMachine>().AsSingle();

            // Movement Controller
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
        }
    }
}