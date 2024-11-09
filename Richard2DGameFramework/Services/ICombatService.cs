using Richard2DGameFramework.Model.Creatures;

namespace Richard2DGameFramework.Services
{

    public interface ICombatService
    {
        void Attack(ICreature attacker, ICreature defender);
    }
}
