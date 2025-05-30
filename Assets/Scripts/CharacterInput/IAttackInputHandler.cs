
namespace CharacterInput
{
    public interface IAttackInputHandler
    {
        bool IsLightAttacking { get; }
        bool IsHeavyAttacking { get; }
    }
}