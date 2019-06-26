using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaportareAjustajV2;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

namespace RaportareAjustajV2.Controllers
{
    public class ElindController : Controller
    {
        private readonly RaportareDbContext _context;

        public ElindController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Elind
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<ElindModel> listaDeAfisat = await _context.ElindModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync()
        {
            List<ElindModel> listaDeAfisat = await _context.ElindModels.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Elind");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "ElindModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Tratament";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Nr bare";
                ws.Cells["I1"].Value = "Lungime";
                ws.Cells["J1"].Value = "Masa";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.ElindModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Tratament;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.NrBare;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.Lungime;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.Masa;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
                stream.Position = 0;
                string excelName = "RaportElind.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            
        }

        // GET: Elind/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elindModel = await _context.ElindModels
                .FirstOrDefaultAsync(m => m.ElindModelId == id);
            if (elindModel == null)
            {
                return NotFound();
            }

            return View(elindModel);
        }

        // GET: Elind/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Elind/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElindModelId,UserName,DataIntroducere,Tratament,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] ElindModel elindModel)
        {            
            if (ModelState.IsValid)
            {
                elindModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                elindModel.Lungime = 6;
                elindModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    elindModel.Diametru, elindModel.NrBare, elindModel.Lungime), 2);
                _context.Add(elindModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele.";
            return View(elindModel);
        }

        // GET: Elind/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elindModel = await _context.ElindModels.FindAsync(id);
            if (elindModel == null)
            {
                return NotFound();
            }
            return View(elindModel);
        }

        // POST: Elind/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElindModelId,UserName,DataIntroducere,Tratament,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] ElindModel elindModel)
        {
            if (id != elindModel.ElindModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elindModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElindModelExists(elindModel.ElindModelId))
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
            return View(elindModel);
        }

        // GET: Elind/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elindModel = await _context.ElindModels
                .FirstOrDefaultAsync(m => m.ElindModelId == id);
            if (elindModel == null)
            {
                return NotFound();
            }

            return View(elindModel);
        }

        // POST: Elind/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elindModel = await _context.ElindModels.FindAsync(id);
            _context.ElindModels.Remove(elindModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElindModelExists(int id)
        {
            return _context.ElindModels.Any(e => e.ElindModelId == id);
        }
    }
}
