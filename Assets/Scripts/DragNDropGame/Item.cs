using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using DG.Tweening;
using UnityEngine.UI;
using Assets.Scripts.Audio;

namespace Assets.Scripts.DragNDropGame
{
	public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private const float MOVE_TIME = 0.5f;
		private const float PUNCH_ANIMATION_TIME = 0.1f;
		private const string GET_CLIP_NAME = "get";
		public FigureType FigureType => _slotType;

		[SerializeField] private FigureType _slotType;
		private Canvas _canvas;
		private AudioService _audioService;
		private RectTransform _rectTransform;
		private Image _image;
		private bool _isInteractive = true;
		private readonly Vector3 _beginDragScale = Vector3.one * 1.01f;
		private readonly Vector3 _wrongDropRotation = Vector3.forward * 15f;
		private Tween _beginDragTween;

		[Inject]
		public void Construct(Canvas canvas, AudioService audioService)
		{
			_canvas = canvas;
			_audioService = audioService;
		}

		public void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
			_image = GetComponent<Image>();
		}

		public void SetPosition(Transform slot)
		{
			transform.SetParent(slot);
			transform.localScale = Vector3.one;
			transform.position = slot.position;
		}

		public void MoveToPosition(Transform slot)
		{
			transform.SetParent(slot);
			transform.localScale = Vector3.one;
			transform.DOMove(slot.position, MOVE_TIME);
		}

		public void SetInteractive(bool isInteractive)
		{
			_isInteractive = isInteractive;
		}

		public void PlayWrongAnimation()
		{
			var rotateAnimationSequence = DOTween.Sequence();
			rotateAnimationSequence.Append(transform.DORotate(_wrongDropRotation, PUNCH_ANIMATION_TIME));
			rotateAnimationSequence.Append(transform.DORotate(-_wrongDropRotation, PUNCH_ANIMATION_TIME));
			rotateAnimationSequence.Append(transform.DORotate(Vector3.zero, PUNCH_ANIMATION_TIME));
			rotateAnimationSequence.Play();
		}

		private void PlayScaleAnimation()
		{
			var scaleAnimationSequence = DOTween.Sequence();
			scaleAnimationSequence.Append(transform.DOScale(_beginDragScale, PUNCH_ANIMATION_TIME));
			scaleAnimationSequence.Append(transform.DOScale(Vector3.one, PUNCH_ANIMATION_TIME));
			scaleAnimationSequence.Play();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!_isInteractive)
				return;

			_audioService.Play(GET_CLIP_NAME);
			DOTween.Kill(_beginDragTween);
			PlayScaleAnimation();
			_image.raycastTarget = false;
			transform.SetParent(transform.parent.parent);
			transform.SetAsLastSibling();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!_isInteractive) 
				return;

			_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
		}

		public void OnEndDrag(PointerEventData eventData) 
		{
			_image.raycastTarget = true;
		}
	}

	public class ItemMoverFactory : IFactory<ItemSpawnData, Item>
	{
		private readonly IInstantiator _instantiator;
		private ItemsStorage _itemsStorage;

		public ItemMoverFactory(IInstantiator instantiator, ItemsStorage itemsStorage)
		{
			_instantiator = instantiator;
			_itemsStorage = itemsStorage;
		}

		public Item Create(ItemSpawnData spawnData)
		{
			var item = _instantiator.InstantiatePrefabForComponent<Item>(spawnData.ItemPrefab);
			_itemsStorage.Add(item);
			return item;
		}
	}
}
