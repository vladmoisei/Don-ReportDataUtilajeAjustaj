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
    public class FierastraieController : Controller
    {
        private readonly RaportareDbContext _context;

        public FierastraieController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Fierastraie
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<FierastraieModel> listaDeAfisat = await _context.FierastraieModels.OrderByDescending(t => t.DataIntroducere).ToListAsync();
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
            List<FierastraieModel> listaSql = await _context.FierastraieModels.ToListAsync();
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<FierastraieModel> listaDeAfisat = listaSql.Where(model => CalculeAuxiliar.IsDateBetween(model.DataIntroducere, dataFrom, dataTo));


            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Fierastraie");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "FierastraieModelId";
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
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.FierastraieModelId;
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
            string excelName = "RaportFierastraie.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: Fierastraie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels
                .FirstOrDefaultAsync(m => m.FierastraieModelId == id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }

            return View(fierastraieModel);
        }

        // GET: Fierastraie/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Fierastraie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FierastraieModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] FierastraieModel fierastraieModel)
        {
            if (ModelState.IsValid)
            {
                fierastraieModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                fierastraieModel.Lungime = 6;
                fierastraieModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    fierastraieModel.Diametru, fierastraieModel.NrBare, fierastraieModel.Lungime), 2);
                _context.Add(fierastraieModel);
                await _context.SaveChangesAsync();
                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
            @ViewBag.UserName = fierastraieModel.UserName;
            return View(fierastraieModel);
        }

        // GET: Fierastraie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels.FindAsync(id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }
            return View(fierastraieModel);
        }

        // POST: Fierastraie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FierastraieModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,NrBare,Lungime,Masa")] FierastraieModel fierastraieModel)
        {
            if (id != fierastraieModel.FierastraieModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fierastraieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FierastraieModelExists(fierastraieModel.FierastraieModelId))
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
            return View(fierastraieModel);
        }

        // GET: Fierastraie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fierastraieModel = await _context.FierastraieModels
                .FirstOrDefaultAsync(m => m.FierastraieModelId == id);
            if (fierastraieModel == null)
            {
                return NotFound();
            }

            return View(fierastraieModel);
        }

        // POST: Fierastraie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fierastraieModel = await _context.FierastraieModels.FindAsync(id);
            _context.FierastraieModels.Remove(fierastraieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FierastraieModelExists(int id)
        {
            return _context.FierastraieModels.Any(e => e.FierastraieModelId == id);
        }
    }
}
