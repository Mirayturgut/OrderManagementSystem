# 🧾 Order Management System

Bu proje, C# dilinde Entity Framework Core ve SQL Server kullanılarak geliştirilen bir **sipariş yönetim sistemi** uygulamasıdır. Console üzerinden çalışan sistem, kullanıcıların rol bazlı işlemler gerçekleştirmesini sağlar.

---

## 🔧 Kullanılan Teknolojiler

- .NET 9.0
- Entity Framework Core
- SQL Server (Code-First yaklaşımı)
- SHA256 ile şifreleme
- Console tabanlı menü sistemi
- Katmanlı mimari (Models, EndPoints, Data)

---

## 📁 Proje Yapısı

OrderManagementSystem/
│
├── Data/ --> AppDbContext (EF Core yapılandırması)
├── Models/ --> Entity sınıfları (User, Product, Order, vb.)
├── EndPoints/ --> Rol bazlı işlemler (Admin, Kitchen, User)
├── Migrations/ --> EF Core migration dosyaları
├── Program.cs --> Giriş noktası
└── README.md --> Bu dosya

---

## 👤 Roller ve İşlevler

| Rol      | Yetkiler |
|----------|----------|
| Admin    | Ürün ekleme/güncelleme, kullanıcı ve sipariş listeleme |
| Kitchen  | Hazırlanacak siparişleri görüntüleme, sipariş durumu güncelleme |
| User     | Ürün listeleme, sipariş oluşturma, sipariş geçmişi görüntüleme |

---

## 🔐 Giriş ve Kayıt

- Kullanıcı kayıt olurken adı, kullanıcı adı, e-posta, şifre ve rol bilgisi girer.
- Şifreler **SHA256** ile güvenli şekilde hashlenir.
- Giriş sonrası kullanıcı, rolüne göre ilgili menüye yönlendirilir.

---

## 💾 Veritabanı Bilgisi

- Uygulama, **SQL Server** kullanır ve veritabanı Code-First yaklaşımı ile yapılandırılmıştır.
- `AppDbContext` üzerinden `DbSet<>` ile tablolar tanımlanmıştır.
- Veritabanı ilk çalıştırmada `EnsureCreated()` ile oluşturulur veya `Add-Migration` ve `Update-Database` komutları ile güncellenebilir.

---

## ✅ Özellikler

- Kullanıcı kayıt ve giriş sistemi
- Rol bazlı işlem menüleri
- Ürün CRUD işlemleri
- Sipariş oluşturma
- Sipariş durum takibi (Hazırlanıyor, Hazır, Teslim Edildi)
- Sipariş geçmişi listesi
- EF Core ile SQL Server bağlantısı

---

## 📷 Ekran Görüntüsü

![Sipariş Yönetim Sistemi](https://github.com/Mirayturgut/Sipariş-Yönetim-Sistemi/blob/main/images/ekran-goruntusu.png?raw=true)


---

## ✍️ Geliştirici

**Miray Turgut**  
📧 miray0853@gmail.com  
🔗 https://www.linkedin.com/in/miray-turgut-71792327a?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app

---

## 🚀 Başlamak İçin

1. Bu repoyu klonla:
   ```bash
   git clone https://github.com/kullaniciadi/OrderManagementSystem.git
 2.appsettings.json dosyasındaki SQL Server bağlantı ayarlarını yapılandır.
 3.Terminal'de aşağıdaki komutlarla veritabanını oluştur:
 dotnet ef database update
 4.Uygulamayı çalıştır:
 dotnet run
