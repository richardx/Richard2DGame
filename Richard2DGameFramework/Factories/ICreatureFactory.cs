using Richard2DGameFramework.Model.Creatures;

namespace Richard2DGameFramework.Factories
{
    public interface ICreatureFactory
    {
        Creature CreateCreature(CreatureData data);
    }
}