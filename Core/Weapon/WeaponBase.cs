using Godot;
using System;

public class WeaponBase : Node2D
{
    public virtual string GunName { get; private set; } = "XYN";
    public virtual string GunDescription { get; private set; } = "an abomination";
    public virtual GunType GunType { get; private set; } = new baloonGunType();
    public virtual int Recoil { get; private set; } = 100;
    public virtual int Damage { get; private set; } = 30;
    public virtual int MagSize { get; private set; } = 30;
    public virtual int ammoPerShot { get; private set; } = 1;
    public virtual double RateOfFire { get; private set; } = .5;
    public virtual double aftershotDelay { get; private set; } = 3;
    public int ammoCountdown { get { return this.ammoCountdown; } private set { this.ammoCountdown = value > 0 ? (value<MagSize?value:MagSize) : 0; } } // ����������� ���������� �������� �������� (�� 0 �� ������� ��������)
    //public virtual double aftershotDelay { get; private set; } = 3; TODO


    private double canShootAt = 0; 


    public void Shoot(float rotationDegrees, Vector2 position)
    {
        
        // �� ��� ������ �������� �������� ��� ������
        GunType.Shoot(this.FindParent("World") as Node2D, rotationDegrees, position, Damage, RateOfFire);
        // aftershot delay (?)
    }

    public void Reload()
    {
        // ����� ���� ����������� �� ���� ��� ��������� � ���� �������� ������ ��������
        ammoCountdown = MagSize;
    }

    // ������������ �������
    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("ui_fire"))
        {
            GD.Print(this.GetChild(0).GetParent());
            // (this.GetParent() as Node2D)
            this.Shoot(this.RotationDegrees, this.GlobalPosition);  // s ����� ������� � ����������� �������� ����� ��� ������ (0 ������) // 
            GD.Print(this.RotationDegrees, this.GlobalPosition);
        }
    }
}



