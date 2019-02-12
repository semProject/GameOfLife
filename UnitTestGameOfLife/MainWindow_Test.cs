using System;
using System.Text.RegularExpressions;
using System.Windows;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestGameOfLife
{
    /// <summary>
    /// UnitTest klasy MainWindow
    /// </summary>
    [TestClass]
    public class MainWindow_Test
    {

        /// <summary>
        /// UnitTest metody ValidationTrue__MainWindow
        /// </summary>
        [TestMethod]
        public void TestValidationTrue__MainWindow()
        {
            MainWindow mw = new MainWindow();
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            mw.Start_Click(sender, e);

            //Default: 20,3,23,999999,100

            Regex liveReg = new Regex(@"[0-9]+");
            Match liveMatch = liveReg.Match((String)mw.liveRules);
            Regex deadReg = new Regex(@"[0-9]+");
            Match deadMatch = deadReg.Match((String)mw.deadRules);
            Regex gridReg = new Regex(@"[0-9]+");
            Match gridMatch = gridReg.Match(mw.gridSize.ToString());
            Regex maxReg = new Regex(@"[0-9]+");
            Match maxMatch = maxReg.Match(mw.maxCycleAmount.ToString());
            Regex delayReg = new Regex(@"[0-9]+");
            Match delayMatch = delayReg.Match(mw.delay.ToString());

            Assert.IsTrue(liveMatch.Success);
            Assert.IsTrue(deadMatch.Success);
            Assert.IsTrue(gridMatch.Success);
            Assert.IsTrue(maxMatch.Success);
            Assert.IsTrue(delayMatch.Success);

        }

        /// <summary>
        /// UnitTest metody ValidationFalse__MainWindow
        /// </summary>
        [TestMethod]
        public void ValidationFalse__MainWindow()
        {
            MainWindow mw = new MainWindow();
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            mw.Start_Click(sender, e);

            //Default: 20,3,23,999999,100

            Regex liveReg = new Regex(@"[0-9]+");
            Match liveMatch = liveReg.Match("Aaaa");
            Regex deadReg = new Regex(@"[0-9]+");
            Match deadMatch = deadReg.Match("asdasd");
            Regex gridReg = new Regex(@"[0-9]+");
            Match gridMatch = gridReg.Match("asdasd");
            Regex maxReg = new Regex(@"[0-9]+");
            Match maxMatch = maxReg.Match("asdasd");
            Regex delayReg = new Regex(@"[0-9]+");
            Match delayMatch = delayReg.Match("asdasd");

            Assert.IsFalse(liveMatch.Success);
            Assert.IsFalse(deadMatch.Success);
            Assert.IsFalse(gridMatch.Success);
            Assert.IsFalse(maxMatch.Success);
            Assert.IsFalse(delayMatch.Success);

        }
    }
}
