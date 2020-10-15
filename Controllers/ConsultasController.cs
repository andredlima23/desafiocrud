using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ConsultasController : Controller
    {
        private BDIntelectahEntities1 db = new BDIntelectahEntities1();

        // GET: Consultas
        public async Task<ActionResult> Index()
        {
            var consultas = db.consultas.Include(c => c.paciente).Include(c => c.tipoexame);
            return View(await consultas.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consulta consulta = await db.consultas.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.id_paciente = new SelectList(db.pacientes, "id_paciente", "nome");
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome");
            return View();
        }

        // POST: Consultas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "protocolo,data_hora,id_tipoexame,id_paciente")] consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.consultas.Add(consulta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_paciente = new SelectList(db.pacientes, "id_paciente", "nome", consulta.id_paciente);
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", consulta.id_tipoexame);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consulta consulta = await db.consultas.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_paciente = new SelectList(db.pacientes, "id_paciente", "nome", consulta.id_paciente);
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", consulta.id_tipoexame);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "protocolo,data_hora,id_tipoexame,id_paciente")] consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_paciente = new SelectList(db.pacientes, "id_paciente", "nome", consulta.id_paciente);
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", consulta.id_tipoexame);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consulta consulta = await db.consultas.FindAsync(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            consulta consulta = await db.consultas.FindAsync(id);
            db.consultas.Remove(consulta);
            await db.SaveChangesAsync();
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
