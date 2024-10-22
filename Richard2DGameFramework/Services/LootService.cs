// File: LootService.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Services
{
    /// <summary>
    /// Håndterer looting af objekter i verdenen.
    /// </summary>
    public class LootService : ILootService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="LootService"/>.
        /// </summary>
        /// <param name="logger">Logger til brug for looting-logik.</param>
        public LootService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Lader en skabning loote et objekt fra verdenen.
        /// </summary>
        /// <param name="creature">Skabningen der looter.</param>
        /// <param name="obj">Objektet der lootes.</param>
        /// <param name="world">Verdenen hvor objektet befinder sig.</param>
        public void Loot(Creature creature, WorldObject obj, World world)
        {
            if (obj is ILootable lootable)
            {
                lootable.Loot(creature, world, _logger);
            }
            else
            {
                _logger.LogWarning($"{obj.Name} kan ikke lootes, da det ikke implementerer ILootable.");
            }
        }
    }
}
