using System.Collections.Generic;

namespace Assets.Scripts.DragNDropGame
{
	public class ItemsStorage
	{
		public  IReadOnlyList<Item> Items => _items;
		private List<Item> _items = new();

		public void Add(Item item)
		{
			_items.Add(item);
		}
	}
}
