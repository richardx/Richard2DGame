namespace Richard2DGameFramework.Model.Defence
{
    public class Shield : DefenceItem
    {
        public Shield()
        {
            Name = "Shield";
            ReduceHitPoint = 11;
            Lootable = true;
            Removable = true;
        }
    }
}
