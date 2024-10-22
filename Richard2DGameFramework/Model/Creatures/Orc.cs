using Richard2DGameFramework.Logging;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Repræsenterer en Orc med forøget skade.
    /// </summary>
    public class Orc : Creature
    {
        protected override int CalculateDamage(ILogger logger)
        {
            var baseDamage = base.CalculateDamage(logger);
            int extraDamage = 5;
            int totalDamage = baseDamage + extraDamage;
            logger.LogInfo($"{Name} bruger råstyrke, tilføjer {extraDamage} ekstra skade. Total skade: {totalDamage}");
            return totalDamage;
        }
    }
}
