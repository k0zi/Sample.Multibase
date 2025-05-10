using FluentAssertions;
using Microsoft.SqlServer.Dac;

namespace TestProject
{
    public class TestDacpacUnpack
    {
#if DEBUG

        private readonly string dacpacPathSdk = Path.Combine(new[]
            { "..", "..", "..", "..", "Beta.Sdk", "bin", "Debug", "Beta.Sdk.dacpac" });
        private readonly string dacpacPathExtended = Path.Combine(new[]
            { "..", "..", "..", "..", "Extended", "bin", "Debug", "Extended.dacpac" });
#else
        private readonly string dacpacPathSdk = @$"..\..\..\..\Beta.Sdk\bin\Release\Beta.Sdk.dacpac";
        private readonly string dacpacPathExtended = @$"..\..\..\..\Extended\bin\Release\Extended.dacpac";
#endif
        [Fact]
        public void Is_Dacpacs_Exists()
        {
            var current = Directory.GetCurrentDirectory();
            var result = File.Exists(dacpacPathSdk) 
                && File.Exists(dacpacPathExtended);
            Assert.True(result);
        }

        [Fact]
        public void Unpack_Should_Not_Throws_Exception_With_Sdk()
        {
            var dacpac = DacPackage.Load(dacpacPathSdk);
            if (Directory.Exists(dacpac.Name))
            {
                Directory.Delete(dacpac.Name, true);
            }
            var directory = Directory.CreateDirectory(dacpac.Name);
            Action act = () => dacpac.Unpack(directory.FullName);
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void Unpack_Should_Not_Throws_Exception_With_Extended()
        {
            var dacpac = DacPackage.Load(dacpacPathExtended);
            if (Directory.Exists(dacpac.Name))
            {
                Directory.Delete(dacpac.Name, true);
            }
            var directory = Directory.CreateDirectory(dacpac.Name);
            Action act = () => dacpac.Unpack(directory.FullName);
            act.Should()
                .NotThrow();
        }
    }
}