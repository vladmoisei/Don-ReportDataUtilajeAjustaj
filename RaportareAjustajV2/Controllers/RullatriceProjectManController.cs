using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync()
        {
            List<RullatriceProjectManModel> listaDeAfisat = await _context.RullatriceProjectManModels.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Rullatrice ProjectMan");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "RullatriceProjectManModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Diametru";
                ws.Cells["E1"].Value = "Calitate";
                ws.Cells["F1"].Value = "Sarja";
                ws.Cells["G1"].Value = "Nr bare";
                ws.Cells["H1"].Value = "Lungime";
                ws.Cells["I1"].Value = "Masa";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.RullatriceProjectManModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.NrBare;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.Lungime;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.Masa;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RullatriceProjectMan.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele.";
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
