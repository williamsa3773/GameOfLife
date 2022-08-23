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
        public static int width = Properties.Settings.Default.Width;
        public static int length = Properties.Settings.Default.Length;
        public static bool[,] universe = new bool[length, width];
        Universe verse;
        Cell cell;
        Random rand = new Random();
        //to toggle games run state on/off
        bool IsPaused = false;

        //below are the settings bools
        //to toggle grid on/off based on user settings
        bool IsGridOn = Properties.Settings.Default.Grid;
        //To toggle shown neighbors on/off based on user settings
        bool IsShowOn = Properties.Settings.Default.Show;
        //To toggle hud on/off based on user settings
        bool hud = Properties.Settings.Default.HUD;
        //To toggle boundary on/off
        bool boundary = Properties.Settings.Default.Boundary;
        //To set speed based on user settings
        int speed = Properties.Settings.Default.Speed;
        //To set cell alive status based on user settings
        int initial = Properties.Settings.Default.InitialAlive;

        // Drawing colors
        Color gridColor;
        Color cellColor;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;
        int alive = 0;

        public Form1()
        {
            InitializeComponent();
            graphicsPanel1.BackColor = Properties.Settings.Default.BackGroundColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            CreateGame();
            // Setup the timer
            timer.Interval = speed; // milliseconds
            timer.Tick += Timer_Tick;
            IsPaused = false;
            timer.Enabled = false; // start timer running, game begins paused            
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            CheckNeighbor();
            Game();
            // Increment generation count
            generations++;
            alive = Universe.Cell.Count(i => i.isAlive);
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelAlive.Text = "Generation = " + alive.ToString();
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
            float cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

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
                    cellRect.X = x * (int)cellWidth;
                    cellRect.Y = y * (int)cellHeight;
                    cellRect.Width = (int)cellWidth;
                    cellRect.Height = (int)cellHeight;

                    
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
                //editing state so that it matches what it going on
                if(universe[x, y] == true)
                {
                    cell.X = x;
                    cell.Y = y;
                    cell.isAlive = true;
                }
                else if(universe[x, y] == false)
                {
                    cell.X = x;
                    cell.Y = y;
                    cell.isAlive = false;
                }

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        //Instance the main variables use and randomize the board
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
                    //default is 15% but user has complete control over this feature
                    if (life < initial)
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

        //check the amount of neighbors each cell has
        private void CheckNeighbor()
        {
            foreach(Cell cell in Universe.Cell)
            {
                //initial coordinates created by each cell
                int xcord = cell.X;
                int ycord = cell.Y;

                //since im only check the surrounding cells the difference from the original cell is 1
                for (int xOff = -1; xOff <= 1; xOff++)
                {
                    for (int yOff = -1; yOff <= 1; yOff++)
                    {
                        int xCheck = xcord + xOff;
                        int yCheck = ycord + yOff;
                        //if both offsets equal  0
                        if (xOff == 0 && yOff == 0)
                        {
                            continue;
                        }
                        //if xychecks are less the 0 then they do not exist on the board
                        else if (xCheck < 0 || yCheck < 0)
                        {
                            continue;
                        }
                        //if xychecks are greater then the length of the board they dont exist
                        else if (xCheck > universe.GetLength(0) - 1 || yCheck > universe.GetLength(1) - 1)
                        {
                            continue;
                        }
                        //if universe is true then the cell is alive, increment numbers if true
                        if (universe[xCheck, yCheck] == true)
                        {
                            cell.Neighbors++;
                        }
                    }
                }
            }
            
        }

        //the rules for the game
        private void Game()
        {
            foreach (Cell cell in Universe.Cell)
            {
                //this part is for alive cells
                if(cell.isAlive == true)
                {
                    //if less then 2 or great than 3
                    if(cell.Neighbors <2 || cell.Neighbors >3)
                    {
                        cell.NextGenAlive = false;
                    } 
                    else
                    {
                        cell.NextGenAlive = true;
                    }
                }
                //for cells that are dead
                else if (cell.isAlive == false)
                {
                    //if neighbors is equal to 3 then next gen is alive
                    if (cell.Neighbors == 3)
                    {
                        cell.NextGenAlive = true;
                    }
                }
            }
            
            //function for deteremining the next board
            foreach(Cell cell in Universe.Cell)
            {
                //if next equal true then isalive gets overrided and universe = true
                if(cell.NextGenAlive == true)
                {
                    cell.isAlive = cell.NextGenAlive;
                    universe[cell.X, cell.Y] = true;
                    cell.Neighbors = 0;
                }
                //if false then cells stay dead or become dead
                else if(cell.NextGenAlive == false)
                {
                    cell.isAlive = cell.NextGenAlive;
                    universe[cell.X, cell.Y] = false;
                    cell.Neighbors = 0;
                }
            }
            //needed to update the board allowing to show progression.
            graphicsPanel1.Invalidate();
        }

        //pause/play
        private void UpdatePaused_Click(object sender, EventArgs e)
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

        //re randomize list and update the board
        private void ResetGame_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            generations = 0;
            Universe.Cell.Clear();
            CreateGame();
            graphicsPanel1.Invalidate();
        }

        //clears list and updates the board
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

        //stops the timer and advances generation by 1
        private void NextGen_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            NextGeneration();
        }

        private void UpdateBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;

            if(DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
                if(IsGridOn == true)
                {
                    gridColor = graphicsPanel1.BackColor;
                }
            }
        }

        private void UpdateGrid_Click(object sender, EventArgs e)
        {
            IsGridOn = false;
            ColorDialog dlg = new ColorDialog();
            dlg.Color = gridColor;

            if(DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }
            graphicsPanel1.Invalidate();
        }

        private void UpdateCellColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if(DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }
            graphicsPanel1.Invalidate();
        }


        private void UpdateGridColor_Click(object sender, EventArgs e)
        {
            IsGridOn = !IsGridOn;
            if(IsGridOn == true)
            {
                gridColor = graphicsPanel1.BackColor;
            }
            else
            {
                gridColor = cellColor;
            }
            graphicsPanel1.Invalidate();
        }

        private void UpdateSpeed_Click(object sender, EventArgs e)
        {
            SpeedDialog dlg = new SpeedDialog();

            dlg.Number = speed;
            if(DialogResult.OK == dlg.ShowDialog())
            {
                
                speed = dlg.Number;
                timer.Interval = speed;
                graphicsPanel1.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateNeighbor_Click(object sender, EventArgs e)
        {
            IsShowOn = !IsShowOn;
            graphicsPanel1.Invalidate();
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
