using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum rarity { COMMON = 0, UNCOMMON, RARE, LEGENDARY, MYTHIC}
enum itemTypes { WEAPON = 0, ARMOR}

namespace ConsoleRPG
{
    abstract class Item
    {
        public Item()
        { }

        public Item(int itemType, int level, int rarity)
        {
            this.rarity = rarity;
            this.level = random.Next(1, level);
            buyValue = level * rarity * 10;
            sellValue = buyValue / 2;
            this.itemType = itemType;
        }



        public string name { get; set; } = "EMPTY";
        public int level { get; set; } = 0;
        public int buyValue { get; set; } = 0;
        public int sellValue { get; set; } = 0;
        public int rarity { get; set; } = -1;
        public int itemType { get; set; } = -1;

        //if constructors wouldn't work
        //private int _buyValue;
        //public int BuyValue
        //{
        //    get { return _buyValue; }
        //    set { _buyValue = level * rarity * 10;}
        //}
        //
        //private int _sellValue;
        //public int SellValue
        //{
        //    get { return _sellValue; }
        //    set { SellValue = buyValue / 2; }
        //}
        //
        //private int _level;
        //public int Level
        //{
        //    get { return _level; }
        //    set { _level = random.Next(1, value);}
        //}

        Random random = new Random();

        public virtual string toString() { return null; }
        public virtual string toStringSave() { return null; }


        //public abstract Item Clone();

        public string debugPrint()
        {
            return name;
        }
    }
}
