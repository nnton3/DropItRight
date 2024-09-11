using Assets.Scripts.ScriptableObjects;
using Zenject;

namespace Assets.Scripts.DragNDropGame
{
	public class LevelCreator
	{
		private const int ITEMS_COUNT = 4;
		private readonly IFactory<ItemSpawnData, Item> _itemsFactory;
		private readonly ItemsRepository _itemsRepository;

		public LevelCreator(
			IFactory<ItemSpawnData, Item> itemsFactory,
			ItemsRepository itemsRepository)
		{
			_itemsFactory = itemsFactory;
			_itemsRepository = itemsRepository;
			SpawnItems();
		}

		private void SpawnItems()
		{
			for (int i = 0; i < ITEMS_COUNT; i++)
				_itemsFactory.Create(_itemsRepository.CubeData);
			
			for (int i = 0; i < ITEMS_COUNT; i++)
				_itemsFactory.Create(_itemsRepository.SphereData);
			
			for (int i = 0; i < ITEMS_COUNT; i++)
				_itemsFactory.Create(_itemsRepository.PyramidData);
		}
	}
}
