using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Model.Creatures
{
    public class CreatureData
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<IAttack> Attacks { get; set; } = new List<IAttack>();
        public List<IDefence> Defences { get; set; } = new List<IDefence>();
        public List<MagicItem> MagicItems { get; set; } = new List<MagicItem>();
    }
}
