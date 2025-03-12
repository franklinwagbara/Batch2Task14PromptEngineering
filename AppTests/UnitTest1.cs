using NUnit.Framework;
using System;
using System.Collections.Generic;
using InMemoryTable;
using static InMemoryTable.ItemMasterTable;

namespace InMemoryTableTests
{
    public class ItemMasterTableTests
    {
        [Test]
        public void Append_ValidItemMasterTable_SuccessfullyAppendsItems()
        {
            // Arrange
            var table1 = new ItemMasterTable();
            table1.Add(new ItemData(1, "Item 1"));
            table1.Add(new ItemData(2, "Item 2"));

            var table2 = new ItemMasterTable();
            table2.Add(new ItemData(3, "Item 3"));
            table2.Add(new ItemData(4, "Item 4"));

            // Act
            table1.Append(table2);

            // Assert
            Assert.That(table1.Find(1), Is.Not.Null);
            Assert.That(table1.Find(2), Is.Not.Null);
            Assert.That(table1.Find(3), Is.Not.Null);
            Assert.That(table1.Find(4), Is.Not.Null);
        }

        [Test]
        public void Append_NullData_ThrowsArgumentNullException()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act & Assert
            Assert.That(() => table.Append(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Append_InvalidDataType_ThrowsArgumentException()
        {
            // Arrange
            var table = new ItemMasterTable();
            var invalidTable = new InvalidMasterTable(); // Some other class inheriting MasterTable_Base

            // Act & Assert
            Assert.That(() => table.Append(invalidTable), Throws.ArgumentException);
        }

        [Test]
        public void Append_DuplicateId_ThrowsArgumentException()
        {
            // Arrange
            var table1 = new ItemMasterTable();
            table1.Add(new ItemData(1, "Item 1"));

            var table2 = new ItemMasterTable();
            table2.Add(new ItemData(1, "Item 1 Duplicate")); // Same ID

            // Act & Assert
            Assert.That(() => table1.Append(table2), Throws.ArgumentException);
        }


        [Test]
        public void Find_ExistingItem_ReturnsItemData()
        {
            // Arrange
            var table = new ItemMasterTable();
            var item = new ItemData(1, "Item 1");
            table.Add(item);

            // Act
            var foundItem = table.Find(1) as ItemData;

            // Assert
            Assert.That(foundItem, Is.Not.Null);
            Assert.That(foundItem.Id, Is.EqualTo(1));
            Assert.That(foundItem.Name, Is.EqualTo("Item 1"));
        }

        [Test]
        public void Find_NonExistingItem_ReturnsNull()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act
            var foundItem = table.Find(1);

            // Assert
            Assert.That(foundItem, Is.Null);
        }

        [Test]
        public void Find_NegativeId_ReturnsNull()
        {
            // Arrange
            var table = new ItemMasterTable();
            table.Add(new ItemData(1, "Positive Item"));

            //Act
            var foundItem = table.Find(-1);

            //Assert
            Assert.That(foundItem, Is.Null);
        }

        [Test]
        public void Find_ZeroId_ReturnsCorrectItem()
        {
            //Arrange
            var table = new ItemMasterTable();
            var item = new ItemData(0, "Zero Item");
            table.Add(item);

            //Act
            var foundItem = table.Find(0) as ItemData;

            //Assert
            Assert.That(foundItem, Is.Not.Null);
            Assert.That(foundItem.Id, Is.EqualTo(0));
            Assert.That(foundItem.Name, Is.EqualTo("Zero Item"));

        }

        [Test]
        public void Add_ValidItem_SuccessfullyAdds()
        {
            // Arrange
            var table = new ItemMasterTable();
            var item = new ItemData(1, "Item 1");

            //Act
            table.Add(item);

            //Assert
            var foundItem = table.Find(1) as ItemData;
            Assert.That(foundItem, Is.Not.Null);
            Assert.That(foundItem.Id, Is.EqualTo(item.Id));
            Assert.That(foundItem.Name, Is.EqualTo(item.Name));
        }

        [Test]
        public void Add_NullItemData_ThrowsArgumentNullException()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act & Assert
            Assert.That(() => table.Add(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Add_DuplicateItemId_ThrowsArgumentException()
        {
            // Arrange
            var table = new ItemMasterTable();
            table.Add(new ItemData(1, "Item 1"));

            // Act & Assert
            Assert.That(() => table.Add(new ItemData(1, "Item 1 Duplicate")), Throws.ArgumentException);
        }

        // Dummy class for testing invalid data type scenario
        private class InvalidMasterTable : MasterTable_Base
        {
            public override void Append(MasterTable_Base data)
            {
                throw new NotImplementedException();
            }

            public override MasterData_Base Find(int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}