using OrderManagementSystem.Data;
using OrderManagementSystem.EndPoints;
using OrderManagementSystem.Models;

namespace OrderManagementSystem;

class Program
{
    private static User? _loggedInUser;
    private static ConsoleMenu _menu = new("SİPARİŞ YÖNETİM SİSTEMİ");
    private static ConsoleMenu _AdminMenu = new("ADMİN YÖNETİMİ");
    private static ConsoleMenu _KitchenMenu = new("MUTFAK YÖNETİMİ");
    private static ConsoleMenu _UserMenu = new("KULLANICI YÖNETİMİ");
    private static AppDbContext _context = new AppDbContext();

    static void Main()
    {
        _menu
            .AddMenu("Giriş Yap", LoginMenu)
            .AddMenu("Kayıt Ol", RegisterMenu)
            .AddMenu("Çıkış", () => Environment.Exit(0));
        _AdminMenu
            .AddMenu("Ürün Ekle", AdminEndPoint.AddProduct )
            .AddMenu("Ürün Güncelle",AdminEndPoint.ProductUpdate )
            .AddMenu("Ürün Listele",AdminEndPoint.ProductList )
            .AddMenu("Tüm Kullanıcıları / Siparişleri Görüntüle", AdminEndPoint.AllList );
        _KitchenMenu
            .AddMenu("Hazırlanacak Siparişleri Listele",KitchenEndPoints.PreparedOrderList)
            .AddMenu("Sipariş Durumunu Güncelle  (Hazırlanıyor, Hazır, Teslim Edildi vs.) ",KitchenEndPoints.StatusUpdate);
        _UserMenu
            .AddMenu("Ürünleri Listele", UserEndPoint.ListOfProducts)
            .AddMenu("Sipariş Oluştur", () => UserEndPoint.CreateOrder(_loggedInUser));
        _menu.Show();
    }                  
    private static void LoginMenu()
    {
        var username = Helper.Ask("Kullanıcı Adınız");
        var password = Helper.AskPassword("Şifre");
        var email = Helper.Ask("E-posta");

        var status = Auth.Login(username, password, email, out _loggedInUser);

        switch (status)
        {
            case Auth.LoginStatus.LoggedIn:
                Helper.ShowSuccessMsg($"Hoş geldiniz {_loggedInUser.Name} ({_loggedInUser.Role})");
                RoleMenu();
                break;
            case Auth.LoginStatus.UserNotFound:
                Helper.ShowErrorMsg("Kullanıcı bulunamadı.");
                break;
            case Auth.LoginStatus.WrongCredentials:
                Helper.ShowErrorMsg("Şifre hatalı.");
                break;
        }
        Console.ReadKey();
    }
    private static void RegisterMenu()
    {
        var inputName = Helper.Ask("Ad", true);
        var inputUsername = Helper.Ask("Kullanıcı adı", true);
        var inputPassword = Helper.AskPassword("Şifre");
        var email = Helper.Ask("E-posta");

        var roleInput = Helper.Ask("Rol (Admin, Kitchen, User)");
        

        if (!Enum.TryParse<Roles>(roleInput, out var role))
        {
            Helper.ShowErrorMsg("Geçersiz rol girdiniz.");
            return;
        }

        var status = Auth.Register(inputName, inputUsername,email, inputPassword, role, out _loggedInUser);

        switch (status)
        {
            case Auth.RegisterStatus.Successful:
               Helper.ShowSuccessMsg("Kayıt başarılı.");
                RoleMenu();
                break;
            case Auth.RegisterStatus.UsernameExists:
                Helper.ShowErrorMsg("Bu kullanıcı adı zaten kayıtlı.");
                break;
        }
        Console.ReadKey();
    }
    private static void RoleMenu()
    {
        if (_loggedInUser == null)
            return;

        switch (_loggedInUser.Role)
        {
            case Roles.Admin:
                _AdminMenu.Show(); break;
            case Roles.Kitchen:
                _KitchenMenu.Show(); break;
            case Roles.User:
                _UserMenu.Show(); break;
        }
    }
    private static void Logout()
    {
        _loggedInUser = null;
        Helper.ShowInfoMsg("Çıkış yapıldı. Ana menüye yönlendiriliyorsunuz.");
        Console.ReadKey();
        _menu.Show();
    }
}

