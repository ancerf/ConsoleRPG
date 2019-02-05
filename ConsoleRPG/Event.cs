using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Event
    {

        Random random = new Random();

        public int nrOfEvents { get; set; } = 2;

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
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

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
            int playerAccuracy = 0;
            int playerDefence = 0;
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

                                    Console.Write("\nChoice: ");
                                    do
                                    {
                                        string strchoice = Console.ReadLine();
                                        bool success = Int32.TryParse(strchoice, out choice);
                                        if (success && Enumerable.Range(0, enemies.Count).Contains(choice))
                                            break;
                                        else
                                            Console.Write($"\nWrong choice. Please enter new choice (0-{enemies.Count}): ");
                                    }
                                    while (true);

                                    //Attack roll
                                    combatTotal = enemies[choice].defence + character.accuracy;
                                    enemyTotal = Convert.ToInt32(enemies[choice].defence / (decimal)combatTotal * 100);
                                    playerTotal = Convert.ToInt32(character.accuracy / (decimal)combatTotal * 100);
                                    combatRollPlayer = random.Next(1, playerTotal);
                                    combatRollEnemy = random.Next(1, enemyTotal);

                                    //Console.WriteLine($"combatTotal: {combatTotal}");
                                    //Console.WriteLine($"enemyTotal: {enemyTotal}");
                                    //Console.WriteLine($"playerTotal: {playerTotal}");
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
                                    //Console.WriteLine("Continue...");
                                    //Console.ReadLine();

                                    //Enemy attack
                                    for (int i = 0; i < enemies.Count; i++)
                                    {
                                        Console.WriteLine("Continue...");
                                        Console.ReadLine();

                                        Console.WriteLine($"ENEMY: {i} \n");


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
            Puzzle puzzle = new Puzzle("1.txt");
            puzzle.getAsString();

            //random chances Exp
            int randExp = (3 * Convert.ToInt32(character.level) * random.Next(1, 10));

            while (!completed)
            {
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

                    //give user exp and continue
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
