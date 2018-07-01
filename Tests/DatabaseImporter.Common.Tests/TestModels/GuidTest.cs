using System;

namespace DatabaseImporter.Common.Tests.TestModels
{
    public interface IGuidTest
    {
        Guid Guid { get; }
    }
    public class GuidTest : IGuidTest
    {
        public Guid Guid { get; }

        public GuidTest()
        {
            Guid = Guid.NewGuid();
        }
    }
}