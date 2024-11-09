using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.WorldObjects
{

    public class MagicItem : WorldObject, ILootable
    {

        public int MagicPower { get; set; }

        public override string ToString()
        {
            return $"{Name} (MagicPower: {MagicPower}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }

        public void Loot(Creature creature, World world, ILogger logger)
        {
            if (!Lootable)
            {
                logger.LogWarning($"{Name} kan ikke samles op.");
                return;
            }

            logger.LogInfo($"{creature.Name} samler {Name} op.");
            creature.AddMagic(this); // Tilføj det magiske objekt til Creature inventar

            if (Removable)
            {
                world.RemoveWorldObject(this);
                logger.LogInfo($"{Name} fjernet fra verdenen.");
            }
        }
    }
}
