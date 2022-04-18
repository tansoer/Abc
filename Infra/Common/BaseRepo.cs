using Abc.Aids.Methods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
namespace Abc.Infra.Common {
    public abstract class BaseRepo {
        protected internal DbContext db;
        protected internal IDbContextTransaction transaction;
        protected BaseRepo(DbContext c) => db = c;
        public string ErrorMessage { get; private set; }
        public bool BeginTransaction() => beginTransaction(callingMethod());
        public bool RollbackTransaction() => rollbackTransaction(callingMethod());
        public bool CommitTransaction() => commitTransaction(callingMethod());
        public bool SaveChanges() => saveChanges(callingMethod());
        public async Task<bool> BeginTransactionAsync() => await beginTransactionAsync(callingMethod());
        public async Task<bool> RollbackTransactionAsync() => await rollbackTransactionAsync(callingMethod());
        public async Task<bool> CommitTransactionAsync() => await commitTransactionAsync(callingMethod());
        public async Task<bool> SaveChangesAsync() => await saveChangesAsync(callingMethod());
        internal bool beginTransaction(string method)
            => run(begin, msg => transactionError(method, msg));
        internal bool rollbackTransaction(string method)
            => run(rollback, msg => transactionError(method, msg));
        internal bool commitTransaction(string method)
            => run(commit, msg => transactionError(method, msg));
        internal bool saveChanges(string method)
            => run(save, msg => onException( method, msg));
        internal async Task<bool> beginTransactionAsync(string method)
            => await runAsync(beginAsync, msg => transactionError(method, msg));
        internal async Task<bool> rollbackTransactionAsync(string method)
            => await runAsync(rollbackAsync, msg => transactionError( method, msg));
        internal async Task<bool> commitTransactionAsync(string method)
            => await runAsync(commitAsync, msg => transactionError(method, msg));
        internal async Task<bool> saveChangesAsync(string method)
            => await runAsync(saveAsync, msg => onException(method, msg));
        protected internal bool begin() {
            transaction = db.Database.BeginTransaction();
            return isTransactionOpen;
        }
        protected internal bool rollback() {
            transaction.Rollback();
            return close();
        }
        protected internal bool commit() {
            save();
            transaction.Commit();
            return close();
        }
        protected internal bool close() {
            transaction = null;
            return true;
        }
        protected internal bool save() {
            db.SaveChanges();
            return true;
        }
        protected internal async Task<bool> beginAsync() {
            transaction = await db.Database.BeginTransactionAsync();
            return isTransactionOpen;
        }
        protected internal async Task<bool> rollbackAsync() {
            await transaction.RollbackAsync();
            return close();
        }
        protected internal async Task<bool> commitAsync() {
            await saveAsync();
            await transaction.CommitAsync();
            return close();
        }
        protected internal async Task<bool> saveAsync() {
            await db.SaveChangesAsync();
            return true;
        }
        protected internal bool isTransactionOpen => transaction != null;
        protected internal bool onItemNotFound(string id, string methodName)
            => reportError(string.Format(itemNotFoundMsg, id, methodName));
        protected internal bool onItemIsNull(string methodName)
            => reportError(string.Format(itemIsNullMsg, methodName));
        protected internal bool onItemFound(string id, string methodName)
            => reportError(string.Format(itemFoundMsg, id, methodName));
        protected internal bool onIdIsNull(string methodName)
            => reportError(string.Format(idIsNullMsg, methodName));
        protected internal bool onException(string methodName, Exception e)
            => reportError(string.Format(exceptionMsg, methodName, e.Message));
        protected internal bool onException(string methodName, string msg)
            => reportError(string.Format(exceptionMsg, methodName, msg));
        protected internal bool reportError(string msg) {
            ErrorMessage = msg;
            return false;
        }
        protected internal dynamic reportError(string msg, dynamic obj) {
            ErrorMessage = msg;
            return obj;
        }
        protected internal bool transactionError(string method, string msg) {
            transaction?.Rollback();
            transaction = null;
            return onException(method, msg);
        }
        protected internal static T run<T>(Func<T> f, Func<string, T> onException)
            => Safe.Run(f, onException, true);
        protected internal static T run<T>(Func<T> f, T onException)
            => Safe.Run(f, onException, true);
        protected internal static async Task<T> runAsync<T>(Func<Task<T>> f, Func<string, T> onException)
            => await Safe.RunAsync(f, onException);
        protected internal static async Task<T> runAsync<T>(Func<Task<T>> f, T onException)
            => await Safe.RunAsync(f, onException);
        protected internal static string callingMethod() {
            string name = string.Empty;
            for (var i = 1; i < 10; i++) {
                var frame = new StackFrame(i);
                var method = frame.GetMethod();
                name = method?.Name ?? string.Empty;
                if (name is "MoveNext" or "Start") continue;
                break;
            }
            return name;
        }
        protected internal static string idIsNullMsg => "Id is null in method <{0}>";
        protected internal static string itemIsNullMsg => "Item is null in method <{0}>";
        protected internal static string itemNotFoundMsg => "Item not found for Id = <{0}> in method <{1}>";
        protected internal static string itemFoundMsg => "Item found for Id = <{0}> in method <{1}>";
        protected internal static string exceptionMsg => "The method <{0}> has thrown the exception <{1}>";
    }
}