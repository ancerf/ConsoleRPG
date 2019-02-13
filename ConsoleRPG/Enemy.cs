using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Enemy
    {
        Random rand = new Random();

        public int level { get; set; }
        public int hp { get; set; }
        public int hpMax { get; set; }
        public int damageMin { get; set; }
        public int damageMax { get; set; }
        public float dropchance { get; set; }
        public int defence { get; set; }
        public int accuracy { get; set; }

        public Enemy (int level)
        {
            this.level = level;
            hpMax = rand.Next((level * 2), level *10);
            hp = hpMax;
            damageMin = level * 1;
            damageMax = level * 2;
            
            dropchance = rand.Next(1, 100);
            defence = rand.Next(1, level*5);
            accuracy = rand.Next(1, level*5);
        }

        public bool isAlive()
        {
            return hp > 0;
        }

        public string getAsString()
        {
            return "Level: " + level.ToString() + "\n" +
                "Hp : " + hp.ToString() + " / " + hpMax.ToString() + "\n" +
                "Damage : " + damageMin.ToString() + " - " + damageMax.ToString() + "\n" +
                "Defence : " + defence.ToString() + "\n" +
                "Accuracy : " + accuracy.ToString() + "\n" +
                "Drop chance : " + dropchance.ToString() + "\n";
        }

        public void takeDamage(int damage)
        {
            hp -= damage;

            if(hp <= 0)
            {
                hp = 0;


            }
        }

        public int getDamage()
        {
            return rand.Next(1, (damageMax - damageMin));
        }

        public int getExp()
        {
            return level*100;
        }
    }
}
