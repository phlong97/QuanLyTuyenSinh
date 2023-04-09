using DevExpress.CodeParser;
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

        public static bool UpsertObj(BsonDocument obj,string ColName)
        {
            var coll = GetDb().GetCollection(ColName);
            return coll.Upsert(obj);
        }
        public static bool DeleteObj(string Id,string ColName)
        {
            var coll = GetDb().GetCollection(ColName);

            return coll.Delete(Id);
        }

        public static List<T> GetData<T>() where T : DBClass
        {
            var coll = GetDb().GetCollection<T>();

            return coll.FindAll().ToList();
        }
    }
}
