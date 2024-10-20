using Richard2DGameFramework.Model.Creatures;

namespace Richard2DGameFramework.Services
{
    /// <summary>
    /// Interface for kamp-logik.
    /// </summary>
    public interface ICombatService
    {
        void Attack(Creature attacker, Creature defender);
    }
}
