using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

// Records each run without changing the exercises or the game logic.
internal static class TerminalTranscript
{
    private static StreamWriter? log;
    private static readonly object gate = new object();

    [ModuleInitializer]
    internal static void Start()
    {
        try
        {
            string project = Assembly.GetExecutingAssembly().GetName().Name ?? "Console";
            string directory = Path.Combine(Environment.CurrentDirectory, "terminal-tests", project);
            Directory.CreateDirectory(directory);
            string file = Path.Combine(directory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fffffff}_{Environment.ProcessId}.txt");
            log = new StreamWriter(new FileStream(file, FileMode.CreateNew, FileAccess.Write, FileShare.Read), new UTF8Encoding(false))
            {
                AutoFlush = true
            };

            Console.SetOut(new TranscriptWriter(Console.Out));
            Console.SetError(new TranscriptWriter(Console.Error));
            Console.SetIn(new TranscriptReader(Console.In));
            AppDomain.CurrentDomain.ProcessExit += (_, _) =>
            {
                lock (gate)
                {
                    log?.Dispose();
                    log = null;
                }
            };
            Console.WriteLine($"Terminal test saved to: {file}");
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Could not start terminal recording: {exception.Message}");
        }
    }

    private static void Record(string text)
    {
        lock (gate)
        {
            try
            {
                log?.Write(text);
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                // Keep the exercise usable if the disk becomes unavailable.
                log = null;
                Console.Error.WriteLine($"Terminal recording stopped: {exception.Message}");
            }
        }
    }

    private sealed class TranscriptWriter : TextWriter
    {
        private readonly TextWriter terminal;

        internal TranscriptWriter(TextWriter terminal) => this.terminal = terminal;
        public override Encoding Encoding => terminal.Encoding;

        public override void Write(char value)
        {
            terminal.Write(value);
            Record(value.ToString());
        }

        public override void Write(string? value)
        {
            terminal.Write(value);
            if (value != null) Record(value);
        }

        public override void WriteLine(string? value) => Write(value + NewLine);
        public override void Flush() => terminal.Flush();
    }

    private sealed class TranscriptReader : TextReader
    {
        private readonly TextReader terminal;

        internal TranscriptReader(TextReader terminal) => this.terminal = terminal;

        public override string? ReadLine()
        {
            string? line = terminal.ReadLine();
            if (line != null) Record(line + Environment.NewLine);
            return line;
        }
    }
}
