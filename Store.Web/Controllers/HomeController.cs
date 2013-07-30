using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstractions;
using Store.Domain.Model;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWorkFactory _factory;

        public HomeController(IUnitOfWorkFactory factory, IAccountRepository accountRepository)
        {
            _factory = factory;
            _accountRepository = accountRepository;
        }

        public ActionResult Index(int? id)
        {
            Account account = null;
            if (id.HasValue) account = _accountRepository.Get(id.Value);
            return View(account);
        }

        public ActionResult Purchases(int? id)
        {
            Account account = null;
            if (id.HasValue) account = _accountRepository.GetWithPurchases(id.Value);
            return View(account);
        }

        public PartialViewResult AccountList(int? id)
        {
            ViewBag.Id = id;
            return PartialView(_accountRepository.GetAll());
        }

        public ActionResult EditPurchases(int id)
        {
            var account = _accountRepository.GetWithPurchases(id);
            ViewBag.Id = id;
            var model = account.Purchases;
            var needToAdd = 5 - model.Count;
            var idBase = 0;
            if (model.Any())
                idBase = account.Purchases.Max(p => p.Id);
            for (int i = 0; i < needToAdd; i++)
            {
                model.Add(new Purchase { Id = ++idBase });
            }

            return View(account.Purchases);
        }
        [HttpPost]
        public ActionResult EditPurchases(List<Purchase> purchases, int id)
        {
            using (var uow = _factory.Create())
            {
                var account = _accountRepository.GetWithPurchases(id);

                var purchasesToDelete = account.Purchases.Where(p => purchases.All(x => x.Id != p.Id)).ToList();
                foreach (var item in purchasesToDelete)
                    account.Purchases.Remove(item);

                var purchasesToUpdate = account.Purchases.Where(p => purchases.Any(x => x.Id == p.Id)).ToList();
                foreach (var item in purchasesToUpdate)
                {
                    var localId = item.Id;
                    var updatedItem = purchases.First(p => p.Id == localId);
                    item.Name = updatedItem.Name;
                    item.Price = updatedItem.Price;
                    item.Count = updatedItem.Count;
                }

                var purchasesToAdd = purchases.Where(p => account.Purchases.All(x => x.Id != p.Id)).ToList();
                foreach (var item in purchasesToAdd)
                {
                    item.Id = 0;
                    account.Purchases.Add(item);
                }
                uow.Commit();
            }
            return RedirectToAction("Index", new { id });
        }
    }
}
