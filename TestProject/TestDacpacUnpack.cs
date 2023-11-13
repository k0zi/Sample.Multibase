using FluentAssertions;
using Microsoft.SqlServer.Dac;

namespace TestProject
{
    public class TestDacpacUnpack
    {
#if DEBUG
        private readonly string dacpacPath = @$"..\..\..\..\Beta\bin\Debug\Beta.dacpac";
        private readonly string dacpacPathSdk = @$"..\..\..\..\Beta.Sdk\bin\Debug\Beta.Sdk.dacpac";
        private readonly string dacpacPathExtended = @$"..\..\..\..\Extended\bin\Debug\Extended.dacpac";
#else
        private readonly string dacpacPath = @$"..\..\..\..\Beta\bin\Release\Beta.dacpac";
        private readonly string dacpacPathSdk = @$"..\..\..\..\Beta.Sdk\bin\Release\Beta.Sdk.dacpac";
        private readonly string dacpacPathExtended = @$"..\..\..\..\Extended\bin\Release\Extended.dacpac";
#endif
        [Fact]
        public void Is_Dacpacs_Exists()
        {
            var result = File.Exists(dacpacPath) 
                && File.Exists(dacpacPathSdk) 
                && File.Exists(dacpacPathExtended);
            Assert.True(result);
        }

        [Fact]
        public void Unpack_Should_Not_Throws_Exception()
        {
            var dacpac = DacPackage.Load(dacpacPath);
            if(Directory.Exists(dacpac.Name))
            {
                Directory.Delete(dacpac.Name, true);
            }
            var directory = Directory.CreateDirectory(dacpac.Name);
            Action act = () => dacpac.Unpack(directory.FullName);
            act.Should()
                .NotThrow();
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