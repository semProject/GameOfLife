using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    ///  Klasa Model
    /// </summary>
     public class Model
    {
        #region Pola i wlasciwosci
        public Cell[,] cells { get; set; }
        public Cell[,] cellsNextGen { get; set; }
        public string liveRule;
        public string deadRule;
        public LinearGradientBrush fiveColorLGB;
        private Canvas board;
        #endregion

        #region Konstructor
        /// <summary>
        ///  Konstruktor klasy Model    
        /// </summary>
        /// <param name="gridSize">Rozmiar siatki planszy</param>
        /// <param name="live">liczba komórek w sąsiedztwie, dla których żywe komórki przeżywają</param>
        /// <param name="dead">liczba komórek w sąsiedztwie, dla których martwe komórki ożywają</param>
        /// <param name="board">Obiekt canvas</param>
        public Model(int gridSize, string live, string dead, Canvas board)
        {
            this.board = board;
            cells = new Cell[gridSize, gridSize]; // Y, X
            cellsNextGen = new Cell[gridSize, gridSize]; // Y, X
            initCells(cells);
            initCells(cellsNextGen);
            liveRule = live;
            deadRule = dead;
            
            // add bird
            //cells[3, 0].isAlive = true;
            //cells[3, 1].isAlive = true;
            //cells[3, 2].isAlive = true;
            //cells[2, 2].isAlive = true;
            //cells[1, 1].isAlive = true;
        }
        
        #endregion

        #region Metody
        /// <summary>
        ///  Metoda klasy Model tworzaca komorki w tablicy    
        /// </summary>
        /// <param name="tab">Pusta tablica</param>
        public void initCells(Cell[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j] = new Cell(false);
                }
            }
        }

        /// <summary>
        ///  Metoda klasy Model ustawia wszystkie komorki na martwe    
        /// </summary>
        /// <param name="tab">Tablica</param>
        public void clearGrid(Cell[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j].isAlive = false;
                }
            }
        }

        /// <summary>
        ///  Metoda klasy Model generujaca nastepne pokolenie komorek   
        /// </summary>      
        public void nextGeneration()
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    // ilosc zywych komorek
                    var livingAmount = 0;

                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (
                                (i + k >= 0) && (j + l >= 0) &&
                                (i + k < cells.GetLength(0)) && (j + l < cells.GetLength(1)))
                            {
                                if (k != 0 || l != 0)
                                {
                                    if (cells[i, j].isAlive == false && cells[i + k, j + l].isAlive == true)
                                    {
                                        livingAmount++;
                                    }
                                    else if (cells[i, j].isAlive == true && cells[i + k, j + l].isAlive == true)
                                    {
                                        livingAmount++;
                                    }
                                }
                            }
                        }
                    }

                    if (cells[i, j].isAlive == false && liveRule.Contains(livingAmount.ToString()))
                    {
                        cellsNextGen[i, j].isAlive = true;
                        cellsNextGen[i, j].cycle = 1;
                    }
                    else if (cells[i, j].isAlive == true && deadRule.Contains(livingAmount.ToString()) == true)
                    {
                        cellsNextGen[i, j].isAlive = true;
                        cellsNextGen[i, j].cycle++;
                    }
                    else if (cells[i, j].isAlive == true && deadRule.Contains(livingAmount.ToString()) == false)
                    {
                        cellsNextGen[i, j].isAlive = false;
                        cellsNextGen[i, j].cycle = 0;
                    }
                    else
                    {
                        cellsNextGen[i, j].isAlive = false;
                        cellsNextGen[i, j].cycle = 0;
                        livingAmount = 0;
                    }
                }
            }

            var subState = cells;
            cells = cellsNextGen;
            cellsNextGen = subState;
        }

        /// <summary>
        ///  Metoda klasy Model  odpowiedzialna za dodanie komorek do canvas  
        /// </summary>  
        public void PaintGrid()
        {
            board.Children.Clear();
            board.Background = Brushes.Black;

            double boxAmount = cells.GetLength(0);
            double h = board.ActualHeight;
            double w = board.ActualWidth;
            double boxPosX = 0;
            double boxPosY = 0;


            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = w / boxAmount;
                    rect.Height = h / boxAmount;
                    //rect.Stroke = new BrushConverter().ConvertFromString("#050505") as Brush;
                    rect.StrokeThickness = 0.1;
                    rect.Stroke = Brushes.Black;                

                    if (cells[i, j].isAlive == true)
                    {
                        if (cells[i, j].cycle == 0)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#FFD600") as Brush;
                        }
                        else if (cells[i, j].cycle == 1)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#AEEA00") as Brush;
                        }
                        else if (cells[i, j].cycle == 2)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#64DD17") as Brush;
                        }
                        else if (cells[i, j].cycle == 3)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#00C853") as Brush;
                        }
                        else if (cells[i, j].cycle == 4)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#00BFA5") as Brush;
                        }
                        else if (cells[i, j].cycle == 5)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#00B8D4") as Brush;
                        }
                        else if (cells[i, j].cycle == 6)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#0091EA") as Brush;
                        }
                        else if (cells[i, j].cycle == 7)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#2962FF") as Brush;
                        }
                        else if (cells[i, j].cycle == 8)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#304FFE") as Brush;
                        }
                        else if (cells[i, j].cycle >= 9)
                        {
                            rect.Fill = new BrushConverter().ConvertFromString("#6200EA") as Brush;
                        }


                    }
                    else if (cells[i, j].isAlive == false)
                    {

                        fiveColorLGB = new LinearGradientBrush();

                        fiveColorLGB.StartPoint = new Point(0, 0);

                        fiveColorLGB.EndPoint = new Point(1, 1);

                        GradientStop blueGS = new GradientStop();

                        blueGS.Color = (Color)ColorConverter.ConvertFromString("#37474F");

                        blueGS.Offset = 0.0;

                        fiveColorLGB.GradientStops.Add(blueGS);

                        GradientStop blueGS2 = new GradientStop();

                        blueGS2.Color = (Color)ColorConverter.ConvertFromString("#424242");

                        blueGS2.Offset = 1.0;

                        fiveColorLGB.GradientStops.Add(blueGS2);

                        rect.Fill = fiveColorLGB;
                    }

                    // Set Canvas position   
                    Canvas.SetLeft(rect, boxPosX);
                    Canvas.SetTop(rect, boxPosY);

                    // Add Rectangle to Canvas 
                    board.Children.Add(rect);
                    boxPosX += (w / boxAmount);
                }
                boxPosX = 0;
                boxPosY += (h / boxAmount);
            }
        }

        #endregion
    }
}
