namespace AlterEgo;

using System.Text;

using static Entities.Player;

using static Program;

public static class Utility
{
    private static StringBuilder StringBuilder { get; set; } = new();

    internal static void WriteMemory<TOut>(Offset offset, TOut amount) where TOut : new()
    {
        StringBuilder = new($"base+{Address:X},{offset:X}");

        switch (new TOut())
        {
            case int:
                Mem.WriteMemory($"{StringBuilder}", "int", $"{amount}");
                break;
            case double:
                Mem.WriteMemory($"{StringBuilder}", "double", $"{amount}");
                break;
            case float:
                Mem.WriteMemory($"{StringBuilder}", "float", $"{amount}");
                break;
            default:
                Mem.WriteMemory($"{StringBuilder}", "float", $"{amount}");
                break;
        }
    }

    internal static void FreezeMemory<TOut>(Offset offset, TOut amount) where TOut : new()
    {
        StringBuilder = new($"base+{Address:X},{offset:X}");

        switch (new TOut())
        {
            case int:
                Mem.FreezeValue($"{StringBuilder}", "int", $"{amount}");
                break;
            case double:
                Mem.FreezeValue($"{StringBuilder}", "double", $"{amount}");
                break;
            case float:
                Mem.FreezeValue($"{StringBuilder}", "float", $"{amount}");
                break;
            default:
                Mem.FreezeValue($"{StringBuilder}", "float", $"{amount}");
                break;
        }
    }

    internal static TOut ReadMemory<TOut>(Offset offset) where TOut : new()
    {
        StringBuilder = new($"base+{Address:X},{offset:X}");

        return new TOut() switch
            {
                int => (TOut)Convert.ChangeType(Mem.ReadInt($"{StringBuilder}"), typeof(TOut)),
                double => (TOut)Convert.ChangeType(Mem.ReadDouble($"{StringBuilder}"), typeof(TOut)),
                float => (TOut)Convert.ChangeType(Mem.ReadFloat($"{StringBuilder}"), typeof(TOut)),
                bool => (TOut)Convert.ChangeType(Mem.ReadByte($"{StringBuilder}"), typeof(TOut)),
                _ => (TOut)Convert.ChangeType(Mem.ReadFloat($"{StringBuilder}"), typeof(TOut))
            };
    }
}