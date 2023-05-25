using LiteDB;

namespace QuanLyTuyenSinh
{
    internal static class _LiteDb
    {
        public static LiteDatabase GetDatabase()
        {
            return new LiteDatabase(new ConnectionString($"{TuDien.LITEDB_DATABASE}")
            {
                Connection = ConnectionType.Shared
            });

        }

        public static ILiteCollection<T> GetColletion<T>() where T : BaseClass => GetDatabase().GetCollection<T>();
        public static bool Upsert<T>(T value) where T : BaseClass => GetDatabase().GetCollection<T>().Upsert(value);
        public static int InsertMany<T>(List<T> list) where T : BaseClass => GetDatabase().GetCollection<T>().InsertBulk(list);
        public static bool Delete<T>(T value) where T : BaseClass => GetDatabase().GetCollection<T>().Delete(value.Id);
        public static bool Delete<T>(string Id) where T : BaseClass => GetDatabase().GetCollection<T>().Delete(Id);
    }
}
