using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Map
    {
        public Map(int x, int y, int r)
        {
            SX = x;
            SY = y;
            Range = r;
            int a = (int)Math.Pow(2, Range);
            directions = new Direction[a];
            for (int  i = 0;i < a; i++ )
            { directions[i] = new Direction(); }
            intsMap = new int[x, y];
            bools = new bool[x, y];
            for (int X = 0; X < SX; X++)
            {
                for (int Y = 0; Y < SY; Y++)
                {
                    bools[X, Y] = false;
                }
            }
        }
        private int[,] intsMap;
        private int 
            StartX, StartY, 
            EndX, EndY,
            SX,SY;
        /// <summary>
        /// Установка точек
        /// Setting points
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public Map Set(Coordinates Start, Coordinates End)
        {
            StartX = Start.X;
            StartY = Start.Y;
            EndX = End.X;
            EndY = End.Y;
            return this;
        }
        /// <summary>
        /// Заполнение карты
        /// Filling out the card
        /// </summary>
        /// <returns></returns>
        public Map Set()
        {
            //С рандомными числами
            //With random numbers
            Random random = new Random(45);
            for (int X = 0;X < SX; X++)
            {
                for (int Y = 0;Y < SY; Y++)
                {
                    intsMap[X, Y] = random.Next(1, 40);
                }
            }
            return this;
        }
        /// <summary>
        /// Заполнение "DataGridView" и поиск
        /// Filling in the "DataGridView" and search
        /// </summary>
        /// <param name="DGV"></param>
        /// <returns></returns>
        public int Set(DataGridView DGV)
        {
            int con  = Search(StartX, StartY, 0);
            DGV.RowCount = SX;
            DGV.ColumnCount = SY;
            for (int X = 0; X < SX; X++)
            {
                for (int Y = 0; Y < SY; Y++)
                {
                    DGV.Rows[X].Cells[Y].Value = intsMap[X, Y].ToString();
                    if (bools[X, Y])
                    {
                        DGV.Rows[X].Cells[Y].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        DGV.Rows[X].Cells[Y].Style.BackColor = Color.White;
                    }
                }
            }
            return con;
        }
        private bool[,] bools;
        public int Range = 2 , Min = int.MaxValue;
        private bool GR = false;
        /// <summary>
        /// Поиск оптимального пути
        /// Finding the optimal path
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Con"></param>
        /// <returns></returns>
        public int Search(int x, int y, int Con)
        {
            bools[y, x] = true;
            if (x == EndX)
            {
                if (y == EndY) { return 0; }
                return Con + Search(x, (y + 1), intsMap[y, x]);
            }
            else if (y == EndY)
            {
                if (x == EndX) { return 0; }
                return Con + Search((x + 1), y, intsMap[y, x]);
            }
            else
            {
                Ghost(x,y);
                //Оценка путей
                //Evaluating paths
                foreach (Direction direction in directions)
                {
                    
                    if (direction.Value > Min)
                    { 
                        direction.Value = int.MaxValue;
                        continue; 
                    }
                    Min = direction.Value;
                    GR = direction.GostRight;
                    direction.Value = int.MaxValue;
                }
                Min = int.MaxValue;
                if (GR)
                {
                    return Con + Search(x, (y + 1), intsMap[y, x]);
                }
                else 
                {
                    return Con + Search((x + 1), y, intsMap[y, x]);
                }
            }
        }
        private Direction[] directions;
        private int Ghosts = 0;
        /// <summary>
        /// Просмотр выгодности пути
        /// Viewing the profitability of the path
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Ghost(int x, int y)
        {
            Ghosts = 0;
            Ghost((x + 1), y, 1, intsMap[y, (x + 1)], false);
            Ghost(x, (y + 1), 1, intsMap[(y + 1), x], true);
        }
        /// <summary>
        /// Просмотр выгодности пути
        /// Viewing the profitability of the path
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Con"></param>
        /// <param name="Poin"></param>
        /// <param name="GostLeft"></param>
        public void Ghost(int x, int y, int Con, int Poin, bool GostLeft)
        {
            if (Con >= Range) 
            {
                directions[Ghosts].Value = Poin;
                directions[Ghosts].GostRight = GostLeft;
                Ghosts++;
                return;
            }
            if (x < (SX -1))
            { 
            Ghost((x+1), y, (Con+1), (Poin + intsMap[y, (x + 1)]), GostLeft);
            }
            if (y < (SY-1))
            {
            Ghost(x, (y+1), (Con+1), (Poin + intsMap[(y + 1), x]), GostLeft);
            }
        }
    }
}
