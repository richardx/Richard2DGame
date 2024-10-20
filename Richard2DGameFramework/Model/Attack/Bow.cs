namespace Richard2DGameFramework.Model.Attack
{
    public class Bow : AttackItem
    {
        public Bow()
        {
            Name = "Bow";
            Hit = 13;
            Range = 6;
            Lootable = true;
            Removable = true;
        }
    }
}
