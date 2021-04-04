﻿using Godot;

namespace SibGameJam2021.Core.Weapons
{
    public abstract class WeaponBase : Node2D
    {
        protected static readonly PackedScene BulletScene = GD.Load<PackedScene>("res://Assets/Prefabs/Bullet.tscn");

        protected Node2D _muzzlePoint;
        private int _ammoCount = 0;
        private Sprite _sprite;
        private float _timeElapsed;

        protected WeaponBase()
        {
            FinishReloading();
        }

        [Export]
        public virtual float Accuracy { get; protected set; } = 100f;

        public int AmmoCount
        {
            get { return _ammoCount; }

            protected set { _ammoCount = value > 0 ? (value < MagSize ? value : MagSize) : 0; }
        }

        [Export]
        public virtual int AmmoPerShot { get; protected set; } = 1;

        [Export]
        public virtual float BulletSpeed { get; protected set; } = 300;

        [Export]
        public virtual float Damage { get; protected set; } = 35;

        [Export]
        public virtual string GunDescription { get; protected set; } = "an abomination";

        [Export]
        public virtual string GunName { get; protected set; } = "XYN";

        [Export]
        public virtual int MagSize { get; protected set; } = 30;

        [Export]
        public virtual float RateOfFire { get; protected set; } = 2f;

        [Export]
        public virtual float Recoil { get; protected set; } = 0f;

        [Export]
        public virtual float ReloadDuration { get; protected set; } = 1f;

        public float ShotDelay => 1f / RateOfFire;

        public override void _Process(float delta)
        {
            if (Input.IsActionPressed("ui_fire"))
            {
                _timeElapsed += delta;

                if (_timeElapsed >= ShotDelay)
                {
                    _timeElapsed -= ShotDelay;

                    Shoot();
                }
            }
            else
            {
                SetProcess(false);
            }
        }

        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");

            _muzzlePoint = GetNode<Node2D>("Muzzle");
        }

        public void FinishReloading()
        {
            AmmoCount = MagSize;
        }

        public void LookLeft()
        {
            _sprite.FlipV = true;
        }

        public void LookRight()
        {
            _sprite.FlipV = false;
        }

        public void StartReloading()
        {
            AmmoCount = 0;
        }

        public void StartShooting()
        {
            Shoot();
            SetProcess(true);
            _timeElapsed = 0f;
        }

        protected abstract void AdditionalLogic();

        protected Bullet InstanceBullet()
        {
            var bullet = (Bullet)BulletScene.Instance();

            bullet.GlobalPosition = _muzzlePoint.GlobalPosition;
            bullet.Direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            bullet.Speed = BulletSpeed;
            bullet.Damage = Damage;

            return bullet;
        }

        protected abstract void SpawnBullets();

        private void Shoot()
        {
            if (AmmoCount <= 0)
            {
                return;
            }

            SpawnBullets();
            AdditionalLogic();
        }
    }
}