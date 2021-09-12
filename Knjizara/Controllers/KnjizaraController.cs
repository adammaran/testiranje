using Knjizara.Logika;
using Knjizara.Models;
using Knjizara.Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class KnjizaraController : Controller
    {
        private Servis _servis;

        public KnjizaraController()
        {
            this._servis = new Servis(new Repozitorijum());
        }

        public ActionResult Index()
        {
            return View(_servis.DajSve());
        }

        public ActionResult Dodaj()
        {
            return View(new KnjigaModel());
        }

        public ActionResult Izmeni(Guid id)
        {
            Knjiga knjiga = _servis.DajPoId(id);

            return View(new KnjigaModel() { Id = knjiga.Id, Naziv = knjiga.Naziv, Cena = knjiga.Cena, Kategorija = knjiga.Kategorija.Id });
        }

        [HttpPost]
        public ActionResult Dodaj(KnjigaModel model)
        {
            if (ModelState.IsValid)
            {
                Kategorija kategorija = _servis.DajKategorijuPoId(model.Kategorija);
                Knjiga knjiga = new Knjiga(Guid.NewGuid(), model.Naziv, model.Cena, kategorija);
                _servis.Dodaj(knjiga);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Izmeni(KnjigaModel model)
        {
            if (ModelState.IsValid)
            {
                Kategorija kategorija = _servis.DajKategorijuPoId(model.Kategorija);
                Knjiga knjiga = new Knjiga(model.Id, model.Naziv, model.Cena, kategorija);
                _servis.Izmeni(knjiga);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Obrisi(Guid id)
        {
            _servis.Obrisi(id);

            return RedirectToAction("Index");
        }
        public ActionResult DodajKategoriju()
        {
            return View(new KategorijaModel());
        }

        [HttpPost]
        public ActionResult DodajKategoriju(KategorijaModel model)
        {
            if (ModelState.IsValid)
            {
                _servis.DodajKategoriju(new Kategorija(Guid.NewGuid(), model.Naziv));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public static List<SelectListItem> DropDown()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            IEnumerable<Kategorija> kategorije = new Servis(new Repozitorijum()).DajSveKategorije();
            foreach (Kategorija kategorija in kategorije)
            {
                lista.Add(new SelectListItem() { Text = kategorija.Naziv, Value = kategorija.Id.ToString() });
            }

            return lista;
        }
    }
}