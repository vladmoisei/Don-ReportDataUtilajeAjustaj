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
    public class EltiController : Controller
    {
        private readonly RaportareDbContext _context;

        public EltiController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Elti
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<EltiModel> listaDeAfisat = await _context.EltiModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: Elti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eltiModel = await _context.EltiModels
                .FirstOrDefaultAsync(m => m.EltiModelId == id);
            if (eltiModel == null)
            {
                return NotFound();
            }

            return View(eltiModel);
        }

        // GET: Elti/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Elti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EltiModelId,UserName,DataIntroducere,Cuptor,TratamentTermic,Diametru,Calitate,Sarja,NumarBare,LungimeBare,Masa,DataIncarcare,OraIncarcare,DataDescarcare,OraDescarcare,ConsumGaz,ConsumElectricitate")] EltiModel eltiModel)
        {
            if (ModelState.IsValid)
            {
                eltiModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                eltiModel.LungimeBare = 6;
                eltiModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    eltiModel.Diametru, eltiModel.NumarBare, eltiModel.LungimeBare), 2);
                _context.Add(eltiModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eltiModel);
        }

        // GET: Elti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eltiModel = await _context.EltiModels.FindAsync(id);
            if (eltiModel == null)
            {
                return NotFound();
            }
            return View(eltiModel);
        }

        // POST: Elti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EltiModelId,UserName,DataIntroducere,Cuptor,TratamentTermic,Diametru,Calitate,Sarja,NumarBare,LungimeBare,Masa,DataIncarcare,OraIncarcare,DataDescarcare,OraDescarcare,ConsumGaz,ConsumElectricitate")] EltiModel eltiModel)
        {
            if (id != eltiModel.EltiModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eltiModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EltiModelExists(eltiModel.EltiModelId))
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
            return View(eltiModel);
        }

        // GET: Elti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eltiModel = await _context.EltiModels
                .FirstOrDefaultAsync(m => m.EltiModelId == id);
            if (eltiModel == null)
            {
                return NotFound();
            }

            return View(eltiModel);
        }

        // POST: Elti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eltiModel = await _context.EltiModels.FindAsync(id);
            _context.EltiModels.Remove(eltiModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EltiModelExists(int id)
        {
            return _context.EltiModels.Any(e => e.EltiModelId == id);
        }
    }
}
