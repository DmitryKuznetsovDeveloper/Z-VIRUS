using Animancer;
using CharacterInput;
using FSM;

public class ShootState : IEnterable, IExitable, ITransitionCondition
{
    private readonly AnimancerComponent _animancer;
    private readonly ClipTransition _shoot;
    private readonly IAttackInputHandler _attackInput;
    private readonly int _layer;

    public ShootState(AnimancerComponent animancer, ClipTransition shoot, IAttackInputHandler attackInput, int layer = 1)
    {
        _animancer = animancer;
        _shoot = shoot;
        _attackInput = attackInput;
        _layer = layer;
    }

    public void Enter()
    {
        _shoot.Events.OnEnd = Exit;

        // Принудительно играем на нужном слое
        _animancer.Layers[_layer].Play(_shoot);
        _animancer.Layers[_layer].Weight = 1f;
    }

    public void Exit()
    {
        _animancer.Layers[_layer].StartFade(0f, 0.1f);
    }

    public bool CanEnter()
    {
        return _attackInput.IsLightAttacking || _attackInput.IsHeavyAttacking;
    }
}