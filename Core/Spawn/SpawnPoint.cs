﻿using Godot;

namespace SibGameJam2021.Core.Spawn
{
    public class SpawnPoint : Node2D
    {
        public void SpawnEnemy(Enemy enemy)
        {
            enemy.GlobalPosition = GlobalPosition;
            GetParent().AddChild(enemy);
        }
    }
}