using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Richard2DGameFramework.Model.Creatures
{
    /// <summary>
    /// Interface for alle creatures.
    /// </summary>
    public interface ICreature
    {
        string Name { get; set; }
        int HitPoint { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool IsAlive { get; set; }

        // Metoder til håndtering af forsvar
        void AddDefence(IDefence defence);
        void RemoveDefence(IDefence defence);
        IEnumerable<IDefence> GetDefences();

        // Metoder til håndtering af angreb
        void AddAttack(IAttack attack);
        void RemoveAttack(IAttack attack);
        IEnumerable<IAttack> GetAttacks();

        // Metoder til håndtering af magi
        void AddMagic(MagicItem magicItem);
        void RemoveMagic(MagicItem magicItem);
        IEnumerable<MagicItem> GetMagicItems();
        void UseMagic(MagicItem magicItem, ICreature target, ILogger logger);

        // Metoder til bevægelse
        void MoveTo(int x, int y);
        void MoveBy(int deltaX, int deltaY);

        // Metoder til kamp og skade
        void PerformAttack(ICreature target, ILogger logger);
        void ReceiveDamage(int damage, ILogger logger);
        void Heal(int amount);
        void Die(ILogger logger);


    }
}
