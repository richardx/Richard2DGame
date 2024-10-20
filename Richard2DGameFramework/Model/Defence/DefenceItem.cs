// File: DefenceItem.cs
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Model.Defence
{
    /// <summary>
    /// Repræsenterer et forsvarsobjekt.
    /// </summary>
    public class DefenceItem : WorldObject
    {
        /// <summary>
        /// Antal hit points objektet reducerer skade med.
        /// </summary>
        public int ReduceHitPoint { get; set; }

        public override string ToString()
        {
            return $"{Name} (ReduceHitPoint: {ReduceHitPoint}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }
    }
}
