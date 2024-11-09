using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Services
{

    public class LootService : ILootService
    {
        private readonly ILogger _logger;
        public LootService(ILogger logger)
        {
            _logger = logger;
        }

        public void Loot(Creature creature, WorldObject obj, World world)
        {
            if (obj is ILootable lootable)
            {
                lootable.Loot(creature, world, _logger); // kalder Loot-metoden på objektet
            }
            else
            {
                _logger.LogWarning($"{obj.Name} kan ikke lootes, da det ikke implementerer ILootable.");
            }
        }
    }
}
