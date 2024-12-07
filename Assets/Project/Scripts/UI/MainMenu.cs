using Military.Scripts.Interfaces;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _buttonPrefab;

        [Header("Buttons")]
        [SerializeField] private Button _gameButton;
        [SerializeField] private Button _mapEditorButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _createMapButton;
        private string[] _mapsName;
        private List<MapButton> _mapButtons;

        private const string MapEditorScene = "Map Editor";
        private const string GameScene = "Game";

        private ISceneLoader _sceneLoader;
        [Inject] private DiContainer _container;

        [Inject]
        private void Constructor(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _mapsName = GetMapNames();
            _mapButtons = new List<MapButton>();
            _gameButton.onClick.AddListener(OpenGameList);
            _mapEditorButton.onClick.AddListener(OpenMapEditorList);
            _exitButton.onClick.AddListener(CloseGame);
            _createMapButton.onClick.AddListener(OpenMapEditor);
        }

        private void OpenMapEditorList()
        {
            _createMapButton.gameObject.SetActive(true);
            CreateMapsButtons();

            foreach (var button in _mapButtons)
            {
                button.SceneLoadName = MapEditorScene;
            }
        }

        private void OpenGameList()
        {
            _createMapButton.gameObject.SetActive(false);
            CreateMapsButtons();

            foreach (var button in _mapButtons)
            {
                button.SceneLoadName = GameScene;
            }
        }

        private void OpenMapEditor()
        {
            _sceneLoader.LoadScene(MapEditorScene);
        }

        private void CloseGame()
        {
            Application.Quit();
        }

        private void CreateMapsButtons()
        {
            if (_content.childCount != 0) return;

            foreach (var map in _mapsName)
            {
                var button = _container.InstantiatePrefab(_buttonPrefab, _content).GetComponent<MapButton>();
                button.MapName = map;
                _mapButtons.Add(button);
            }
        }

        private string[] GetMapNames()
        {
            string directoryPath = Application.persistentDataPath + "/Maps/";
            if (!Directory.Exists(directoryPath))
            {
                return new string[0];
            }

            string[] filePaths = Directory.GetFiles(directoryPath);

            string[] fileNamesWithoutExtension = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                fileNamesWithoutExtension[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
            }

            return fileNamesWithoutExtension;
        }
    }
}