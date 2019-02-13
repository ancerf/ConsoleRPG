using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Event
    {
        static int nrOfEvents = 3;
        Random random = new Random();

        public void generateEvent(Character character, List <Enemy> enemies)
        {
            Random rand = new Random();
            int i = rand.Next(0, nrOfEvents);

            switch (i)
            {
                case 0:
                    //Enemy encounter
                    enemyEncounter(character, enemies);
                    break;
                case 1:
                    //Puzzle
                    puzzleEncounter(character);
                    break;
                    //Shop encounter
                case 2:
                    shopEncounter(character);
                    break;
                default:
                    break;
            }
        }

        public void shopEncounter(Character character)
        {
            int choice = 0;
            bool shopping = true;
            string inv = "";

            //Init merchant inv
            int nrOfItems = random.Next(10, 20);
            int coinToss = 0;
            Inventory merchantInv = new Inventory();

            for (int i = 0; i < nrOfItems; i++)
            {
                coinToss = random.Next(0, 100);
                if (coinToss > 50)
                {
                    merchantInv.addItem(new Weapon(random.Next((int)character.level, (int)character.level + random.Next(1, 5)), random.Next(0, 5)));
                }
                else
                    merchantInv.addItem(new Armor(random.Next((int)character.level, (int)character.level + random.Next(1, 5)), random.Next(0, 5), -1));
            }
            do
            {
                Console.Clear();
                Console.WriteLine("= Shop Menu =\n");
                Console.WriteLine("0: Leave ");
                Console.WriteLine("1: Buy ");
                Console.WriteLine("2: Sell ");
                do
                {
                    Console.Write("\nYour choice: ");
                    string strchoice = Console.ReadLine();
                    bool success = Int32.TryParse(strchoice, out choice);
                    if (success && Enumerable.Range(0, 3).Contains(choice))
                        break;
                    else
                        Console.Write("\nFault input. Please enter new choice (0-2): ");
                }
                while (true);

            
                switch (choice)
                {
                    case 0://leave
                        shopping = false;
                        break;
                    case 1://Buy     
                        do
                        {
                            Console.Write($"= Buy Menu =\n");

                            Console.Write($"Your Gold: {character.gold} \n\n");
                            for (int i = 0; i < merchantInv.size(); i++)
                            {
                                inv += i.ToString() + ": " + merchantInv[i].toString() + " " + " |Price: " + merchantInv[i].buyValue.ToString() + "\n";
                            }
                            Console.WriteLine(inv);
                            Console.WriteLine($"({merchantInv.size()}) to cancel");
                            Console.Write("\nYour choice: ");
                            string strchoice = Console.ReadLine();
                            Console.Write($"Your Gold: {character.gold} \n\n");
                            bool success = Int32.TryParse(strchoice, out choice);
                            if (success && Enumerable.Range(0, merchantInv.size()+1).Contains(choice))
                                break;
                            else
                                Console.Write($"\nFault input. Please enter new choice (0-{merchantInv.size()}): ");
                        }
                        while (true);

                        if (choice == merchantInv.size())
                        {
                            Console.WriteLine($"Cancelled...");
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }

                        else
                        {
                            if (character.gold >= merchantInv[choice].buyValue)
                            {
                                character.payGold(merchantInv[choice].buyValue);
                                character.addItem(merchantInv[choice]);

                                Console.WriteLine($"Bought item {merchantInv[choice].name} - {merchantInv[choice].buyValue}");

                                merchantInv.removeItem(choice);
                                nrOfItems--;
                            }
                            else
                            {
                                Console.Write($"Can't buy selected item!");
                            }
                        }
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadLine();

                        break;
                    case 2://Sell 
                        Console.WriteLine($"{character.getInvAsString(true)}");

                        Console.Write($"= Sell Menu =\n");

                        Console.Write($"Your Gold: {character.gold} \n\n");

                        if (character.getInventorySize() > 0)
                        {
                            do
                            {
                                Console.WriteLine($"{character.getInventorySize()} to cancel");
                                Console.Write("\nYour choice: ");
                                string strchoice = Console.ReadLine();
                                Console.Write($"Your Gold: {character.gold} \n\n");
                                bool success = Int32.TryParse(strchoice, out choice);
                                if (success && Enumerable.Range(0, character.getInventorySize()+1).Contains(choice))
                                    break;
                                else
                                    Console.Write($"\nFault input. Please enter new choice (0-{character.getInventorySize()}): ");
                            }
                            while (true);

                            if (choice == character.getInventorySize())
                            {
                                Console.WriteLine($"Cancelled...");
                                Console.WriteLine("Press ENTER to continue...");
                                Console.ReadLine();
                                break;
                            }

                            character.gainGold(character.getItem(choice).sellValue);
                            Console.WriteLine("Item sold!");
                            Console.WriteLine($"Gold earned: {character.getItem(choice).sellValue}!\n\n");
                            character.removeItem(choice);
                        }

                        else
                        {
                            Console.Write($"\nYou don't have any inventory for sell!\n");
                        }
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }
            while (shopping == true);
            Console.WriteLine("\nYou left the shop.. \n");
        }
        public void enemyEncounter(Character character, List<Enemy> enemies)
        {
            bool playerTurn = false;
            int choice;

            //Coin toss for turn
            int coinToss = random.Next(1, 2);

            if (coinToss == 1)
                playerTurn = true;
            else
                playerTurn = false;

            //End conditions
            bool escape = false;
            bool playerDefeated = false;
            bool enemiesDefeated = false;

            //Enemies
            int nrOfEnemies = random.Next(1, 5);

            for (int i = 0; i < nrOfEnemies; i++)
            {
                enemies.Add(new Enemy(Convert.ToInt32(character.level + random.Next(0, 3))));
            }

            //Battle variables
            int damage = 0;
            int gainExp = 0;
            int playerTotal = 0;
            int enemyTotal = 0;
            int combatTotal = 0;
            int combatRollPlayer = 0;
            int combatRollEnemy = 0;

            while (!escape && !playerDefeated && !enemiesDefeated)
            {
                if (playerTurn && !playerDefeated)
                {
                    //Menu
                    Console.Clear();
                    Console.WriteLine("= PLAYER TURN =\n\n");
                    Console.WriteLine("= BATTLE MENU =");

                    Console.WriteLine("\n0: Escape");
                    Console.WriteLine("1: Attack");
                    Console.WriteLine("2: Defent");
                    Console.WriteLine("3: Use Item");
                    Console.Write("\nChoice: ");
                    do
                    {
                        string strchoice = Console.ReadLine();
                        bool success = Int32.TryParse(strchoice, out choice);
                        if (success && Enumerable.Range(0, 4).Contains(choice))
                            break;
                        else
                            Console.Write("\nWrong choice. Please enter new choice (0-3): ");
                    }
                    while (true);

                    //Moves
                    switch (choice)
                    {
                        case 0://escape
                            escape = true;
                            break;
                        case 1://attack
                            do
                            {
                                if (playerTurn && !enemiesDefeated && !playerDefeated)
                                {
                                    //Select enemy
                                    Console.WriteLine("Select enemy");

                                    for (int i = 0; i < enemies.Count; i++)
                                    {
                                        Console.WriteLine($"{i}: Level: {enemies[i].level} - HP: {enemies[i].hp}/{enemies[i].hpMax} - Defence: {enemies[i].defence} - Accuracy: {enemies[i].accuracy}");
                                    }
                                    Console.WriteLine($"({enemies.Count}) to cancel");
                                    Console.Write("\nChoice: ");
                                    
                                    do
                                    {
                                        string strchoice = Console.ReadLine();
                                        bool success = Int32.TryParse(strchoice, out choice);
                                        if (success && Enumerable.Range(0, enemies.Count + 1).Contains(choice))
                                            break;
                                        else
                                            Console.Write($"\nWrong choice. Please enter new choice (0-{enemies.Count}): ");
                                    }
                                    while (true);

                                    if (choice == enemies.Count)
                                    {
                                        Console.WriteLine($"Cancelled...");
                                        break;
                                    }

                                    //Attack roll
                                    combatTotal = enemies[choice].defence + character.accuracy;
                                    enemyTotal = Convert.ToInt32(enemies[choice].defence / (decimal)combatTotal * 100);
                                    playerTotal = Convert.ToInt32(character.accuracy / (decimal)combatTotal * 100);
                                    combatRollPlayer = random.Next(1, playerTotal);
                                    combatRollEnemy = random.Next(1, enemyTotal);

                                    Console.WriteLine($"Player role: {combatRollPlayer}");
                                    Console.WriteLine($"Enemy role: {combatRollEnemy}");

                                    if (combatRollPlayer > combatRollEnemy)//hit
                                    {
                                        Console.WriteLine("HIT!");
                                        damage = random.Next(character.damageMin, character.damageMax);
                                        enemies[choice].takeDamage(damage);

                                        Console.WriteLine($"Damage: {damage}! \n");

                                        if (!enemies[choice].isAlive())
                                        {
                                            Console.WriteLine($"Enemy {choice} defeated! \n");
                                            gainExp = enemies[choice].getExp();
                                            character.gainExp(gainExp);
                                            Console.WriteLine($"EXP gained {gainExp}! \n");

                                            //Item roll
                                            int roll = random.Next(1, 100);
                                            int rarity = 1;

                                            if (roll > 20)
                                            {
                                                rarity = 2; //Common

                                                roll = random.Next(1, 100);
                                                if (roll > 30)
                                                {
                                                    rarity = 3; //Uncommon

                                                    roll = random.Next(1, 100);
                                                    if (roll > 50)
                                                    {
                                                        rarity = 4; //Rare

                                                        roll = random.Next(1, 100);
                                                        if (roll > 70)
                                                        {
                                                            rarity = 5; //Legendary

                                                            roll = random.Next(1, 100);
                                                            if (roll > 79)
                                                            {
                                                                rarity = 6; //Mychic
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if(roll >= 0)
                                            {
                                                roll = random.Next(1, 100);

                                                if(roll >50)
                                                {
                                                    character.addItem(new Weapon((int)character.level, rarity));
                                                    Console.WriteLine("Weapon drop!");
                                                }
                                                else
                                                {
                                                    character.addItem(new Armor((int)character.level, rarity, -1));
                                                    Console.WriteLine("Armor drop!");
                                                }
                                            }

                                            enemies.RemoveAt(choice);
                                        }
                                    }
                                    else//miss
                                    {
                                        Console.WriteLine($"Missed! \n" + enemies[choice].getDamage());
                                    }

                                    //End turn
                                    playerTurn = false;
                                }

                                else if (!playerTurn && !enemiesDefeated && !playerDefeated)
                                {
                                    Console.WriteLine($"= ENEMY TURN =");

                                    //Enemy attack
                                    for (int i = 0; i < enemies.Count; i++)
                                    {
                                        //Console.WriteLine("Continue...");
                                        //Console.ReadLine();

                                        Console.WriteLine($"ENEMY: {i}");

                                        //Attack roll
                                        combatTotal = enemies[i].defence + character.accuracy;
                                        enemyTotal = Convert.ToInt32(enemies[i].defence / (decimal)combatTotal * 100);
                                        playerTotal = Convert.ToInt32(character.accuracy / (decimal)combatTotal * 100);
                                        combatRollPlayer = random.Next(1, playerTotal);
                                        combatRollEnemy = random.Next(1, enemyTotal);

                                        Console.WriteLine($"Player role: {combatRollPlayer}");
                                        Console.WriteLine($"Enemy role: {combatRollEnemy}");

                                        if (combatRollPlayer < combatRollEnemy)//hit
                                        {
                                            Console.WriteLine("HIT!");
                                            damage = enemies[i].getDamage();
                                            character.takeDamage(damage);

                                            Console.WriteLine($"Damage: {damage}! \n");
                                            Console.WriteLine("Continue...");
                                            Console.ReadLine();

                                            if (!character.isAlive())
                                            {
                                                Console.WriteLine($"You are defeated! \n");
                                                playerDefeated = true;
                                            }
                                        }
                                        else//miss
                                        {
                                            Console.WriteLine($"Missed! \n");
                                        }

                                        //End turn
                                        playerTurn = true;
                                    }
                                }
                            }
                            while(enemies.Count > 0 && !playerDefeated);
                            break;
                        case 2://defend
                            break;
                        case 3://item
                            break;
                        default:
                            break;
                    }
                }
                
                //Conditions
                if (character.isAlive())
                {
                    playerDefeated = true;
                }
                if (enemies.Count <= 0)
                {
                    enemiesDefeated = true;
                }
            }
        }

        public void puzzleEncounter(Character character)
        {
            bool completed = false;
            int userAnswer = 0;
            int nrOfPuzzles = 3;
            string puzzleToDisplay = random.Next(1, nrOfPuzzles).ToString() + ".txt";
            Puzzle puzzle = new Puzzle(puzzleToDisplay);
            puzzle.getAsString();

            //random chances Exp
            int randExp = (3 * Convert.ToInt32(character.level) * random.Next(1, 10));

            while (!completed)
            {
                Console.Clear();
                Console.WriteLine(puzzle.getAsString());
                Console.Write("\nYour answer: ");

                do
                {
                    bool success = Int32.TryParse(Console.ReadLine(), out userAnswer);
                    Console.Write("\n");

                    if (success && Enumerable.Range(0, 3).Contains(userAnswer))
                        break;
                    else
                        Console.Write("Wrong choice. Please enter new choice (0-2): ");
                }
                while (true);

                if (puzzle.correctAnswer == userAnswer)
                {
                    completed = true;

                    character.gainExp(randExp);
                    Console.Write("You've gained: " + randExp + " EXP! \n\n");
                }
            }

            if(completed)
            {
                Console.WriteLine("Congrats! You've succeded! \n");
            }
            else
            {
                Console.WriteLine("Failed! \n");
            }
        }
    }
}
