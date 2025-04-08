using System.Diagnostics;

namespace WealthMind.Utils
{
    public static class PackagesInstaler
    {
        public static void PackageDownload()
        {
            string baseDirectory = @"C:\Users\gilth\source\repos\WealthMind";

            var projects = new Dictionary<string, List<string>>
            {
                { "WealthMind.Core.Application/WealthMind.Core.Application.csproj", new List<string>
                    { "AutoMapper@13.0.1", "FluentValidation@11.9.2", "MediatR@12.4.0", "MediatR.Extensions.Microsoft.DependencyInjection",
                      "Microsoft.AspNetCore.Http@2.2.2", "Microsoft.AspNetCore.Http.Abstractions@2.2.0", "Microsoft.AspNetCore.Http.Extensions@2.2.0",
                      "Microsoft.EntityFrameworkCore@7.0.20", "Microsoft.Extensions.Options.ConfigurationExtensions@7.0.0", "Newtonsoft.Json@13.0.3",
                      "Swashbuckle.AspNetCore.Annotations@7.2.0", "System.Text.Encodings.Web@8.0.0"
                    }
                },
                { "WealthMind.Infrastructure.Identity/WealthMind.Infrastructure.Identity.csproj", new List<string>
                    { "Microsoft.AspNetCore.Authentication.JwtBearer@7.0.20", "Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore@7.0.20",
                      "Microsoft.AspNetCore.Identity.EntityFrameworkCore@7.0.20", "Microsoft.EntityFrameworkCore.InMemory@7.0.20",
                      "Microsoft.EntityFrameworkCore.SqlServer@7.0.20", "Microsoft.EntityFrameworkCore.Tools@7.0.20",
                      "Microsoft.Extensions.DependencyInjection@7.0.0", "Microsoft.Extensions.Options.ConfigurationExtensions@7.0.0",
                      "MimeKit@4.7.1", "System.IdentityModel.Tokens.Jwt@7.7.1"
                    }
                },
                { "WealthMind.Infrastructure.Persistence/WealthMind.Infrastructure.Persistence.csproj", new List<string>
                    { "Microsoft.EntityFrameworkCore.InMemory@7.0.20", "Microsoft.EntityFrameworkCore.SqlServer@7.0.20",
                      "Microsoft.Extensions.Options.ConfigurationExtensions@7.0.0"
                    }
                },
                { "WealthMind.Infrastructure.Shared/WealthMind.Infrastructure.Shared.csproj", new List<string>
                    { "MailKit@4.7.1.1", "MimeKit@4.7.1", "Microsoft.Extensions.Options.ConfigurationExtensions@7.0.0"
                    }
                },
                {"WealthMind/WealthMind.csproj", new List<string>
                    {"FluentValidation.DependencyInjectionExtensions@11.9.2", "Microsoft.AspNetCore.Mvc.NewtonsoftJson@7.0.20", "Microsoft.AspNetCore.Mvc.Versioning@5.1.0", "Microsoft.AspNetCore.OpenApi@7.0.20", "Microsoft.EntityFrameworkCore.Tools@7.0.20",
                        "Microsoft.VisualStudio.Web.CodeGeneration.Design@7.0.12", "Swashbuckle.AspNetCore@6.5.0", "Swashbuckle.AspNetCore.Swagger@6.6.2"
                    }
                }
            };

            foreach (var project in projects)
            {
                string projectPath = Path.Combine(baseDirectory, Path.GetDirectoryName(project.Key) ?? "");

                Console.WriteLine($"Project folder: {projectPath}");

                if (!Directory.Exists(projectPath))
                {
                    Console.WriteLine($"Error: La carpeta del proyecto '{projectPath}' no existe.");
                    continue;
                }

                InstallPackages(projectPath, project.Value);
            }

            static void InstallPackages(string projectPath, List<string> packages)
            {
                foreach (string package in packages)
                {
                    string[] parts = package.Split('@');
                    string packageName = parts[0];
                    string version = parts.Length > 1 ? parts[1] : "";

                    Process process = new Process();
                    process.StartInfo.FileName = "dotnet";
                    process.StartInfo.Arguments = $"add package {packageName} {(string.IsNullOrEmpty(version) ? "" : $"-v {version}")}";
                    process.StartInfo.WorkingDirectory = projectPath;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine($"Output of installing {packageName} in {projectPath}:\n{output}");
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"Error installing {packageName} in {projectPath}:\n{error}");
                    }
                }
            }
        }
    }
}
