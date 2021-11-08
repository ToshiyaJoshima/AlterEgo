namespace AlterEgo.Entities;

using System.Numerics;

using static Utility;

public class Player
{
    internal static int Address => 0x0010F4F4;

    protected internal int PrimaryAmmo
    {
        get => ReadMemory<int>(Offset.PrimaryAmmo);
        set => WriteMemory(Offset.PrimaryAmmo, value);
    }

    protected internal int Health
    {
        get => ReadMemory<int>(Offset.Health);
        set => WriteMemory(Offset.Health, value);
    }

    protected internal int SecondaryAmmo
    {
        get => ReadMemory<int>(Offset.SecondaryAmmo);
        set => WriteMemory(Offset.SecondaryAmmo, value);
    }

    private float X
    {
        get => ReadMemory<float>(Offset.X);
        set => WriteMemory(Offset.X, value);
    }

    private float Y
    {
        get => ReadMemory<float>(Offset.Y);
        set => WriteMemory(Offset.Y, value);
    }

    private float Z
    {
        get => ReadMemory<float>(Offset.Z);
        set => WriteMemory(Offset.Z, value);
    }

    public Vector3 Position
    {
        get => new(X, Y, Z);
        set
        {
            WriteMemory(Offset.X, value.X);
            WriteMemory(Offset.Y, value.Y);
            WriteMemory(Offset.Z, value.Z);
        }
    }


    internal enum Offset
    {
        PrimaryAmmo = 0x0150,
        SecondaryAmmo = 0x013C,
        Health = 0x00F8,
        FireRate = 0x0178,
        X = 0x4,
        Y = 0x8,
        Z = 0xC,
        InOnFloor = 0x0069,
        IsCrouching = 0x006C
    }
}