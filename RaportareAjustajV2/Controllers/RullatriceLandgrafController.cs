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

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync(string dataFrom, string dataTo)
        {
            //return Content(dataFrom + "<==>" + dataTo);
            List<RuillatriceLandgrafModel> listaSql = await _context.RuillatriceLandgrafModels.ToListAsync();
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<RuillatriceLandgrafModel> listaDeAfisat = listaSql.Where(model => CalculeAuxiliar.IsDateBetween(model.DataIntroducere, dataFrom, dataTo));

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Rullatrice Landgraf");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "RullatriceLandgrafModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Data Control";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Nr bare";
                ws.Cells["I1"].Value = "Motiv";
                ws.Cells["J1"].Value = "Lungime";
                ws.Cells["K1"].Value = "Masa";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.RuillatriceLandgrafModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.DataControl.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.NrBare;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.Motiv;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.Lungime;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.Masa;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RullatriceLandgraf.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
        public async Task<IActionResult> Create([Bind("RuillatriceLandgrafModelId,UserName,DataIntroducere,DataControl,Diametru,Calitate,Sarja,NrBare,Motiv,Lungime,Masa")] RuillatriceLandgrafModel ruillatriceLandgrafModel)
        {
            if (ModelState.IsValid)
            {
                ruillatriceLandgrafModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                ruillatriceLandgrafModel.Lungime = 6;
                ruillatriceLandgrafModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    ruillatriceLandgrafModel.Diametru, ruillatriceLandgrafModel.NrBare, ruillatriceLandgrafModel.Lungime), 2);
                _context.Add(ruillatriceLandgrafModel);
                await _context.SaveChangesAsync();
                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
            @ViewBag.UserName = ruillatriceLandgrafModel.UserName;
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
        public async Task<IActionResult> Edit(int id, [Bind("RuillatriceLandgrafModelId,UserName,DataIntroducere,DataControl,Diametru,Calitate,Sarja,NrBare,Motiv,Lungime,Masa")] RuillatriceLandgrafModel ruillatriceLandgrafModel)
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
