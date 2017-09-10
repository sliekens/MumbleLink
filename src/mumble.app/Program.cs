using System;
using System.Threading.Tasks;
using mumblelib;
using Microsoft.Extensions.CommandLineUtils;

namespace mumble.app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "mumblelink";
            app.HelpOption("-?|-h|--help");
            app.OptionHelp.Description = "Show supported commands";
            app.OnExecute(
                () =>
                {
                    app.ShowHelp();
                    return 0;
                });
            app.Command(
                "init",
                cmd =>
                {
                    cmd.Description = "Initializes the MumbleLink shared memory, useful for when you don't have the Mumble client installed.";
                    cmd.OnExecute(
                        () =>
                        {
                            var tcs = new TaskCompletionSource<ConsoleCancelEventArgs>();
                            Console.CancelKeyPress += (sender, eventArgs) => tcs.SetResult(eventArgs);
                            CreateFile(tcs.Task);
                            return 0;
                        });
                });
            app.Command(
                "raw",
                cmd =>
                {
                    cmd.Description = "Writes the current MumbleLink frame to stdout in binary format.";
                    cmd.HelpOption("-?|-h|--help");
                    cmd.OnExecute(
                        () =>
                        {
                            WriteRaw();
                            return 0;
                        });
                });
            app.Command(
                "identity",
                cmd =>
                {
                    cmd.Description = "Prints the identity field in text format.";
                    cmd.HelpOption("-?|-h|--help");
                    cmd.OnExecute(
                        () =>
                        {
                            WriteIdentity();
                            return 0;
                        });
                });
            app.Execute(args);
        }

        private static void CreateFile(Task<ConsoleCancelEventArgs> task)
        {
            using (MumbleLinkFile.CreateOrOpen())
            {
                Console.WriteLine(
                    "MumbleLink initialized. Open or restart your game while this program is still running. After that, you can stop this program. (Ctrl+C)");
                task.Wait();
            }
        }

        private static void WriteIdentity()
        {
            using (var file = MumbleLinkFile.CreateOrOpen())
            {
                var frame = file.Read();
                Console.WriteLine(frame.identity);
            }
        }

        private static void WriteRaw()
        {
            using (var file = MumbleLinkFile.CreateOrOpen())
            {
                var frame = file.ReadRaw();
                var length = frame.Length;
                Console.OpenStandardOutput(length).Write(frame, 0, length);
            }
        }
    }
}
