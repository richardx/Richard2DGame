// File: AttackItem.cs
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Model.Attack
{
    /// <summary>
    /// Repræsenterer et angrebsobjekt.
    /// </summary>
    public class AttackItem : WorldObject
    {
        /// <summary>
        /// Antal hit points objektet kan påføre.
        /// </summary>
        public int Hit { get; set; }

        /// <summary>
        /// Rækkevidden for angrebsobjektet.
        /// </summary>
        public int Range { get; set; }

        public override string ToString()
        {
            return $"{Name} (Hit: {Hit}, Range: {Range}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }
    }
}
