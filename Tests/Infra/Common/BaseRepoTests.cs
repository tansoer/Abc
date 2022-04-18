using Abc.Aids.Random;
using Abc.Infra.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Infra.Common {
    [TestClass] public class BaseRepoTests :AbstractTests<BaseRepo, object> {
        private MockDbContext db;
        private class testClass :BaseRepo {
            public testClass(DbContext c) :base(c) { }
        }
        protected override BaseRepo createObject() => new testClass(db = new MockDbContext());
        [TestMethod] public void ErrorMessageTest() => isReadOnly();
        [TestMethod] public void BeginTransactionTest() {
            Assert.IsNull(obj.transaction);
            Assert.AreEqual(0, db.Mock.CalledMethods.Count);
            runTest(() => obj.BeginTransaction(), true, "BeginTransaction");
        }
        [TestMethod] public void RollbackTransactionTest() {
            BeginTransactionTest();
            runTest(() => obj.RollbackTransaction(), false, "BeginTransaction", "Rollback");
        }
        [TestMethod] public void CommitTransactionTest() {
            BeginTransactionTest();
            runTest(() => obj.CommitTransaction(), false,
                "BeginTransaction", "SaveChanges", "Commit");
        }
        [TestMethod] public void SaveChangesTest() {
            BeginTransactionTest();
            runTest(() => obj.SaveChanges(), true,
                "BeginTransaction", "SaveChanges");
        }
        [TestMethod] public async Task BeginTransactionAsyncTest() {
            Assert.IsNull(obj.transaction);
            Assert.AreEqual(0, db.Mock.CalledMethods.Count);
            await runTestAsync(() => obj.BeginTransactionAsync(), true,
                "BeginTransactionAsync");
        }
        [TestMethod] public async Task RollbackTransactionAsyncTest() {
            await BeginTransactionAsyncTest();
            await runTestAsync(() => obj.RollbackTransactionAsync(), false,
                "BeginTransactionAsync", "RollbackAsync");
        }
        [TestMethod] public async Task CommitTransactionAsyncTest() {
            await BeginTransactionAsyncTest();
            await runTestAsync(() => obj.CommitTransactionAsync(), false,
                "BeginTransactionAsync", "SaveChangesAsync", "CommitAsync");
        }
        [TestMethod] public async Task SaveChangesAsyncTest() {
            await BeginTransactionAsyncTest();
            await runTestAsync(() => obj.SaveChangesAsync(), true,
                "BeginTransactionAsync", "SaveChangesAsync");
        }
        [TestMethod] public void BeginTransactionOnExceptionTest() {
            obj = new testClass(null);
            areEqual(false, obj.BeginTransaction());
            validateExceptionMsg("BeginTransaction");
        }
        [TestMethod] public void RollbackTransactionOnExceptionTest() 
            => noTransactionTest(() => obj.RollbackTransaction(), "RollbackTransaction");
        [TestMethod] public void CommitTransactionOnExceptionTest()
            => noTransactionTest(() => obj.CommitTransaction(), "CommitTransaction", "SaveChanges");
        [TestMethod] public void SaveChangesOnExceptionTest() {
            obj = new testClass(null);
            areEqual(false, obj.SaveChanges());
            validateExceptionMsg("SaveChanges");
        }
        [TestMethod] public async Task BeginTransactionAsyncOnExceptionTest() {
            obj = new testClass(null);
            areEqual(false, await obj.BeginTransactionAsync());
            validateExceptionMsg("BeginTransactionAsync");
        }
        [TestMethod] public async Task RollbackTransactionAsyncOnExceptionTest()
            => await noTransactionTestAsync(() => obj.RollbackTransactionAsync(), "RollbackTransactionAsync");
        [TestMethod] public async Task CommitTransactionAsyncOnExceptionTest() 
            => await noTransactionTestAsync(() => obj.CommitTransactionAsync(), 
                "CommitTransactionAsync", "SaveChangesAsync");
        [TestMethod] public async Task SaveChangesAsyncOnExceptionTest() {
            obj = new testClass(null);
            areEqual(false, await obj.SaveChangesAsync());
            validateExceptionMsg("SaveChangesAsync");
        }
        [TestMethod] public void BeginTest() 
            => runTrue(()=>obj.begin(), true, "BeginTransaction");
        [TestMethod] public void RollbackTest() {
            BeginTest();
            runTrue(() => obj.rollback(), false, "BeginTransaction","Rollback");
        }
        [TestMethod] public void CommitTest() {
            BeginTest();
            runTrue(() => obj.commit(), false, 
                "BeginTransaction", "SaveChanges", "Commit");
        }
        [TestMethod] public void CloseTest() {
            obj.transaction = new MockDbConnectTransaction(null);
            runTrue(() => obj.close(), false);
        }
        [TestMethod] public void SaveTest() {
            obj.transaction = new MockDbConnectTransaction(null);
            runTrue(() => obj.save(), true,"SaveChanges");
        }
        [TestMethod] public async Task BeginAsyncTest() 
            => await runTrueAsync(() => obj.beginAsync(), true, "BeginTransactionAsync");
        [TestMethod] public async Task RollbackAsyncTest() {
            await BeginAsyncTest();
            await runTrueAsync(() => obj.rollbackAsync(), false, 
                "BeginTransactionAsync", "RollbackAsync");
        }
        [TestMethod] public async Task CommitAsyncTest() {
            await BeginAsyncTest();
            await runTrueAsync(() => obj.commitAsync(), false,
                "BeginTransactionAsync", "SaveChangesAsync", "CommitAsync");
        }
        [TestMethod] public async Task SaveAsyncTest() {
            obj.transaction = new MockDbConnectTransaction(null);
            await runTrueAsync(() => obj.saveAsync(), true, "SaveChangesAsync");
        }
        [TestMethod] public void IsTransactionOpenTest() {
            isFalse(obj.isTransactionOpen);
            obj.transaction = new MockDbConnectTransaction(null);
            isTrue(obj.isTransactionOpen);
        }
        [TestMethod] public void OnItemNotFoundTest()
            => errorTest<string, string>((x,y) 
                => obj.onItemNotFound(x, y), BaseRepo.itemNotFoundMsg);
        [TestMethod] public void OnItemIsNullTest() 
            => errorTest<string>(x => obj.onItemIsNull(x), BaseRepo.itemIsNullMsg);
        [TestMethod] public void OnItemFoundTest() 
            => errorTest<string, string>((x, y)
                => obj.onItemFound(x, y), BaseRepo.itemFoundMsg);
        [TestMethod] public void OnIdIsNullTest() 
             => errorTest<string>(x => obj.onIdIsNull(x), BaseRepo.idIsNullMsg);
        [TestMethod] public void OnExceptionTest() {
            var e = new ArgumentException();
            var method = random<string>();
            obj.onException(method, e);
            areEqual(string.Format(BaseRepo.exceptionMsg, method, e.Message), obj.ErrorMessage);
        }
        [TestMethod] public void OnExceptionMsgTest()
            => errorTest<string, string>((x, y)
                => obj.onException(x, y), BaseRepo.exceptionMsg);
        [TestMethod] public void ReportErrorBoolTest() {
            var s = random<string>();
            isFalse(obj.reportError(s));
            areEqual(s, obj.ErrorMessage);
        }
        [TestMethod] public void ReportErrorObjectTest() {
            var o = GetRandom.AnyValue();
            var s = random<string>();
            areEqual(o, obj.reportError(s, o));
            areEqual(s, obj.ErrorMessage);
        }
        [TestMethod] public void TransactionErrorTest() {
            isFalse(obj.isTransactionOpen);
            obj.transaction = new MockDbConnectTransaction(null);
            isTrue(obj.isTransactionOpen);
        }
        [TestMethod] public void RunValueTest() 
            => areEqual("", BaseRepo.run(()=> throw new ArgumentException(), ""));
        [TestMethod] public void RunFuncTest() 
            => areEqual("", BaseRepo.run(() => throw new ArgumentException(), _ => ""));
        [TestMethod] public async Task RunAsyncValueTest() 
            => areEqual("", await BaseRepo.runAsync(() => throw new ArgumentException(), ""));
        [TestMethod] public async Task RunAsyncFuncTest() 
            => areEqual("", await BaseRepo.runAsync(() => throw new ArgumentException(), _ => ""));
        [TestMethod] public void CallingMethodTest() 
            => areEqual(nameof(CallingMethodTest), BaseRepo.callingMethod());
        [TestMethod] public void IdIsNullMsgTest() 
            => areEqual("Id is null in method <{0}>", BaseRepo.idIsNullMsg);
        [TestMethod] public void ItemIsNullMsgTest() 
            => areEqual("Item is null in method <{0}>", BaseRepo.itemIsNullMsg);
        [TestMethod] public void ItemNotFoundMsgTest() 
            => areEqual("Item not found for Id = <{0}> in method <{1}>", BaseRepo.itemNotFoundMsg);
        [TestMethod] public void ItemFoundMsgTest() 
            => areEqual("Item found for Id = <{0}> in method <{1}>", BaseRepo.itemFoundMsg);
        [TestMethod] public void ExceptionMsgTest() 
            => areEqual("The method <{0}> has thrown the exception <{1}>", BaseRepo.exceptionMsg);
        private void runTrue(Func<bool> func, bool hasTransaction, params string[] calledMethods) {
            isTrue(func());
            validate(hasTransaction, calledMethods);
        }
        private async Task runTrueAsync(Func<Task<bool>> func, bool hasTransaction, params string[] calledMethods) {
            isTrue( await func());
            validate(hasTransaction, calledMethods);
        }
        private void runTest<T>(Func<T> func, bool hasTransaction, params string[] calledMethods) {
            func();
            validate(hasTransaction, calledMethods);
        }
        private async Task runTestAsync<T>(Func<Task<T>> func, bool hasTransaction, params string[] calledMethods) {
            await func();
            validate(hasTransaction, calledMethods);
        }
        private void validate(bool hasTransaction, params string[] calledMethods) {
            if (hasTransaction) isNotNull(obj.transaction); else isNull(obj.transaction);
            var count = calledMethods.Length;
            areEqual(count, db.Mock.CalledMethods.Count);
            for (var i = 0; i < count; i++)
                areEqual(calledMethods[i], db.Mock.CalledMethods[i]);
        }
        private void errorTest<T>(Func<T, bool> func, string msg) {
            var s = random<T>();
            func(s);
            areEqual(string.Format(msg, s), obj.ErrorMessage);
        }
        private void errorTest<T1, T2>(Func<T1, T2, bool> func, string msg) {
            var x = random<T1>();
            var y = random<T2>();
            func(x, y);
            areEqual(string.Format(msg, x, y), obj.ErrorMessage);
        }
        private void noTransactionTest(Func<bool> func, string method, params string[] calledMethods) {
            runTest(func, false, calledMethods);
            validateExceptionMsg(method);
        }
        private async Task noTransactionTestAsync(Func<Task<bool>> func, string method, params string[] calledMethods) {
            await runTestAsync(func, false, calledMethods);
            validateExceptionMsg(method);
        }
        private void validateExceptionMsg(string method) {
            var msg = string.Format(BaseRepo.exceptionMsg.Replace("<{1}>", ""), method);
            isTrue(obj.ErrorMessage.StartsWith(msg));
        }
    }
}