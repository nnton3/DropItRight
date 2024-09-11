using Assets.Scripts.Audio;
using Assets.Scripts.DragNDropGame;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.UI;
using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[Header("Environment")]
	[SerializeField] private Canvas _canvas;
	[SerializeField] private SlotsManager _slotManager;
	[SerializeField] private EmptyDropArea _emptyDropArea;
	[SerializeField] private AudioSource _source;
	
	[Header("UI")]
	[SerializeField] private EndGameMenu _endGameMenu;

	[Header("Repositories")]
	[SerializeField] private ItemsRepository _itemsRepository;
	[SerializeField] private AudioClipsRepository _clipsRepository;

	public override void InstallBindings()
	{
		BindEnvironment();
		BindUI();
		BindDatas();
		BindItems();
		BindGameplayServices();
	}

	private void BindEnvironment()
	{
		Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
		Container.Bind<AudioSource>().FromInstance(_source).AsSingle();
		Container.Bind<ItemsStorage>().AsSingle();
		Container.Bind<EmptyDropArea>().FromInstance(_emptyDropArea).AsSingle();
	}

	private void BindUI()
	{
		Container.Bind<EndGameMenu>().FromInstance(_endGameMenu).AsSingle();
	}

	private void BindDatas()
	{
		Container.Bind<ItemsRepository>().FromInstance(_itemsRepository).AsSingle();
		Container.Bind<AudioClipsRepository>().FromInstance(_clipsRepository).AsSingle();
	}

	private void BindItems()
	{
		Container.Bind<IFactory<ItemSpawnData, Item>>().To<ItemMoverFactory>().AsSingle();
	}

	private void BindGameplayServices()
	{
		Container.Bind<AudioService>().AsSingle();
		Container.Bind<LevelCreator>().AsSingle().NonLazy();
		Container.Bind<SlotsManager>().FromInstance(_slotManager).AsSingle().NonLazy();
		Container.Bind<DropHandler>().AsSingle().NonLazy();
		Container.Bind<VictoryObserver>().AsSingle().NonLazy();
		Container.Bind<GameController>().AsSingle().NonLazy();
	}
}
