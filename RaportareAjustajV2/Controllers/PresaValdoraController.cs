using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaportareAjustajV2;

namespace RaportareAjustajV2.Controllers
{
    public class PresaValdoraController : Controller
    {
        private readonly RaportareDbContext _context;

        public PresaValdoraController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: PresaValdora
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<PresaValdoraModel> listaDeAfisat = await _context.PresaValdoraModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: PresaValdora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels
                .FirstOrDefaultAsync(m => m.PresaValdoraModelId == id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }

            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: PresaValdora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresaValdoraModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] PresaValdoraModel presaValdoraModel)
        {
            if (ModelState.IsValid)
            {
                presaValdoraModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                presaValdoraModel.Lungime = 6;
                presaValdoraModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    presaValdoraModel.Diametru, presaValdoraModel.NrBare, presaValdoraModel.Lungime), 2);
                _context.Add(presaValdoraModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels.FindAsync(id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }
            return View(presaValdoraModel);
        }

        // POST: PresaValdora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresaValdoraModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] PresaValdoraModel presaValdoraModel)
        {
            if (id != presaValdoraModel.PresaValdoraModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presaValdoraModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresaValdoraModelExists(presaValdoraModel.PresaValdoraModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels
                .FirstOrDefaultAsync(m => m.PresaValdoraModelId == id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }

            return View(presaValdoraModel);
        }

        // POST: PresaValdora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presaValdoraModel = await _context.PresaValdoraModels.FindAsync(id);
            _context.PresaValdoraModels.Remove(presaValdoraModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresaValdoraModelExists(int id)
        {
            return _context.PresaValdoraModels.Any(e => e.PresaValdoraModelId == id);
        }
    }
}
