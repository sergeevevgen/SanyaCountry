using SanyaCountryBusinessLogic.OfficePackage.HelperModels;
using SanyaCountryBusinessLogic.OfficePackage.HelperEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"с{ info.DateFrom.ToShortDateString() } по { DateTime.Now.ToShortDateString() }",
                Style = "Normal"
            });

            //Название, количество общее, Название постройки, количество таких построек
            CreateTable(new List<string> { "4cm", "3cm", "3cm", "4cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string>
                {
                    "Название", "Общее количество",
                    "Название строения", "Количество"
                },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var sb in info.SettlementBuildings)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string>
                    {
                        sb.SettlementName,
                        sb.TotalCount.ToString(),
                        "",
                        ""
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
                foreach(var b in sb.Buildings)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Texts = new List<string>
                    {
                        "",
                        "",
                        b.Item1,
                        b.Item2.ToString()
                    },
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
                }
            }
            SavePdf(info);
        }

        /// <summary>
        /// Cоздание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreatePdf(PdfInfo info);

        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);

        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SavePdf(PdfInfo info);
    }
}
