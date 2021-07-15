using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaportareAjustajV2.Models;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RaportareAjustajV2.Data
{
    public static class ServicesStrungarie
    {
        // Get Updated Model from Strungarie Cilindri Form
        public static StrungarieCilindriModel GetEditStrungarieModel(StrungarieCilindriModel strungarieCilindriFormModel, StrungarieCilindriModel strungarieCilindriDBContextModel)
        {
            if (strungarieCilindriDBContextModel.StrungarieCilindriModelId != strungarieCilindriFormModel.StrungarieCilindriModelId)
                return strungarieCilindriDBContextModel;

            strungarieCilindriDBContextModel.IsInLucru = strungarieCilindriFormModel.IsInLucru;
            strungarieCilindriDBContextModel.DataBifatInLucru = strungarieCilindriFormModel.DataBifatInLucru;
            strungarieCilindriDBContextModel.DiametruFinal = strungarieCilindriFormModel.DiametruFinal;
            strungarieCilindriDBContextModel.DataDiamFinal = strungarieCilindriFormModel.DataDiamFinal;
            strungarieCilindriDBContextModel.ComentariuStrungarie = strungarieCilindriFormModel.ComentariuStrungarie;

            return strungarieCilindriDBContextModel;
        }
        // Task returnare lista strungarie date din fisier excel 
        // Introducere in fisier camp cu camp
        public static async Task<List<StrungarieCilindriModel>> GetBlumsListFromFileAsync(IFormFile formFile)
        //public static List<Blum> GetBlumsListFromFileAsync(IFormFile formFile)
        {
            var list = new List<StrungarieCilindriModel>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //Id = id, *Deoarece introduc in SQL server las sa fie adaugat de sql, pentru a fi sigur ca sunt diferite*
                        //int id = int.TryParse(worksheet.Cells[row, 1].Value.ToString().Trim(), out int i) ? i : 0;
                        //string CodCartellino = worksheet.Cells[row, 5].Value.ToString().Trim();
                        //string Furnizor = worksheet.Cells[row, 6].Value.ToString().Trim();
                        //string Sarja = worksheet.Cells[row, 7].Value.ToString().Trim();
                        //string Diametru = worksheet.Cells[row, 10].Value.ToString().Trim();
                        //string Calitate = worksheet.Cells[row, 12].Value.ToString().Trim();
                        //string Lungime = worksheet.Cells[row, 14].Value.ToString().Trim();
                        //string Greutate = worksheet.Cells[row, 16].Value.ToString().Trim();
                        //string MotivNeexpediere = worksheet.Cells[row, 21].Value.ToString().Trim();
                        //string DescrSdF = worksheet.Cells[row, 24].Value.ToString().Trim();
                        try
                        {
                            if (worksheet.Cells[row, 5].Value == null || string.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString())) break;
                            //string CodCartelino1 = worksheet.Cells[row, 5].Value.ToString().Trim();
                        }
                        catch (NullReferenceException ex)
                        {
                            break;
                        }

                        list.Add(new StrungarieCilindriModel
                        {
                            // Id = id, *Deoarece introduc in SQL server las sa fie adaugat de sql, pentru a fi sigur ca sunt diferite*
                            DataIntroducere = DateTime.Now,
                            CodCartellino = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            Furnizor = worksheet.Cells[row, 6].Value.ToString().Trim(),
                            Sarja = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            Diametru = worksheet.Cells[row, 10].Value.ToString().Trim(),
                            Calitate = worksheet.Cells[row, 12].Value.ToString().Trim(),
                            Lungime = worksheet.Cells[row, 14].Value.ToString().Trim(),
                            Greutate = worksheet.Cells[row, 16].Value.ToString().Trim(),
                            MotivNeexpediere = worksheet.Cells[row, 21].Value.ToString().Trim(),
                            DescrSdF = !(worksheet.Cells[row, 25].Value == null)? worksheet.Cells[row, 25].Value.ToString().Trim(): "-",
                            IsInLucru = false,
                            DiametruFinal = 0,
                            DataBifatInLucru = new DateTime(),
                            DataDiamFinal = new DateTime(),
                            ComentariuStrungarie = "-",
                        });
                    }
                }
            }


            return list;
        }

    }
}
