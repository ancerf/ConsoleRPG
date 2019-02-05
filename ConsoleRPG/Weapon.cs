using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Weapon : Item
    {
        public Weapon()
        {
            //blah blah
            initNames();
        }

        public Weapon(int level, int rarity) : base((int)itemTypes.WEAPON, level, rarity)
        {
            initNames();
            damageMax = random.Next(1, (level * (rarity+1)+1)) + (rarity+1) * 5;
            damageMin = damageMax / 2;
            name = names[random.Next(0, names.Count)];

            if (rarity == 3)
                damageMax += level * 5;
            else if (rarity == 4)
                damageMax += level * 10;
        }
        public int damageMin { get; set; } = 0;
        public int damageMax { get; set; } = 0;

        //if constructors wouldn't work
        /*private int _damageMax;
        public int DamageMax
        {
            get { return _damageMax; }
            set { _damageMax = random.Next(1, Level * (rarity + 1)) + (rarity + 1)  * 2; }
        }
        private int _damageMin;
        public int DamageMin
        {
            get { return _damageMin; }
            set { _damageMin = DamageMax / 2; }
        }*/

        public override string toString()
        {
            string str = $"{name} |Lvl: {level.ToString()} |Rarity: {rarity.ToString()} |Damage: {damageMin.ToString()}/{damageMax.ToString()} |Value: {sellValue.ToString()}";
            return str;
        }

        public override string toStringSave()
        {
            string str = $"{itemType.ToString()} {name} {level.ToString()} {rarity.ToString()} {buyValue.ToString()} {sellValue.ToString()} {damageMin.ToString()} {damageMax.ToString()} ";
            return str;
        }


        List<string> names = new List<string>();

        public void initNames()
        {
            names.Add("Butter_Knife");
            names.Add("Leaf_Cutter");
            names.Add("Face_Shredder");
            names.Add("Bro_Knife");
            names.Add("Butter_Knife");
            names.Add("Katana_Sword");
            names.Add("Brutal_Murder");
        }

        

        Random random = new Random();
    }
}
