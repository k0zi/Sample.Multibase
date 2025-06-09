using System.Diagnostics;
using FluentAssertions;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;

namespace TestProject
{
    public class EfcptTests
    {
#if DEBUG
        private const string CONFIGURATION = "Debug";
#else
        private const string CONFIGURATION = "Release";
#endif
        private readonly string betaPath = Path.Combine(new[]
            { "..", "..", "..", "..", "Beta.Sdk", "bin", CONFIGURATION, "Beta.Sdk.dacpac" });
        private readonly string gammaPath = Path.Combine(new[]
            { "..", "..", "..", "..", "Gamma.Sdk", "bin", CONFIGURATION, "Gamma.Sdk.dacpac" });

        [Fact]
        public void Is_Beta_Folder_Exists()
        {
            var directory = new FileInfo(betaPath).Directory.FullName;
            Assert.True(Directory.Exists(directory));
        }

        [Fact]
        public void Is_Beta_Dacpac_Exists()
        {
            var file = new FileInfo(betaPath).FullName;
            Assert.True(File.Exists(file));
        }
        
        [Fact]
        public void Is_Gamma_Folder_Exists()
        {
            var directory = new FileInfo(gammaPath).Directory.FullName;
            Assert.True(Directory.Exists(directory));
        }

        [Fact]
        public void Is_Gamma_Dacpac_Exists()
        {
            var file = new FileInfo(gammaPath).FullName;
            Assert.True(File.Exists(file));
        }

        [Fact]
        public void Run_Efcpt_On_Beta_WO_Error()
        {
            var file = new FileInfo(betaPath).FullName;
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "efcpt",
                Arguments = $"\"{file}\" mssql",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            var process = System.Diagnostics.Process.Start(processStartInfo);
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            
            Assert.DoesNotContain("System.InvalidOperationException: Sequence contains no elements", result);
        }
        
        [Fact]
        public void Run_Efcpt_On_Gamma_WO_Error()
        {
            var file = new FileInfo(gammaPath).FullName;
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "efcpt",
                Arguments = $"\"{file}\" mssql",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            var process = System.Diagnostics.Process.Start(processStartInfo);
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            
            Assert.DoesNotContain("System.InvalidOperationException: Sequence contains no elements", result);
        }

        [Fact]
        public void Load_Only_2_Tables_In_TsqlModel()
        {
            var model = new TSqlModel(betaPath);
            var tables = model.GetObjects(DacQueryScopes.UserDefined)
                .Where(t=>t.ObjectType.Name == "Table")
                .Count();
            Assert.Equal(2, tables);
        }

        [Fact]
        public void Load_All_Tables_In_TsqlModel()
        {
            var model = new TSqlModel(betaPath);
            var tables = model.GetObjects(DacQueryScopes.SameDatabase|DacQueryScopes.UserDefined)
                .Where(t=>t.ObjectType.Name == "Table")
                .Count();
            Assert.Equal(6, tables);
        }
    }
}