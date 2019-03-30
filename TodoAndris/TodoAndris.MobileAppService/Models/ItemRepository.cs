using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TodoAndris.Models
{
	public class ItemRepository : IItemRepository
	{
		private static ConcurrentDictionary<string, Item> items =
			new ConcurrentDictionary<string, Item>();

		public ItemRepository()
		{
			Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Read a book", Description = "Read that new book about Xamarin programming.", Days=30, Progress=0 });
			Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Walk the dog", Description = "Give that little guy some fresh air.", Days=365, Progress=0 });
			Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Do something good", Description = "Say a compliment to a stranger or something.", Days=365, Progress=0 });
		}

		public Item Get(string id)
		{
			return items[id];
		}

		public IEnumerable<Item> GetAll()
		{
			return items.Values;
		}

		public void Add(Item item)
		{
			item.Id = Guid.NewGuid().ToString();
            item.Progress = 0;
			items[item.Id] = item;
		}

		public Item Find(string id)
		{
			Item item;
			items.TryGetValue(id, out item);

			return item;
		}

		public Item Remove(string id)
		{
			Item item;
			items.TryRemove(id, out item);

			return item;
		}

		public void Update(Item item)
		{
			items[item.Id] = item;
		}
	}
}
