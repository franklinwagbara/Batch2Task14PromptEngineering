using NUnit.Framework;
using System;
using System.Collections.Generic;
using InMemoryTable;
using static InMemoryTable.ItemMasterTable;
using NUnit.Framework.Interfaces;

namespace InMemoryTableTests
{
    public class ItemMasterTableTests
    {
        [Test]
        public void AddItem_ValidItem_ItemAddedSuccessfully()
        {
            // Arrange
            var table = new ItemMasterTable();
            var item = new ItemMasterTable.ItemData(1, "Sword");

            // Act
            table.AddItem(item);

            // Assert
            Assert.That(table.Find(1), Is.EqualTo(item));
        }

        [Test]
        public void AddItem_NullItem_ThrowsArgumentNullException()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => table.AddItem(null));
        }

        [Test]
        public void AddItem_DuplicateId_ThrowsArgumentException()
        {
            // Arrange
            var table = new ItemMasterTable();
            table.AddItem(new ItemMasterTable.ItemData(1, "Sword"));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => table.AddItem(new ItemMasterTable.ItemData(1, "Shield")));
        }

        [Test]
        public void Find_ExistingItem_ReturnsItem()
        {
            // Arrange
            var table = new ItemMasterTable();
            var item = new ItemMasterTable.ItemData(1, "Sword");
            table.AddItem(item);

            // Act
            var foundItem = table.Find(1);

            // Assert
            Assert.That(foundItem, Is.EqualTo(item));
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
        public void Append_ValidItemMasterTable_ItemsAppendedSuccessfully()
        {
            // Arrange
            var table1 = new ItemMasterTable();
            table1.AddItem(new ItemMasterTable.ItemData(1, "Sword"));
            var table2 = new ItemMasterTable();
            table2.AddItem(new ItemMasterTable.ItemData(2, "Shield"));

            // Act
            table1.Append(table2);

            // Assert
            Assert.That(table1.Find(1), Is.Not.Null);
            Assert.That(table1.Find(2), Is.Not.Null);
        }

        [Test]
        public void Append_InvalidMasterTableType_ThrowsArgumentException()
        {
            // Arrange
            var table1 = new ItemMasterTable();
            var invalidTable = new InvalidMasterTable(); // Assuming you have an InvalidMasterTable for testing

            // Act & Assert
            Assert.Throws<ArgumentException>(() => table1.Append(invalidTable));
        }

        [Test]
        public void Find_ItemWithNegativeId_ReturnsNull()
        {
            // Arrange
            var table = new ItemMasterTable();
            table.AddItem(new ItemData(1, "Positive Item"));

            // Act
            var result = table.Find(-1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddItems_NullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => table.AddItems(null));
        }

        [Test]
        public void AddItems_CollectionWithDuplicateIds_ThrowsArgumentException()
        {
            // Arrange
            var table = new ItemMasterTable();
            var items = new List<ItemData>
            {
                new ItemData(1, "Item 1"),
                new ItemData(1, "Item 2") // Duplicate ID
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => table.AddItems(items));
        }

        [Test]
        public void AddItems_ValidCollection_ItemsAddedSuccessfully()
        {
            // Arrange
            var table = new ItemMasterTable();
            var items = new List<ItemData>
            {
                new ItemData(1, "Item 1"),
                new ItemData(2, "Item 2")
            };

            // Act
            table.AddItems(items);

            // Assert
            Assert.That(table.Find(1), Is.Not.Null);
            Assert.That(table.Find(2), Is.Not.Null);
        }

        [Test]
        public void Find_ZeroId_ReturnsNullIfNotFound()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act
            var result = table.Find(0);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_VeryLargeId_ReturnsNullIfNotFound()
        {
            // Arrange
            var table = new ItemMasterTable();

            // Act
            var result = table.Find(int.MaxValue);

            // Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void Clear_RemovesAllItems()
        {
            // Arrange
            var table = new ItemMasterTable();
            table.AddItem(new ItemData(1, "Item 1"));
            table.AddItem(new ItemData(2, "Item 2"));

            // Act
            table.Clear();

            // Assert
            Assert.That(table.Find(1), Is.Null);
            Assert.That(table.Find(2), Is.Null);
        }

        [Test]
        public void Append_WithDuplicateIds_KeepsOriginalItems()
        {
            var table1 = new ItemMasterTable();
            table1.AddItem(new ItemData(1, "Sword"));
            table1.AddItem(new ItemData(2, "Shield"));
            var table2 = new ItemMasterTable();
            table2.AddItem(new ItemData(2, "Duplicate Shield"));
            table2.AddItem(new ItemData(3, "Bow"));
            table1.Append(table2);

            var item1 = (ItemData)table1.Find(1);
            var item2 = (ItemData)table1.Find(2);
            var item3 = (ItemData)table1.Find(3);

            Assert.That(item1, Is.Not.Null);
            Assert.That(item1.Name, Is.EqualTo("Sword"));

            Assert.That(item2, Is.Not.Null);
            Assert.That(item2.Name, Is.EqualTo("Shield"));

            Assert.That(item3, Is.Not.Null);
            Assert.That(item3.Name, Is.EqualTo("Bow"));
        }
    }



    // Dummy class for testing invalid Append scenario
    public class InvalidMasterTable : MasterTable_Base
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