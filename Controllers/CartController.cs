using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazyn.Data;
using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class CartController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Pobierz zawartość koszyka (bez autoryzacji)
        var cartItems = await _context.CartItems
            .Include(ci => ci.Products)
            .ToListAsync();

        return View(cartItems);
    }

    [Authorize]
    public async Task<IActionResult> AddToCart(int productId)
    {
        // Pobierz zalogowanego użytkownika
        var user = await _userManager.GetUserAsync(User);

        // Dodaj produkt do koszyka
        var product = await _context.Products.FindAsync(productId);

        if (product != null)
        {
            var cartItem = new CartItems
            {
                UserId = user.Id,
                ProductId = productId,
                Quantity = 1 // Domyślnie dodajemy jedną sztukę
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Checkout()
    {
        // Pobierz zawartość koszyka (bez autoryzacji)
        var cartItems = await _context.CartItems
            .Include(ci => ci.Products)
            .ToListAsync();

        if (cartItems.Count == 0)
        {
            // Jeśli koszyk jest pusty, zwróć widok z informacją, że nie można złożyć pustego zamówienia
            return View("EmptyCart");
        }

        // Pobierz zalogowanego użytkownika
        var user = await _userManager.GetUserAsync(User);

        // Możesz dodać logikę do obliczania całkowitej ceny zamówienia na podstawie zawartości koszyka
        double totalAmount = cartItems.Sum(ci => ci.Products.Price * ci.Quantity);

        // Tworzenie nowego zamówienia
        var order = new Orders
        {
            UserId = user.Id, // Przypisz UserId do zamówienia
            TotalPrice = totalAmount,
            Status = "Pending", // Możesz ustawić status zamówienia
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Dodaj produkty zamówienia do bazy danych
        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItems
            {
                OrderId = order.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Products.Price
            };

            _context.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();

        // Usuń zawartość koszyka po złożeniu zamówienia
        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        // Przykład: Zwróć widok potwierdzenia zamówienia wraz z informacjami o zamówieniu
        return View("OrderConfirmation", order);
    }

}
