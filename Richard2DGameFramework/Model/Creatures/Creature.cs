
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Repræsenterer en skabning i verdenen.
    /// </summary>
    public class Creature : ICreature
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public bool IsAlive { get; set; } = true;
        public int X { get; set; }
        public int Y { get; set; }
        public List<IAttack> Attacks { get; set; } = new List<IAttack>();
        public List<IDefence> Defences { get; set; } = new List<IDefence>();
        public List<MagicItem> MagicItems { get; set; } = new List<MagicItem>();

        // Metoder til håndtering af angreb
        public void AddAttack(IAttack attack) => Attacks.Add(attack);
        public void RemoveAttack(IAttack attack) => Attacks.Remove(attack);
        public IEnumerable<IAttack> GetAttacks() => Attacks;

        // Metoder til håndtering af forsvar
        public void AddDefence(IDefence defence) => Defences.Add(defence);
        public void RemoveDefence(IDefence defence) => Defences.Remove(defence);
        public IEnumerable<IDefence> GetDefences() => Defences;

        // Metoder til håndtering af magi
        public void AddMagic(MagicItem magicItem) => MagicItems.Add(magicItem);
        public void RemoveMagic(MagicItem magicItem) => MagicItems.Remove(magicItem);
        public IEnumerable<MagicItem> GetMagicItems() => MagicItems;

        public void UseMagic(MagicItem magicItem, ICreature target, ILogger logger)
        {
            if (MagicItems.Contains(magicItem))
            {
                // Implementér magi-effekten her
                logger.LogInfo($"{Name} bruger {magicItem.Name} på {target.Name}.");

                // Efter brug kan du fjerne magi-genstanden, hvis det er en engangsgenstand
                // MagicItems.Remove(magicItem);
            }
            else
            {
                logger.LogWarning($"{Name} har ikke {magicItem.Name}.");
            }
        }

        /// <summary>
        /// Metode til at flytte skabningen til en ny position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Metode til at flytte skabningen i forhold til den nuværende position.
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void MoveBy(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        /// <summary>
        /// Metode til at udføre et angreb på en anden skabning.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="logger"></param>
        public void PerformAttack(ICreature target, ILogger logger)
        {
            int damage = Attacks.Sum(a => a.Hit);
            logger.LogInfo($"{Name} angriber {target.Name} og gør {damage} skade!");
            target.ReceiveDamage(damage, logger);
        }

        /// <summary>
        /// Metode til at modtage skade fra et angreb.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="logger"></param>
        public void ReceiveDamage(int damage, ILogger logger)
        {
            int totalDefense = Defences.Sum(d => d.ReduceHitPoint); // LINQ: Sum
            int actualDamage = damage - totalDefense;
            if (actualDamage < 0) actualDamage = 0;

            HitPoint -= actualDamage;

            logger.LogInfo($"{Name} modtog {actualDamage} skade. Remaining HitPoints: {HitPoint}");

            if (HitPoint <= 0 && IsAlive)
            {
                IsAlive = false;
                logger.LogInfo($"{Name} er død.");
                OnDied(); // Udløs Died-event
            }
        }

        /// <summary>
        /// Metode til at helbrede creature.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount)
        {
            HitPoint += amount;
            if (HitPoint > 0 && !IsAlive)
            {
                IsAlive = true;
            }
        }

        /// <summary>
        /// Metode til at dræbe skabningen.
        /// </summary>
        /// <param name="logger"></param>
        public void Die(ILogger logger)
        {
            if (IsAlive)
            {
                HitPoint = 0;
                IsAlive = false;
                logger.LogInfo($"{Name} er død.");
                OnDied(); // Udløs Died-event
            }
        }

        // Event for når skabningen dør
        public event EventHandler<CreatureEventArgs> Died;

        // Beskyttet metode til at udløse Died-eventen
        protected void OnDied()
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
