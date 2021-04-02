using Godot;
using System;

public class GunType
{
    public virtual int bulletSpeed { get; private set; } = 500;
    public virtual string GunTypeName { get; private set; } = "base gun type (not implemented)";

    virtual public void Shoot(Node2D root, float rotationDegrees, Vector2 position, int Damage, double RateOfFire) => throw new NotImplementedException("no shoot logic for base gun type");

    public PackedScene bulet = (PackedScene)ResourceLoader.Load("res://Assets/Prefab/Bullet.tscn"); // �������� ������� ����

}

public class baloonGunType : GunType
{
    public override int bulletSpeed => 100;
    public override string GunTypeName => "baloon gun type";
    public override void Shoot(Node2D root, float rotationDegrees, Vector2 position, int Damage, double RateOfFire)
    {
        throw new NotImplementedException("no shoot logic"); //todo
    }
}
public class laserGunType : GunType
{
    public override int bulletSpeed => 600;
    public override string GunTypeName => "laser gun type";

    public override void Shoot(Node2D root, float rotationDegrees, Vector2 position, int Damage, double RateOfFire)
    {
        throw new NotImplementedException("no shoot logic"); //todo
    }
}
public class tripleShotGunType : GunType
{
    public override string GunTypeName => "triple shot gun type";

    public override void Shoot(Node2D root, float rotationDegrees, Vector2 position, int Damage, double RateOfFire)
    {
        RigidBody2D buletInstance = bulet.Instance() as RigidBody2D;

        buletInstance.Position = position;
        buletInstance.RotationDegrees = rotationDegrees;
        buletInstance.ApplyImpulse(new Vector2(), new Vector2(bulletSpeed, 0).Rotated(rotationDegrees));
        /*
         
         buletInstance.Position = position.Rotated(Mathf.Deg2Rad(rotationDegrees));
        //buletInstance.RotationDegrees = rotationDegrees;
        buletInstance.ApplyImpulse(new Vector2(), new Vector2(bulletSpeed, 0).Rotated(rotationDegrees));
         
         */


        root.AddChild(buletInstance);

    }
}
