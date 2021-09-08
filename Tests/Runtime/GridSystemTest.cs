using NUnit.Framework;

namespace The25thStudio.GridSystem.Tests
{
    [TestFixture]
    public class GridSystemTest
    {
        [Test]
        public void TestSize()
        {
            const int width = 10;
            const int height = 15;
            const float cellSize = 22f;

            var grid = new GridSystem<string>(width, height, cellSize);

            Assert.AreEqual(width, grid.Width);
            Assert.AreEqual(height, grid.Height);
            Assert.AreEqual(cellSize, grid.CellSize);
        }

        [Test]
        public void TestCannotAddOutOfRange()
        {
            var grid = new GridSystem<string>(2, 2);

            var result = grid.SetValue(5, 5, "Out of Range");
            Assert.IsFalse(result);
        }

        [Test]
        public void TestCellIsEmpty()
        {
            var grid = new GridSystem<string>(10, 10);

            var isEmpty = grid.IsEmpty(1, 1);
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void TestCellIsNotEmpty()
        {
            var grid = new GridSystem<string>(10, 10);
            var expected = "Unit Test";
            grid.SetValue(1, 1, expected);

            Assert.IsFalse(grid.IsEmpty(1, 1));
            Assert.IsTrue(grid.IsEmpty(0, 0));
            Assert.IsTrue(grid.IsEmpty(0, 1));
            Assert.IsTrue(grid.IsEmpty(0, 2));
            Assert.IsTrue(grid.IsEmpty(1, 0));
            Assert.IsTrue(grid.IsEmpty(1, 2));
            Assert.IsTrue(grid.IsEmpty(2, 0));
            Assert.IsTrue(grid.IsEmpty(2, 1));
            Assert.IsTrue(grid.IsEmpty(2, 2));

            Assert.AreEqual(expected, grid.GetValue(1, 1));
        }

        [Test]
        public void TestCell2X1()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "2x1 Cell";
            grid.SetValue(1, 1, expected, 2);

            Assert.IsFalse(grid.IsEmpty(1, 1));
            Assert.IsFalse(grid.IsEmpty(2, 1));

            Assert.IsTrue(grid.IsEmpty(0, 0));
            Assert.IsTrue(grid.IsEmpty(0, 1));
            Assert.IsTrue(grid.IsEmpty(0, 2));
            Assert.IsTrue(grid.IsEmpty(0, 3));
            Assert.IsTrue(grid.IsEmpty(1, 0));
            Assert.IsTrue(grid.IsEmpty(1, 2));
            Assert.IsTrue(grid.IsEmpty(1, 3));
            Assert.IsTrue(grid.IsEmpty(2, 0));
            Assert.IsTrue(grid.IsEmpty(2, 2));
            Assert.IsTrue(grid.IsEmpty(2, 3));

            Assert.AreEqual(expected, grid.GetValue(1, 1));
            Assert.AreEqual(expected, grid.GetValue(2, 1));
        }

        [Test]
        public void TestCell1X2()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "1x2 Cell";
            grid.SetValue(1, 1, expected, 1, 2);

            Assert.IsFalse(grid.IsEmpty(1, 1));
            Assert.IsFalse(grid.IsEmpty(1, 2));

            Assert.IsTrue(grid.IsEmpty(0, 0));
            Assert.IsTrue(grid.IsEmpty(0, 1));
            Assert.IsTrue(grid.IsEmpty(0, 2));
            Assert.IsTrue(grid.IsEmpty(0, 3));
            Assert.IsTrue(grid.IsEmpty(1, 0));
            Assert.IsTrue(grid.IsEmpty(1, 3));
            Assert.IsTrue(grid.IsEmpty(2, 0));
            Assert.IsTrue(grid.IsEmpty(2, 1));
            Assert.IsTrue(grid.IsEmpty(2, 2));
            Assert.IsTrue(grid.IsEmpty(2, 3));

            Assert.AreEqual(expected, grid.GetValue(1, 1));
            Assert.AreEqual(expected, grid.GetValue(1, 2));
        }

        [Test]
        public void TestCell2X2()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "2x2 Cell";
            grid.SetValue(1, 1, expected, 2, 2);

            Assert.IsFalse(grid.IsEmpty(1, 1));
            Assert.IsFalse(grid.IsEmpty(1, 2));
            Assert.IsFalse(grid.IsEmpty(2, 1));
            Assert.IsFalse(grid.IsEmpty(2, 2));

            Assert.IsTrue(grid.IsEmpty(0, 0));
            Assert.IsTrue(grid.IsEmpty(0, 1));
            Assert.IsTrue(grid.IsEmpty(0, 2));
            Assert.IsTrue(grid.IsEmpty(0, 3));
            Assert.IsTrue(grid.IsEmpty(1, 0));
            Assert.IsTrue(grid.IsEmpty(1, 3));
            Assert.IsTrue(grid.IsEmpty(2, 0));
            Assert.IsTrue(grid.IsEmpty(2, 3));

            Assert.AreEqual(expected, grid.GetValue(1, 1));
            Assert.AreEqual(expected, grid.GetValue(1, 2));
            Assert.AreEqual(expected, grid.GetValue(2, 1));
            Assert.AreEqual(expected, grid.GetValue(2, 2));
        }

        [Test]
        public void TestCellCannotAdd2X1()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "2x1 Cell";
            var setValue1X1 = grid.SetValue(1, 1, expected, 2);
            const string otherValue = "Some other value";
            var setValue2X1 = grid.SetValue(2, 1, otherValue);

            Assert.IsTrue(setValue1X1);
            Assert.IsFalse(setValue2X1);

            Assert.AreEqual(expected, grid.GetValue(1, 1));
            Assert.AreEqual(expected, grid.GetValue(2, 1));
        }

        [Test]
        public void TestCellCannotAdd1X2()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "1x2 Cell";
            var setValue1X1 = grid.SetValue(1, 1, expected, 1, 2);
            const string otherValue = "Some other value";
            var setValue1X2 = grid.SetValue(1, 2, otherValue);

            Assert.IsTrue(setValue1X1);
            Assert.IsFalse(setValue1X2);

            Assert.AreEqual(expected, grid.GetValue(1, 1));
            Assert.AreEqual(expected, grid.GetValue(1, 2));
        }

        [Test]
        public void TestCellCannotAdd1X2InTheEnd()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "2x1 Cell";
            var setValue2X1 = grid.SetValue(9, 1, expected, 2);

            Assert.IsFalse(setValue2X1);
            Assert.IsTrue(grid.IsEmpty(9, 1));
        }

        [Test]
        public void TestRemoveCell1X1()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "1x1 Cell";
            var setValue1X1 = grid.SetValue(9, 1, expected);

            Assert.IsTrue(setValue1X1);

            var removedValue = grid.RemoveValue(9, 1);
            Assert.AreEqual(expected, removedValue);
            Assert.IsTrue(grid.IsEmpty(9, 1));
        }

        [Test]
        public void TestRemoveCell2X1()
        {
            var grid = new GridSystem<string>(10, 10);
            const string expected = "2x1 Cell";
            var setValue1X1 = grid.SetValue(1, 1, expected, 2);

            Assert.IsTrue(setValue1X1);
            Assert.IsFalse(grid.IsEmpty(1, 1));
            Assert.IsFalse(grid.IsEmpty(2, 1));


            var removedValue = grid.RemoveValue(1, 1);
            Assert.AreEqual(expected, removedValue);
            Assert.IsTrue(grid.IsEmpty(1, 1));
            Assert.IsTrue(grid.IsEmpty(2, 1));
        }
    }
}