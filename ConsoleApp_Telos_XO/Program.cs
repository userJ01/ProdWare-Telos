using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Telos_XO
{
    class Program
    {
        //making array and   
        //by default I am providing 0-9 where no use of zero  
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int matrixNum = 0;
        static string[] arr2 = null;
        static int player = 1; //By default player 1 is set  
        static int choice; //This holds the choice at which position user want to mark   
        // The flag veriable checks who has won if it's value is 1 then some one has won the match if -1 then Match has Draw if 0 then match is still running  
        static int flag = 0;
        static bool bingo = false;
        static void Main(string[] args)
        {
            do 
            {
                Console.Clear();
                Console.WriteLine("Enter The Number Of Matrix:");
                int.TryParse(Console.ReadKey().KeyChar.ToString(), out matrixNum);
            }
            while (!(matrixNum > 1));

            arr = new char[(matrixNum*matrixNum)+1];
            arr2 = new string[((matrixNum * matrixNum) + 1)];
            Board2(true);
            do
            {
                #region Region Print
                Console.Clear();// whenever loop will be again start then screen will be clear  
                Console.Write("Player1:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("X ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("and Player2:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n");

                if (player % 2 == 0)//checking the chance of the player  
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Player 2 Chance");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player 1 Chance");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine("\n"); 
                #endregion

                Board2(false);// calling the board Function  
                if((arr2.Length-1)>9)
                    choice = int.Parse(Console.ReadLine().Trim());//Taking users choice   
                else
                    choice = int.Parse(Console.ReadKey().KeyChar.ToString());//Taking users choice  
                // checking that position where user want to run is marked (with X or O) or not  
                if (arr2[choice] != "X" && arr2[choice] != "O")
                {
                    if (player % 2 == 0) //if chance is of player 2 then mark O else mark X  
                    {
                        arr2[choice] = "O";
                        player++;
                    }
                    else
                    {
                        arr2[choice] = "X";
                        player++;
                    }
                }
                else //If there is any possition where user want to run and that is already marked then show message and load board again  
                {
                    Console.WriteLine("\n"); 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry the row {0} is already marked with {1}", choice, arr2[choice]);
                    Console.WriteLine("\n");
                    Console.WriteLine("Please wait 2 second board is loading again.....");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                flag = CheckWin2();// calling of check win  

            } while (flag != 1 && flag != -1);// This loof will be run until all cell of the grid is not marked with X and O or some player is not win  

            Console.Clear();// clearing the console  

            Board2(false);// getting filled board again  


            #region Cheked wich user is WIN
            if (flag == 1)// if flag value is 1 then some one has win or means who played marked last time which has win  
            {
                Console.WriteLine("Player {0} has won", (player % 2) + 1);
            }
            else// if flag value is -1 the match will be draw and no one is winner  
            {
                Console.WriteLine("Draw");
            } 
            #endregion

            Console.ReadLine();
        }

        // Board method which creats board  
        private static void Board2(bool isFirstTime)
        {
            int tempRow = 0;
            int tempColl = 0;

            Console.Write("\n");
            DrowDefaultRow();
            Console.Write("\n");

            for (int i = 0; i < (matrixNum * matrixNum); i++)
            {
                int tj = (i + 1);
                if (isFirstTime)
                {
                    arr2[i + 1] = tj.ToString();
                    DrowNumRow((i + 1).ToString(), tempColl++);
                }
                else
                {
                    DrowNumRow(arr2[i + 1], tempColl++);
                }
                if (tempColl.Equals(matrixNum))
                {
                    Console.Write("\n");
                    DrowLineRow();
                    Console.Write("\n");
                    tempColl = 0;
                    tempRow++;
                }
            }
            DrowDefaultRow();
            Console.Write("\n");
        }
        
        #region Draw Matrix

        private static void DrowDefaultRow()
        {
            int temp = matrixNum - 1;
            for (int i = 0; i < temp; i++)
            { Console.Write("    |"); }
            Console.Write("    ");
        }
        private static void DrowLineRow()
        {
            int temp = matrixNum - 1;
            for (int i = 0; i < temp; i++)
            { Console.Write("____|"); }
            Console.Write("____ ");
        }
        private static void DrowNumRow(string arrParam, int colNum)
        {
            if (colNum < (matrixNum - 1))
            {
                if (arrParam.Length.Equals(1))
                    Console.Write("  {0} |", arrParam);
                else
                    Console.Write(" {0} |", arrParam);
            }
            else
            {
                if (arrParam.Length.Equals(1))
                    Console.Write("  {0} ", arrParam);
                else
                    Console.Write(" {0} ", arrParam);
            }
        }

        #endregion

        //Checking that any player has won or not  
        private static int CheckWin2()
        {
            int result = 0;
            int counterOf_X_ROW = 0;
            int counterOf_O_ROW = 0;
            int counterOf_X_COLL = 0;
            int counterOf_O_COLL = 0;
            int counterOf_X_Diagonal = 0;
            int counterOf_O_Diagonal = 0;
            int counterRow = matrixNum;
            int counterCol = matrixNum;
            int counterr_Diagonal = 0;
            int tempIndex_Diagonal = 0;

            #region Rows
            for (int ii = 1; ii < (arr2.Length - 1); ii++)
            {
                if ((counterOf_X_ROW < matrixNum) && (counterOf_O_ROW < matrixNum))
                {
                    CheckRowOrColl(ref counterOf_X_ROW, ref counterOf_O_ROW, arr2[ii]);
                }
                else if ((counterOf_X_ROW == matrixNum) || (counterOf_O_ROW == matrixNum))
                { result = 1; bingo = true; }
                else result = (0);
            }
            #endregion

            #region COLUMNS
            if(!bingo)
            for (int ii = 1; ii < (arr2.Length - 1); ii++)
            {
                    if ((counterOf_X_COLL < matrixNum) && (counterOf_O_COLL < matrixNum))
                    {
                        CheckRowOrColl(ref counterOf_X_COLL, ref counterOf_O_COLL, arr2[ii]);
                    }
                    else if ((counterOf_X_COLL == matrixNum) || (counterOf_O_COLL == matrixNum))
                    { result = 1; bingo = true;}
                    else result = (0);
            }
            #endregion

            #region Left Diagonal
            if(!bingo)
            for (int ii = 1; ii < (arr2.Length - 1); ii++)
            {
                if (counterr_Diagonal<matrixNum)
                { 
                    if (ii == 1)
                    { tempIndex_Diagonal = ii; }
                    else if ((ii + matrixNum) <= (arr2.Length - 1)) { ii = tempIndex_Diagonal = (ii + matrixNum); }
                
                    if (arr2[tempIndex_Diagonal].Equals("X"))
                    { 
                        counterOf_X_Diagonal++;
                    }
                    else if (arr2[tempIndex_Diagonal].Equals("O"))
                    { counterOf_O_Diagonal++; }
                    counterr_Diagonal++;
                }
                    if ((counterOf_X_Diagonal == matrixNum) || (counterOf_O_Diagonal == matrixNum))
                    { result = 1;  bingo = true;}
                    else result = (0);
            }
            #endregion

            #region Rigth Diagonal
            counterr_Diagonal = 0;
            tempIndex_Diagonal = 0;
            counterOf_X_Diagonal = 0;
            counterOf_O_Diagonal = 0;
            if(!bingo)
            for (int ii = matrixNum; ii < (arr2.Length - 1); ii++)
            {
                if (counterr_Diagonal < matrixNum)
                {
                    if (ii == matrixNum)
                    { tempIndex_Diagonal = ii; }
                    else if ((ii + (matrixNum-2)) <= (arr2.Length - 1)) { ii = tempIndex_Diagonal = (ii + (matrixNum - 2)); }

                    if (arr2[tempIndex_Diagonal].Equals("X"))
                    { counterOf_X_Diagonal++; }
                    else if (arr2[tempIndex_Diagonal].Equals("O"))
                    { counterOf_O_Diagonal++; }
                    counterr_Diagonal++;
                }
                    if ((counterOf_X_Diagonal == matrixNum) || (counterOf_O_Diagonal == matrixNum))
                    { result = 1; bingo = true; }
                    else result = (0);
            }
            #endregion

            //if (!bingo)
            //    if (counterOf_X_ROW != matrixNum ||
            // counterOf_O_ROW != matrixNum ||
            // counterOf_X_COLL != matrixNum ||
            // counterOf_O_COLL != matrixNum ||
            // counterOf_X_Diagonal != matrixNum ||
            // counterOf_O_Diagonal != matrixNum)
            //{ result = 1; bingo = true; }
            //else
            //    result = 0;
            return result;
        }

        private static void CheckRowOrColl(ref int counterOf_X_ROW,ref int counterOf_O_ROW,string arr)
        {
            int counterRow = matrixNum;
            int counterCol = matrixNum;
            if (counterCol == 0)
            {
                counterCol = matrixNum;
                counterRow--; counterOf_X_ROW = counterOf_O_ROW = 0;
            }
            if (counterCol > 0)//colums
            {
                if (arr.Equals("X"))
                { counterOf_X_ROW++; }
                else if (arr.Equals("O"))
                { counterOf_O_ROW++; }
                counterCol--;
            }
        }
    }
}
