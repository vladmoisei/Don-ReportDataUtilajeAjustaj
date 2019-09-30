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
    public class PresaValdoraController : Controller
    {
        private readonly RaportareDbContext _context;

        public PresaValdoraController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: PresaValdora
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<PresaValdoraModel> listaDeAfisat = await _context.PresaValdoraModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync(string dataFrom, string dataTo)
        {
            //return Content(dataFrom + "<==>" + dataTo);
            List<PresaValdoraModel> listaSql = await _context.PresaValdoraModels.ToListAsync();
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<PresaValdoraModel> listaDeAfisat = listaSql.Where(model => CalculeAuxiliar.IsDateBetween(model.DataIntroducere, dataFrom, dataTo));

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Presa Valdora");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "PresaValdoraModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Data indreptare material";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Eticheta";
                ws.Cells["I1"].Value = "Nr bare";
                ws.Cells["J1"].Value = "Lungime";
                ws.Cells["K1"].Value = "Masa";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.PresaValdoraModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.DataIndreptareMaterial;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.Eticheta;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.NrBare;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.Lungime;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.Masa;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportPresaVladora.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: PresaValdora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels
                .FirstOrDefaultAsync(m => m.PresaValdoraModelId == id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }

            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: PresaValdora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresaValdoraModelId,UserName,DataIntroducere,DataIndreptareMaterial,Diametru,Calitate,Sarja,Eticheta,NrBare,Lungime,Masa")] PresaValdoraModel presaValdoraModel)
        {
            if (ModelState.IsValid)
            {
                presaValdoraModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                presaValdoraModel.Lungime = 6;
                presaValdoraModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    presaValdoraModel.Diametru, presaValdoraModel.NrBare, presaValdoraModel.Lungime), 2);
                _context.Add(presaValdoraModel);
                await _context.SaveChangesAsync();
                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels.FindAsync(id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }
            return View(presaValdoraModel);
        }

        // POST: PresaValdora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresaValdoraModelId,UserName,DataIntroducere,DataIndreptareMaterial,Diametru,Calitate,Sarja,Eticheta,NrBare,Lungime,Masa")] PresaValdoraModel presaValdoraModel)
        {
            if (id != presaValdoraModel.PresaValdoraModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presaValdoraModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresaValdoraModelExists(presaValdoraModel.PresaValdoraModelId))
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
            return View(presaValdoraModel);
        }

        // GET: PresaValdora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presaValdoraModel = await _context.PresaValdoraModels
                .FirstOrDefaultAsync(m => m.PresaValdoraModelId == id);
            if (presaValdoraModel == null)
            {
                return NotFound();
            }

            return View(presaValdoraModel);
        }

        // POST: PresaValdora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presaValdoraModel = await _context.PresaValdoraModels.FindAsync(id);
            _context.PresaValdoraModels.Remove(presaValdoraModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresaValdoraModelExists(int id)
        {
            return _context.PresaValdoraModels.Any(e => e.PresaValdoraModelId == id);
        }
    }
}
