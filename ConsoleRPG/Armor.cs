using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum armorType {HEAD = 0, CHEST, ARMS, LEGS}

namespace ConsoleRPG
{
    class Armor : Item
    {
        public Armor()
        {
            initNames();
        }
        public Armor(int level, int rarity) : base((int)itemTypes.ARMOR ,level, rarity)
        {
            initNames();
            defence = random.Next(1, (level * (rarity+1))+1) + (rarity+1) * 5;
            type = random.Next(0, 4);
            name = names[random.Next(0, names.Count)];

            switch(type)
            {
                case (int)armorType.HEAD:
                    typeStr = "Head";
                    break;
                case (int)armorType.CHEST:
                    typeStr = "Chest";
                    break;
                case (int)armorType.ARMS:
                    typeStr = "Arms";
                    break;
                case (int)armorType.LEGS:
                    typeStr = "Legs";
                    break;
                default:
                    typeStr = "Error! Invalid type!";
                    break;
            }

            if (rarity == 3)
                defence += level * 5;
            else if (rarity == 4)
                defence += level * 10;
        }

        public int type { get; set; } = -1;
        public int defence { get; set; } = 0;
        public string typeStr { get; set; }

        //if constructors woudln't work
        /*private int _defence;
        public int Defence
        {
            get { return _defence; }
            set { _defence = random.Next(1, level * (rarity+1)) + (rarity+1) * 2; }
        }*/

        public override string toString()
        {
            string str = $"{name} |Type: {(armorType)type} |Lvl: {level.ToString()} |Rarity: {rarity.ToString()} |Def: {defence.ToString()}";
            return str;
        }

        public override string toStringSave()
        {
            string str = $"{itemType.ToString()} {name} {level.ToString()} {rarity.ToString()} {buyValue.ToString()} {sellValue.ToString()} {defence.ToString()} {type.ToString()} ";
            return str;
        }

        List<string> names = new List<string>();

        public void initNames()
        {
            names.Add("Bro_Saver");
            names.Add("Cotton_Rag");
            names.Add("Devlish_Defender");
            names.Add("Angels_Balls");
            names.Add("Iron_Sheets");
            names.Add("Assassin_Bedsheets");
        }

        

        Random random = new Random();
    }
}
