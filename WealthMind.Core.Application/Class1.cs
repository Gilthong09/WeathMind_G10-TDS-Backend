namespace WealthMind.Core.Application
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static void Main()
        {
            string[] projects = { "WealthMind.Core.Application.csproj" };
            // Define los paquetes y sus versiones aquí
            string[] packages = { "Microsoft.EntityFrameworkCore@7.0.20", "Newtonsoft.Json@13.0.3", "AutoMapper@13.0.1", "Microsoft.AspNetCore.Http@2.2.2", "Microsoft.AspNetCore.Http.Abstractions@2.2.0", "Microsoft.AspNetCore.Http.Extensions@2.2.0", "Microsoft.Extensions.Options.ConfigurationExtension@7.0.0" };

            foreach (string project in projects)
            {
                InstallPackages(project, packages);
            }
        }

        static void InstallPackages(string project, string[] packages)
        {
            foreach (string package in packages)
            {
                Process process = new Process();
                process.StartInfo.FileName = "dotnet";
                process.StartInfo.Arguments = $"add {project} package {package.Replace("@", " -v ")}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine($"Output of installing {package} in {project}:\n{output}");
            }
        }
    }
}
