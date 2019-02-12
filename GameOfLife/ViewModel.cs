using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameOfLife
{
    /// <summary>
    ///  Klasa ViweModel
    /// </summary>
    public class ViewModel
    {

        #region Pola i wlasciwosci     
        public Model gameOfLifeModel;    
        public Canvas board;
        private double enterBoxX;       
        private double enterBoxY;
        #endregion

        #region Konstructor
        /// <summary>
        ///  Konstruktor klasy ViewModel    
        /// </summary>
        /// <param name="gridSize">Rozmiar siatki planszy</param>
        /// <param name="liveRules">liczba komórek w sąsiedztwie, dla których żywe komórki przeżywają</param>
        /// <param name="deadRules">liczba komórek w sąsiedztwie, dla których martwe komórki ożywają</param>
        /// <param name="board">Obiekt canvas</param>
        public ViewModel(int gridSize,string liveRules,string deadRules, Canvas board)
        {
            this.board = board;
            gameOfLifeModel = new Model(gridSize, liveRules, deadRules, board);
        }
        #endregion

        /// <summary>
        ///  Metoda asynchroniczna klasy ViewModel odpowiedzialna za nastepna generacje komorek    
        /// </summary>    
        public async void Next()
        {
            await Task.Run(() => gameOfLifeModel.nextGeneration());      
            gameOfLifeModel.PaintGrid();        
        }

        /// <summary>
        ///  Metoda klasy ViewModel obslugujaca kliknienie myszka   
        /// </summary>  
        public void mouseClick()
        {
            IInputElement clickedElement = Mouse.DirectlyOver;
            if (clickedElement is Rectangle)
            {          
                Rectangle rect = clickedElement as Rectangle;

                double x = Canvas.GetLeft(rect);
                double y = Canvas.GetTop(rect);

                int X = (int)x / (int)rect.ActualWidth;
                int Y = (int)y / (int)rect.ActualHeight;

                if (gameOfLifeModel.cells[Y, X].cycle != 0)
                {
                    return;
                }
                gameOfLifeModel.cells[Y, X].isAlive = !gameOfLifeModel.cells[Y, X].isAlive;
                gameOfLifeModel.cells[Y, X].cycle = 0;
                gameOfLifeModel.PaintGrid();
            }
        }

        /// <summary>
        ///  Metoda klasy ViewModel obslugujaca przytrzymanie lewego klawisza myszy  
        /// </summary> 
        public void mouseMoveLeft()
        {

            IInputElement clickedElement = Mouse.DirectlyOver;
            if (clickedElement is Rectangle)
            {              
                Rectangle rect = clickedElement as Rectangle;
                double x = Canvas.GetLeft(rect);
                double y = Canvas.GetTop(rect);

                if ((enterBoxX != x) || (enterBoxY != y))
                {
                    int X = (int)x / (int)rect.ActualWidth;
                    int Y = (int)y / (int)rect.ActualHeight;
                    try
                    {
                        if (gameOfLifeModel.cells[Y, X].isAlive == false)
                        {
                            gameOfLifeModel.cells[Y, X].isAlive = true;
                            gameOfLifeModel.cells[Y, X].cycle = 0;
                            rect.Fill = new BrushConverter().ConvertFromString("#FFD600") as Brush;
                        }
                    }
                    catch
                    {
                    }
                    enterBoxX = x;
                    enterBoxY = y;
                }
            }
        }

        /// <summary>
        ///  Metoda klasy ViewModel obslugujaca przytrzymanie prawego klawisza myszy  
        /// </summary> 
        public void mouseMoveRight()
        {
            IInputElement clickedElement = Mouse.DirectlyOver;
            if (clickedElement is Rectangle)
            {                
                Rectangle rect = clickedElement as Rectangle;
                double x = Canvas.GetLeft(rect);
                double y = Canvas.GetTop(rect);

                try
                {       
                    int X = (int)x / (int)rect.ActualWidth;
                    int Y = (int)y / (int)rect.ActualHeight;

                    if (gameOfLifeModel.cells[Y, X].isAlive == true)
                    {
                        gameOfLifeModel.cells[Y, X].isAlive = false;
                        gameOfLifeModel.cells[Y, X].cycle = 0;
                        rect.Fill = gameOfLifeModel.fiveColorLGB;
                    }
                 
                }
                catch
                {
                }
                }
            }

        /// <summary>
        ///  Metoda klasy ViewModel resetujaca canvas  
        /// </summary> 
        public void resetGrid()
        {
            board.Children.Clear();
            gameOfLifeModel.clearGrid(gameOfLifeModel.cells);
            gameOfLifeModel.clearGrid(gameOfLifeModel.cellsNextGen);
           // gameOfLifeModel.PaintGrid();
        }


       

    }
}
