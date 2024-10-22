using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.WorldObjects
{
    /// <summary>
    /// Interface for objekter, der kan lootes.
    /// </summary>
    public interface ILootable
    {
        void Loot(Creature creature, World world, ILogger logger);
    }
}
