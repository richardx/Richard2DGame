
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.WorldObjects
{

    public class WorldObject
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Lootable { get; set; }
        public bool Removable { get; set; }

        // Template Method
        public virtual void Loot(Creature creature, World world, ILogger logger)
        {
            if (!Lootable)
            {
                logger.LogWarning($"{Name} kan ikke samles op.");
                return;
            }

            logger.LogInfo($"{creature.Name} samler {Name} op.");

            if (Removable)
            {
                world.RemoveWorldObject(this);
                logger.LogInfo($"{Name} fjernet fra verdenen.");
            }
        }

        public override string ToString()
        {
            return $"{Name} (Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }
    }
}
