﻿// File: AttackItem.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Model.Attack
{
    /// <summary>
    /// Repræsenterer et angrebsobjekt.
    /// </summary>
    public class AttackItem : WorldObject, ILootable, IAttack
    {
        public int Hit { get; set; }
        public int Range { get; set; }

        public override string ToString()
        {
            return $"{Name} (Hit: {Hit}, Range: {Range}, Position: ({X}, {Y}), Lootable: {Lootable}, Removable: {Removable})";
        }

        /// <summary>
        /// Implementering af Loot-metoden fra ILootable.
        /// </summary>
        public void Loot(Creature creature, World world, ILogger logger)
        {
            if (!Lootable)
            {
                logger.LogWarning($"{Name} kan ikke samles op.");
                return;
            }

            logger.LogInfo($"{creature.Name} samler {Name} op.");
            creature.AddAttack(this);

            if (Removable)
            {
                world.RemoveWorldObject(this);
                logger.LogInfo($"{Name} fjernet fra verdenen.");
            }
        }
    }
}
