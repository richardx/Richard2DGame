// File: Creature.cs
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
        /// <param name="attack">Angrebsobjektet, der skal tilføjes.</param>
        public void AddAttack(IAttack attack)
        {
            Attacks.Add(attack);
        }

        /// <summary>
        /// Tilføjer et forsvarsobjekt til skabningens inventar.
        /// </summary>
        /// <param name="defence">Forsvarsobjektet, der skal tilføjes.</param>
        public void AddDefence(IDefence defence)
        {
            Defences.Add(defence);
        }

        /// <summary>
        /// Tilføjer et magisk objekt til skabningens inventar.
        /// </summary>
        /// <param name="magicItem">Det magiske objekt, der skal tilføjes.</param>
        public void AddMagic(MagicItem magicItem)
        {
            MagicItems.Add(magicItem);
        }

        public override string ToString()
        {
            return $"{Name} (HP: {HitPoint}, Position: ({X}, {Y}))";
        }
    }
}
