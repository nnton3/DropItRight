using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	[CreateAssetMenu(fileName = "ItemsRepository", menuName = "Game/Repositories/ItemsRepository")]
	public class ItemsRepository : ScriptableObject
	{
		public ItemSpawnData CubeData => _cubeData;
		public ItemSpawnData SphereData => _sphereData;
		public ItemSpawnData PyramidData => _pyramidData;

		[SerializeField] private ItemSpawnData _cubeData;
		[SerializeField] private ItemSpawnData _sphereData;
		[SerializeField] private ItemSpawnData _pyramidData;
	}
}
