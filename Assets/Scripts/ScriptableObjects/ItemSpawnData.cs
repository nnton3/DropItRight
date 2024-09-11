using Assets.Scripts.DragNDropGame;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	[CreateAssetMenu(fileName = "ItemSpawnData", menuName = "Game/ItemSpawnData")]
	public class ItemSpawnData : ScriptableObject
	{
		public Item ItemPrefab => _itemPrefab;

		[SerializeField] private Item _itemPrefab;
	}
}
