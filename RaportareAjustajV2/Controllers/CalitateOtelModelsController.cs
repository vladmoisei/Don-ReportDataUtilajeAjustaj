using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaportareAjustajV2;

namespace RaportareAjustajV2.Controllers
{
    public class CalitateOtelModelsController : Controller
    {
        private readonly RaportareDbContext _context;

        public CalitateOtelModelsController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: CalitateOtelModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalitateOtelModels.ToListAsync());
        }

        public async Task<IActionResult> AdaugaValori()
        {
            _context.CalitateOtelModels.AddRange(
                    new CalitateOtelModel { Valoare = "25"},
                    new CalitateOtelModel { Valoare = "34" },
                    new CalitateOtelModel { Valoare = "35" },
                    new CalitateOtelModel { Valoare = "51" },
                    new CalitateOtelModel { Valoare = "105" },
                    new CalitateOtelModel { Valoare = "460" },
                    new CalitateOtelModel { Valoare = "41L" },
                    new CalitateOtelModel { Valoare = "41M" },
                    new CalitateOtelModel { Valoare = "LF2" },
                    new CalitateOtelModel { Valoare = "100KS" },
                    new CalitateOtelModel { Valoare = "11SM" },
                    new CalitateOtelModel { Valoare = "18NCM5" },
                    new CalitateOtelModel { Valoare = "18PA" },
                    new CalitateOtelModel { Valoare = "20B" },
                    new CalitateOtelModel { Valoare = "20VS6" },
                    new CalitateOtelModel { Valoare = "30CNM" }, 
                    new CalitateOtelModel { Valoare = "31CMV9" },
                    new CalitateOtelModel { Valoare = "34W" },
                    new CalitateOtelModel { Valoare = "37.3" },
                    new CalitateOtelModel { Valoare = "38VS6" },
                    new CalitateOtelModel { Valoare = "39NCM" },
                    new CalitateOtelModel { Valoare = "42DH" },
                    new CalitateOtelModel { Valoare = "42DI" },
                    new CalitateOtelModel { Valoare = "42DP" },
                    new CalitateOtelModel { Valoare = "42SEW" },
                    new CalitateOtelModel { Valoare = "45I" },
                    new CalitateOtelModel { Valoare = "45S" },
                    new CalitateOtelModel { Valoare = "45SEW" },
                    new CalitateOtelModel { Valoare = "50DP" },
                    new CalitateOtelModel { Valoare = "52.3" },
                    new CalitateOtelModel { Valoare = "52.3I" },
                    new CalitateOtelModel { Valoare = "52.3M" },
                    new CalitateOtelModel { Valoare = "52.3S" },
                    new CalitateOtelModel { Valoare = "80M" },
                    new CalitateOtelModel { Valoare = "817M" },
                    new CalitateOtelModel { Valoare = "CM16SEW" },
                    new CalitateOtelModel { Valoare = "CM18I" },
                    new CalitateOtelModel { Valoare = "CM18INA" },
                    new CalitateOtelModel { Valoare = "CM18S" },
                    new CalitateOtelModel { Valoare = "CM20I" },
                    new CalitateOtelModel { Valoare = "CM20S" },
                    new CalitateOtelModel { Valoare = "E0621" },
                    new CalitateOtelModel { Valoare = "E1239G" },
                    new CalitateOtelModel { Valoare = "KAL" }
                    );
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: CalitateOtelModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calitateOtelModel = await _context.CalitateOtelModels
                .FirstOrDefaultAsync(m => m.CalitateOtelModelId == id);
            if (calitateOtelModel == null)
            {
                return NotFound();
            }

            return View(calitateOtelModel);
        }

        // GET: CalitateOtelModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalitateOtelModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalitateOtelModelId,Valoare")] CalitateOtelModel calitateOtelModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calitateOtelModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calitateOtelModel);
        }

        // GET: CalitateOtelModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calitateOtelModel = await _context.CalitateOtelModels.FindAsync(id);
            if (calitateOtelModel == null)
            {
                return NotFound();
            }
            return View(calitateOtelModel);
        }

        // POST: CalitateOtelModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalitateOtelModelId,Valoare")] CalitateOtelModel calitateOtelModel)
        {
            if (id != calitateOtelModel.CalitateOtelModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calitateOtelModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalitateOtelModelExists(calitateOtelModel.CalitateOtelModelId))
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
            return View(calitateOtelModel);
        }

        // GET: CalitateOtelModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calitateOtelModel = await _context.CalitateOtelModels
                .FirstOrDefaultAsync(m => m.CalitateOtelModelId == id);
            if (calitateOtelModel == null)
            {
                return NotFound();
            }

            return View(calitateOtelModel);
        }

        // POST: CalitateOtelModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calitateOtelModel = await _context.CalitateOtelModels.FindAsync(id);
            _context.CalitateOtelModels.Remove(calitateOtelModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalitateOtelModelExists(int id)
        {
            return _context.CalitateOtelModels.Any(e => e.CalitateOtelModelId == id);
        }
    }
}
