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
    public class RullatriceLandgrafController : Controller
    {
        private readonly RaportareDbContext _context;

        public RullatriceLandgrafController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: RullatriceLandgraf
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<RuillatriceLandgrafModel> listaDeAfisat = await _context.RuillatriceLandgrafModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: RullatriceLandgraf/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruillatriceLandgrafModel = await _context.RuillatriceLandgrafModels
                .FirstOrDefaultAsync(m => m.RuillatriceLandgrafModelId == id);
            if (ruillatriceLandgrafModel == null)
            {
                return NotFound();
            }

            return View(ruillatriceLandgrafModel);
        }

        // GET: RullatriceLandgraf/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: RullatriceLandgraf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RuillatriceLandgrafModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] RuillatriceLandgrafModel ruillatriceLandgrafModel)
        {
            if (ModelState.IsValid)
            {
                ruillatriceLandgrafModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                ruillatriceLandgrafModel.Lungime = 6;
                ruillatriceLandgrafModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    ruillatriceLandgrafModel.Diametru, ruillatriceLandgrafModel.NrBare, ruillatriceLandgrafModel.Lungime), 2);
                _context.Add(ruillatriceLandgrafModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ruillatriceLandgrafModel);
        }

        // GET: RullatriceLandgraf/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruillatriceLandgrafModel = await _context.RuillatriceLandgrafModels.FindAsync(id);
            if (ruillatriceLandgrafModel == null)
            {
                return NotFound();
            }
            return View(ruillatriceLandgrafModel);
        }

        // POST: RullatriceLandgraf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RuillatriceLandgrafModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] RuillatriceLandgrafModel ruillatriceLandgrafModel)
        {
            if (id != ruillatriceLandgrafModel.RuillatriceLandgrafModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruillatriceLandgrafModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RuillatriceLandgrafModelExists(ruillatriceLandgrafModel.RuillatriceLandgrafModelId))
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
            return View(ruillatriceLandgrafModel);
        }

        // GET: RullatriceLandgraf/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruillatriceLandgrafModel = await _context.RuillatriceLandgrafModels
                .FirstOrDefaultAsync(m => m.RuillatriceLandgrafModelId == id);
            if (ruillatriceLandgrafModel == null)
            {
                return NotFound();
            }

            return View(ruillatriceLandgrafModel);
        }

        // POST: RullatriceLandgraf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruillatriceLandgrafModel = await _context.RuillatriceLandgrafModels.FindAsync(id);
            _context.RuillatriceLandgrafModels.Remove(ruillatriceLandgrafModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RuillatriceLandgrafModelExists(int id)
        {
            return _context.RuillatriceLandgrafModels.Any(e => e.RuillatriceLandgrafModelId == id);
        }
    }
}
