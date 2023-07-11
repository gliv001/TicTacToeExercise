using System.Runtime.InteropServices;

namespace TicTacToeExercise
{
    internal class Program
    {
        static char[,] playfield = 
        {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
        static int player = 2;
        static List<int> filledSlots = new List<int>();

        static void Main(string[] args)
        {
            int input = 0;
            bool inputCorrect = true;
            do
            {
                SetField(playfield);
                if(player == 2)
                    player = 1;
                else if(player == 1)
                    player = 2;
                
                do
                {
                    while(true)
                    {
                        try
                        {
                            Console.WriteLine("Player {0}, Please enter a value for your move [1-9]:", player);
                            input = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Incorrect input! Try again! [1-9]:");
                        }

                    }
                    if(input < 1 || input > 9) 
                    {
                        inputCorrect = false;
                        Console.WriteLine("Incorrect value! Try again! [1-9]:");
                    }
                    else if(filledSlots.Contains(input))
                    {
                        inputCorrect = false;
                        Console.WriteLine("Slot already taken! Try again!:");
                    }
                    else
                    {
                        inputCorrect = true;
                        filledSlots.Add(input);
                    }
                }
                while(!inputCorrect);

                EnterVal(player, input);
                if (CheckWinCondition(player, playfield))
                {
                    Console.WriteLine("Do you want to play again? [Y/N]:");
                    char cont = Convert.ToChar(Console.ReadLine());
                    cont = Char.ToLower(cont);
                    if (cont == 'y')
                        RestartGame();
                    else
                    {
                        Console.WriteLine("Game Over! Thank you for playing!");
                        break;
                    }
                }

                if (CheckPlayFieldFilled(playfield))
                {
                    Console.WriteLine("Game Tied! Do you want to play again? [Y/N]");
                    char cont = Convert.ToChar(Console.ReadLine());
                    cont = Char.ToLower(cont);
                    if (cont == 'y')
                    {
                        RestartGame();
                    }
                    else
                    {
                        Console.WriteLine("Game Over! Thank you for playing!");
                        break;
                    }
                }

            } while(true);
        }



        public static void SetField(char[,] pf)
        {
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", pf[0,0], pf[0,1], pf[0,2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", pf[1, 0], pf[1, 1], pf[1, 2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", pf[2, 0], pf[2, 1], pf[2, 2]);
            Console.WriteLine("     |     |     ");
        }

        public static void EnterVal(int player, int input)
        {
            char pSign = ' ';
            if(player == 1)
                pSign = 'X';
            else
                pSign = 'O';

            switch(input)
            {
                case 1: playfield[0, 0] = pSign; break;
                case 2: playfield[0, 1] = pSign; break;
                case 3: playfield[0, 2] = pSign; break;
                case 4: playfield[1, 0] = pSign; break;
                case 5: playfield[1, 1] = pSign; break;
                case 6: playfield[1, 2] = pSign; break;
                case 7: playfield[2, 0] = pSign; break;
                case 8: playfield[2, 1] = pSign; break;
                case 9: playfield[2, 2] = pSign; break;
            }
        }

        public static bool CheckWinCondition(int player, char[,] pf)
        {
            char pSign = ' ';
            if (player == 1)
                pSign = 'X';
            else
                pSign = 'O';

            // Check rows for winning condition
            for (int i = 0; i < 3; i++)
            {
                int count_horizontal = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (pf[i, j] == pSign)
                        count_horizontal++;
                }
                if (count_horizontal >= 3)
                {
                    SetField(pf);
                    Console.WriteLine("Gameover! Player {0} wins!", player);
                    return true;
                }
            }

            // Check columns for winning condition
            for (int i = 0; i < 3; i++)
            {
                int count_vertical = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (pf[j, i] == pSign)
                        count_vertical++;
                }
                if (count_vertical >= 3)
                {
                    Console.WriteLine("Gameover! Player {0} wins!", player);
                    return true;
                }
            }

            // Check diagonals for winning condition
            int count_diagonal_1 = 0;
            int count_diagonal_2 = 0;
            for (int i = 0; i < 3; i++)
            {
                if (pf[i,i] == pSign)
                    count_diagonal_1++;
                if (pf[2 - i, i] == pSign)
                    count_diagonal_2++;
            }
            if (count_diagonal_1 >= 3 || count_diagonal_2 >= 3)
            {
                Console.WriteLine("Gameover! Player {0} wins!", player);
                return true;
            }

            return false;
        }

        public static bool CheckPlayFieldFilled(char[,] pf) 
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(pf[i,j] == 'X' || pf[i,j] == 'O')
                        count++;
                }
            }
            if (count == 9)
                return true;
            else
                return false;
        }

        public static void RestartGame()
        {
            Console.WriteLine("Restarting Game..");
            playfield = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };
            player = 2;
            filledSlots.Clear();
            return;
        }
    }
}