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
    public class FierastraieController : Controller
    {
        private readonly RaportareDbContext _context;

        public FierastraieController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Fierastraie
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<FierastraieModel> listaDeAfisat = await _context.FierastraieModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: Fierastraie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels
                .FirstOrDefaultAsync(m => m.FierastraieModelId == id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }

            return View(fierastraieModel);
        }

        // GET: Fierastraie/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Fierastraie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FierastraieModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] FierastraieModel fierastraieModel)
        {
            if (ModelState.IsValid)
            {
                fierastraieModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                fierastraieModel.Lungime = 6;
                fierastraieModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    fierastraieModel.Diametru, fierastraieModel.NrBare, fierastraieModel.Lungime), 2);
                _context.Add(fierastraieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fierastraieModel);
        }

        // GET: Fierastraie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels.FindAsync(id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }
            return View(fierastraieModel);
        }

        // POST: Fierastraie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FierastraieModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] FierastraieModel fierastraieModel)
        {
            if (id != fierastraieModel.FierastraieModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fierastraieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FierastraieModelExists(fierastraieModel.FierastraieModelId))
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
            return View(fierastraieModel);
        }

        // GET: Fierastraie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels
                .FirstOrDefaultAsync(m => m.FierastraieModelId == id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }

            return View(fierastraieModel);
        }

        // POST: Fierastraie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fierastraieModel = await _context.FierastraieModels.FindAsync(id);
            _context.FierastraieModels.Remove(fierastraieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FierastraieModelExists(int id)
        {
            return _context.FierastraieModels.Any(e => e.FierastraieModelId == id);
        }
    }
}
