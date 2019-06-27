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

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync(string dataFrom, string dataTo)
        {
            return Content(dataFrom + "<==>" + dataTo);
            List<EltiModel> listaDeAfisat = await _context.EltiModels.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Elti");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "EltiModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Cuptor";
                ws.Cells["E1"].Value = "Tratament";
                ws.Cells["F1"].Value = "Data Incarcare";
                ws.Cells["G1"].Value = "Ora Incarcare";
                ws.Cells["H1"].Value = "Data Descarcare";
                ws.Cells["I1"].Value = "Ora Descarcare";
                ws.Cells["J1"].Value = "Consum Gaz";
                ws.Cells["K1"].Value = "Consum Electricitate";
                ws.Cells["L1"].Value = "Diametru";
                ws.Cells["M1"].Value = "Calitate";
                ws.Cells["N1"].Value = "Sarja";
                ws.Cells["O1"].Value = "Nr bare";
                ws.Cells["P1"].Value = "Lungime";
                ws.Cells["Q1"].Value = "Masa";


                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.EltiModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Cuptor;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.TratamentTermic;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.DataIncarcare;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.OraIncarcare;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.DataDescarcare;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.OraDescarcare;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.ConsumGaz;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.ConsumElectricitate;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("O{0}", rowStart)].Value = elem.NumarBare;
                    ws.Cells[string.Format("P{0}", rowStart)].Value = elem.LungimeBare;
                    ws.Cells[string.Format("Q{0}", rowStart)].Value = elem.Masa;
                    rowStart++;                    
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportElti.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
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
