using LiteDB;
using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
    public abstract class BaseClass
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }

        public BaseClass()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

    }
    public abstract class DBClass : BaseClass
    {
        public abstract bool Save();
        public abstract bool Delete();
    }

    public class User : DBClass
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Permissons { get; set; }

        public override bool Delete() => _LiteDb.Delete<Nghe>(Id);

        public override bool Save() => _LiteDb.Upsert(this);
    }
}
