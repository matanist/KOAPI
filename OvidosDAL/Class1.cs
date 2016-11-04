using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvidosDAL
{
    public class Kitap
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ad { get; set; }
        [Required]
        [StringLength(50)]
        public string Yazar { get; set; }

        public short StokMiktari { get; set; }
        
        public ICollection<KullaniciKitap> Kullanicilar { get; set; }
    }

    public class KullaniciKitap
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int KullaniciID { get; set; }
        [Key]
        [Column(Order=3)]
        public int KitapID { get; set; }
        public DateTime OduncAlmaZamani { get; set; }

        public virtual Kitap Kitap { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
    public class Kullanici
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Ad { get; set; }
        [Required]
        [StringLength(30)]
        public string Soyad { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Sifre { get; set; }
        
        public ICollection<KullaniciKitap> Kitaplar { get; set; }
    }
    //Veritabanı verilerini içerisinde barındıracak context hazırlanıyor.
    public class KitapDBEntities : DbContext
    {
        //constructor'da strateji belirleniyor.
        public KitapDBEntities()
            :base("name=KitapDB")
        {
            Database.SetInitializer<KitapDBEntities>(new CreateDatabaseIfNotExists<KitapDBEntities>());
            //Test sırasında model değiştiğinde veri tabanı yeniden oluşturulacak.
            //DİKKAT: Testler bittiğinde kaldırılabilir
            //Database.SetInitializer<KitapDBEntities>(new DropCreateDatabaseIfModelChanges<KitapDBEntities>());
        }
        //tablolar ayarlanıyor
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KullaniciKitap> KullaniciKitaplar { get; set; }
    }
}
