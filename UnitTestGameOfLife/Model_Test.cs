using System;
using System.Reflection;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;


namespace UnitTestGameOfLife
{
    /// <summary>
    /// Zestaw UnitTestow klasy Model  
    /// </summary>
    [TestClass]
    public class ModelTest
    {
        /// <summary>
        /// UnitTest metody nextGeneration Model
        /// </summary>
        [TestMethod]
        public void nextGeneration__Test()
        {
            Canvas cvs = new Canvas();
            // gridSize , deadToLive , liveStillLive , Canvas
            GameOfLife.Model mc = new GameOfLife.Model(10, "3", "23", cvs);

            mc.cells[0, 1].isAlive = true;
            mc.cells[1, 1].isAlive = true;
            mc.cells[2, 1].isAlive = true;

            mc.nextGeneration();

            Assert.IsTrue(mc.cells[1, 0].isAlive);
            Assert.IsTrue(mc.cells[1, 1].isAlive);
            Assert.IsTrue(mc.cells[1, 2].isAlive);

            Assert.IsTrue(mc.cells[0, 1].isAlive == false);
            Assert.IsTrue(mc.cells[2, 2].isAlive == false);

        }
        /// <summary>
        /// UnitTest metody initCell Model
        /// </summary>
        [TestMethod]
        public void initCell__Test()
        {
            Canvas cvs = new Canvas();
            // gridSize , deadToLive , liveStillLive , Canvas
            GameOfLife.Model mc = new GameOfLife.Model(10, "3", "23", cvs);

            bool bfr = true;

            foreach (var item in mc.cells)
            {
                bfr = item.isAlive;
            }

            Assert.IsTrue(bfr == false);

        }
        /// <summary>
        /// UnitTest metody clearGrid Model
        /// </summary>
        [TestMethod]
        public void clearGrid__Test()
        {
            Canvas cvs = new Canvas();
            // gridSize , deadToLive , liveStillLive , Canvas
            GameOfLife.Model mc = new GameOfLife.Model(10, "3", "23", cvs);

            bool bfr = true;
            mc.cells[0, 1].isAlive = true;
            mc.cells[1, 1].isAlive = true;
            mc.cells[2, 1].isAlive = true;

            mc.clearGrid(mc.cells);

            foreach (var item in mc.cells)
            {
                bfr = item.isAlive;
            }

            Assert.IsTrue(bfr == false);
        }
    }
}
