using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tickets.Models;
using Tickets.Models.Repository;
using Tickets.Models.Core;

namespace Tickets.Controllers
{
    public class RailwayController : Controller
    {
        private IPassengersRepository repository = null;
        private IPassengerTypesRepository repositoryPassengerTypes = null;
        public RailwayController()
        {
            this.repository = new PassengersRepository();
            this.repositoryPassengerTypes = new PassengerTypesRepository();
        }
        
        // зависимости внедрим через конструктор, тот самый "D" из "SOLID"
        public RailwayController(IPassengersRepository repository, IPassengerTypesRepository repositoryPassengerTypes)
        {
            this.repository = repository;
            this.repositoryPassengerTypes = repositoryPassengerTypes;
        }

        // GET: /Railway/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Railway/Info?id=X
        // вообще добавление /Add и изменение /Edit обычно висят на разных url и в разных экшенах и сразу видно где и что,
        // но в т.з. прописано так, чтобы всё висело на /info, поэтому разлепливаем формы внутри одно экшена
        public ActionResult Info(int? id)
        {
            Passengers model = new Passengers();

            IEnumerable<SelectListItem> modelPassengerTypes = Enumerable.Empty<SelectListItem>();

            model.PassengerTypesSelectList = modelPassengerTypes;

            if (id == null)
            {
                // форма добавления

                modelPassengerTypes = repositoryPassengerTypes.SelectAll().ToSelectListItems(1);

                model.PassengerTypesSelectList = modelPassengerTypes;

                return View("InfoAdd", model);
            }
            else
            {
                // форма редактирования

                model = (Passengers)repository.SelectByID((int)id);

                // не нашли в бд такой записи
                if (model == null)
                {
                    return HttpNotFound();
                }

                modelPassengerTypes = repositoryPassengerTypes.SelectAll().ToSelectListItems(model.TypeID);

                model.PassengerTypesSelectList = modelPassengerTypes;

                return View("InfoEdit", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(Passengers passenger)
        {
            Passengers model = new Passengers();

            IEnumerable<SelectListItem> modelPassengerTypes = Enumerable.Empty<SelectListItem>();

            if (ModelState.IsValid)
            {
                // форма редактирования
                if (passenger.id > 0)
                {
                    repository.Update(passenger);
                }
                else
                {
                    // форма добавления
                    repository.Insert(passenger);
                }

                repository.Save();

                return RedirectToAction("passengers");
            }
            else // валидация данных не прошла, снова показываем форму
            {
                if (passenger.id > 0)
                {
                    model = (Passengers)repository.SelectByID(passenger.id);

                    // не нашли в бд такой записи
                    if (model == null)
                    {
                        return HttpNotFound();
                    }

                    modelPassengerTypes = repositoryPassengerTypes.SelectAll().ToSelectListItems(model.TypeID);

                    model.PassengerTypesSelectList = modelPassengerTypes;

                    return View("InfoEdit", model);
                }
                else
                {
                    model = passenger;

                    modelPassengerTypes = repositoryPassengerTypes.SelectAll().ToSelectListItems(passenger.TypeID);

                    model.PassengerTypesSelectList = modelPassengerTypes;

                    return View("InfoAdd", model);
                }
            }
        }

        // GET: /Railway/Passengers
        public ActionResult Passengers()
        {
            List<Passengers> model = (List<Passengers>)repository.SelectAll().OrderBy(x => x.Created).ToList();

            return View(model);
        }
	}
}