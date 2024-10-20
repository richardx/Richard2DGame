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
        public void Attack(Creature attacker, Creature defender)
        {
            if (attacker.Attacks.Count == 0)
            {
                _logger.LogWarning($"{attacker.Name} har ingen angrebsobjekter og kan ikke angribe.");
                return;
            }

            // Beregn total skade fra angrebsobjekter
            int totalHit = attacker.Attacks.Sum(a => a.Hit);

            _logger.LogInfo($"{attacker.Name} rammer {defender.Name} med {totalHit} hit points.");

            ReceiveHit(defender, totalHit);
        }

        /// <summary>
        /// Håndterer modtagelse af skade for en skabning.
        /// </summary>
        /// <param name="defender">Den skabning der modtager skade.</param>
        /// <param name="hit">Antal hit points der modtages.</param>
        private void ReceiveHit(Creature defender, int hit)
        {
            // Beregn total forsvar fra forsvarsobjekter
            int totalDefense = defender.Defences.Sum(d => d.ReduceHitPoint);

            int actualHit = hit - totalDefense;
            if (actualHit < 0) actualHit = 0;

            defender.HitPoint -= actualHit;

            _logger.LogInfo($"{defender.Name} modtog {actualHit} hit points skade. Remaining HitPoints: {defender.HitPoint}");

            if (defender.HitPoint <= 0)
            {
                _logger.LogInfo($"{defender.Name} er død.");
            }
        }
    }
}
