using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Core.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Extensions.Logging;
using orch.Domain.Models;

namespace Orch.Services.ExcellProcess
{
    public class ExcellService
    {
        public ExcellService() { }

        public void Print(List<AssesmentRequestDTO> resultList, string environment, string type ) 
        {
            switch (type)
            {
                case "Absence":
                    PrintAbsenceAssessment($"Absence Assessments ({environment})", "AbsenceAssessment.xlsx", resultList);
                break;
            }
        }

        private void PrintAbsenceAssessment(string tagMessage, string fileName, List<AssesmentRequestDTO> assesmentRequest)
        {
            //TODO Generic call to set Spreadsheet
            List<PrintAbscenceEntity> printEntity = new List<PrintAbscenceEntity>();
            string cResult = "";
            foreach (var item in assesmentRequest)
            {
                cResult = item.PatientDetails.FirstOrDefault()!.Clinic.ToString();
                printEntity.Add(new PrintAbscenceEntity()
                {
                    PatientOid = item.PatientOid.ToString()!,
                    PatientVisitOid = item.PatientVisitOid.ToString()!,
                    AssessmentId = item.AssessmentId.ToString()!,
                    CaseNumber = (
                        (item.CaseDetails.FirstOrDefault()!.caseNumber.ToString()!) != "0" ?
                        (item.CaseDetails.FirstOrDefault()!.caseNumber.ToString()!) : "-1"),
                    Mrn = item.PatientDetails.FirstOrDefault()!.Mrn.ToString(),
                    Clinic = cResult.Split(":")[0],
                    AssessmentCreationDate = item.AssessmentDate
                });
            }
            var wbook = new XLWorkbook();
            var wsheet = wbook.Worksheets.Add(tagMessage);
            wsheet.Cell("A1").Value = "PatientOId";
            wsheet.Cell("B1").Value = "PatientVisitID";
            wsheet.Cell("C1").Value = "AssessmentID";
            wsheet.Cell("D1").Value = "CaseNumber";
            wsheet.Cell("E1").Value = "Mrn";
            wsheet.Cell("F1").Value = "Clinic";
            wsheet.Cell("G1").Value = "CollectedDate";
            //if (cResult)
            //{

            //}

            var range = wsheet.Range("A1:H1");
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Font.FontSize = 11;
            range.Style.Fill.BackgroundColor = XLColor.Green;
            range.Style.Font.FontColor = XLColor.White;
            range.Style.Font.SetBold();
            range.Style.Font.SetFontName("Calibri");
            wsheet.Columns(1, 11).AdjustToContents();
            wsheet.Cell("A2").InsertData(printEntity);
            wbook.SaveAs(fileName);
            wbook.Dispose();
        }

    }
}
