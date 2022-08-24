using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLProject
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe;
        bool[,] scratch;
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
        //To toggle boundary on/off
        bool IsBoundOn = Properties.Settings.Default.Boundary;
        //To set speed based on user settings
        int speed = Properties.Settings.Default.Speed;
        //To set cell alive status based on user settings
        int initial = Properties.Settings.Default.InitialAlive;
        //To set how wide the grid will be based on user settings
        int width = Properties.Settings.Default.Width;
        //To set how long the grid will be based on user settings
        int length = Properties.Settings.Default.Length;

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
            universe = new bool[length, width];
            graphicsPanel1.BackColor = Properties.Settings.Default.BackGroundColor;
            cellColor = Properties.Settings.Default.CellColor;
            gridColor = Properties.Settings.Default.GridColor;
            CreateGame();
            scratch = universe;
            // Setup the timer
            timer.Interval = speed; // milliseconds
            timer.Tick += Timer_Tick;
            IsPaused = false;
            timer.Enabled = false; // start timer running, game begins paused            
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            if(IsBoundOn == false)
            {
                CheckNeighborFinite();
            } 
            else if(IsBoundOn == true)
            {
                CheckNeighborToroidal();
            }
            Game();
            // Increment generation count
            generations++;
            graphicsPanel1.Invalidate();
            alive = Universe.Cell.Count(i => i.isAlive);
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelAlive.Text = "Cells Alive = " + alive.ToString();
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
            Brush numBrush = new SolidBrush(Color.Black);

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
                cell.X = x;
                cell.Y = y;
                if (universe[cell.X, cell.Y] == false)
                {
                    cell.isAlive = true;
                    scratch[cell.X, cell.Y] = true;
                }
                else if(universe[cell.X, cell.Y] == true)
                {
                    cell.isAlive = false;
                    scratch[cell.X, cell.Y] = false;
                }
                bool[,] temp = universe;
                universe = scratch;
                temp = universe;
                scratch = temp;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        //Instance the main variables use and randomize the board
        public void CreateGame()
        {
            bool[,] newVerse = new bool[length, width];
            verse = new Universe(newVerse.GetLength(0), newVerse.GetLength(1));
            universe = newVerse;
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
        // only use if IsBoundOn = false
        private void CheckNeighborFinite()
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
                        if (xCheck < 0)
                        {
                            continue;
                        }
                        if(yCheck < 0)
                        {
                            continue;
                        }
                        //if xychecks are greater then the length of the board they dont exist
                        if (xCheck >= universe.GetLength(0) - 1)
                        {
                            continue;
                        }
                        if(yCheck >= universe.GetLength(1) - 1)
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

        private void CheckNeighborToroidal()
        {
            foreach (Cell cell in Universe.Cell)
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
                        if(xCheck < 0)
                        {
                            xCheck = universe.GetLength(0) - 1;
                        }
                        if(yCheck < 0)
                        {
                            yCheck = universe.GetLength(1) - 1;
                        }
                        //if xychecks are greater then the length of the board they dont exist
                        if(xCheck > universe.GetLength(0) -1)
                        {
                            xCheck = 0;
                        }
                        if( yCheck > universe.GetLength(1) - 1)
                        {
                            yCheck = 0;
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
                if(cell.isAlive == false)
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
                bool[,] temp;
                //if next equal true then isalive gets overrided and universe = true
                if(cell.NextGenAlive == true)
                {
                    cell.isAlive = cell.NextGenAlive;
                    scratch[cell.X, cell.Y] = true;
                    cell.Neighbors = 0;
                }
                //if false then cells stay dead or become dead
                else if(cell.NextGenAlive == false)
                {
                    cell.isAlive = cell.NextGenAlive;
                    scratch[cell.X, cell.Y] = false;
                    cell.Neighbors = 0;
                }
                universe = scratch;
                temp = universe;
                scratch = temp;
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
                        universe[x, y] = true;
                    }
                    else
                    {
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

        private void UpdateGridColor_Click(object sender, EventArgs e)
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


        private void UpdateGrid_Click(object sender, EventArgs e)
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

        private void UpdateSize_Click(object sender, EventArgs e)
        {
            SizeDialog dlg = new SizeDialog();

            dlg.X = length;
            dlg.Y = width;
            if(DialogResult.OK == dlg.ShowDialog())
            {
                length = dlg.X;
                width = dlg.Y;
                graphicsPanel1.Invalidate();
            }
            CreateGame();
        }
        private void UpdateBoundary_Click(object sender, EventArgs e)
        {
            IsBoundOn = !IsBoundOn;
        }

        private void SaveGame()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            dlg.DefaultExt = "cells";
            string currentRow = String.Empty;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter sw = new StreamWriter(dlg.FileName);
                sw.WriteLine("!--Start Universe--");
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        currentRow += (universe[x, y] == true) ? "O" : ".";
                    }
                    sw.WriteLine(currentRow);
                    currentRow = string.Empty;
                }
                sw.WriteLine("!--End Universe");
                sw.Flush();
                sw.Close();
            }
        }

        private void LoadGame()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            string line = string.Empty ;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader sr = new StreamReader(dlg.FileName);
                int maxWidth = 0;
                int maxHeight = 0;

                while(!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    if (row != null)
                    {
                        line = row.Substring(0, 1);
                        if(line.Substring(0,1) == "!")
                        {
                            continue;
                        }
                        else if(line == "O" || line == "." )
                        {
                            maxWidth = row.Length;
                            maxHeight++;
                        }
                    }
                    line = String.Empty;
                }

                bool[,] newVerse = new bool[maxHeight, maxWidth];
                verse = new Universe(newVerse.GetLength(0), newVerse.GetLength(1));
                universe = newVerse;
                Universe.Cell.Clear();

                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        cell = new Cell(x, y);
                        //default is 15% but user has complete control over this feature
                        
                        Universe.Cell.Add(cell);
                    }
                }
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                int yPos = 0;
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    if (row != null)
                    {
                        line = row.Substring(0, 1);
                        if (line == "!")
                        {
                            continue;
                        }
                        else
                        {
                            for (int xPos = 0; xPos < row.Length; xPos++)
                            {
                                if (row[xPos] == 'O')
                                {
                                    scratch[xPos, yPos] = true;
                                    cell.X = xPos;
                                    cell.Y = yPos;
                                    cell.isAlive = true;

                                }
                                else if (row[xPos] == '.')
                                {
                                    scratch[xPos, yPos] = false;
                                    cell.X = xPos;
                                    cell.Y = yPos;
                                    cell.isAlive = false; ;
                                }
                            }
                        }
                    }
                    yPos++;
                }
                universe = scratch;
                graphicsPanel1.Invalidate();
                sr.Close();
            }
        }

        private void SaveGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void SaveAsGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            ClearBoard_Click(sender, e);
            LoadGame();
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
