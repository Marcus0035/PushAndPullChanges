using System.Diagnostics;

namespace PushAndPullChanges
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string repoPath = AppDomain.CurrentDomain.BaseDirectory;
            repoPath = repoPath.Replace("\\", "/");

            RunCommand($"git -C \"{repoPath}\" pull");
            RunCommand($"git -C \"{repoPath}\" add .");
            RunCommand($"git -C \"{repoPath}\" commit -m \"{DateTime.Now.ToString("dd-MM-yyyy-ss")}\"");
            RunCommand($"git -C \"{repoPath}\" push");

        }

        static void RunCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
                Console.WriteLine(output);
            if (!string.IsNullOrEmpty(error))
                Console.WriteLine("Error: " + error);
        }
    }
}
