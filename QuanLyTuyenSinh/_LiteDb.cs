using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh
{
    public class _LiteDb
    {
        private static LiteDatabase Db;
        private _LiteDb() { }
        public static LiteDatabase GetDb()
        {
            if (Db == null)
                Db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH);
            return Db;
        }

        public static bool UpsertObj<T>(T obj) where T : BaseClass
        {
            var coll = GetDb().GetCollection<T>();
            return coll.Upsert(obj);
        }
        public static bool DeleteObj<T>(T obj) where T : BaseClass
        {
            var coll = GetDb().GetCollection<T>();

            return coll.Delete(obj.Id);
        }
    }
}
