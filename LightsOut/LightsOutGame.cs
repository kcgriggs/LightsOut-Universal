using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LightsOut
{
    class LightsOutGame
    {
        private int gridSize;
        private bool[,] grid;           // Store the on/off state of the grid
        private Random rand;

        public const int MaxGridSize = 7;
        public const int MinGridSize = 3;

        // Returns the grid size
        public int GridSize
        {
            get
            {
                return gridSize;
            }
            set
            {
                if (value >= MinGridSize && value <= MaxGridSize)
                {
                    gridSize = value;
                    grid = new bool[gridSize, gridSize];
                    NewGame();
                }
            }
        }

        public string Grid
        {
            get
            {
                return GridToString();
            }
            set
            {
                StringToGrid(value);
            }
        }

        public LightsOutGame()
        {
            rand = new Random();
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("gridSize"))
            {
                GridSize = Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["gridSize"]);
            }
            else
            {
                GridSize = MinGridSize;
            }
        }

        // Returns the grid value at the given row and column
        public bool GetGridValue(int row, int col)
        {
            return grid[row, col];
        }

        // Randomizes the grid
        public void NewGame()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    grid[r, c] = rand.Next(2) == 1;
                }
            }
        }

        // Inverts the selected box and all surrounding boxes
        public void Move(int row, int col)
        {
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                throw new ArgumentException("Row or column is outside the legal range of 0 to "
                    + (gridSize - 1));
            }

            // Invert selected box and all surrounding boxes
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }
        }

        // Returns true if all cells are off
        public bool IsGameOver()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    if (grid[r, c])
                    {
                        return false;
                    }
                }
            }

            // All values must be false (off)
            return true;
        }

        string GridToString()
        {
            string tempGrid = "";
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j])
                    {
                        tempGrid += "T";
                    }
                    else
                    {
                        tempGrid += "F";
                    }
                }
            }
            return tempGrid.ToString();
        }

        void StringToGrid(string s)
        {
            int stringIndex = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (s[stringIndex] == 'T')
                    {
                        grid[i, j] = true;
                        stringIndex++;
                    }
                    else
                    {
                        grid[i, j] = false;
                        stringIndex++;
                    }
                }
            }
        }
    }
}


