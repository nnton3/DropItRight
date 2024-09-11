using Assets.Scripts.Audio;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.DragNDropGame
{
	public class VictoryObserver : IDisposable
	{
		public event Action OnVictory;

		private const int TARGET_CORRECT_FIGURES_COUNT = 12;
		private const string VICTORY_CLIP_NAME = "final";
		private readonly DropHandler _dropHandler;
		private readonly SlotsManager _slotManager;
		private readonly EndGameMenu _endGameMenu;
		private readonly AudioService _audioService;
		private int _currentCorrectFiguresCount;

		public VictoryObserver(DropHandler dropHandler, SlotsManager slotManager, EndGameMenu endGameMenu, AudioService audioService)
		{
			_dropHandler = dropHandler;
			_slotManager = slotManager;
			_endGameMenu = endGameMenu;
			_audioService = audioService;
			_dropHandler.OnDropOnCorrectSlot += IncreaseCorrectFigures;
			_currentCorrectFiguresCount += slotManager.SpawnInCorrectSlotFiguresCount;
			_endGameMenu.OnResetBtnClicked += Reset;
		}

		private void IncreaseCorrectFigures()
		{
			_currentCorrectFiguresCount++;
			Debug.Log($"correct answers {_currentCorrectFiguresCount}");
			if (_currentCorrectFiguresCount == TARGET_CORRECT_FIGURES_COUNT)
			{
				_audioService.Play(VICTORY_CLIP_NAME);
				OnVictory?.Invoke();
			}
		}

		private void Reset()
		{
			_currentCorrectFiguresCount = _slotManager.SpawnInCorrectSlotFiguresCount;
		}

		public void Dispose()
		{
			_dropHandler.OnDropOnCorrectSlot -= IncreaseCorrectFigures;
		}
	}
}
