using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR.Service.Tests;
public class TestBase<T> where T : class
{
    protected readonly AutoMocker autoMocker = new AutoMocker(MockBehavior.Default);

    public TestBase()
    {
    }

    protected T CreateSubjectUnderTest()
    {
        return this.autoMocker.CreateInstance<T>();
    }
}
