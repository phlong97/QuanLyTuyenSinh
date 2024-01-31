using LiteDB;
using QuanLyTuyenSinh.Models;

namespace QuanLyTuyenSinh
{
    internal static class _LiteDb
    {
        public static LiteDatabase GetDatabase()
        {
            return new LiteDatabase(new ConnectionString($"{Properties.Settings.Default.DBPATH}\\{TuDien.LITEDB_DATABASE}")
            {
                Connection = ConnectionType.Shared
            });
        }
        public static LiteDatabase GetDatabase(string path)
        {
            return new LiteDatabase(new ConnectionString($"{path}\\{TuDien.LITEDB_DATABASE}")
            {
                Connection = ConnectionType.Shared
            });
        }

        public static ILiteCollection<T> GetCollection<T>(string name) where T : BaseClass => GetDatabase().GetCollection<T>(name);
        public static ILiteCollection<T> GetCollection<T>() where T : BaseClass => GetDatabase().GetCollection<T>();

        public static bool Upsert<T>(T value, string name) where T : BaseClass => GetDatabase().GetCollection<T>(name).Upsert(value);
        public static bool Upsert<T>(T value) where T : BaseClass => GetDatabase().GetCollection<T>().Upsert(value);
        public static int InsertMany<T>(List<T> list, string name) where T : BaseClass => GetDatabase().GetCollection<T>(name).InsertBulk(list);
        public static bool Delete<T>(string Id, string name) where T : BaseClass => GetDatabase().GetCollection<T>(name).Delete(Id);
        public static bool Delete<T>(string Id) where T : BaseClass => GetDatabase().GetCollection<T>().Delete(Id);

    }
}
