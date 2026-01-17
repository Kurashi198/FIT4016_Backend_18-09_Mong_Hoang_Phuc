public class Order
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string OrderNumber { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    public string CustomerName { get; set; }

    [Required, EmailAddress]
    public string CustomerEmail { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public Product Product { get; set; }
}
