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
    public class UsBarController : Controller
    {
        private readonly RaportareDbContext _context;

        public UsBarController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: UsBar
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<UsBarModel> listaDeAfisat = await _context.UsBarModels.Include(u => u.CalitateOtel).ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(model.DataIntroducere)));

        }

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync(string dataFrom, string dataTo)
        {
            //return Content(dataFrom + "<==>" + dataTo);
            List<UsBarModel> listaSql = await _context.UsBarModels.Include(u => u.CalitateOtel).ToListAsync();
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<UsBarModel> listaDeAfisat = listaSql.Where(model => CalculeAuxiliar.IsDateBetween(model.DataIntroducere.ToString("dd/MM/yyyy HH:mm"), dataFrom, dataTo));

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("UsBars");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "UsBarModelId";
                ws.Cells["B1"].Value = "User Name";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Data Control";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Furnizor";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Calitate Otel";
                ws.Cells["I1"].Value = "Stare Material";
                ws.Cells["J1"].Value = "Clasa 3";
                ws.Cells["K1"].Value = "Marime Defect [mm]";
                ws.Cells["L1"].Value = "Clasa 2";
                ws.Cells["M1"].Value = "Marime Defect [mm]";
                ws.Cells["N1"].Value = "Clasa 2+";
                ws.Cells["O1"].Value = "Marime Defect [mm]";
                ws.Cells["P1"].Value = "Clasa 1";
                ws.Cells["Q1"].Value = "Marime Defect [mm]";
                ws.Cells["R1"].Value = "Clasa SS";
                ws.Cells["S1"].Value = "Marime Defect [mm]";
                ws.Cells["T1"].Value = "Tip Discontinuitate";
                ws.Cells["U1"].Value = "Observatii";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.UsBarModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.DataControl;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Furnizor;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.CalitateOtel.Valoare;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.StareMaterial;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.Clasa3;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.MarimeDefect3;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = elem.Clasa2;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = elem.MarimeDefect2;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = elem.Clasa2Plus;
                    ws.Cells[string.Format("O{0}", rowStart)].Value = elem.MarimeDefect2Plus;
                    ws.Cells[string.Format("P{0}", rowStart)].Value = elem.Clasa1;
                    ws.Cells[string.Format("Q{0}", rowStart)].Value = elem.MarimeDefect1;
                    ws.Cells[string.Format("R{0}", rowStart)].Value = elem.SS;
                    ws.Cells[string.Format("S{0}", rowStart)].Value = elem.MarimeDefectSS;
                    ws.Cells[string.Format("T{0}", rowStart)].Value = elem.TipDiscontinuitate;
                    ws.Cells[string.Format("U{0}", rowStart)].Value = elem.Observatii;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportUsBars.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: UsBar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usBarModel = await _context.UsBarModels
                .Include(u => u.CalitateOtel)
                .FirstOrDefaultAsync(m => m.UsBarModelId == id);
            if (usBarModel == null)
            {
                return NotFound();
            }

            return View(usBarModel);
        }

        // GET: UsBar/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewData["CalitateOtelModelId"] = new SelectList(_context.CalitateOtelModels, "CalitateOtelModelId", "Valoare");
            return View();
        }

        // POST: UsBar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsBarModelId,UserName,DataControl,Diametru,Furnizor,Sarja,CalitateOtelModelId,StareMaterial,Clasa3,MarimeDefect3,Clasa2,MarimeDefect2,Clasa2Plus,MarimeDefect2Plus,Clasa1,MarimeDefect1,SS,MarimeDefectSS,TipDiscontinuitate,Observatii")] UsBarModel usBarModel)
        {
            if (ModelState.IsValid)
            {
                usBarModel.DataIntroducere = DateTime.Now;
                
                _context.Add(usBarModel);
                await _context.SaveChangesAsync();

                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalitateOtelModelId"] = new SelectList(_context.CalitateOtelModels, "CalitateOtelModelId", "Valoare", usBarModel.CalitateOtelModelId);
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
            @ViewBag.UserName = usBarModel.UserName;
            return View(usBarModel);
        }

        // GET: UsBar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usBarModel = await _context.UsBarModels.FindAsync(id);
            if (usBarModel == null)
            {
                return NotFound();
            }
            ViewData["CalitateOtelModelId"] = new SelectList(_context.CalitateOtelModels, "CalitateOtelModelId", "Valoare", usBarModel.CalitateOtelModelId);
            return View(usBarModel);
        }

        // POST: UsBar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsBarModelId,UserName,DataIntroducere,DataControl,Diametru,Furnizor,Sarja,CalitateOtelModelId,StareMaterial,Clasa3,MarimeDefect3,Clasa2,MarimeDefect2,Clasa2Plus,MarimeDefect2Plus,Clasa1,MarimeDefect1,SS,MarimeDefectSS,TipDiscontinuitate,Observatii")] UsBarModel usBarModel)
        {
            if (id != usBarModel.UsBarModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usBarModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsBarModelExists(usBarModel.UsBarModelId))
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
            ViewData["CalitateOtelModelId"] = new SelectList(_context.CalitateOtelModels, "CalitateOtelModelId", "Valoare", usBarModel.CalitateOtelModelId);
            return View(usBarModel);
        }

        // GET: UsBar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usBarModel = await _context.UsBarModels
                .Include(u => u.CalitateOtel)
                .FirstOrDefaultAsync(m => m.UsBarModelId == id);
            if (usBarModel == null)
            {
                return NotFound();
            }

            return View(usBarModel);
        }

        // POST: UsBar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usBarModel = await _context.UsBarModels.FindAsync(id);
            _context.UsBarModels.Remove(usBarModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsBarModelExists(int id)
        {
            return _context.UsBarModels.Any(e => e.UsBarModelId == id);
        }
    }
}
