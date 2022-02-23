using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager 
{
    public enum Weapon : int
    {
        shotgun,
        Bazooka,
        DoubleHandgun
    }
    public enum Sub : int
    {
        none,
        bulletDivision,
        swiftAttack,
        guidedMissile,
        exploisionBullet
    }

    public static Sub sub = Sub.none;
    public static Weapon weapon = Weapon.shotgun;
}
