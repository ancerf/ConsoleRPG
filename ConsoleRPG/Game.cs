﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleRPG
{
    class Game
    {
        //Operators

        //Functions
        public void initGame()
        {

            if(File.Exists("characters.txt"))
            {
                loadCharacters();
            }
            else
            {
                createNewCharacter();
                saveCharacters();
            }

            Weapon initW = new Weapon();
            Armor initA = new Armor();

            //initW.initNames();
            //initA.initNames();

            //Weapon w1 = new Weapon (10, random.Next(1, 5));
            //Weapon w2 = new Weapon (10, random.Next(1, 5));
            //Weapon w3 = new Weapon (10, random.Next(1, 5));
            //
            //Console.WriteLine($"R:{w1.rarity} D:{w1.damageMax} L:{w1.level} {w1.name}");
            //Console.WriteLine($"R:{w2.rarity} D:{w2.damageMax} L:{w2.level} {w2.name}");
            //Console.WriteLine($"R:{w3.rarity} D:{w3.damageMax} L:{w3.level} {w3.name}");

            
            
        }

        public void createNewCharacter()
        {
            Console.Write("Enter character name: ");
            string name = Console.ReadLine();
            
            for (int i = 0; i < characters.Count; i++)
            {
                if (name == characters[i].name)
                {
                    i = -1;
                    Console.Write("Error! Character already exists!");
                    Console.Write("\nEnter character name: ");
                    name = Console.ReadLine();
                }

            }
        
            characters.Add(new Character ());
            activeCharacter = characters.Count - 1;
            characters[activeCharacter].initialize(name);
           
        }

        public void levelUpCharacter()
        {
            characters[activeCharacter].levelUp();

            //stat upgrade & check loop
            if (characters[activeCharacter].statPoints > 0)
            {
                Console.Write("\nYou have stat point to allocate!\n");

                Console.WriteLine("Stat to upgrade: ");
                Console.WriteLine("0: Strength ");
                Console.WriteLine("1: Vitality ");
                Console.WriteLine("2: Dexterity ");
                Console.WriteLine("3: Intelligence ");

                do
                {
                    Console.Write("\nYour choice: ");
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 4).Contains(choice))
                        break;
                    else
                        Console.Write("\nFault input. Please enter new choice (0-3): ");
                }
                while (true);


                switch(choice)
                {
                    case 0://strength
                        characters[activeCharacter].addToStat(0, 1);
                        break;                                 
                    case 1://vitality                          
                        characters[activeCharacter].addToStat(1, 1);
                        break;
                    case 2://dexterity 
                        characters[activeCharacter].addToStat(2, 1);
                        break;
                    case 3://intelligence                         
                        characters[activeCharacter].addToStat(3, 1);
                        break;
                    default:
                        break;
                }
            }
        }

        public void characterMenu()
        {
            do
            {
                if (choice == 5) //print stats from main menu
                {
                    characters[activeCharacter].printStats();
                }

                Console.WriteLine("Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("= CHARACTER MENU =\n\n");

                Console.WriteLine("0: Back to main menu");
                Console.WriteLine("1: Print inventory");
                Console.WriteLine("2: Equip item");

                Console.Write("Your choice: ");
                do
                {
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 3).Contains(choice))
                        break;
                    else
                        Console.Write("\nWrong choice. Please enter new choice (0-1): ");
                }
                while (true);

                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine(characters[activeCharacter].getInvAsString());
                        break;
                    case 2:
                        Console.WriteLine(characters[activeCharacter].getInvAsString());

                        Console.WriteLine("Item index");

                       
                        Console.Write("Your choice: ");
                        do
                        {
                            string strchoice = Console.ReadLine();
                            bool success = Int32.TryParse(strchoice, out choice);
                            if (success && Enumerable.Range(0, characters[activeCharacter].getInventorySize()).Contains(choice))
                                break;
                            else
                                Console.Write("\nWrong choice. Please enter new choice (0-1): ");
                        }
                        while (true);

                        characters[activeCharacter].equipItem(choice);
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);
            
        }

        public void saveCharacters()
        {
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    writetext.WriteLine($"{characters[i].getAsString()}");
                    //Console.WriteLine($"{characters[i].getAsString()}");
                    writetext.WriteLine($"{characters[i].getInvAsStringSave()}");
                }
            }
        }

        public void loadCharacters()
        {
            characters.Clear();

            string profile = "";
            string name = "";
            int distanceTravelled = 0;
            int gold = 0;
            int level = 0;
            int exp = 0;
            int strength = 0;
            int vitality = 0;
            int dexterity = 0;
            int intelligence = 0;
            int hp = 0;
            int stamina = 0;
            int statPoints = 0;

            //Item
            int itemType = 0;
            int defence = 0;
            int type = 0;
            int damageMin = 0;
            int damageMax = 0;
            int itemLevel = 0;
            //name
            //level
            int buyValue = 0;
            int sellValue = 0;
            int rarity = 0;

            //using (StreamReader readtext = new StreamReader(fileName))
            //{
            //
            //    while ((profile = readtext.ReadLine()) != null)
            //    {
            //        end = false;
            //        List <string> itemsList = new List<string>();
            //
            //        itemsList = profile.Split(' ').ToList();
            //
            //        //string[] items = profile.Split(' ');
            //
            //        Character temp = new Character();
            //
            //        if (itemsList.Count == 12)
            //        {
            //
            //            name = itemsList[0];
            //            int.TryParse(itemsList[1], out distanceTravelled);
            //            int.TryParse(itemsList[2], out gold);
            //            int.TryParse(itemsList[3], out level);
            //            int.TryParse(itemsList[4], out exp);
            //            int.TryParse(itemsList[5], out strength);
            //            int.TryParse(itemsList[6], out vitality);
            //            int.TryParse(itemsList[7], out dexterity);
            //            int.TryParse(itemsList[8], out intelligence);
            //            int.TryParse(itemsList[9], out hp);
            //            int.TryParse(itemsList[10], out stamina);
            //            int.TryParse(itemsList[11], out statPoints);
            //
            //            temp = new Character
            //            {
            //                name = name,
            //                distanceTravelled = distanceTravelled,
            //                gold = gold,
            //                level = level,
            //                exp = exp,
            //                strength = strength,
            //                vitality = vitality,
            //                dexterity = dexterity,
            //                intelligence = intelligence,
            //                hp = hp,
            //                stamina = stamina,
            //                statPoints = statPoints
            //            };
            //
            //            temp.updateStats(); //updating formulas/statistics
            //
            //            //Weapon
            //            int.TryParse(itemsList[h], out itemType);
            //            name = itemsList[h];
            //            h++;
            //            int.TryParse(itemsList[h], out level);
            //            h++;
            //            int.TryParse(itemsList[h], out rarity);
            //            h++;
            //            int.TryParse(itemsList[h], out buyValue);
            //            h++;
            //            int.TryParse(itemsList[h], out sellValue);
            //            h++;
            //            int.TryParse(itemsList[h], out damageMin);
            //            h++;
            //            int.TryParse(itemsList[h], out damageMax);
            //
            //            Weapon weapon = new Weapon { damageMin = damageMin, damageMax = damageMax, name = name, level = level, itemType = itemType, rarity = rarity, buyValue = buyValue, sellValue = sellValue};
            //            temp.weapon = weapon;
            //
            //            //Armors head
            //            int.TryParse(itemsList[h], out itemType);
            //            name = itemsList[h];
            //            h++;
            //            int.TryParse(itemsList[h], out level);
            //            h++;
            //            int.TryParse(itemsList[h], out rarity);
            //            h++;
            //            int.TryParse(itemsList[h], out buyValue);
            //            h++;
            //            int.TryParse(itemsList[h], out sellValue);
            //            h++;
            //            int.TryParse(itemsList[h], out defence);
            //            h++;
            //            int.TryParse(itemsList[h], out type);
            //
            //            Armor armor_head = new Armor { type = type, defence = defence, name = name,  level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity};
            //            temp.armor_head = armor_head;
            //
            //            //armors chest
            //            int.TryParse(itemsList[h], out itemType);
            //            name = itemsList[h];
            //            h++;
            //            int.TryParse(itemsList[h], out level);
            //            h++;
            //            int.TryParse(itemsList[h], out rarity);
            //            h++;
            //            int.TryParse(itemsList[h], out buyValue);
            //            h++;
            //            int.TryParse(itemsList[h], out sellValue);
            //            h++;
            //            int.TryParse(itemsList[h], out defence);
            //            h++;
            //            int.TryParse(itemsList[h], out type);
            //
            //            Armor armor_chest = new Armor { type = type, defence = defence, name = name, level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity };
            //            temp.armor_chest = armor_chest;
            //
            //            //armors arms
            //            int.TryParse(itemsList[h], out itemType);
            //            name = itemsList[h];
            //            h++;
            //            int.TryParse(itemsList[h], out level);
            //            h++;
            //            int.TryParse(itemsList[h], out rarity);
            //            h++;
            //            int.TryParse(itemsList[h], out buyValue);
            //            h++;
            //            int.TryParse(itemsList[h], out sellValue);
            //            h++;
            //            int.TryParse(itemsList[h], out defence);
            //            h++;
            //            int.TryParse(itemsList[h], out type);
            //
            //            Armor armor_arms = new Armor { type = type, defence = defence, name = name, level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity };
            //            temp.armor_arms = armor_arms;
            //
            //            //armors legs
            //            int.TryParse(itemsList[h], out itemType);
            //            name = itemsList[h];
            //            h++;
            //            int.TryParse(itemsList[h], out level);
            //            h++;
            //            int.TryParse(itemsList[h], out rarity);
            //            h++;
            //            int.TryParse(itemsList[h], out buyValue);
            //            h++;
            //            int.TryParse(itemsList[h], out sellValue);
            //            h++;
            //            int.TryParse(itemsList[h], out defence);
            //            h++;
            //            int.TryParse(itemsList[h], out type);
            //
            //            Armor armor_legs = new Armor { type = type, defence = defence, name = name, level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity };
            //            characters[activeCharacter].armor_legs = armor_legs;
            //
            //        }
            //
            //        else
            //        {
            //            
            //
            //            
            //            
            //            int h = -1;
            //            int position = 0;
            //            while (end == false)
            //            {
            //                for (int i = position; i < itemsList.Count -2; i++)
            //                {
            //                    h++;
            //                    int.TryParse(itemsList[h], out itemType);
            //                    h++;
            //                    if (itemType == 0)
            //                    {
            //                        name = itemsList[h];
            //                        h++;
            //                        int.TryParse(itemsList[h], out level);
            //                        h++;
            //                        int.TryParse(itemsList[h], out rarity);
            //                        h++;
            //                        int.TryParse(itemsList[h], out buyValue);
            //                        h++;
            //                        int.TryParse(itemsList[h], out sellValue);
            //                        h++;
            //                        int.TryParse(itemsList[h], out damageMin);
            //                        h++;
            //                        int.TryParse(itemsList[h], out damageMax);
            //
            //                        temp.addItem(new Weapon { damageMin = damageMin, damageMax = damageMax, name = name, level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity });
            //                    }
            //
            //                    else if(itemType == 1)
            //                    {
            //
            //                        name = itemsList[h];
            //                        h++;
            //                        int.TryParse(itemsList[h], out level);
            //                        h++;
            //                        int.TryParse(itemsList[h], out rarity);
            //                        h++;
            //                        int.TryParse(itemsList[h], out buyValue);
            //                        h++;
            //                        int.TryParse(itemsList[h], out sellValue);
            //                        h++;
            //                        int.TryParse(itemsList[h], out defence);
            //                        h++;
            //                        int.TryParse(itemsList[h], out type);
            //
            //                        temp.addItem(new Armor { type = type, defence = defence, name = name, level = level, buyValue = buyValue, sellValue = sellValue, rarity = rarity });
            //
            //                    }
            //
            //                    characters.Add(temp);
            //                    i = i + 7;
            //
            //                    if (h == (itemsList.Count -2))
            //                        end = true;
            //
            //                }
            //
            //                
            //            }
            //
            //            
            //
            //            
            //
            //            Console.WriteLine($"Character {temp.name} loaded!");
            //        }
            //    }
            //}

            if (characters.Count <= 0)
                Console.WriteLine("No chracters found!");
        }

        public void selectCharacter()
        {
            Console.WriteLine("\nSelect character: ");

            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"Index: {i} = {characters[i].name} Level: {characters[i].level}"); 
            }

            do
            {
                Console.Write("\nYour choice: ");
                string strchoice = Console.ReadLine();
                bool success = Int32.TryParse(strchoice, out choice);
                if (success && Enumerable.Range(0, characters.Count).Contains(choice))
                    break;
                else
                    Console.Write($"\nFault input. Please enter new choice (0-{characters.Count}): ");
            }
            while (true);

            activeCharacter = choice;

            Console.WriteLine($"{characters[activeCharacter].name} was selected!");
        }

        public void travel()
        {
            characters[activeCharacter].distanceTravelled++;

            Event ev = new Event();
            ev.generateEvent(characters[activeCharacter], enemies);
        }

        public void rest()
        {
            int restCost = characters[activeCharacter].hpMax - characters[activeCharacter].hp;
            Console.WriteLine("= REST =\n");
            Console.WriteLine($"Resting costs you: {restCost}");
            Console.WriteLine($"Your gold: {characters[activeCharacter].gold}");
            Console.WriteLine($"Your HP: {characters[activeCharacter].hp} / {characters[activeCharacter].hpMax}\n\n");

            if ((characters[activeCharacter].hpMax - characters[activeCharacter].hp) < restCost)
            {
                Console.WriteLine("Not enough money! \n\n");
            }
            else if(characters[activeCharacter].hp >= characters[activeCharacter].hpMax)
            {
                Console.WriteLine("You already have full health!");
            }
            else
            {
                Console.WriteLine("Rest? (0) Yes, (1) No");
                do
                {
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 2).Contains(choice))
                        break;
                    else
                        Console.Write("\nWrong choice. Please enter new choice (0-1): ");
                }
                while (true);

                if (choice == 0)
                {
                    characters[activeCharacter].resetHP();
                    characters[activeCharacter].payGold(restCost);
                    Console.WriteLine("You are rested now!\n");
                }
                else
                {
                    Console.WriteLine("See you soon!\n");
                }
            }

        }

        public void mainMenu()
        {
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            if (characters[activeCharacter].isAlive())
            {
                if (characters[activeCharacter].exp >= characters[activeCharacter].expNext)
                {
                    Console.WriteLine("!Level up Available! ");
                }
                //Console.Write("= MAIN MENU =\n\\n1: Travel \n2: Shop \n3: Level up \n4: Rest \n5: Character sheet \n\nChoice: ");
                Console.Write("\n= MAIN MENU = \n");
                Console.WriteLine($"\n= Active character: {characters[activeCharacter].name} Nr: {activeCharacter + 1} / {characters.Count}");
                Console.WriteLine("\n0: Quit ");
                Console.WriteLine("1: Travel ");
                Console.WriteLine("2: Shop ");
                Console.WriteLine("3: Level up ");
                Console.WriteLine("4: Rest ");
                Console.WriteLine("5: Character sheet ");
                Console.WriteLine("6: Create a new character ");
                Console.WriteLine("7: Select character");
                Console.WriteLine("8: Save characters");
                Console.WriteLine("9: Load characters");

                Console.Write("\nChoice: ");

                //choice = Convert.ToInt32(Console.ReadLine());

                //choice get & check loop
                do
                {
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 11).Contains(choice))
                        break;
                    else
                        Console.Write("\nWrong choice. Please enter new choice (0-10): ");
                }
                while (true);

                switch (choice)
                {
                    case 0: //quit
                        playing = false;
                        saveCharacters();
                        break;
                    case 1: // travel
                        travel();
                        break;
                    case 3: // level up
                        levelUpCharacter();
                        break;
                    case 4: // rest
                        rest();
                        break;
                    case 5: //character sheet
                        characterMenu();
                        break;
                    case 6: //create new character
                        createNewCharacter();
                        saveCharacters();
                        break;
                    case 7: //save characters
                        selectCharacter();
                        break;
                    case 8: //save characters
                        saveCharacters();
                        break;
                    case 9: //load characters
                        loadCharacters();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.Write("\n= YOU ARE DEAD, LOAD?=");
                Console.Write("(0) Yes, (1) No \n");

                do
                {
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 2).Contains(choice))
                        break;
                    else
                        Console.Write("\nWrong choice. Please enter new choice (0-1): ");
                }
                while (true);

                if (choice == 0)
                    loadCharacters();
                else
                    playing = false;
            }
        }

        //Accesors
        public bool getPlaying()
        {
            return this.playing;
        }

        private int choice;
        public bool playing { get; set; } = true;

        public int activeCharacter;
        List<Character> characters = new List<Character>();
        List<Inventory> tempItems = new List<Inventory>();

        public string fileName { get; set; } = "characters.txt";

        public int help { get; set; } = 0;
        public bool end { get; set; } = false;

        List<Enemy> enemies = new List<Enemy>();

        Random random = new Random();
       
    }

}