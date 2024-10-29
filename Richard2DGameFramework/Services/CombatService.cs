// File: CombatService.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;

namespace Richard2DGameFramework.Services
{
    /// <summary>
    /// Håndterer kamp-logik mellem skabninger.
    /// </summary>
    public class CombatService : ICombatService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="CombatService"/>.
        /// </summary>
        /// <param name="logger">Logger til brug for kamp-logik.</param>
        public CombatService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Udfører et angreb fra én skabning mod en anden.
        /// </summary>
        /// <param name="attacker">Den angribende skabning.</param>
        /// <param name="defender">Den forsvarende skabning.</param>
        public void Attack(Creature attacker, Creature target)
        {
            if (attacker == null || target == null)
            {
                _logger.LogError("Attacker eller target er null.");
                return;
            }

            attacker.PerformAttack(target, _logger);
        }
    }
}
