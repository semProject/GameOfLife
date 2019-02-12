using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    /// <summary>
    ///  Klasa Cell
    /// </summary>
    public class Cell
    {
        #region Pola i wlasciwosci 
        public bool isAlive ;
        public int cycle = 0;
        //public string signTrue = "X";
        //public string signFalse = "O";
        #endregion

        #region Konstruktor
        /// <summary>
        ///  Konstruktor klasy Cell
        /// </summary>
        public Cell(bool state)
        {
            isAlive = state;            
        }
        #endregion
    }
}
