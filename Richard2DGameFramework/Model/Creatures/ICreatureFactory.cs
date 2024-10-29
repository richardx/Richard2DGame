namespace Richard2DGameFramework.Model.Creatures
{
    public interface ICreatureFactory
    {
        Creature CreateCreature(CreatureData data);
    }
}
