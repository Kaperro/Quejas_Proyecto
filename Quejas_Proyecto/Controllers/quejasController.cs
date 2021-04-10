using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quejas_Proyecto.context;

namespace Quejas_Proyecto.Controllers
{
    public class quejasController : Controller
    {
        private quejasEntities db = new quejasEntities();

        // GET: quejas
        public ActionResult Index()
        {
            var quejas = db.quejas.Include(q => q.sucursal);
            return View(quejas.ToList());
        }

        // GET: quejas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            queja queja = db.quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        public List<departamento> GetDepartamentoList()
        {

            List<departamento> departamentos = db.departamentoes.ToList();
            return departamentos;
        }
        public ActionResult GetMunicipioSucursal(int Depa)
        {
            List<municipio> selectlist = db.municipios.Where(m => m.iddepartamento == Depa).ToList();
            ViewBag.MuniListSucursal = new SelectList(selectlist, "idmunicipio", "nombre_municipio");
            return PartialView("DisplayMuniSucursal");
        }
        public ActionResult GetSucursal(int ComeId,int Munici_id)
        {
            List<sucursal> seleclist = db.sucursals.Where(m => m.idcomercio == ComeId).Where(m => m.idmunicipio == Munici_id).ToList();
            ViewBag.sucursalList = new SelectList(seleclist, "idsucursal", "nombre_sucursal");
            return PartialView("DisplaySucursal");
        }
        public ActionResult GetComercio(int Munic)
        {
            List<sucursal> seleclist = db.sucursals.Where(m => m.idmunicipio == Munic).ToList();
          /*  var fields = new List<SelectListItem>();
            for (int i = 0; i < seleclist.Count; i++)
            {

                fields.Add(
                    new SelectListItem
                    {
                        Text = seleclist[i].comercio.nombre_comercio,
                        Value = seleclist[i].comercio.idcomercio.ToString()
                    }); ;
            }*/
            ViewBag.SucursalComercio = new SelectList(seleclist, "comercio.idcomercio","comercio.nombre_comercio");
            //ViewBag.SucursalComercio = fields;
            return PartialView("DisplayComercio");
        }

        // GET: quejas/Create
        public ActionResult Create()
        {
            ViewBag.idsucursal = new SelectList(db.sucursals, "idsucursal", "nombre_sucursal");
            ViewBag.GetDepa = new SelectList(GetDepartamentoList(), "iddepartamento", "nombre_departamento");
            return View();
        }

        // POST: quejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( queja_dep_sucursal queja)
        {
            if (ModelState.IsValid)
            {
                db.quejas.Add(queja.QuejaView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idsucursal = new SelectList(db.sucursals, "idsucursal", "nombre_sucursal", queja.QuejaView.idsucursal);
            ViewBag.GetDepa = new SelectList(GetDepartamentoList(), "iddepartamento", "nombre_departamento");
            return View(queja.QuejaView);
        }

        // GET: quejas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            queja queja = db.quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            ViewBag.idsucursal = new SelectList(db.sucursals, "idsucursal", "nombre_sucursal", queja.idsucursal);
            return View(queja);
        }

        // POST: quejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idqueja,idsucursal,queja1,fecha_queja")] queja queja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idsucursal = new SelectList(db.sucursals, "idsucursal", "nombre_sucursal", queja.idsucursal);
            return View(queja);
        }

        // GET: quejas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            queja queja = db.quejas.Find(id);
            if (queja == null)
            {
                return HttpNotFound();
            }
            return View(queja);
        }

        // POST: quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            queja queja = db.quejas.Find(id);
            db.quejas.Remove(queja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
