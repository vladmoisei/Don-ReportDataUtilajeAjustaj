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
    public class GaddaController : Controller
    {
        private readonly RaportareDbContext _context;

        public GaddaController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Gadda
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<GaddaModel> listaDeAfisat = await _context.GaddaModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync()
        {
            List<GaddaModel> listaDeAfisat = await _context.GaddaModels.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Elti");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "GaddaModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Tratament";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Nr bare";
                ws.Cells["I1"].Value = "Lungime";
                ws.Cells["J1"].Value = "Data Incarcare";
                ws.Cells["K1"].Value = "Ora Incarcare";
                ws.Cells["L1"].Value = "Data Descarcare";
                ws.Cells["M1"].Value = "Ora Descarcare";
                ws.Cells["N1"].Value = "Consum Gaz";
                ws.Cells["O1"].Value = "Consum Electricitate";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.GaddaModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.TratamentTermic;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.NumarBare;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.LungimeBare;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.DataIncarcare;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.OraIncarcare;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = elem.DataDescarcare;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = elem.OraDescarcare;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = elem.ConsumGaz;
                    ws.Cells[string.Format("O{0}", rowStart)].Value = elem.ConsumElectricitate;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportGadda.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: Gadda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gaddaModel = await _context.GaddaModels
                .FirstOrDefaultAsync(m => m.GaddaModelId == id);
            if (gaddaModel == null)
            {
                return NotFound();
            }

            return View(gaddaModel);
        }

        // GET: Gadda/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Gadda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GaddaModelId,UserName,DataIntroducere,Cuptor,TratamentTermic,Diametru,Calitate,Sarja,NumarBare,LungimeBare,Masa,DataIncarcare,OraIncarcare,DataDescarcare,OraDescarcare,ConsumGaz,ConsumElectricitate")] GaddaModel gaddaModel)
        {
            if (ModelState.IsValid)
            {
                gaddaModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                gaddaModel.LungimeBare = 6;
                gaddaModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    gaddaModel.Diametru, gaddaModel.NumarBare, gaddaModel.LungimeBare), 2);
                _context.Add(gaddaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gaddaModel);
        }

        // GET: Gadda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gaddaModel = await _context.GaddaModels.FindAsync(id);
            if (gaddaModel == null)
            {
                return NotFound();
            }
            return View(gaddaModel);
        }

        // POST: Gadda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GaddaModelId,UserName,DataIntroducere,Cuptor,TratamentTermic,Diametru,Calitate,Sarja,NumarBare,LungimeBare,Masa,DataIncarcare,OraIncarcare,DataDescarcare,OraDescarcare,ConsumGaz,ConsumElectricitate")] GaddaModel gaddaModel)
        {
            if (id != gaddaModel.GaddaModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gaddaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaddaModelExists(gaddaModel.GaddaModelId))
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
            return View(gaddaModel);
        }

        // GET: Gadda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gaddaModel = await _context.GaddaModels
                .FirstOrDefaultAsync(m => m.GaddaModelId == id);
            if (gaddaModel == null)
            {
                return NotFound();
            }

            return View(gaddaModel);
        }

        // POST: Gadda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gaddaModel = await _context.GaddaModels.FindAsync(id);
            _context.GaddaModels.Remove(gaddaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaddaModelExists(int id)
        {
            return _context.GaddaModels.Any(e => e.GaddaModelId == id);
        }
    }
}
