using System;
using System.Collections.Generic;

namespace SDG6_ShowerGame
{
    internal class Program
    {
        // ---------------- PLAYER STATS ----------------
        static int waterSupply = 100;
        static int hygiene = 70;
        static int score = 0;
        static int currentDay = 1;

        // stores shower type + water usage
        static Dictionary<string, int> showerTypes = new Dictionary<string, int>()
        {
            {"Quick Shower", 10},
            {"Normal Shower", 20},
            {"Long Shower", 35}
        };


        static Stack<string> history = new Stack<string>(); // keeps track of last 3 showers or stores recent actions

        static Random random = new Random(); //gamble

        static void Main(string[] args)
        {
            StartGame();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void StartGame()
        {
            Console.Title = "SAVE EVERY DROP! - SDG 6"; //gibs the program a title rather than just a file name

            Intro(); //show intro then goes into da loop

            // game loop
            while (currentDay <= 7) //if more than 7 days the game finishes!
            {
                Console.Clear();

                DisplayStats();

                PlayerChoice();

                RandomEvent();

                CheckStats();

                currentDay++;


                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

            EndGame();
            
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void Intro() //lil greetings
        {
            Console.WriteLine("======================================");
            Console.WriteLine("      SAVE EVERY DROP - SDG 6");
            Console.WriteLine("======================================");
            Console.Write("\nGoal:");
            Console.Write("\nManage your showers wisely!");
            Console.Write("\nSave water while staying clean.");
            Console.Write("\nSDG 6 focuses on Clean Water");
            Console.Write("\nand Sanitation for everyone.");
            Console.Write("\n\n======================================");

            Console.WriteLine("\nPress any key to start!");
            Console.ReadKey();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void DisplayStats()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("DAY: " + currentDay); 
            Console.WriteLine("======================================");
            Console.WriteLine("Water Supply : " + waterSupply + "L");
            Console.WriteLine("Hygiene      : " + hygiene + "%");
            Console.WriteLine("Score        : " + score);
            Console.WriteLine("======================================");

            // show recent history
            Console.WriteLine("\nRecent Actions:");

            if (history.Count == 0)
            {
                Console.WriteLine("No recent showers yet.");
            }
            else
            {
                foreach (string item in history) //turns a history/stack item into item and gets put inside da writeline
                {
                    Console.WriteLine("- " + item); // display history
                }
            }

            Console.WriteLine("\nChoose Your Shower Type:");
            Console.WriteLine("[1] Quick Shower  (10L)");
            Console.WriteLine("[2] Normal Shower (20L)");
            Console.WriteLine("[3] Long Shower   (35L)");
            Console.WriteLine("[4] Skip Shower");
        }

        static void PlayerChoice()
        {
            Console.Write("\nEnter choice: ");
            string choice = Console.ReadLine();

            switch (choice) // player's shower decision
            {
                case "1":
                    TakeShower("Quick Shower", 15, 15); //values are to add onto hyeigine and score (i forgor how to spell hyegine)
                    break;

                case "2":
                    TakeShower("Normal Shower", 25, 10);
                    break;

                case "3":
                    TakeShower("Long Shower", 40, -5);
                    break;

                case "4":
                    SkipShower();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid choice! Penalty of -5 on your hyegiene. >:(");
                    hygiene -= 5; // penalty for NOT TYPING DOWN ANY OF THE 4 NUMBEERRS but still rando event happens
                    Console.ResetColor();
                    break;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void TakeShower(string type, int hygieneGain, int scoreGain)
        {
            // get water usage from dictionary (iz basically a history tab)
            int waterUsed = showerTypes[type]; //shower type used depends how much water u lose 

            waterSupply -= waterUsed; //wator minuses depending on da shower
            hygiene += hygieneGain; //hyegine adds if u actually shower 
            score += scoreGain; //score just goes up EXCEPT on long showers cause u wasted too much

            // prevent hygiene from exceeding 100 
            if (hygiene > 100)
            {
                hygiene = 100;
            }

            // addz the shower type to stack history
            history.Push(type);

            // keep history short and basically showering only the first 3 showers
            if (history.Count > 3)
            {
                Stack<string> temp = new Stack<string>();

                while (history.Count > 1)
                {
                    temp.Push(history.Pop()); //moves everythin into temp but removes the top/last item 
                }

                history.Pop(); //kills the top or last one

                while (temp.Count > 0)
                {
                    history.Push(temp.Pop()); //pushes new history while removes the top/last item
                }
            }

            Console.WriteLine("\nYou took a " + type + "!");
            Console.WriteLine("Water Used: " + waterUsed + "L");

            // SDG 6 message 
            if (type == "Quick Shower")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Great job conserving water!");
                Console.ResetColor();
            }
            else if (type == "Long Shower")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Too much water was wasted!");
                Console.ResetColor();
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void SkipShower()
        {
            hygiene -= 15;

            history.Push("Skipped Shower"); //puts skipped shower in the stack history

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYou skipped your shower and played League of Legends.");
            Console.WriteLine("Your hygiene decreased.");
            Console.ResetColor();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void RandomEvent() //lil chances of things happening
        {
            int eventChance = random.Next(1, 5);

            Console.WriteLine("\n--- RANDOM EVENT ---");

            switch (eventChance)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Water Leak!");
                    Console.WriteLine("You lost 10L of water.");
                    Console.ResetColor();
                    waterSupply -= 10;
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You installed a low-flow shower!");
                    Console.WriteLine("+10 score");
                    Console.ResetColor();
                    score += 10;
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Community clean water campaign!");
                    Console.WriteLine("+5 hygiene");
                    Console.ResetColor();
                    hygiene += 5;
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Rainwater collected!");
                    Console.WriteLine("+15L water supply");
                    Console.ResetColor();
                    waterSupply += 15;
                    break;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void CheckStats()
        {
            // hygiene limits
            if (hygiene > 100)
            {
                hygiene = 100;
            }

            // lose conditions
            if (waterSupply <= 0)
            {
                Console.WriteLine("======================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou ran out of water!");
                Console.WriteLine("Game Over!\n");
                Console.ResetColor();
                Console.WriteLine("======================================");
                Environment.Exit(0); //alt 4s 
            }

            if (hygiene <= 0)
            {
                Console.WriteLine("======================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour hygiene became too low! You smell bad :( ");
                Console.WriteLine("Game Over!\n");
                Console.ResetColor();
                Console.WriteLine("======================================");
                Environment.Exit(0); //alt 4s
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------

        static void EndGame()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("            GAME COMPLETE");
            Console.WriteLine("======================================");

            Console.WriteLine("Final Score: " + score);

            if (score >= 60) //good ending
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nNice Job!");
                Console.WriteLine("You supported SDG 6 successfully! Keep going!");
                Console.ResetColor();
            }
            else if (score >= 30) //decent ending
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nGood Job!");
                Console.WriteLine("You balanced hygiene and water use but you can do better!");
                Console.ResetColor();
            }
            else //doom
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou need to improve your water habits. Try again!");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nRemember:");
            Console.WriteLine("\"Every Drop Counts!\""); //lil reminder
            Console.ResetColor();

            Console.WriteLine("\nPress any key to exit");
            Console.ReadLine();
        }
    }
}
