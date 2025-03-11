using System;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryTable
{
    [Serializable]
    public abstract class MasterTable_Base
    {
        public abstract class MasterData_Base
        {
        }

        public abstract void Append(MasterTable_Base data);

        public abstract MasterData_Base Find(int id);
    }

    [Serializable]
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
            if (data is ItemMasterTable other)
            {
                _items.AddRange(other._items);
            }
            else
            {
                throw new ArgumentException("Invalid data type. Expected ItemMasterTable.", nameof(data));
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

        /// <summary>
        /// Adds a new ItemData object to the internal list of items.
        /// </summary>
        /// <param name="item">The ItemData object to add.</param>
        public void AddItem(ItemData item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }

            if (_items.Any(existingItem => existingItem.Id == item.Id))
            {
                throw new ArgumentException($"An item with ID {item.Id} already exists.", nameof(item));
            }
            _items.Add(item);
        }

        public void AddItems(IEnumerable<ItemData> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "Items collection cannot be null.");
            }

            foreach (var item in items)
            {
                AddItem(item); // Reuse AddItem to ensure individual item validation.
            }
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}