using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    public class Sudoku
    {
        public int[,] sudokuGrid = new int[9, 9];
        public int N = 9;
        public int SRN = 3;
        Random random = new Random();
        public void GenerateSudoku()
        {
            for (int i = 0; i < N; i += SRN)
            {
                int num;
                for (int row = 0; row < SRN; row++)
                {
                    for (int col = 0; col < SRN; col++)
                    {
                        do
                        {
                            num = random.Next(1, N + 1);
                        }
                        while (!IsSafeInBox(i, i, num));

                        sudokuGrid[i + row, i + col] = num;
                    }
                }
            }

            FillRemaining(0, SRN);

            RemoveCells(55); 

            bool IsSafeInBox(int rowStart, int colStart, int num)
            {
                for (int r = 0; r < SRN; r++)
                {
                    for (int c = 0; c < SRN; c++)
                    {
                        if (sudokuGrid[rowStart + r, colStart + c] == num)
                            return false;
                    }
                }
                return true;
            }

            bool FillRemaining(int i, int j)
            {
                if (i == N - 1 && j == N)
                    return true;

                if (j == N)
                {
                    i++;
                    j = 0;
                }

                if (sudokuGrid[i, j] != 0)
                    return FillRemaining(i, j + 1);

                for (int num = 1; num <= N; num++)
                {
                    if (IsSafe(i, j, num))
                    {
                        sudokuGrid[i, j] = num;
                        if (FillRemaining(i, j + 1))
                            return true;
                        sudokuGrid[i, j] = 0;
                    }
                }
                return false;
            }

            bool IsSafe(int row, int col, int num)
            {
                for (int x = 0; x < N; x++)
                {
                    if (sudokuGrid[row, x] == num)
                        return false;
                }

                for (int x = 0; x < N; x++)
                {
                    if (sudokuGrid[x, col] == num)
                        return false;
                }

                int startRow = row - row % SRN;
                int startCol = col - col % SRN;
                return IsSafeInBox(startRow, startCol, num);
            }

            void RemoveCells(int numberOfCellsToRemove)
            {
                int cellsRemoved = 0;
                while (cellsRemoved < numberOfCellsToRemove)
                {
                    int i = random.Next(0, N);
                    int j = random.Next(0, N);
                    if (sudokuGrid[i, j] != 0)
                    {
                        sudokuGrid[i, j] = 0; // Đặt ô thành 0 (xóa ô)
                        cellsRemoved++;
                    }
                }
            }
        }

    }
}
