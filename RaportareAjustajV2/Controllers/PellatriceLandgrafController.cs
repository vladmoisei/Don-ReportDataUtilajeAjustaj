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
    public class PellatriceLandgrafController : Controller
    {
        private readonly RaportareDbContext _context;

        public PellatriceLandgrafController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: PellatriceLandgraf
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<PellatriceLandgrafModel> listaDeAfisat = await _context.PellatriceLandgrafModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // GET: PellatriceLandgraf/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pellatriceLandgrafModel = await _context.PellatriceLandgrafModels
                .FirstOrDefaultAsync(m => m.PellatriceLandgrafModelId == id);
            if (pellatriceLandgrafModel == null)
            {
                return NotFound();
            }

            return View(pellatriceLandgrafModel);
        }

        // GET: PellatriceLandgraf/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: PellatriceLandgraf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PellatriceLandgrafModelId,UserName,DataIntroducere,DiametruIntrare,DiametruIesire,Calitate,Sarja,NrBare,Lungime,Masa")] PellatriceLandgrafModel pellatriceLandgrafModel)
        {
            if (ModelState.IsValid)
            {
                pellatriceLandgrafModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                pellatriceLandgrafModel.Lungime = 6;
                pellatriceLandgrafModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    (int)pellatriceLandgrafModel.DiametruIntrare, pellatriceLandgrafModel.NrBare, pellatriceLandgrafModel.Lungime), 2);
                _context.Add(pellatriceLandgrafModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pellatriceLandgrafModel);
        }

        // GET: PellatriceLandgraf/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pellatriceLandgrafModel = await _context.PellatriceLandgrafModels.FindAsync(id);
            if (pellatriceLandgrafModel == null)
            {
                return NotFound();
            }
            return View(pellatriceLandgrafModel);
        }

        // POST: PellatriceLandgraf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PellatriceLandgrafModelId,UserName,DataIntroducere,DiametruIntrare,DiametruIesire,Calitate,Sarja,NrBare,Lungime,Masa")] PellatriceLandgrafModel pellatriceLandgrafModel)
        {
            if (id != pellatriceLandgrafModel.PellatriceLandgrafModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pellatriceLandgrafModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PellatriceLandgrafModelExists(pellatriceLandgrafModel.PellatriceLandgrafModelId))
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
            return View(pellatriceLandgrafModel);
        }

        // GET: PellatriceLandgraf/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pellatriceLandgrafModel = await _context.PellatriceLandgrafModels
                .FirstOrDefaultAsync(m => m.PellatriceLandgrafModelId == id);
            if (pellatriceLandgrafModel == null)
            {
                return NotFound();
            }

            return View(pellatriceLandgrafModel);
        }

        // POST: PellatriceLandgraf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pellatriceLandgrafModel = await _context.PellatriceLandgrafModels.FindAsync(id);
            _context.PellatriceLandgrafModels.Remove(pellatriceLandgrafModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PellatriceLandgrafModelExists(int id)
        {
            return _context.PellatriceLandgrafModels.Any(e => e.PellatriceLandgrafModelId == id);
        }
    }
}
