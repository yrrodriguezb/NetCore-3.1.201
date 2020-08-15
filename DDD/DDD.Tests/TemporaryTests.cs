using Xunit;
using DDD.Logic;
using NHibernate;

namespace DDD.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init("Data Source=SnackMachine.db;Version=3;");

            using (ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }
    }
}