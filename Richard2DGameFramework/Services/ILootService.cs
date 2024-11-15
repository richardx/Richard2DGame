﻿using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;

namespace Richard2DGameFramework.Services
{
    public interface ILootService
    {
        void Loot(Creature creature, WorldObject obj, World world);
    }
}
