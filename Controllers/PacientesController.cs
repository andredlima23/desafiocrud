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
    public class PacientesController : Controller
    {
        private BDIntelectahEntities1 db = new BDIntelectahEntities1();

        // GET: Pacientes
        public async Task<ActionResult> Index()
        {
            return View(await db.pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = await db.pacientes.FindAsync(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_paciente,nome,cpf,data_de_nascimento,sexo,telefone,email")] paciente paciente)
        {
            if (validarCpf(paciente.cpf) && (validarDataNascimento(paciente.data_de_nascimento)))
            {
                if (ModelState.IsValid)
                {
                    db.pacientes.Add(paciente);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Console.WriteLine("CPF Inválido!!!");
            }

            return View(paciente);
        }

        private bool validarCpf(string cpf)
        {
            if (cpf.Length != 11)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool validarDataNascimento(DateTime data_nascimento)
        {
            if (data_nascimento.Year < 1900 && data_nascimento.Year > 2020)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: Pacientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = await db.pacientes.FindAsync(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_paciente,nome,cpf,data_de_nascimento,sexo,telefone,email")] paciente paciente)
        {
            if (editarcpf(paciente.cpf))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(paciente).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("CPF Inválido!!!");
                }
            }
            return View(paciente);
        }
        private bool editarcpf(string cpf)
        {
            if (cpf.Length != 11)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: Pacientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = await db.pacientes.FindAsync(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            paciente paciente = await db.pacientes.FindAsync(id);
            db.pacientes.Remove(paciente);
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
