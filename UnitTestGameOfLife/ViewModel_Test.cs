using System;
using System.Reflection;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;

namespace UnitTestGameOfLife
{
    /// <summary>
    /// Zestaw UnitTestow klasy ViewModel  
    /// </summary>
    [TestClass]
    public class ViewModel_Test
    {
        [TestMethod]
        /// <summary>
        /// UnitTest metody resetGrid_ViewModel
        /// </summary>
        public void resetGrid__Test()
        {
            Canvas cvs = new Canvas();
            // gridSize , deadToLive , liveStillLive , Canvas
            GameOfLife.ViewModel vm = new GameOfLife.ViewModel(10, "3", "23", cvs);

            vm.resetGrid();

            Assert.IsTrue(vm.board.Children.Count <= 0);

            bool bfr = true;     
                    
            foreach (var item in vm.gameOfLifeModel.cells)
            {
                bfr = item.isAlive;
            }
            Assert.IsTrue(bfr == false);

            foreach (var item in vm.gameOfLifeModel.cellsNextGen)
            {
                bfr = item.isAlive;
            }
            Assert.IsTrue(bfr == false);
        }
    }
}
