using Assets.Scripts.Extenstions;
using Assets.Scripts.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DragNDropGame
{
	public class SlotsManager : MonoBehaviour
	{
		public Dictionary<Item, ItemSlot> ItemToSlot => _itemToSlot;
		public Dictionary<ItemSlot, Item> SlotToItem => _slotToItem;
		public int SpawnInCorrectSlotFiguresCount => _spawnInCorrectSlotFiguresCount;

		[SerializeField] private ItemSlot[] _slots;
		private Dictionary<Item, ItemSlot> _itemToSlot;
		private Dictionary<ItemSlot, Item> _slotToItem;
		private ItemsStorage _itemsStorage;
		private EndGameMenu _endGameMenu;
		private int _spawnInCorrectSlotFiguresCount;

		[Inject]
		public void Construct(ItemsStorage itemsStorage, EndGameMenu endGameMenu)
		{
			_itemsStorage = itemsStorage;
			_endGameMenu = endGameMenu;
			_endGameMenu.OnResetBtnClicked += InitStartPlacements;

			InitStartPlacements();
		}

		private async void InitStartPlacements()
		{
			var randomList = _itemsStorage.Items.Shuffle().ToList();
			var pairsCount = _itemsStorage.Items.Count;

			_itemToSlot = new Dictionary<Item, ItemSlot>();
			_slotToItem = new Dictionary<ItemSlot, Item>();
			_spawnInCorrectSlotFiguresCount = 0;

			for (int i = 0; i < pairsCount; i++)
			{
				var slot = _slots[i];
				var item = randomList[i];
				_itemToSlot.Add(item, slot);
				_slotToItem.Add(slot, item);

				if (item.FigureType == slot.FigureType)
					_spawnInCorrectSlotFiguresCount++;
				
				item.SetInteractive(item.FigureType != slot.FigureType);
				_itemsStorage.Items[i].SetPosition(slot.transform);
			}

			await Task.Delay(2000);
			UpdatePositions();
		}

		private void UpdatePositions()
		{
			foreach (var pair in _itemToSlot)
				pair.Key.MoveToPosition(pair.Value.transform);
		}

		private void Reset()
		{
			_endGameMenu.OnResetBtnClicked -= InitStartPlacements;
		}
	}
}
