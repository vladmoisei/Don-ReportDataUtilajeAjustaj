﻿using System;
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
    public class NovafluxController : Controller
    {
        private readonly RaportareDbContext _context;

        public NovafluxController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: Novaflux
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            List<NovafluxModel> listaDeAfisat = await _context.NovafluxModels.ToListAsync();
            // Daca e admin afisam toata lista
            if (ViewBag.IsAdmin == "True")
                return View(listaDeAfisat);
            // Daca nu e admin afisam doar datele introduse in ziua curenta
            return View(listaDeAfisat.Where(model => CalculeAuxiliar.IsCurrentDay(CalculeAuxiliar.ReturnareDataFromString(model.DataIntroducere))));
        }

        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync()
        {
            List<NovafluxModel> listaDeAfisat = await _context.NovafluxModels.ToListAsync();

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("NovaFlux");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "NovafluxModelId";
                ws.Cells["B1"].Value = "UserName";
                ws.Cells["C1"].Value = "Data introducere";
                ws.Cells["D1"].Value = "Diametru";
                ws.Cells["E1"].Value = "Calitate";
                ws.Cells["F1"].Value = "Sarja";
                ws.Cells["G1"].Value = "Defect Etalon";
                ws.Cells["H1"].Value = "Nr bare Conforme";
                ws.Cells["I1"].Value = "Masa Conform";
                ws.Cells["J1"].Value = "Nr bare neconforme";
                ws.Cells["K1"].Value = "Masa Neconform";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.NovafluxModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.DataIntroducere;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.DefectEtalon;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.NrBareConform;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.MasaConform;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.NrBareNeConform;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.MasaNeConform;
                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportNovaFlux.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // GET: Novaflux/Details/5ovaFlux
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels
                .FirstOrDefaultAsync(m => m.NovafluxModelId == id);
            if (novafluxModel == null)
            {
                return NotFound();
            }

            return View(novafluxModel);
        }

        // GET: Novaflux/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Novaflux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NovafluxModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,DefectEtalon,NrBareConform,MasaConform,NrBareNeConform,MasaNeConform")] NovafluxModel novafluxModel)
        {
            if (ModelState.IsValid)
            {
                novafluxModel.DataIntroducere = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                novafluxModel.MasaConform = Math.Round(CalculeAuxiliar.CalculMasa(
                    novafluxModel.Diametru, novafluxModel.NrBareConform, 6), 2);
                novafluxModel.MasaNeConform = Math.Round(CalculeAuxiliar.CalculMasa(
                    novafluxModel.Diametru, novafluxModel.NrBareNeConform, 6), 2);
                _context.Add(novafluxModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novafluxModel);
        }

        // GET: Novaflux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels.FindAsync(id);
            if (novafluxModel == null)
            {
                return NotFound();
            }
            return View(novafluxModel);
        }

        // POST: Novaflux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NovafluxModelId,UserName,DataIntroducere,Diametru,Calitate,Sarja,DefectEtalon,NrBareConform,MasaConform,NrBareNeConform,MasaNeConform")] NovafluxModel novafluxModel)
        {
            if (id != novafluxModel.NovafluxModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novafluxModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovafluxModelExists(novafluxModel.NovafluxModelId))
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
            return View(novafluxModel);
        }

        // GET: Novaflux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novafluxModel = await _context.NovafluxModels
                .FirstOrDefaultAsync(m => m.NovafluxModelId == id);
            if (novafluxModel == null)
            {
                return NotFound();
            }

            return View(novafluxModel);
        }

        // POST: Novaflux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novafluxModel = await _context.NovafluxModels.FindAsync(id);
            _context.NovafluxModels.Remove(novafluxModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovafluxModelExists(int id)
        {
            return _context.NovafluxModels.Any(e => e.NovafluxModelId == id);
        }
    }
}