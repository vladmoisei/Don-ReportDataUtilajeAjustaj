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
    public class RullatriceProjectManController : Controller
    {
        private readonly RaportareDbContext _context;

        public RullatriceProjectManController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: RullatriceProjectMan
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<RullatriceProjectManModel> listaDeAfisat = await _context.RullatriceProjectManModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: RullatriceProjectMan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rullatriceProjectManModel = await _context.RullatriceProjectManModels
                .FirstOrDefaultAsync(m => m.RullatriceProjectManModelId == id);
            if (rullatriceProjectManModel == null)
            {
                return NotFound();
            }

            return View(rullatriceProjectManModel);
        }

        // GET: RullatriceProjectMan/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: RullatriceProjectMan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RullatriceProjectManModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] RullatriceProjectManModel rullatriceProjectManModel)
        {
            if (ModelState.IsValid)
            {
                rullatriceProjectManModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                rullatriceProjectManModel.Lungime = 6;
                rullatriceProjectManModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    rullatriceProjectManModel.Diametru, rullatriceProjectManModel.NrBare, rullatriceProjectManModel.Lungime), 2);
                _context.Add(rullatriceProjectManModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rullatriceProjectManModel);
        }

        // GET: RullatriceProjectMan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rullatriceProjectManModel = await _context.RullatriceProjectManModels.FindAsync(id);
            if (rullatriceProjectManModel == null)
            {
                return NotFound();
            }
            return View(rullatriceProjectManModel);
        }

        // POST: RullatriceProjectMan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RullatriceProjectManModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] RullatriceProjectManModel rullatriceProjectManModel)
        {
            if (id != rullatriceProjectManModel.RullatriceProjectManModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rullatriceProjectManModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RullatriceProjectManModelExists(rullatriceProjectManModel.RullatriceProjectManModelId))
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
            return View(rullatriceProjectManModel);
        }

        // GET: RullatriceProjectMan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rullatriceProjectManModel = await _context.RullatriceProjectManModels
                .FirstOrDefaultAsync(m => m.RullatriceProjectManModelId == id);
            if (rullatriceProjectManModel == null)
            {
                return NotFound();
            }

            return View(rullatriceProjectManModel);
        }

        // POST: RullatriceProjectMan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rullatriceProjectManModel = await _context.RullatriceProjectManModels.FindAsync(id);
            _context.RullatriceProjectManModels.Remove(rullatriceProjectManModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RullatriceProjectManModelExists(int id)
        {
            return _context.RullatriceProjectManModels.Any(e => e.RullatriceProjectManModelId == id);
        }
    }
}
