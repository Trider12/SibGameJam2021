using Godot;
using SibGameJam2021.Core.Managers;

namespace SibGameJam2021.Core.Enemies
{
    public class Enemy : Entity
    {
        // AI
        [Export]
        private float ActivationRadius = 300;

        [Export]
        private float SightActivationRadius = 350;

        public Enemy() : base()
        {
        }

        [Export]
        public float Damage { get; set; } = 25;

        public SpawnManager SpawnManager { get; set; }

        public override void _PhysicsProcess(float delta)
        {
            var player = GameManager.Instance.Player;

            UpdatePosition(player);

            var direction = (player.Position - Position).Normalized();
        }

        virtual public void UpdatePosition(Player player)
        {
            if (player.Position.DistanceSquaredTo(Position) < ActivationRadius * ActivationRadius)
            {
                // базовое перемещение в сторону игрока если он находится в некотром радиусе от моба
                Position += (player.Position - Position) / 50;
                SetAnimationRun();
            }
            else
            {
                SetAnimationIdle();
            }
        }

        protected override void Die()
        {
            QueueFree();
            SpawnManager.EnemiesAlive--;
        }

        private void _on_Area2D_body_entered(Node body)
        {
            var bullet = body as Bullet;

            if (bullet == null)
            {
                return;
            }

            GetDamage(bullet.Damage);
            bullet.QueueFree();
        }

        private void SetAnimationAttack()
        {
            _animationState.Travel("Attack");
        }

        private void SetAnimationHurt()
        {
            _animationState.Travel("Hurt");
        }

        private void SetAnimationIdle()
        {
            _animationState.Travel("Idle");
        }

        private void SetAnimationRun()
        {
            _animationState.Travel("Run");
        }

        private void UpdateAnimationTreeState(Vector2 direction)
        {
            _animationTree.Set("parameters/Attack/blend_position", direction);
            _animationTree.Set("parameters/Hurt/blend_position", direction);
            _animationTree.Set("parameters/Idle/blend_position", direction);
            _animationTree.Set("parameters/Run/blend_position", direction);
        }
    }
}