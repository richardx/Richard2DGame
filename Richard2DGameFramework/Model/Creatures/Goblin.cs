using Richard2DGameFramework.Logging;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Repræsenterer en Goblin med specifik angrebsadfærd.
    /// </summary>
    public class Goblin : Creature
    {
        protected override bool CanAttack(Creature target, ILogger logger)
        {
            // Goblin kan kun angribe, hvis dens HP er over 0
            bool canAttack = HitPoint > 0;
            if (!canAttack)
            {
                logger.LogWarning($"{Name} kan ikke angribe, da den har {HitPoint} HP.");
            }
            return canAttack;
        }

        protected override int CalculateDamage(ILogger logger)
        {
            var baseDamage = base.CalculateDamage(logger);
            var random = new Random();
            bool criticalHit = random.Next(0, 100) < 20; // 20% chance

            if (criticalHit)
            {
                int criticalDamage = baseDamage * 2;
                logger.LogInfo($"{Name} udfører et kritisk angreb! Skade: {criticalDamage}");
                return criticalDamage;
            }

            return baseDamage;
        }

        protected override void AfterAttack(Creature target, ILogger logger)
        {
            logger.LogInfo($"{Name} griner ondt efter at have angrebet {target.Name}.");
        }

    }
}
