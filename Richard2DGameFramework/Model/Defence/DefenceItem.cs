
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.Defence
{

    public class DefenceItem : WorldObject, ILootable, IDefence
    {
        public int ReduceHitPoint { get; set; }

        public override string ToString()
        {
            return $"{Name} (ReduceHitPoint: {ReduceHitPoint}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }

        public void Loot(Creature creature, World world, ILogger logger)
        {
            if (!Lootable)
            {
                logger.LogWarning($"{Name} kan ikke samles op.");
                return;
            }

            logger.LogInfo($"{creature.Name} samler {Name} op.");
            creature.AddDefence(this);

            if (Removable)
            {
                world.RemoveWorldObject(this);
                logger.LogInfo($"{Name} fjernet fra verdenen.");
            }
        }
    }
}
