﻿using Richard2DGameFramework.Model.Creatures;

namespace GameTestApp.Factories
{
    public class CreatureFactory : ICreatureFactory
    {
        public Creature CreateCreature(CreatureData data)
        {
            Creature creature = new Creature
            {
                Name = data.Name,
                HitPoint = data.HitPoint,
                X = data.X,
                Y = data.Y,
                Attacks = data.Attacks,
                Defences = data.Defences,
                MagicItems = data.MagicItems
            };

            // Tilføj eventuel spil-specifik logik her
            return creature;
        }
    }
}
