using Assets.Scripts.Audio;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DragNDropGame
{
	public class DropHandler : IDisposable
	{
		private const string WRONG_CLIP_NAME = "wrong";
		public event Action OnDropOnCorrectSlot;
		private readonly EmptyDropArea _emptyDropArea;
		private readonly SlotsManager _slotsManager;
		private readonly EndGameMenu _endGameMenu;
		private readonly AudioService _audioService;
		private Dictionary<Item, ItemSlot> _itemToSlot;
		private Dictionary<ItemSlot, Item> _slotToItem;

		public DropHandler(EmptyDropArea emptyDropArea, SlotsManager slotsManager, EndGameMenu endGameMenu, AudioService audioService)
		{
			_emptyDropArea = emptyDropArea;
			_slotsManager = slotsManager;
			_endGameMenu = endGameMenu;
			_audioService = audioService;
			UpdateItemsInfo();

			Subscribe();
		}

		private void Subscribe()
		{
			_endGameMenu.OnResetBtnClicked += UpdateItemsInfo;
			_emptyDropArea.OnDropEvent += DropOnEmpty;
			foreach (var slot in _slotToItem.Keys)
				slot.OnDropEvent += DropInSlot;
		}

		private void Unsubscribe()
		{
			_endGameMenu.OnResetBtnClicked -= UpdateItemsInfo;
			_emptyDropArea.OnDropEvent -= DropOnEmpty;
			foreach (var slot in _slotToItem.Keys)
				slot.OnDropEvent -= DropInSlot;
		}

		private void DropOnEmpty(Item item)
		{
			var currentSlot = _itemToSlot[item].transform;
			item.MoveToPosition(currentSlot);
		}

		private void DropInSlot(ItemSlot slot, Item item)
		{
			var tmpItem = _slotToItem[slot];
			var tmpSlot = _itemToSlot[item];

			if (item.FigureType == slot.FigureType)
			{
				OnDropOnCorrectSlot?.Invoke();
				item.MoveToPosition(slot.transform);
				item.SetInteractive(false);
				tmpItem.MoveToPosition(tmpSlot.transform);
				
				var swapInCorrect = tmpItem.FigureType == tmpSlot.FigureType;
				if (swapInCorrect)
					OnDropOnCorrectSlot?.Invoke();
					
				tmpItem.SetInteractive(!swapInCorrect);

				_slotToItem[slot] = item;
				_slotToItem[tmpSlot] = tmpItem;
				_itemToSlot[item] = slot;
				_itemToSlot[tmpItem] = tmpSlot;
			}
			else
			{
				if (slot != _itemToSlot[item])
					_audioService.Play(WRONG_CLIP_NAME);

				var itemCurrentSlot = _itemToSlot[item];
				item.MoveToPosition(itemCurrentSlot.transform);
				item.SetInteractive(true);
				item.PlayWrongAnimation();
			}
		}

		private void UpdateItemsInfo()
		{
			_itemToSlot = _slotsManager.ItemToSlot;
			_slotToItem = _slotsManager.SlotToItem;
		}

		public void Dispose()
		{
			Unsubscribe();
		}
	}
}
