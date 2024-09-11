using Assets.Scripts.UI;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts.DragNDropGame
{
	public class GameController : IDisposable
	{
		private readonly VictoryObserver _victoryObserver;
		private readonly EndGameMenu _endGameMenu;

		public GameController(VictoryObserver victoryObserver, EndGameMenu endGameMenu)
		{
			_victoryObserver = victoryObserver;
			_endGameMenu = endGameMenu;

			_victoryObserver.OnVictory += ShowEndGameMenu;
		}

		private void ShowEndGameMenu()
		{
			_endGameMenu.Show();
		}

		public void Dispose()
		{
			_victoryObserver.OnVictory -= ShowEndGameMenu;
		}
	}
}
