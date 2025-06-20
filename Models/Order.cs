namespace OrderManagementSystem.Models;

public class Order
{
    public int Id { get; set; }

    // Siparişi veren kullanıcı
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    // Hazırlanıyor, Hazır, Teslim Edildi gibi durumlar
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // Sipariş detayları (ürünler, adet, fiyat vs.)
    public List<OrderItem> OrderItems { get; set; } = new();
}