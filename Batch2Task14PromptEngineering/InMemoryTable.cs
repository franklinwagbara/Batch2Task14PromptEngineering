using System;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryTable
{
    public class ItemMasterTable : MasterTable_Base
    {
        [Serializable]
        public class ItemData : MasterData_Base
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ItemData(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        private List<ItemData> _items = new List<ItemData>();

        /// <summary>
        /// Appends items from another MasterTable_Base instance if it is of type ItemMasterTable.
        /// </summary>
        /// <param name="data">The MasterTable_Base instance to append data from.</param>
        /// <exception cref="ArgumentException">Thrown if the data type is invalid.</exception>
        public override void Append(MasterTable_Base data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Input data cannot be null.");
            }

            if (!(data is ItemMasterTable itemTable))
            {
                throw new ArgumentException("Data must be of type ItemMasterTable.", nameof(data));
            }

            foreach (var item in itemTable._items)
            {
                if (_items.Any(existingItem => existingItem.Id == item.Id))
                {
                    throw new ArgumentException($"An item with ID {item.Id} already exists.", nameof(data));
                }
                _items.Add(item);
            }
        }

        /// <summary>
        /// Finds an ItemData object by its Id from the internal list of items.
        /// </summary>
        /// <param name="id">The Id of the ItemData to find.</param>
        /// <returns>The ItemData object if found, otherwise null.</returns>
        public override MasterData_Base Find(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public void Add(ItemData itemData)
        {
            if (itemData == null)
            {
                throw new ArgumentNullException(nameof(itemData), "Item data cannot be null.");
            }
            if (_items.Any(existingItem => existingItem.Id == itemData.Id))
            {
                throw new ArgumentException($"An item with ID {itemData.Id} already exists.", nameof(itemData));
            }
            _items.Add(itemData);
        }
    }

    [Serializable]
    public abstract class MasterTable_Base
    {
        [Serializable]
        public abstract class MasterData_Base
        {
        }

        public abstract void Append(MasterTable_Base data);

        public abstract MasterData_Base Find(int id);
    }
}