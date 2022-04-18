using System;
using System.Collections.Generic;
using Abc.Aids.Logging;
using Abc.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra {
    public class DbInitializer {
        static internal protected void addSet<T>(IEnumerable<T> set, DbContext db) where T : BaseData {
            try {
                db?.AddRange(set);
                var save = db?.SaveChanges();
            }
            catch (Exception e) {
                Log.Exception(e);
                rollBack(db);
                addItems(set, db);
            }
        }
        static internal protected void addItems<T>(IEnumerable<T> set, DbContext db) where T : BaseData {
            foreach (var e in set)
                addItem(e, db);
        }
        static internal protected void addItem<T>(T item, DbContext db) where T : BaseData {
            try {
                if (db?.Find<T>(item?.Id) is not null) return;
                var add = db?.Add(item);
                var save = db?.SaveChanges();
            }
            catch (Exception e) {
                Log.Exception(e);
                rollBack(db);
                try {
                    
                    db?.Update(item);
                    db?.SaveChanges();
                }
                catch (Exception e1) {
                    Log.Exception(e1);
                    rollBack(db);
                }
            }
        }
        static internal protected void rollBack(DbContext db)  => db.ChangeTracker.Clear();
    }
}
