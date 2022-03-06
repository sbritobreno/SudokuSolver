using System;

namespace SudokuSolver
{
    class Program
    {
        private const int _gridSize = 9;
        static void Main(string[] args)
        {
            int[,] board =
            {
                {7,0,2,0,5,0,6,0,0 },
                {0,0,0,0,0,3,0,0,0 },
                {1,0,0,0,0,9,5,0,0 },
                {8,0,0,0,0,0,0,9,0 },
                {0,4,3,0,0,0,7,5,0 },
                {0,9,0,0,0,0,0,0,8 },
                {0,0,9,7,0,0,0,0,5 },
                {0,0,0,2,0,0,0,0,0 },
                {0,0,7,0,4,0,2,0,3 }
            };

            PrintBoard(board);

            if (SolveBoard(board))
            {
                Console.WriteLine("\nSolved successfully!\n");
            }
            else
            {
                Console.WriteLine("\nUnsolvable board :(\n");
            }

            PrintBoard(board);
        }

        private static void PrintBoard(int[,] board)
        {
            for (int row = 0; row < _gridSize; row++)
            {
                if(row % 3 == 0 && row != 0)
                    Console.WriteLine("-----------");

                for (int column = 0; column < _gridSize; column++)
                {
                    if(column % 3 == 0 && column != 0)
                        Console.Write("|");

                        Console.Write(board[row,column]);
                }
                Console.WriteLine();
            }
        }

        private static bool isNumberInRow(int[,] board, int number, int row)
        {
            for (int i = 0; i < _gridSize; i++)
            {
                if(board[row,i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isNumberInColumn(int[,] board, int number, int column)
        {
            for (int i = 0; i < _gridSize; i++)
            {
                if (board[i, column] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isNumberInBox(int[,] board, int number, int column, int row)
        {
            int localBoxRow = row - row % 3;
            int localBoxColumn = column - column % 3;

            for (int i = localBoxRow; i < localBoxRow + 3; i++)
            {
                for (int j = localBoxColumn; j < localBoxColumn + 3; j++)
                {
                    if (board[i, j] == number)
                        return true;
                }
            }
            return false;
        }

        private static bool isValidPlacement(int[,] board, int number, int column, int row)
        {
            return !isNumberInRow(board, number, row) &&
                !isNumberInColumn(board, number, column) &&
                !isNumberInBox(board, number, column, row);
        }

        private static bool SolveBoard(int[,] board)
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int column = 0; column < _gridSize; column++)
                {
                    if (board[row,column] == 0)
                    {
                        for (int numberToTry = 1; numberToTry <= _gridSize; numberToTry++)
                        {
                            if (isValidPlacement(board, numberToTry, column, row))
                            {
                                board[row, column] = numberToTry;

                                if (SolveBoard(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row, column] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
