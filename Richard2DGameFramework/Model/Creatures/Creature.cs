using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Repræsenterer en skabning i verdenen.
    /// </summary>
    public class Creature
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<IAttack> Attacks { get; set; }
        public List<IDefence> Defences { get; set; }
        public List<MagicItem> MagicItems { get; set; }

        /// <summary>
        /// Konstruktor, der initialiserer listerne.
        /// </summary>
        public Creature()
        {
            Attacks = new List<IAttack>();
            Defences = new List<IDefence>();
            MagicItems = new List<MagicItem>();
        }

        /// <summary>
        /// Tilføjer et angrebsobjekt til skabningens inventar.
        /// </summary>
        public void AddAttack(IAttack attack)
        {
            Attacks.Add(attack);
        }

        /// <summary>
        /// Tilføjer et forsvarsobjekt til skabningens inventar.
        /// </summary>
        public void AddDefence(IDefence defence)
        {
            Defences.Add(defence);
        }

        /// <summary>
        /// Tilføjer et magisk objekt til skabningens inventar.
        /// </summary>
        public void AddMagic(MagicItem magicItem)
        {
            MagicItems.Add(magicItem);
        }

        public override string ToString()
        {
            return $"{Name} (HP: {HitPoint}, Position: ({X}, {Y}))";
        }

        // Template Method til at udføre et angreb
        public void PerformAttack(Creature target, ILogger logger)
        {
            if (CanAttack(target, logger))
            {
                int damage = CalculateDamage(logger);
                target.ReceiveDamage(damage, logger);
                AfterAttack(target, logger);
            }
            else
            {
                logger.LogWarning($"{Name} kan ikke angribe {target.Name}.");
            }
        }

        // Trin der kan overskrives af underklasser
        protected virtual bool CanAttack(Creature target, ILogger logger)
        {
            // Standardimplementering: altid sand
            return true;
        }

        protected virtual int CalculateDamage(ILogger logger)
        {
            // Standardimplementering: brug total skade fra angrebsobjekter
            int totalHit = Attacks.Sum(a => a.Hit);
            if (totalHit > 0)
            {
                logger.LogInfo($"{Name} angriber med samlet skade på {totalHit}.");
            }
            else
            {
                logger.LogWarning($"{Name} har ingen angrebsobjekter.");
            }
            return totalHit;
        }

        protected virtual void AfterAttack(Creature target, ILogger logger)
        {
            // Standardimplementering: gør intet
        }

        // Metode til at modtage skade
        public void ReceiveDamage(int damage, ILogger logger)
        {
            int totalDefense = Defences.Sum(d => d.ReduceHitPoint);
            int actualDamage = damage - totalDefense;
            if (actualDamage < 0) actualDamage = 0;

            HitPoint -= actualDamage;

            logger.LogInfo($"{Name} modtog {actualDamage} skade. Remaining HitPoints: {HitPoint}");

            if (HitPoint <= 0)
            {
                logger.LogInfo($"{Name} er død.");
            }
        }
    }
}
