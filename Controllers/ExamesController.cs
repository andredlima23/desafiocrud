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
    public class ExamesController : Controller
    {
        private BDIntelectahEntities1 db = new BDIntelectahEntities1();

        // GET: Exames
        public async Task<ActionResult> Index()
        {
            var exames = db.exames.Include(e => e.tipoexame);
            return View(await exames.ToListAsync());
        }

        // GET: Exames/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exame exame = await db.exames.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome");
            return View();
        }

        // POST: Exames/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_exame,nome,observacao,id_tipoexame")] exame exame)
        {
            if (ModelState.IsValid)
            {
                db.exames.Add(exame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", exame.id_tipoexame);
            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exame exame = await db.exames.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", exame.id_tipoexame);
            return View(exame);
        }

        // POST: Exames/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_exame,nome,observacao,id_tipoexame")] exame exame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_tipoexame = new SelectList(db.tipoexames, "id_tipoexame", "nome", exame.id_tipoexame);
            return View(exame);
        }

        // GET: Exames/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exame exame = await db.exames.FindAsync(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            exame exame = await db.exames.FindAsync(id);
            db.exames.Remove(exame);
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
