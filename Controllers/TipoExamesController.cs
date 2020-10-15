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
    public class TipoExamesController : Controller
    {
        private BDIntelectahEntities1 db = new BDIntelectahEntities1();

        // GET: TipoExames
        public async Task<ActionResult> Index()
        {
            return View(await db.tipoexames.ToListAsync());
        }

        // GET: TipoExames/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoexame tipoexame = await db.tipoexames.FindAsync(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        // GET: TipoExames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoExames/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_tipoexame,nome,descricao")] tipoexame tipoexame)
        {
            if (ModelState.IsValid)
            {
                db.tipoexames.Add(tipoexame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoexame);
        }

        // GET: TipoExames/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoexame tipoexame = await db.tipoexames.FindAsync(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        // POST: TipoExames/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_tipoexame,nome,descricao")] tipoexame tipoexame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoexame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoexame);
        }

        // GET: TipoExames/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoexame tipoexame = await db.tipoexames.FindAsync(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        // POST: TipoExames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tipoexame tipoexame = await db.tipoexames.FindAsync(id);
            db.tipoexames.Remove(tipoexame);
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
