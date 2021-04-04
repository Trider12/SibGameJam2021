using Godot;
using SibGameJam2021.Core.Managers;
using SibGameJam2021.Core.UI;
using SibGameJam2021.Core.Weapons;

namespace SibGameJam2021.Core.Enemies
{
    public class Enemy : Entity
    {
        private HealthBar _healthbar;
        private MovementOnNavigation2D movementOnNav2D;

        // AI
        [Export]
        private float ActivationRadius = 300;

        [Export]
        private float SightActivationRadius = 350;
        [Export]
        private float StopAtRadius = 5;

        public Enemy() : base()
        {
        }

        [Export]
        public float Damage { get; set; } = 25;

        public SpawnManager SpawnManager { get; set; }

        public override void _PhysicsProcess(float delta)
        {
            var player = GameManager.Instance.Player;

            UpdatePosition(player, delta);

            UpdateAnimation(player);
        }

        public override void _Ready()
        {
            base._Ready();
            movementOnNav2D = new MovementOnNavigation2D(GameManager.Instance.CurrentLevel.Navigation2D);
            AddChild(movementOnNav2D);
            _healthbar = GetNode<HealthBar>("HealthBar");
        }

        public override void GetDamage(float damage)
        {
            base.GetDamage(damage);

            _healthbar.UpdateHealth(CurrentHealth, MAX_HEALTH);
            SetAnimationHurt();
        }

        virtual public void UpdateAnimation(Player player)
        {
            if (player.Position.DistanceSquaredTo(Position) < SightActivationRadius * SightActivationRadius)
            {
                // обновление анимаций моба если игрок входит в определенный радиус
                UpdateAnimationTreeState((player.Position - Position).Normalized());
            }
        }

        virtual public void UpdatePosition(Player player, float delta)
        {
            float distanceSquared = player.Position.DistanceSquaredTo(Position);
            if ((distanceSquared < ActivationRadius * ActivationRadius) && (distanceSquared > StopAtRadius*StopAtRadius))
            {
                // базовое перемещение в сторону игрока если он находится в некотром радиусе от моба
                //Position += (player.Position - Position) / 50;
                //this.MoveAndCollide(); TODO
                FollowPath(player.GlobalPosition);
                SetAnimationRun();
            }
            else
            {
                SetAnimationIdle();
            }
        }
        //
        private void FollowPath( Vector2 destiny)
        {
            // https://youtu.be/0fPOt0Jw52s
            Vector2 nextPoint = movementOnNav2D.GetPointTowardsDestiny(GlobalPosition, destiny);
            var velocity = (nextPoint - GlobalPosition).Normalized() * MAX_SPEED;

            MoveAndSlide(velocity);
        }
        //

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

            GetDamage(bullet.Pop());
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