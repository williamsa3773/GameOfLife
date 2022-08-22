using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLProject
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[100, 100];
        Universe verse;
        Cell cell;
        bool IsPaused = false;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();
            CreateGame();
            // Setup the timer
            timer.Interval = 1; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            CheckNeighbor();
            Game();
            // Increment generation count
            generations++;
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    
                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];
                cell.X = x;
                cell.Y = y;
                cell.isAlive = true;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        Random rand = new Random();

        public void CreateGame()
        {
            verse = new Universe(universe.GetLength(0), universe.GetLength(1));

            Universe.Cell.Clear();

            for(int y = 0; y < universe.GetLength(1); y++)
            {
                for(int x = 0; x < universe.GetLength(0); x++)
                {
                    cell = new Cell(x,y);
                    int life = rand.Next(100);
                    if (life < 20)
                    {
                        cell.isAlive = true; 
                        universe[x, y] = true;
                    }
                    else
                    {
                        cell.isAlive = false;
                        universe[x, y] = false;
                    }
                    Universe.Cell.Add(cell);
                }
            }
        }

        private void CheckNeighbor()
        {
            foreach(Cell cell in Universe.Cell)
            {
                int xcord = cell.X;
                int ycord = cell.Y;

                for (int xOff = -1; xOff <= 1; xOff++)
                {
                    for (int yOff = -1; yOff <= 1; yOff++)
                    {
                        int xCheck = xcord + xOff;
                        int yCheck = ycord + yOff;
                        if (xOff == 0 && yOff == 0)
                        {
                            continue;
                        }
                        else if (xCheck < 0 || yCheck < 0)
                        {
                            continue;
                        }
                        else if (xCheck > universe.GetLength(1) - 1 || yCheck > universe.GetLength(0) - 1)
                        {
                            continue;
                        }
                        if (universe[xCheck, yCheck] == true)
                        {
                            cell.Neighbors++;
                        }
                    }
                }
            }
            
        }

        private void Game()
        {
            foreach (Cell cell in Universe.Cell)
            {
                if(cell.isAlive == true)
                {
                    if(cell.Neighbors <2 || cell.Neighbors >3)
                    {
                        cell.NextGenAlive = false;
                    } else
                    {
                        cell.NextGenAlive = true;
                    }
                } else if (cell.isAlive == false)
                {
                    if (cell.Neighbors == 3)
                    {
                        cell.NextGenAlive = true;
                    }
                }
            }
            
            foreach(Cell cell in Universe.Cell)
            {
                if(cell.NextGenAlive == true)
                {
                    cell.isAlive = cell.NextGenAlive;
                    universe[cell.X, cell.Y] = true;
                    cell.Neighbors = 0;
                }
                else if(cell.NextGenAlive == false)
                {
                    cell.isAlive = cell.NextGenAlive;
                    universe[cell.X, cell.Y] = false;
                    cell.Neighbors = 0;
                }
            }
            graphicsPanel1.Invalidate();
        }

        private void IsPaused_Click(object sender, EventArgs e)
        {
            if(IsPaused == false)
            {
                timer.Enabled = true;
                NextGeneration();
                IsPaused = true;
            } else if(IsPaused == true)
            {
                timer.Enabled = false;
                IsPaused = false;
            }
        }

        private void ResetGame_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            generations = 0;
            Universe.Cell.Clear();
            CreateGame();
            graphicsPanel1.Invalidate();
        }

        private void ClearBoard_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            generations = 0;
            Universe.Cell.Clear();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    cell = new Cell(x, y);
                    int life = rand.Next(100);
                    if (life < 0)
                    {
                        cell.isAlive = true;
                        universe[x, y] = true;
                    }
                    else
                    {
                        cell.isAlive = false;
                        universe[x, y] = false;
                    }
                    Universe.Cell.Add(cell);
                }
            }
            graphicsPanel1.Invalidate();
        }

        private void NextGen_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }
    }

    public class Universe
    {
        public static List<Cell> Cell = new List<Cell>();
        private int Rows;
        private int Cols;

        public Universe(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }

        public int rows
        {
            get { return Rows; }
            set { Rows = value; }
        }

        public int cols
        {
            get { return Cols; }
            set { Cols = value; }
        }
    }

    public class Cell
    {
        private bool IsAlive;
        private int XPos;
        private int YPos;
        private int Alive;
        private bool NextGen;

        public Cell(int x, int y)
        {
            XPos = x;
            YPos = y;
        }
        public bool isAlive
        {
            get { return IsAlive; }
            set { IsAlive = value; }
        }

        public int Neighbors
        {
            get { return Alive; }
            set { Alive = value; }
        }

        public int X
        {
            get { return XPos; }
            set { XPos = value; }
        }

        public int Y
        {
            get { return YPos; }
            set { YPos = value; }
        }

        public bool NextGenAlive
        {
            get { return NextGen; }
            set { NextGen = value; }
        }

    }
}
