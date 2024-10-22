
namespace Richard2DGameFramework.Model.WorldObjects
{
    /// <summary>
    /// Repræsenterer et objekt i verdenen.
    /// </summary>
    public class WorldObject
    {
        /// <summary>
        /// Navnet på objektet.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// X-koordinaten for objektets position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-koordinaten for objektets position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Angiver om objektet kan lootes.
        /// </summary>
        public bool Lootable { get; set; }

        /// <summary>
        /// Angiver om objektet kan fjernes fra verdenen.
        /// </summary>
        public bool Removable { get; set; }

        public override string ToString()
        {
            return $"{Name} (Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }
    }
}
