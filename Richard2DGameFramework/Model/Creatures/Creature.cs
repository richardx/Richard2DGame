// File: Creature.cs
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Defence;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Repræsenterer en skabning i verdenen.
    /// </summary>
    public class Creature
    {
        /// <summary>
        /// Navnet på skabningen.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Antal hit points skabningen har.
        /// </summary>
        public int HitPoint { get; set; }

        /// <summary>
        /// X-koordinaten for skabningens position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-koordinaten for skabningens position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Liste over angrebsobjekter skabningen har.
        /// </summary>
        public List<AttackItem> Attacks { get; set; }

        /// <summary>
        /// Liste over forsvarsobjekter skabningen har.
        /// </summary>
        public List<DefenceItem> Defences { get; set; }

        /// <summary>
        /// Tilføjer et angrebsobjekt til skabningen.
        /// </summary>
        /// <param name="attack">Angrebsobjektet der skal tilføjes.</param>
        public void AddAttack(AttackItem attack)
        {
            Attacks.Add(attack);
        }

        /// <summary>
        /// Tilføjer et forsvarsobjekt til skabningen.
        /// </summary>
        /// <param name="defence">Forsvarsobjektet der skal tilføjes.</param>
        public void AddDefence(DefenceItem defence)
        {
            Defences.Add(defence);
        }

        public override string ToString()
        {
            return $"{Name} (HP: {HitPoint}, Position: ({X}, {Y}))";
        }
    }
}
