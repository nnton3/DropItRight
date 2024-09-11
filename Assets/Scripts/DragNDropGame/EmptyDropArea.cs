using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.DragNDropGame
{
	public class EmptyDropArea : MonoBehaviour, IDropHandler
	{
		public event Action<Item> OnDropEvent;

		public void OnDrop(PointerEventData eventData)
		{
			if (eventData.pointerDrag == null)
				return;

			var item = eventData.pointerDrag.GetComponent<Item>();
			if (item == null)
				return;

			OnDropEvent?.Invoke(item);
		}
	}
}
