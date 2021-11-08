namespace AlterEgo;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using Entities;

using Memory;

using static Entities.Player;

internal static class Program
{
    private static BackgroundWorker BackgroundWorker { get; } = new()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

    internal static Mem Mem { get; } = new();

    private static string ProcessName => "ac_client";

    private static bool IsProcessOpen => Mem.OpenProcess(ProcessName);

    private static bool IsInjected { get; set; }

    private static Player Player { get; } = new();

    [ModuleInitializer]
    public static void Initialize()
    {
        Console.Title = $"{ProcessName} Trainer";

        #region Console Events

        Console.CancelKeyPress += ConsoleOnCancelKeyPress;

        #endregion

        #region BackgroundWorker Events

        BackgroundWorker.DoWork += BackgroundWorkerOnDoWork;
        BackgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
        BackgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;

        #endregion

        BackgroundWorker.RunWorkerAsync();

        Console.WriteLine("Initialized.");
    }

    private static void BackgroundWorkerOnProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        if (e.ProgressPercentage == 0)
        {
            return;
        }

        if (IsInjected)
        {
            return;
        }

        IsInjected = true;
        Utility.FreezeMemory(Offset.PrimaryAmmo, 1337);
        Utility.FreezeMemory(Offset.Health, 1337);
    }

    private static void BackgroundWorkerOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) => BackgroundWorker.RunWorkerAsync();

    private static void BackgroundWorkerOnDoWork(object? sender, DoWorkEventArgs e)
    {
        Thread.Sleep(250);

        if (!IsProcessOpen)
        {
            IsInjected = false;
            BackgroundWorker.ReportProgress(0);
            return;
        }

        if (!IsInjected)
        {
            BackgroundWorker.ReportProgress(100);
        }
    }

    public static void Main(string[] args)
    {
        while (IsProcessOpen) { }
    }

    private static void ConsoleOnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        if (e.SpecialKey != ConsoleSpecialKey.ControlC)
        {
            return;
        }

        e.Cancel = false;
    }
}