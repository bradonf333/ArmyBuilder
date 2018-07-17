using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder
{
    public class Soldier
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Classification { get; set; }
        public string Rank { get; set; }
        public int HitPoints { get; set; }
        public DateTime Birthdate { get; set; }
        public int AttackStrength { get; set; }
        public int SorceryStrength { get; set; }

        public void Attack()
        {

        }

        public void Defend()
        {

        }
    }
}
