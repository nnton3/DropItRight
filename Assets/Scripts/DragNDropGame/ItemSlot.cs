using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.DragNDropGame
{
	public class ItemSlot : MonoBehaviour, IDropHandler
	{
		public event Action<ItemSlot, Item> OnDropEvent;
		public FigureType FigureType => _slotType;

		[SerializeField] private FigureType _slotType;

		public void OnDrop(PointerEventData eventData)
		{
			if (eventData.pointerDrag == null)
				return;

			var item = eventData.pointerDrag.GetComponent<Item>();
			if (item == null)
				return;

			OnDropEvent?.Invoke(this, item);
		}
	}
}
