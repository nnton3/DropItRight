using Assets.Scripts.UI;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts.DragNDropGame
{
	public class GameController : IDisposable
	{
		private const int START_GAME_DELAY = 1000;

		public event Action onStartGame;
		public event Action OnGameReset;
		private readonly VictoryObserver _victoryObserver;
		private readonly EndGameMenu _endGameMenu;

		public GameController(VictoryObserver victoryObserver, EndGameMenu endGameMenu)
		{
			_victoryObserver = victoryObserver;
			_endGameMenu = endGameMenu;

			_victoryObserver.OnVictory += ShowEndGameMenu;
			_endGameMenu.OnResetBtnClicked += StartGameAsync;

			StartGameAsync();
		}

		private async void StartGameAsync()
		{
			await Task.Delay(START_GAME_DELAY);
			onStartGame?.Invoke();
		}

		private void ShowEndGameMenu()
		{
			_endGameMenu.Show();
		}

		public void Dispose()
		{
			_victoryObserver.OnVictory -= ShowEndGameMenu;
			_endGameMenu.OnResetBtnClicked -= StartGameAsync;
		}
	}
}
