// Fil: Creature.cs
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
        public List<IAttack> Attacks { get; set; } = new List<IAttack>();
        public List<IDefence> Defences { get; set; } = new List<IDefence>();
        public List<MagicItem> MagicItems { get; set; } = new List<MagicItem>();

        public void AddAttack(IAttack attack) => Attacks.Add(attack);
        public void AddDefence(IDefence defence) => Defences.Add(defence);
        public void AddMagic(MagicItem magicItem) => MagicItems.Add(magicItem);

        public event EventHandler<CreatureEventArgs> Died;

        public void PerformAttack(Creature target, ILogger logger)
        {
            int damage = Attacks.Sum(a => a.Hit);
            logger.LogInfo($"{Name} angriber {target.Name} og gør {damage} skade!");
            target.ReceiveDamage(damage, logger);
        }

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
                OnDied(); // Udløs Died-event
            }
        }

        // Beskyttet metode til at udløse Died-eventen
        protected virtual void OnDied()
        {
            Died?.Invoke(this, new CreatureEventArgs { Creature = this });
        }

        public override string ToString()
        {
            return $"{Name} (HP: {HitPoint}, Position: ({X}, {Y}))";
        }
    }

    // EventArgs klasse til at sende ekstra data med eventen
    public class CreatureEventArgs : EventArgs
    {
        public Creature Creature { get; set; }
    }
}
