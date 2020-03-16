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
            List<PellatriceLandgrafModel> listaDeAfisat = await _context.PellatriceLandgrafModels.OrderByDescending(t => t.DataIntroducere).ToListAsync();
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
            List<PellatriceLandgrafModel> listaSql = await _context.PellatriceLandgrafModels.ToListAsync();
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<PellatriceLandgrafModel> listaDeAfisat = listaSql.Where(model => CalculeAuxiliar.IsDateBetween(model.DataIntroducere, dataFrom, dataTo));

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Pellatrice Landgraf");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "PellatriceLandgrafModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Diametru Intrare";
                ws.Cells["E1"].Value = "Diametru Iesire";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Sarja";
                ws.Cells["H1"].Value = "Eticheta";
                ws.Cells["I1"].Value = "Nr bare";
                ws.Cells["J1"].Value = "Lungime";
                ws.Cells["K1"].Value = "Masa";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.PellatriceLandgrafModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.DiametruIntrare;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.DiametruIesire;
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
            string excelName = "RaportPellatrice.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
        public async Task<IActionResult> Create([Bind("PellatriceLandgrafModelId,UserName,DataIntroducere,DiametruIntrare,DiametruIesire,Calitate,Sarja,Eticheta,NrBare,Lungime,Masa")] PellatriceLandgrafModel pellatriceLandgrafModel)
        {
            if (ModelState.IsValid)
            {
                pellatriceLandgrafModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                pellatriceLandgrafModel.Lungime = 6;
                pellatriceLandgrafModel.Masa = Math.Round(CalculeAuxiliar.CalculMasa(
                    (int)pellatriceLandgrafModel.DiametruIntrare, pellatriceLandgrafModel.NrBare, pellatriceLandgrafModel.Lungime), 2);
                _context.Add(pellatriceLandgrafModel);
                await _context.SaveChangesAsync();
                ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Eroare conexiune server SQL.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesaj = "Atentie! Nu s-au introdus datele. Datele nu sunt valide.";
            @ViewBag.UserName = pellatriceLandgrafModel.UserName;
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
        public async Task<IActionResult> Edit(int id, [Bind("PellatriceLandgrafModelId,UserName,DataIntroducere,DiametruIntrare,DiametruIesire,Calitate,Sarja,Eticheta,NrBare,Lungime,Masa")] PellatriceLandgrafModel pellatriceLandgrafModel)
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
