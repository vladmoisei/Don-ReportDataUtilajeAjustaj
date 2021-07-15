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
using RaportareAjustajV2.Data;
using RaportareAjustajV2.Models;

namespace RaportareAjustajV2.Controllers
{
    public class StrungarieCilindriController : Controller
    {
        private readonly RaportareDbContext _context;

        public StrungarieCilindriController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: StrungarieCilindri
        //public async Task<IActionResult> IndexStrungarie()
        //{
        //    return View(await _context.StrungarieCilindriModel.ToListAsync());
        //}

        // GET: StrungarieCilindri
        public async Task<IActionResult> Index()
        {
            ViewBag.Utilaj = (UtilajeAjustaj)Enum.Parse(typeof(UtilajeAjustaj), HttpContext.Session.GetString("Utilaj"));
            return View(await _context.StrungarieCilindriModel.OrderByDescending(item => item.DataBifatInLucru).ToListAsync());
        }

        // GET: StrungarieCilindri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strungarieCilindriModel = await _context.StrungarieCilindriModel
                .FirstOrDefaultAsync(m => m.StrungarieCilindriModelId == id);
            if (strungarieCilindriModel == null)
            {
                return NotFound();
            }

            return View(strungarieCilindriModel);
        }

        // GET: StrungarieCilindri/Create
        public IActionResult Create()
        {
            ViewBag.Utilaj = (UtilajeAjustaj)Enum.Parse(typeof(UtilajeAjustaj), HttpContext.Session.GetString("Utilaj"));
            return View();
        }

        // POST: StrungarieCilindri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StrungarieCilindriModelId,CodCartellino,Furnizor,Sarja,Diametru,Calitate,Lungime,Greutate,MotivNeexpediere,DescrSdF,IsInLucru,DiametruFinal,DataBifatInLucru,DataDiamFinal,ComentariuStrungarie")] StrungarieCilindriModel strungarieCilindriModel)
        {
            ViewBag.Utilaj = (UtilajeAjustaj)Enum.Parse(typeof(UtilajeAjustaj), HttpContext.Session.GetString("Utilaj"));
            if (ModelState.IsValid)
            {
                strungarieCilindriModel.DataIntroducere = DateTime.Now;
                _context.Add(strungarieCilindriModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(strungarieCilindriModel);
        }

        // GET: StrungarieCilindri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strungarieCilindriModel = await _context.StrungarieCilindriModel.FindAsync(id);
            if (strungarieCilindriModel == null)
            {
                return NotFound();
            }
            return View(strungarieCilindriModel);
        }

        // POST: StrungarieCilindri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StrungarieCilindriModelId,DataIntroducere,CodCartellino,Furnizor,Sarja,Diametru,Calitate,Lungime,Greutate,MotivNeexpediere,DescrSdF,IsInLucru,DiametruFinal,DataBifatInLucru,DataDiamFinal,ComentariuStrungarie")] StrungarieCilindriModel strungarieCilindriModel)
        {
            if (id != strungarieCilindriModel.StrungarieCilindriModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strungarieCilindriModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrungarieCilindriModelExists(strungarieCilindriModel.StrungarieCilindriModelId))
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
            return View(strungarieCilindriModel);
        }

        // GET: StrungarieCilindri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strungarieCilindriModel = await _context.StrungarieCilindriModel
                .FirstOrDefaultAsync(m => m.StrungarieCilindriModelId == id);
            if (strungarieCilindriModel == null)
            {
                return NotFound();
            }

            return View(strungarieCilindriModel);
        }

        // POST: StrungarieCilindri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strungarieCilindriModel = await _context.StrungarieCilindriModel.FindAsync(id);
            _context.StrungarieCilindriModel.Remove(strungarieCilindriModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrungarieCilindriModelExists(int id)
        {
            return _context.StrungarieCilindriModel.Any(e => e.StrungarieCilindriModelId == id);
        }

        public IActionResult ImportFile()
        {
            ViewBag.Hidden = "hidden";
            return View();
        }

        // Actiune import data from excel file
        [HttpPost]
        public async Task<IActionResult> ImportFile(List<IFormFile> files)
        {
            // Verificam daca lista de fisiera incarcata  are 0 elemente si returnam msj
            if (files.Count == 0)
            {
                ViewBag.Hidden = "";
                ViewBag.Mesaj = "Fisierul nu s-a incarcat";
                return View();
            }

            // Cream fisier din primul lelement din lista de fisiere
            IFormFile formFile = files[0];
            // Verificam daca fisierul are extensia .xlsx
            if (!formFile.FileName.EndsWith(".xlsx"))
            {
                ViewBag.Hidden = "";
                ViewBag.Mesaj = "Fisierul nu are extensia .xlsx!";
                return View();
            }

            //Cream lista de blumuri din fisier excel
            List<StrungarieCilindriModel> listaDateStrungarie = await ServicesStrungarie.GetBlumsListFromFileAsync(formFile);

            // Actualizam baza de date cu lista de blumuri din fisier
            if (listaDateStrungarie != null)
            {
                foreach (StrungarieCilindriModel item in listaDateStrungarie)
                {
                    _context.Add(item);

                }
                await _context.SaveChangesAsync();
            }

            // Redirection la Index
            return RedirectToAction(nameof(Index));
        }

        // Actiune import data from excel file
        [HttpGet]
        public async Task<IActionResult> ExportExcelFile()
        {
            IEnumerable<StrungarieCilindriModel> listaDeAfisat = await _context.StrungarieCilindriModel.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Strungarie Cilindri");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Data Introducere";
                ws.Cells["C1"].Value = "Eticheta";
                ws.Cells["D1"].Value = "Furnizor";
                ws.Cells["E1"].Value = "Sarja";
                ws.Cells["F1"].Value = "Diametru";
                ws.Cells["G1"].Value = "Calitate";
                ws.Cells["H1"].Value = "Lungime";
                ws.Cells["I1"].Value = "Greutate";
                ws.Cells["J1"].Value = "Motiv declasare";
                ws.Cells["K1"].Value = "Descr.SdF";
                ws.Cells["L1"].Value = "In Lucru";
                ws.Cells["M1"].Value = "Diam Final";
                ws.Cells["N1"].Value = "Data In Lucru";
                ws.Cells["O1"].Value = "Data Diam Final";
                ws.Cells["P1"].Value = "Comentariu Strungarie";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.StrungarieCilindriModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.DataIntroducere.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.CodCartellino;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Furnizor;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.Lungime;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.Greutate;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.MotivNeexpediere;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.DescrSdF;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = elem.IsInLucru;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = elem.DiametruFinal;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = elem.DataBifatInLucru.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("O{0}", rowStart)].Value = elem.DataDiamFinal.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("P{0}", rowStart)].Value = elem.ComentariuStrungarie;

                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "Raport Strungarie.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: StrungarieCilindri/Edit/5
        public async Task<IActionResult> EditStrungarie(int? id)
        {
            if (id == null)
            {
                ViewBag.Arata = "hide";
                return View();
            }

            var strungarieCilindriModel = await _context.StrungarieCilindriModel.FindAsync(id);
            if (strungarieCilindriModel == null)
            {
                ViewBag.Arata = "hide";
                return View();
            }
            return View(strungarieCilindriModel);
        }

        // POST: StrungarieCilindri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStrungarie(int searchCod, string submit, [Bind("StrungarieCilindriModelId,DataIntroducere,CodCartellino,Furnizor,Sarja,Diametru,Calitate,Lungime,Greutate,MotivNeexpediere,DescrSdF,IsInLucru,DiametruFinal,DataBifatInLucru,DataDiamFinal,ComentariuStrungarie")] StrungarieCilindriModel strungarieCilindriModel)
        {
            if (submit == "Cauta")
            {
                var strModelCautat = await _context.StrungarieCilindriModel
                .FirstOrDefaultAsync(m => m.CodCartellino == searchCod.ToString().Trim());
                if (strModelCautat != null) return View(strModelCautat);
                @ViewBag.Mesaj = $"Nu s-a gasit elementul cu codul: {searchCod}";
                ViewBag.Arata = "hide";
                return View();
            }
            if (strungarieCilindriModel.StrungarieCilindriModelId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var strungarieCilindriDBContextModel = await _context.StrungarieCilindriModel
                .FirstOrDefaultAsync(m => m.StrungarieCilindriModelId == strungarieCilindriModel.StrungarieCilindriModelId);

                    _context.Update(ServicesStrungarie.GetEditStrungarieModel(strungarieCilindriModel, strungarieCilindriDBContextModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrungarieCilindriModelExists(strungarieCilindriModel.StrungarieCilindriModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View(await _context.StrungarieCilindriModel
                .FirstOrDefaultAsync(m => m.StrungarieCilindriModelId == strungarieCilindriModel.StrungarieCilindriModelId));
            }
            return View(strungarieCilindriModel);
        }
    }
}
