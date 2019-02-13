using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleRPG
{
    class Game
    {
        Random random = new Random();

        List<Enemy> enemies = new List<Enemy>();
        List<Character> characters = new List<Character>();
        List<Inventory> tempItems = new List<Inventory>();

        private int choice;
        public bool playing { get; set; } = true;
        public int activeCharacter;
        public string fileName { get; set; } = "characters.txt";
        public int help { get; set; } = 0;
        public bool end { get; set; } = false;

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
                if (choice == 4) //print stats from main menu
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
                        Console.Write("\nWrong choice. Please enter new choice (0-2): ");
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
                    writetext.WriteLine($"3 {characters[i].getAsString()}");
                    if (characters[i].getInvAsStringSave() != "")
                    {
                        writetext.WriteLine($"{characters[i].getInvAsStringSave()}");
                    }
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
            int buyValue = 0;
            int sellValue = 0;
            int rarity = 0;

            using (StreamReader readtext = new StreamReader(fileName))
            {

                Character temp = new Character();
                bool secondCharacter = false;

                while ((profile = readtext.ReadLine()) != null)
                {
                    end = false;
                        List<string> itemsList = new List<string>();

                        itemsList = profile.Split(' ').ToList();
                    if(itemsList[0] == "")
                    {
                        break;
                    }

                    if (Convert.ToInt32(itemsList[0]) == 3)
                    {
                        if (secondCharacter)
                        {
                            characters.Add(temp);
                        }

                        if (profile != null)
                        {
                            name = itemsList[1];
                            int.TryParse(itemsList[2], out distanceTravelled);
                            int.TryParse(itemsList[3], out gold);
                            int.TryParse(itemsList[4], out level);
                            int.TryParse(itemsList[5], out exp);
                            int.TryParse(itemsList[6], out strength);
                            int.TryParse(itemsList[7], out vitality);
                            int.TryParse(itemsList[8], out dexterity);
                            int.TryParse(itemsList[9], out intelligence);
                            int.TryParse(itemsList[10], out hp);
                            int.TryParse(itemsList[11], out stamina);
                            int.TryParse(itemsList[12], out statPoints);

                            temp = new Character
                            {
                                name = name,
                                distanceTravelled = distanceTravelled,
                                gold = gold,
                                level = level,
                                exp = exp,
                                strength = strength,
                                vitality = vitality,
                                dexterity = dexterity,
                                intelligence = intelligence,
                                hp = hp,
                                stamina = stamina,
                                statPoints = statPoints
                            };

                            temp.updateStats(); //updating formulas/statistics

                            bool whil = true;
                            int actualPosition = 13;


                            while (whil)
                            {
                                if (Convert.ToInt32(itemsList[actualPosition]) == 0)
                                {
                                    //Weapon
                                    int.TryParse(itemsList[actualPosition], out itemType);
                                    actualPosition++;
                                    name = itemsList[actualPosition];
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out level);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out rarity);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out buyValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out sellValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out damageMin);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out damageMax);

                                    Weapon weapon = new Weapon(level, rarity) { damageMin = damageMin, damageMax = damageMax, name = name, itemType = itemType, buyValue = buyValue, sellValue = sellValue };
                                    temp.weapon = weapon;
                                }

                                else if (Convert.ToInt32(itemsList[actualPosition]) == 1 && Convert.ToInt32(itemsList[actualPosition + 7]) == 0)
                                {
                                    //Armors head
                                    int.TryParse(itemsList[actualPosition], out itemType);
                                    actualPosition++;
                                    name = itemsList[actualPosition];
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out level);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out rarity);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out buyValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out sellValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out defence);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out type);

                                    Armor armor_head = new Armor(level, rarity, type) {defence = defence, name = name, buyValue = buyValue, sellValue = sellValue };
                                    temp.armor_head = armor_head;
                                }

                                else if (Convert.ToInt32(itemsList[actualPosition]) == 1 && Convert.ToInt32(itemsList[actualPosition + 7]) == 1)
                                {
                                    //armors chest
                                    int.TryParse(itemsList[actualPosition], out itemType);
                                    actualPosition++;
                                    name = itemsList[actualPosition];
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out level);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out rarity);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out buyValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out sellValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out defence);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out type);

                                    Armor armor_chest = new Armor(level, rarity, type) { defence = defence, name = name, buyValue = buyValue, sellValue = sellValue };
                                    temp.armor_chest = armor_chest;
                                }

                                else if (Convert.ToInt32(itemsList[actualPosition]) == 1 && Convert.ToInt32(itemsList[actualPosition + 7]) == 2)
                                {
                                    //armors arms
                                    int.TryParse(itemsList[actualPosition], out itemType);
                                    actualPosition++;
                                    name = itemsList[actualPosition];
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out level);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out rarity);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out buyValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out sellValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out defence);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out type);

                                    Armor armor_arms = new Armor(level, rarity, type) { defence = defence, name = name, buyValue = buyValue, sellValue = sellValue };
                                    temp.armor_arms = armor_arms;
                                }

                                else if (Convert.ToInt32(itemsList[actualPosition]) == 1 && Convert.ToInt32(itemsList[actualPosition + 7]) == 3)
                                {
                                    //armors legs
                                    int.TryParse(itemsList[actualPosition], out itemType);
                                    actualPosition++;
                                    name = itemsList[actualPosition];
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out level);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out rarity);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out buyValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out sellValue);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out defence);
                                    actualPosition++;
                                    int.TryParse(itemsList[actualPosition], out type);

                                    Armor armor_legs = new Armor(level, rarity, type) { defence = defence, name = name, buyValue = buyValue, sellValue = sellValue };
                                    temp.armor_legs = armor_legs;
                                }

                                else if (Convert.ToInt32(itemsList[actualPosition]) == -1 && actualPosition == 12)
                                {
                                    actualPosition += 7;
                                }

                                else if(Convert.ToInt32(itemsList[actualPosition]) == - 1)
                                {
                                    actualPosition += 7;
                                }


                                actualPosition++;
                                if (actualPosition + 1 == itemsList.Count)
                                    whil = false;
                            }
                            Console.WriteLine($"Character {temp.name} loaded!");
                            secondCharacter = true;
                        }
                    }
                    else if (Convert.ToInt32(itemsList[0]) == 0 || Convert.ToInt32(itemsList[0]) == 1)
                    {
                        int h = 0;
                        int position = 0;
                        while (end == false)
                        {
                            if (itemsList.Count == 1)
                                end = true;
                            for (int i = position; i < itemsList.Count - 2; i++)
                            {
                                int.TryParse(itemsList[h], out itemType);
                                h++;
                                if (itemType == 0)
                                {
                                    name = itemsList[h];
                                    h++;
                                    int.TryParse(itemsList[h], out level);
                                    h++;
                                    int.TryParse(itemsList[h], out rarity);
                                    h++;
                                    int.TryParse(itemsList[h], out buyValue);
                                    h++;
                                    int.TryParse(itemsList[h], out sellValue);
                                    h++;
                                    int.TryParse(itemsList[h], out damageMin);
                                    h++;
                                    int.TryParse(itemsList[h], out damageMax);
                                    h++;
            
                                    temp.addItem(new Weapon(level, rarity) { damageMin = damageMin, damageMax = damageMax, name = name, buyValue = buyValue, sellValue = sellValue});
                                }
            
                                else if(itemType == 1)
                                {
            
                                    name = itemsList[h];
                                    h++;
                                    int.TryParse(itemsList[h], out level);
                                    h++;
                                    int.TryParse(itemsList[h], out rarity);
                                    h++;
                                    int.TryParse(itemsList[h], out buyValue);
                                    h++;
                                    int.TryParse(itemsList[h], out sellValue);
                                    h++;
                                    int.TryParse(itemsList[h], out defence);
                                    h++;
                                    int.TryParse(itemsList[h], out type);
                                    h++;
            
                                    temp.addItem(new Armor (level, rarity, type) {defence = defence, name = name,  buyValue = buyValue, sellValue = sellValue});
            
                                }

                                i = i + 7;
            
                                if (h == (itemsList.Count -1))
                                    end = true;
                            }
                        }

                        characters.Add(temp);
                        secondCharacter = false;
                        temp = null;
                    }

                }
                
                if (secondCharacter = true && temp != null)
                {
                    characters.Add(temp);
                }

                else
                {
                    if (characters.Count <= 0)
                        Console.WriteLine("No chracters found!");
                }
            }
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
                Console.Clear();
                if (characters[activeCharacter].exp >= characters[activeCharacter].expNext)
                {
                    Console.WriteLine("!Level up Available! ");
                }
                Console.Write("\n= MAIN MENU = \n");
                Console.WriteLine($"\n= Active character: {characters[activeCharacter].name} Nr: {activeCharacter + 1} / {characters.Count}");
                Console.WriteLine("\n0: Quit ");
                Console.WriteLine("1: Travel ");
                Console.WriteLine("2: Level up ");
                Console.WriteLine("3: Rest ");
                Console.WriteLine("4: Character sheet ");
                Console.WriteLine("5: Create a new character ");
                Console.WriteLine("6: Select character");
                Console.WriteLine("7: Save characters");
                Console.WriteLine("8: Load characters");

                Console.Write("\nChoice: ");

                //choice get & check loop
                do
                {
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 9).Contains(choice))
                        break;
                    else
                        Console.Write("\nWrong choice. Please enter new choice (0-8): ");
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
                    case 2: // level up
                        levelUpCharacter();
                        break;
                    case 3: // rest
                        rest();
                        break;
                    case 4: //character sheet
                        characterMenu();
                        break;
                    case 5: //create new character
                        createNewCharacter();
                        saveCharacters();
                        break;
                    case 6: //save characters
                        selectCharacter();
                        break;
                    case 7: //save characters
                        saveCharacters();
                        break;
                    case 8: //load characters
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
        
        public bool getPlaying()
        {
            return this.playing;
        }
       
    }
}
