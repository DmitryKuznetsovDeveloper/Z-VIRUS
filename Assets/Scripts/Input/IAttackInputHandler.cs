
namespace Input
{
    public interface IAttackInputHandler
    {
        bool IsLightAttacking { get; }
        bool IsHeavyAttacking { get; }
    }
}