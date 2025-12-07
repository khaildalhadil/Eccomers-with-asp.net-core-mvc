using System.Security.Claims;
using System.Text.RegularExpressions;
using BulkeyBook.DataAccess.Repository.IRepository;
using BulkeyBook.Model;
using BulkeyBook.Model.ViewModel;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyBookWeb.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        public ShoppingCartVM carts { get; set; }

        private readonly IUnitOfWork _untiOfWord;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _untiOfWord = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = _untiOfWord.product.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _untiOfWord.product.GetAll(includeProperties: "Category").ToList().Find(prodcut => prodcut.Id == id);

            ShoppingCart shoppingCart = new() { Product = product };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;
            shoppingCart.ProductId = shoppingCart.Id;
            shoppingCart.Id = 0;

            //ShoppingCart cartFromDb = _untiOfWord.shoppingCart.Get()

            _untiOfWord.shoppingCart.Add(shoppingCart);
            _untiOfWord.Save();
            TempData["success"] = "The Shopping Cart Added Successfuly";
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Cart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //List<ShoppingCart> shoppings = _untiOfWord.shoppingCart.GetAll("Product").Where(cart => cart.ApplicationUserId == userId).ToList();

            ShoppingCartVM shoppingCart = new()
            {
                ShoppingCartList = _untiOfWord.shoppingCart.GetAll(cart => cart.ApplicationUserId == userId, "Product").ToList(),
                OrderHeader = new()
            };


            foreach (var cart in shoppingCart.ShoppingCartList)
            {
                shoppingCart.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
                //shoppingCart.ShoppingCartList = price * cart.Count;
            }

            return View(shoppingCart);
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _untiOfWord.shoppingCart.Get(t=> t.Id == cartId);
            cartFromDb.Count += 1;
            _untiOfWord.shoppingCart.Update(cartFromDb);
            _untiOfWord.Save();
            return RedirectToAction("Cart");
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _untiOfWord.shoppingCart.Get(t => t.Id == cartId);

            if (cartFromDb.Count == 1)
            {
                _untiOfWord.shoppingCart.Delete(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _untiOfWord.shoppingCart.Update(cartFromDb);
            }

            _untiOfWord.Save();
            return RedirectToAction("Cart");
        }
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _untiOfWord.shoppingCart.Get(t => t.Id == cartId);
            _untiOfWord.shoppingCart.Delete(cartFromDb);
            _untiOfWord.Save();
            return RedirectToAction("Cart");
        }

        public IActionResult Summary()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //List<ShoppingCart> shoppings = _untiOfWord.shoppingCart.GetAll("Product").Where(cart => cart.ApplicationUserId == userId).ToList();

            ShoppingCartVM shoppingCart = new()
            {
                ShoppingCartList = _untiOfWord.shoppingCart.GetAll(cart => cart.ApplicationUserId == userId, "Product").ToList(),
                OrderHeader = new()
            };

            //ApplicationUser user = _untiOfWord.users.Get(userId);
            shoppingCart.OrderHeader.ApplicationUser = _untiOfWord.users.Get(t => t.Id == userId);

            //shoppingCart.OrderHeader.ApplicationUser = shoppingCart.OrderHeader.ApplicationUser.Name;
            shoppingCart.OrderHeader.Name = shoppingCart.OrderHeader.ApplicationUser.Name;
            shoppingCart.OrderHeader.PhoneNumber = shoppingCart.OrderHeader.ApplicationUser.PhoneNumber;
            shoppingCart.OrderHeader.StreetAddress = shoppingCart.OrderHeader.ApplicationUser.StreetAddress;
            shoppingCart.OrderHeader.City = shoppingCart.OrderHeader.ApplicationUser.City;
            shoppingCart.OrderHeader.State = shoppingCart.OrderHeader.ApplicationUser.State;
            shoppingCart.OrderHeader.PostalCode = shoppingCart.OrderHeader.ApplicationUser.PostalCode;

            carts = shoppingCart;

            foreach (var cart in shoppingCart.ShoppingCartList)
            {
                shoppingCart.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                //shoppingCart.ShoppingCartList = price * cart.Count;
            }
            //shoppingCart.OrderHeader.OrderTotal = price;
            return View(shoppingCart);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST(ShoppingCartVM shoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //List<ShoppingCart> shoppings = _untiOfWord.shoppingCart.GetAll("Product").Where(cart => cart.ApplicationUserId == userId).ToList();

            shoppingCartVM.ShoppingCartList = _untiOfWord.shoppingCart.GetAll(cart => cart.ApplicationUserId == userId, "Product");

            shoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            shoppingCartVM.OrderHeader.ApplicationUserId = userId;
            shoppingCartVM.OrderHeader.ApplicationUser = _untiOfWord.users.Get(t => t.Id == userId);

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                //shoppingCart.ShoppingCartList = price * cart.Count;
            }

            _untiOfWord.orderHeader.Add(shoppingCartVM.OrderHeader);
            _untiOfWord.Save();

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = shoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _untiOfWord.orderDetail.Add(orderDetail);
                _untiOfWord.Save();
            }

            shoppingCartVM.OrderHeader.PaymentStatus = "Pending";
            shoppingCartVM.OrderHeader.OrderStatus = "Pending";


            
            return RedirectToAction(nameof(PayMant), new { id = shoppingCartVM.OrderHeader.Id });
        }

        public IActionResult PayMant(int id)
        {
            return View();
        }

        [HttpPost]
        [ActionName("PayMant")]
        public IActionResult PayMantPOST(int id)
        {
            return RedirectToAction(nameof(OrderConfirmation), new {id= id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<ShoppingCart> carts = _untiOfWord.shoppingCart.GetAll(cart => cart.ApplicationUserId == userId).ToList();


            foreach (var cart in carts)
            {
                _untiOfWord.shoppingCart.Delete(cart);
                _untiOfWord.Save();
            }

            return View(id);
        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}
