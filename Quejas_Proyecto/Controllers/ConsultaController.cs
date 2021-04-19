using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySqlConnector;
using Quejas_Proyecto.context;

namespace Quejas_Proyecto.Controllers
{
    public class ConsultaController : Controller
    {
        private quejasEntities db = new quejasEntities();
        // GET: Consulta
        public ActionResult Consulta_btn()

        {



            string cs = @"server=localhost;user id=root;password=pass;persistsecurityinfo=True;database=quejas";
            var con = new MySql.Data.MySqlClient.MySqlConnection(cs); con.Open();
            var stm = "select count(queja.queja) as QUEJA , comercio1.nombre_comercio as COMERCIO , comercio1.idcomercio   " +
                "from comercio as comercio1 " +
                "inner join sucursal on comercio1.idcomercio = sucursal.idcomercio " +
                "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                "inner join municipio on sucursal.idmunicipio = municipio.idmunicipio " +
                "inner join departamento on municipio.iddepartamento = departamento.iddepartamento " +
                "inner join region on departamento.idregion = region.idregion " +
                "where queja.fecha_queja BETWEEN CAST('2021-01-01' AS DATE) AND CAST('2021-12-31' AS DATE) and ( " +
                "select count(comercio.idcomercio)   from comercio " +
                     "inner join sucursal on comercio.idcomercio = sucursal.idcomercio " +
                     "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                     "inner join municipio on sucursal.idmunicipio = municipio.idmunicipio " +
                     "inner join departamento on municipio.iddepartamento = departamento.iddepartamento " +
                     "inner join region on departamento.idregion = region.idregion  " +
                     "where queja.fecha_queja BETWEEN CAST('2021-01-01' AS DATE) AND CAST('2021-12-31' AS DATE) and departamento.idregion = 3 and comercio.idcomercio = comercio1.idcomercio) = 0  " +
                     "group by comercio1.idcomercio";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataAdapter mySQLDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            mySQLDataAdapter.Fill(data);
            var datos = data.Tables[0].Rows;
            List<Consulta> detalles = new List<Consulta>();
            foreach (DataRow item in datos)
            {
                detalles.Add( new Consulta()
                {
                Queja = item.ItemArray[0].ToString(),
                 Comercio = item.ItemArray[1].ToString(),
                Id_comercio = item.ItemArray[2].ToString()
                });
         
            }









            return View(detalles);
        }
        public ActionResult PorMunicipio()
        {

            string cs = @"server=localhost;user id=root;password=pass;persistsecurityinfo=True;database=quejas";
            var con = new MySql.Data.MySqlClient.MySqlConnection(cs); con.Open();
            var stm = "select count(queja.queja) as QUEJA , municipio.nombre_municipio as Municipio , municipio.idmunicipio   from municipio  " +
                "inner join sucursal on municipio.idmunicipio = sucursal.idmunicipio " +
                "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                "inner join departamento on municipio.iddepartamento = departamento.iddepartamento " +
                "inner join region on departamento.idregion = region.idregion " +
                "group by municipio.idmunicipio";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataAdapter mySQLDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            mySQLDataAdapter.Fill(data);
            var datos = data.Tables[0].Rows;
            List<Consulta> detalles = new List<Consulta>();
            foreach (DataRow item in datos)
            {
                detalles.Add(new Consulta()
                {
                    Queja = item.ItemArray[0].ToString(),
                    Comercio = item.ItemArray[1].ToString(),
                    Id_comercio = item.ItemArray[2].ToString()
                });

            }



            return View(detalles);
        }

        public ActionResult PorDepartamento()
        {

            string cs = @"server=localhost;user id=root;password=pass;persistsecurityinfo=True;database=quejas";
            var con = new MySql.Data.MySqlClient.MySqlConnection(cs); con.Open();
            var stm = "select count(queja.queja) as QUEJA , departamento.nombre_departamento as Departamento , departamento.iddepartamento   from departamento " +
                "inner join municipio on departamento.iddepartamento = municipio.iddepartamento " +
                "inner join sucursal on municipio.idmunicipio = sucursal.idmunicipio " +
                "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                "inner join region on departamento.idregion = region.idregion " +
                "group by departamento.iddepartamento";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataAdapter mySQLDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            mySQLDataAdapter.Fill(data);
            var datos = data.Tables[0].Rows;
            List<Consulta> detalles = new List<Consulta>();
            foreach (DataRow item in datos)
            {
                detalles.Add(new Consulta()
                {
                    Queja = item.ItemArray[0].ToString(),
                    Comercio = item.ItemArray[1].ToString(),
                    Id_comercio = item.ItemArray[2].ToString()
                });

            }



            return View(detalles);
        }

        public ActionResult PorRegion()
        {

            string cs = @"server=localhost;user id=root;password=pass;persistsecurityinfo=True;database=quejas";
            var con = new MySql.Data.MySqlClient.MySqlConnection(cs); con.Open();
            var stm = "select count(queja.queja) as QUEJA , region.nombre_region as Region , region.idregion  from region " +
                "inner join departamento on region.idregion = departamento.idregion " +
                "inner join municipio on departamento.iddepartamento = municipio.iddepartamento " +
                "inner join sucursal on municipio.idmunicipio = sucursal.idmunicipio " +
                "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                "group by departamento.iddepartamento";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataAdapter mySQLDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            mySQLDataAdapter.Fill(data);
            var datos = data.Tables[0].Rows;
            List<Consulta> detalles = new List<Consulta>();
            foreach (DataRow item in datos)
            {
                detalles.Add(new Consulta()
                {
                    Queja = item.ItemArray[0].ToString(),
                    Comercio = item.ItemArray[1].ToString(),
                    Id_comercio = item.ItemArray[2].ToString()
                });

            }



            return View(detalles);
        }

        public ActionResult PorComercio()
        {

            string cs = @"server=localhost;user id=root;password=pass;persistsecurityinfo=True;database=quejas";
            var con = new MySql.Data.MySqlClient.MySqlConnection(cs); con.Open();
            var stm = "select count(queja.queja) as QUEJA , comercio.nombre_comercio as COMERCIO , comercio.idcomercio   from comercio " +
                "inner join sucursal on comercio.idcomercio = sucursal.idcomercio " +
                "inner join queja on sucursal.idsucursal = queja.idsucursal " +
                "inner join municipio on sucursal.idmunicipio = municipio.idmunicipio " +
                "inner join departamento on municipio.iddepartamento = departamento.iddepartamento " +
                "inner join region on departamento.idregion = region.idregion" +
                " group by  comercio.idcomercio";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataAdapter mySQLDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            mySQLDataAdapter.Fill(data);
            var datos = data.Tables[0].Rows;
            List<Consulta> detalles = new List<Consulta>();
            foreach (DataRow item in datos)
            {
                detalles.Add(new Consulta()
                {
                    Queja = item.ItemArray[0].ToString(),
                    Comercio = item.ItemArray[1].ToString(),
                    Id_comercio = item.ItemArray[2].ToString()
                });

            }



            return View(detalles);
        }
        public ActionResult Getquejas()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Getquejas(DateTime date_ini, DateTime date_fin)
        {
            List<queja> quejas = db.quejas.Include(m=>m.sucursal.comercio).Where(m => m.fecha_queja >= date_ini &&  m.fecha_queja <= date_fin).ToList();
            ViewBag.ListaQueja = quejas;
            return PartialView("_ListaQuejaFecha");
        }
    }
}