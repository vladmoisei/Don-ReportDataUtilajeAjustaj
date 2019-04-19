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
    public class NovafluxController : Controller
    {
        private readonly RaportareDbContext _context;

        public NovafluxController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Novaflux
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<NovafluxModel> listaDeAfisat = await _context.NovafluxModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: Novaflux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels
                .FirstOrDefaultAsync(m => m.NovafluxModelId == id);
            if (novafluxModel == null)
            {
                return NotFound();
            }

            return View(novafluxModel);
        }

        // GET: Novaflux/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Novaflux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NovafluxModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,DefectEtalon,NrBareConform,MasaConform,NrBareNeConform,MasaNeConform")] NovafluxModel novafluxModel)
        {
            if (ModelState.IsValid)
            {
                novafluxModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                novafluxModel.MasaConform = Math.Round(CalculeAuxiliar.CalculMasa(
                    novafluxModel.Diametru, novafluxModel.NrBareConform, 6), 2);
                novafluxModel.MasaNeConform = Math.Round(CalculeAuxiliar.CalculMasa(
                    novafluxModel.Diametru, novafluxModel.NrBareNeConform, 6), 2);
                _context.Add(novafluxModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novafluxModel);
        }

        // GET: Novaflux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels.FindAsync(id);
            if (novafluxModel == null)
            {
                return NotFound();
            }
            return View(novafluxModel);
        }

        // POST: Novaflux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NovafluxModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,DefectEtalon,NrBareConform,MasaConform,NrBareNeConform,MasaNeConform")] NovafluxModel novafluxModel)
        {
            if (id != novafluxModel.NovafluxModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novafluxModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovafluxModelExists(novafluxModel.NovafluxModelId))
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
            return View(novafluxModel);
        }

        // GET: Novaflux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels
                .FirstOrDefaultAsync(m => m.NovafluxModelId == id);
            if (novafluxModel == null)
            {
                return NotFound();
            }

            return View(novafluxModel);
        }

        // POST: Novaflux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novafluxModel = await _context.NovafluxModels.FindAsync(id);
            _context.NovafluxModels.Remove(novafluxModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovafluxModelExists(int id)
        {
            return _context.NovafluxModels.Any(e => e.NovafluxModelId == id);
        }
    }
}
