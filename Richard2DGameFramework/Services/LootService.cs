// File: LootService.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.Defence;
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
            if (!obj.Lootable)
            {
                _logger.LogWarning($"{obj.Name} kan ikke samles op.");
                return;
            }

            _logger.LogInfo($"{creature.Name} samler {obj.Name} op.");

            // Tilføj objektet til skabningen baseret på typen
            if (obj is AttackItem attackItem)
            {
                if (!creature.Attacks.Any(a => a.Name == attackItem.Name))
                {
                    creature.AddAttack(attackItem);
                    _logger.LogInfo($"{creature.Name} tilføjer angrebsobjektet {attackItem.Name}.");
                }
                else
                {
                    _logger.LogWarning($"{creature.Name} har allerede angrebsobjektet {attackItem.Name}.");
                }
            }
            else if (obj is DefenceItem defenceItem)
            {
                if (!creature.Defences.Any(d => d.Name == defenceItem.Name))
                {
                    creature.AddDefence(defenceItem);
                    _logger.LogInfo($"{creature.Name} tilføjer forsvarsobjektet {defenceItem.Name}.");
                }
                else
                {
                    _logger.LogWarning($"{creature.Name} har allerede forsvarsobjektet {defenceItem.Name}.");
                }
            }

            // Fjern objektet fra verdenen, hvis det er removable
            if (obj.Removable)
            {
                world.RemoveWorldObject(obj);
                _logger.LogInfo($"{obj.Name} fjernet fra verdenen.");
            }
        }
    }
}
