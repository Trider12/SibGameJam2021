using Godot;
using SibGameJam2021.Core.Managers;
using SibGameJam2021.Core.World;

namespace SibGameJam2021.Core
{
    public class Level : Node2D
    {
        private Gate _gate;
        private SpawnManager _spawnManager;

        public Navigation2D Navigation2D { get; private set; } = null;

        public override void _Ready()
        {
            _gate = GetNode<Gate>("YSort/Gate");
            _spawnManager = GetNode<SpawnManager>("YSort/SpawnManager");
            Navigation2D = GetNode<Navigation2D>("Navigation2D");

            _spawnManager.Connect(nameof(SpawnManager.LevelCleared), this, nameof(OnLevelCleared));
        }

        public void RemovePlayer()
        {
            _spawnManager.RemovePlayer();
        }

        public void SpawnPlayer()
        {
            _spawnManager.SpawnPlayer();
        }

        private void OnLevelCleared()
        {
            _gate.Open();
        }
    }
}