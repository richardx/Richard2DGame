// File: CombatService.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;

namespace Richard2DGameFramework.Services
{
    public class CombatService : ICombatService
    {
        private readonly ILogger _logger;

        public CombatService(ILogger logger)
        {
            _logger = logger;
        }

        public void Attack(ICreature attacker, ICreature target)
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
