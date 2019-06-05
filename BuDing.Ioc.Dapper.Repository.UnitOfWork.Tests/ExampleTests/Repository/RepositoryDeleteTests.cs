using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryDeleteTests : CommonTestDataSetup
    {
        [Test]
        public static void DeleteByKey_Removes_EntityOnKey()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = 2;
            var resultBrave = new Brave();
            Assert.DoesNotThrow(() =>
            {
                using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
                {
                    result = repo.DeleteByKey(expected, uow);
                    resultBrave = repo.GetByKey(expected, uow);
                    uow.Rollback();
                }
            });
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void DeleteByKeyAsync_Removes_EntityOnKey()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = 2;
            var resultBrave = new Brave();
            Assert.DoesNotThrowAsync(async () =>
           {
               using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
               {
                   result = await repo.DeleteByKeyAsync(expected, uow);
                   resultBrave = await repo.GetByKeyAsync(expected, uow);
                   uow.Rollback();
               }
           });
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }
        [Test]
        public static void DeleteByKey_Removes_EntityOnKeyWithSessionGeneric()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave() { NewId = 2 };
            var resultBrave = new Brave();
            Assert.DoesNotThrow(() =>
            {
                repo.SaveOrUpdate<ITestSession>(expected);
                result = repo.DeleteByKey<ITestSession>(expected.Id);
                resultBrave = repo.Get<ITestSession>(expected);
            });
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void DeleteByKeyAsync_Removes_EntityOnKeyWithSessionGeneric()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave() { NewId = 2 };
            var resultBrave = new Brave();
            Assert.DoesNotThrowAsync(async () =>
            {
                repo.SaveOrUpdate<ITestSession>(expected);
                result = await repo.DeleteByKeyAsync<ITestSession>(expected.Id);
                resultBrave = await repo.GetAsync<ITestSession>(expected);
            });
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void Delete_Removes_EntityOnKey()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave { Id = 1 };
            Brave resultBrave = new Brave();

            Assert.DoesNotThrow(() =>
                {
                    using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
                    {
                        result = repo.Delete(expected, uow);
                        resultBrave = repo.Get(expected, uow);
                        uow.Rollback();
                    }
                }
            );
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void DeleteAsync_Removes_EntityOnKey()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave { Id = 1 };
            Brave resultBrave = new Brave();

            Assert.DoesNotThrowAsync(async () =>
                {
                    using (var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
                    {
                        result = await repo.DeleteAsync(expected, uow);
                        resultBrave = await repo.GetAsync(expected, uow);
                        uow.Rollback();
                    }
                }
            );
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void Delete_Removes_EntityOnKeyWithSessionGeneric()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave { NewId = 2 };
            Brave resultBrave = new Brave();

            Assert.DoesNotThrow(() =>
                {
                    repo.SaveOrUpdate<ITestSession>(expected);
                    result = repo.Delete<ITestSession>(expected);
                    resultBrave = repo.Get<ITestSession>(expected);
                }
            );
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }

        [Test]
        public static void DeleteAsync_Removes_EntityOnKeyWithSessionGeneric()
        {
            var repo = new BraveRepository(Factory);
            var result = false;
            var expected = new Brave { NewId = 2 };
            Brave resultBrave = new Brave();

            Assert.DoesNotThrowAsync(async () =>
                {
                    repo.SaveOrUpdate<ITestSession>(expected);
                    result = await repo.DeleteAsync<ITestSession>(expected);
                    resultBrave = await repo.GetAsync<ITestSession>(expected);
                }
            );
            Assert.That(result, Is.True);
            Assert.That(resultBrave, Is.Null);
        }
    }
}
