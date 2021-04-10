using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quejas_Proyecto.context;

namespace Quejas_Proyecto.Controllers
{
    public class sucursalController : Controller
    {
        private quejasEntities db = new quejasEntities();

        // GET: sucursal
        public ActionResult Index()
        {
            var sucursals = db.sucursals.Include(s => s.comercio).Include(s => s.municipio);
            return View(sucursals.ToList());
        
        }
    

     
        public List<departamento> GetDepartamentoList()
        {
           
            List<departamento> departamentos = db.departamentoes.ToList();
            return departamentos;
        }
        public ActionResult GetMunicipio(int Depa)
        {
            List<municipio> selectlist = db.municipios.Where(m => m.iddepartamento == Depa).ToList();
            ViewBag.MuniList = new SelectList(selectlist, "idmunicipio", "nombre_municipio");
            return PartialView("DisplayMuni");
        }

        // GET: sucursal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // GET: sucursal/Create
        public ActionResult Create()
        {
            ViewBag.idcomercio = new SelectList(db.comercios, "idcomercio", "nombre_comercio");
            ViewBag.idmunicipio = new SelectList(db.municipios, "idmunicipio", "nombre_municipio");
             ViewBag.depas = new SelectList(GetDepartamentoList(), "iddepartamento", "nombre_departamento");
            ViewBag.Message = "Datos ingresados correctamente";
            return View();
        }

        // POST: sucursal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(depa_sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.sucursals.Add(sucursal.SucursalView);
                db.SaveChanges();
                return RedirectToAction("Create");
                
            }
            
            ViewBag.idcomercio = new SelectList(db.comercios, "idcomercio", "nombre_comercio", sucursal.SucursalView.idcomercio);
            ViewBag.idmunicipio = new SelectList(db.municipios, "idmunicipio", "nombre_municipio", sucursal.SucursalView.idmunicipio);
            ViewBag.depas = new SelectList(GetDepartamentoList(), "iddepartamento", "nombre_departamento");
            return View(sucursal.SucursalView);
        }

        // GET: sucursal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcomercio = new SelectList(db.comercios, "idcomercio", "nombre_comercio", sucursal.idcomercio);
            ViewBag.idmunicipio = new SelectList(db.municipios, "idmunicipio", "nombre_municipio", sucursal.idmunicipio);
            return View(sucursal);
        }

        // POST: sucursal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsucursal,idcomercio,idmunicipio,nombre_sucursal")] sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcomercio = new SelectList(db.comercios, "idcomercio", "nombre_comercio", sucursal.idcomercio);
            ViewBag.idmunicipio = new SelectList(db.municipios, "idmunicipio", "nombre_municipio", sucursal.idmunicipio);
            return View(sucursal);
        }

        // GET: sucursal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sucursal sucursal = db.sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sucursal sucursal = db.sucursals.Find(id);
            db.sucursals.Remove(sucursal);
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
