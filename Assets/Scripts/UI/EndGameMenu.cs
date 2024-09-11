using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class EndGameMenu : MonoBehaviour
	{
		public event Action OnResetBtnClicked;
		[SerializeField] private Button _resetBtn;
		[SerializeField] private CanvasGroup _group;

		private void Awake()
		{
			_resetBtn.onClick.AddListener(() =>
			{
				OnResetBtnClicked?.Invoke();
				Hide();
			});
		}

		public void Show()
		{
			_group.blocksRaycasts = true;
			_group.DOFade(1f, 1f).Play().Complete(_group.interactable = true);
		}

		public void Hide()
		{
			_group.interactable = false;
			_group.DOFade(0f, 1f).Play().Complete(_group.blocksRaycasts = true);
		}

		private void OnDestroy()
		{
			_resetBtn.onClick.RemoveAllListeners();
		}
	}
}
