using Godot;

namespace SibGameJam2021.Core.Weapons
{
    public class Rifle : WeaponBase
    {
        public Rifle() : base()
        {
        }

        [Export]
        public override string GunDescription { get; protected set; } = "старый образец вооружения, который все еще каким то немыслемым образом не развалился ";

        [Export]
        public override string GunName { get; protected set; } = "пушка";

        [Export]
        public override float RateOfFire { get; protected set; } = 10;

        protected override void AdditionalLogic()
        {
            AmmoCount -= AmmoPerShot;
        }

        protected override void SpawnProjectiles()
        {
            var bullet = InstanceBullet();

            GetParent().GetParent().GetParent().AddChild(bullet);
        }
    }
}