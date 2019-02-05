using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Inventory
    {

        private int cap = 5;
        private int nrOfItems = 0;

        public List<Item> itemArr = new List<Item>();

        //private void expand()
        //{
        //    cap *= 2;
        //
        //    List<Item>tempArr = new List<Item>();
        //
        //    for (int i = 0; i < nrOfItems; i++)
        //    {
        //        tempArr[i] = itemArr[i];
        //    }
        //
        //    //for (int i = 0; i < nrOfItems; i++)
        //    //{
        //    //    itemArr[i] = null;
        //    //}
        //
        //    itemArr = null;
        //
        //    itemArr = tempArr;
        //
        //    initialize(nrOfItems);
        //}

        private void initialize(int from)
        {
            List<Item> itemArr = new List<Item>();
        }

        public void addItem(Item item)

        {
            
            itemArr.Add(item);
        }

        public void removeItem(int index)
        {
            //if (index < 0 || index >= nrOfItems)
            //    Console.WriteLine("Error! Out of bounds remove item inventory.");
            itemArr.RemoveAt(index);
        }

        public int size()
        {
            return itemArr.Count;
        }

        public Item this[int index]
        {
            get
            {
                return itemArr[index];
            }
        }


        //public void debugPrint()
        //{
        //    for (int i = 0; i < nrOfItems; i++)
        //    {
        //        Console.WriteLine(itemArr[i].debugPrint());
        //    }
        //}
    }
}
