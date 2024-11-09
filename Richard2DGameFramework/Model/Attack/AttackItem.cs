using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.Attack
{

    public class AttackItem : WorldObject, ILootable, IAttack
    {
        public int Hit { get; set; }
        public int Range { get; set; }

        public override string ToString()
        {
            return $"{Name} (Hit: {Hit}, Range: {Range}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }

        public void Loot(Creature creature, World world, ILogger logger)
        {
            if (!Lootable)
            {
                logger.LogWarning($"{Name} kan ikke samles op.");
                return;
            }

            logger.LogInfo($"{creature.Name} samler {Name} op.");
            creature.AddAttack(this); // Tilføj angrebsobjektet til creature

            if (Removable)
            {
                world.RemoveWorldObject(this);
                logger.LogInfo($"{Name} fjernet fra verdenen.");
            }
        }
    }
}
