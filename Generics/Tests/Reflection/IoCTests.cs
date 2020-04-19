using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reflection.Test
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void Can_Resolve_Types()
        {
            var IoC = new Container();
            IoC.For<ILogger>().Use<SqlServerLogger>();

            var logger = IoC.Resolve<ILogger>();

            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Types_Without_Default_Constructor()
        {
            var IoC = new Container();

             IoC.For<ILogger>()
                .Use<SqlServerLogger>();

            IoC.For<IRepository<Employee>>()
                .Use<SqlRepository<Employee>>();

            var repository = IoC.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Concrete_Type()
        {
            var IoC = new Container();

             IoC.For<ILogger>()
                .Use<SqlServerLogger>();

            IoC.For(typeof(IRepository<>))
                .Use(typeof(SqlRepository<>));

            var service = IoC.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }
    }
}