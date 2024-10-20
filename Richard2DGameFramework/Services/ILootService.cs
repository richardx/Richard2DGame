using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Services
{
    /// <summary>
    /// Interface for looting-logik.
    /// </summary>
    public interface ILootService
    {
        void Loot(Creature creature, WorldObject obj, World world);
    }
}
