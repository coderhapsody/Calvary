using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using NPOI.HSSF.UserModel;
using System.IO;
using Calvary.Providers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Calvary.Extensions.Numeric;
using Calvary.Klasis.Models;

namespace Calvary.Klasis
{
    public enum DBAJMappingEnum
    {
        EducationGrade,
        Ethnic
    }

    public class DBAJGenerator : BaseKlasisGenerator, IKlasisGenerator
    {
        private string sourceExcelFile;
        private string destExcelFile;
        private ConfigurationProvider configurationProvider;

        private List<Member> members;
        private List<MemberStateHistoryModel> stateHistories;

        public DBAJGenerator(CalvaryContext context, IPrincipal principal, ConfigurationProvider configurationProvider)
            : base(context, principal)
        {
            this.configurationProvider = configurationProvider;
            members = new List<Member>();
            stateHistories = new List<MemberStateHistoryModel>();
        }

        public void SetInputOutputFile(string sourceExcelFile, string destExcelFile)
        {
            if (!File.Exists(sourceExcelFile))
                throw new CalvaryException($"File {sourceExcelFile} tidak ditemukan");

            this.sourceExcelFile = sourceExcelFile;
            this.destExcelFile = destExcelFile;
        }

        public async Task Generate(int fromYear, int toYear)
        {
            if (String.IsNullOrEmpty(sourceExcelFile))
                throw new ArgumentException("File template tidak tersedia");
            if (String.IsNullOrEmpty(destExcelFile))
                throw new ArgumentNullException("File tujuan belum diberikan");

            // Make a copy of template for pushing out the resul                
            File.Copy(sourceExcelFile, destExcelFile, true);

            HSSFWorkbook workbook = null;
            using (FileStream fileStream = new FileStream(destExcelFile, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new HSSFWorkbook(fileStream);
                workbook.ForceFormulaRecalculation = true;
            }
            ProcessSheet2(workbook, fromYear, toYear);

            // Prepare members information
            members.Clear();
            stateHistories.Clear();
            members = await RetrieveProcessedMembers();
            stateHistories = GetMemberStateHistories(members);

            ProcessSheet3(workbook, fromYear, toYear);

            ISheet sheet4 = workbook.GetSheetAt(4);
            sheet4.ForceFormulaRecalculation = true;

            ISheet sheet5 = workbook.GetSheetAt(5);
            ProcessSheet5(workbook, fromYear, toYear);
            sheet5.ForceFormulaRecalculation = true;

            ForceWorkbookRecalculation(workbook);

            using (FileStream fileStream = new FileStream(destExcelFile, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook.ForceFormulaRecalculation = true;
                workbook.SetActiveSheet(2);
                workbook.Write(fileStream);
                fileStream.Flush();
            }

            DateTime now = DateTime.Now;
            File.SetLastWriteTime(destExcelFile, now);
            File.SetLastAccessTime(destExcelFile, now);
            File.SetCreationTime(destExcelFile, now);

            members = null;
            stateHistories = null;
        }

        private void ProcessSheet5(HSSFWorkbook workbook, int fromYear, int toYear)
        {
            ISheet sheet = workbook.GetSheetAt(5);
            var startDatePeriod = new DateTime(2016, 4, 1);
            var endDatePeriod = new DateTime(toYear, 3, DateTime.DaysInMonth(toYear, 3));
            var query = from state in stateHistories
                        where state.EffectiveDate >= startDatePeriod && state.EffectiveDate <= endDatePeriod
                        select state;


            var countATL = query.Where(s => s.StateCode == MutationCodes.ATL).Count();
            var countATIOT = query.Where(s => s.StateCode == MutationCodes.ATIOT).Count();
            var countATP2 = query.Where(s => s.StateCode == MutationCodes.ATP2).Count();
            sheet.GetRow(10).GetCell(2).SetCellValue(countATL);
            sheet.GetRow(11).GetCell(2).SetCellValue(countATIOT);
            sheet.GetRow(12).GetCell(2).SetCellValue(countATP2);

            var countAPA = members.Where(s => s.WhenFlagApa.HasValue).Count();
            sheet.GetRow(17).GetCell(2).SetCellValue(countAPA);

            var countATIS = query.Where(s => s.StateCode == MutationCodes.ATIS).Count();
            var countATPS = query.Where(s => s.StateCode == MutationCodes.ATPS).Count();
            var countATP1 = query.Where(s => s.StateCode == MutationCodes.ATP1).Count();
            sheet.GetRow(23).GetCell(2).SetCellValue(countATIS);
            sheet.GetRow(24).GetCell(2).SetCellValue(countATPS);
            sheet.GetRow(25).GetCell(2).SetCellValue(countATP1);

            var countExDKH123 = query.Where(s => new[] { MutationCodes.ExDKH1, MutationCodes.ExDKH2, MutationCodes.ExDKH3}.Contains(s.StateCode)).Count();
            var countExDKH4 = query.Where(s => s.StateCode == MutationCodes.ExDKH4).Count();
            sheet.GetRow(31).GetCell(2).SetCellValue(countExDKH123);
            sheet.GetRow(32).GetCell(2).SetCellValue(countExDKH4);

            var countATD1 = query.Where(s => s.StateCode == MutationCodes.ATD1).Count();
            var countATD2 = query.Where(s => s.StateCode == MutationCodes.ATD2).Count();
            sheet.GetRow(38).GetCell(2).SetCellValue(countATD1);
            sheet.GetRow(39).GetCell(2).SetCellValue(countATD2);

            var countAKK1 = query.Where(s => s.StateCode == MutationCodes.AKK1).Count();
            var countAKK2 = query.Where(s => s.StateCode == MutationCodes.AKK2).Count();
            var countAKK3 = query.Where(s => s.StateCode == MutationCodes.AKK3).Count();
            sheet.GetRow(47).GetCell(2).SetCellValue(countAKK1);
            sheet.GetRow(48).GetCell(2).SetCellValue(countAKK2);
            sheet.GetRow(49).GetCell(2).SetCellValue(countAKK3);

            var countDKH1 = query.Where(s => s.StateCode == MutationCodes.DKH1).Count();
            var countDKH2 = query.Where(s => s.StateCode == MutationCodes.DKH2).Count();
            var countDKH3 = query.Where(s => s.StateCode == MutationCodes.DKH3).Count();
            var countDKH4 = query.Where(s => s.StateCode == MutationCodes.DKH4).Count();
            sheet.GetRow(56).GetCell(2).SetCellValue(countDKH1);
            sheet.GetRow(57).GetCell(2).SetCellValue(countDKH2);
            sheet.GetRow(58).GetCell(2).SetCellValue(countDKH3);
            sheet.GetRow(59).GetCell(2).SetCellValue(countDKH4);

            var countAKM1 = query.Where(s => s.StateCode == MutationCodes.AKM1).Count();
            var countAKM2 = query.Where(s => s.StateCode == MutationCodes.AKM2).Count();
            sheet.GetRow(66).GetCell(2).SetCellValue(countAKM1);
            sheet.GetRow(67).GetCell(2).SetCellValue(countAKM2);
        }

        private static void ForceWorkbookRecalculation(HSSFWorkbook workbook)
        {
            try
            {
                HSSFFormulaEvaluator.EvaluateAllFormulaCells(workbook);
            }
            catch { }
        }

        private void ProcessSheet3(HSSFWorkbook workbook, int fromYear, int toYear)
        {
            fromYear = 2016;
            DateTime startDatePeriod = GetStartDatePeriod(fromYear);
            DateTime endDatePeriod = GetEndDatePeriod(toYear);

            ISheet sheet = workbook.GetSheetAt(3);

            int rowIndex = 0;

            sheet.GetRow(1).GetCell(17).SetCellValue(DateTime.Today);

            foreach (Member member in members)
            {
                IRow currentRow = sheet.GetRow(5 + rowIndex);

                // MemberNo
                currentRow.GetCell(1).SetCellValue(member.MemberNo);

                // Nama
                currentRow.GetCell(2).SetCellValue(member.Name);

                // Alamat
                currentRow.GetCell(3).SetCellValue(member.Address);

                // No. Telp
                currentRow.GetCell(6).SetCellValue(member.HomePhone);

                // No. HP
                currentRow.GetCell(7).SetCellValue(member.CellPhone1);

                // Wilayah
                if (member.RegionId.HasValue)
                {
                    string regionCode = member.Region.Code;
                    string wilayah = regionCode.Any(chr => chr == '-') ? member.Region.Code.Split('-')[0] : regionCode;
                    int wilayahInt;
                    if (Int32.TryParse(wilayah, out wilayahInt))
                        currentRow.GetCell(8).SetCellValue(wilayahInt.ToRoman());
                }

                // Gender
                currentRow.GetCell(9).SetCellValue(member.Gender == "L" ? "P" : "W");

                // Gol Darah
                if (!String.IsNullOrEmpty(member.BloodType?.Trim()))
                {
                    string rhesus = String.IsNullOrEmpty(member.Rhesus) ? "+" : member.Rhesus;
                    currentRow.GetCell(10).SetCellValue(String.Format("{0}{1}", member.BloodType, rhesus));
                }

                // Status Anggota                
                currentRow.GetCell(11).SetCellValue(member.ChrismationDate.HasValue ? "S" : "B");

                // Umur tidak perlu dihitung karena sudah ada formulanya di Excel
                //if(!member.DeceasedDate.HasValue)
                //    currentRow.GetCell(12).SetCellValue(member.BirthDate.HasValue ? (DateTime.Today.Year - member.BirthDate.Value.Year).ToString() : "");

                // Pendidikan
                string pendidikan = member.EducationGrade?.Name;
                if (!String.IsNullOrEmpty(pendidikan))
                    currentRow.GetCell(13).SetCellValue(pendidikan);

                // Pekerjaan
                int? jobId = member.JobId;
                string pekerjaan = String.Empty;
                if (jobId.HasValue)
                {
                    Job job = context.Jobs.SingleOrDefault(j => j.Id == jobId.Value);
                    if (job != null)
                    {
                        pekerjaan = job.Name ?? String.Empty;
                    }
                    currentRow.GetCell(14).SetCellValue(pekerjaan);
                }

                // Kelompok Etnis
                string etnis = GetKlasisMappingValue(member.EthnicId.GetValueOrDefault(), DBAJMappingEnum.Ethnic);
                if (!String.IsNullOrEmpty(etnis))
                {
                    currentRow.GetCell(15).SetCellValue(etnis);
                }

                // Lahir
                if (member.BirthDate.HasValue)
                    currentRow.GetCell(16).SetCellValue(member.BirthDate.Value);

                // Baptis Dewasa / Anak
                if (!String.IsNullOrEmpty(member.ChrismationType))
                {
                    if (member.ChrismationType == "B" && member.ChrismationDate.HasValue)
                        currentRow.GetCell(17).SetCellValue(member.ChrismationDate.Value);
                }
                else
                {
                    if (member.ChildhoodBaptizedDate.HasValue)
                        currentRow.GetCell(17).SetCellValue(member.ChildhoodBaptizedDate.Value);
                }

                // Sidi
                if (!String.IsNullOrEmpty(member.ChrismationType) && member.ChrismationType == "S" && member.ChrismationDate.HasValue)
                    currentRow.GetCell(18).SetCellValue(member.ChrismationDate.Value);

                // Atestasi Masuk
                if (member.JoinDate.HasValue)
                    currentRow.GetCell(19).SetCellValue(member.JoinDate.Value);

                // Atestasi Keluar
                if (member.ResignDate.HasValue)
                    currentRow.GetCell(20).SetCellValue(member.ResignDate.Value);

                // Meninggal dunia
                if (member.DeceasedDate.HasValue)
                    currentRow.GetCell(21).SetCellValue(member.DeceasedDate.Value);

                IEnumerable<MemberStateHistoryModel> validStateHistories =
                    (from state in stateHistories
                     where state.MemberId == member.Id && state.EffectiveDate >= startDatePeriod && state.EffectiveDate <= endDatePeriod
                     select state).ToList();

                if (validStateHistories != null)
                {
                    // Tanggal DKH
                    MemberStateHistoryModel dkhState = validStateHistories.FirstOrDefault(m => m.StateCode.Contains("DKH"));
                    if (dkhState != null)
                        currentRow.GetCell(22).SetCellValue(dkhState.EffectiveDate);

                    // Ex DKH
                    MemberStateHistoryModel exDkhState = validStateHistories.FirstOrDefault(m => m.StateCode.Contains("Ex.DKH"));
                    if (exDkhState != null)
                        currentRow.GetCell(23).SetCellValue(exDkhState.EffectiveDate);

                    // Ex DKH4
                    MemberStateHistoryModel exDkh4State = validStateHistories.FirstOrDefault(m => m.StateCode.Contains("Ex.DKH-4"));
                    if (exDkh4State != null)
                        currentRow.GetCell(24).SetCellValue(exDkh4State.EffectiveDate);

                    // Status DKH dan lainnya
                    int cellIndex = 25;
                    foreach (MemberStateHistoryModel stateHistory in validStateHistories)
                    {
                        currentRow.GetCell(cellIndex).SetCellValue(stateHistory.StateCode);
                        cellIndex++;

                        // insufficient column in the Excel template
                        if (cellIndex > 27)
                            break;
                    }
                }

                rowIndex++;
            }

            sheet.ForceFormulaRecalculation = true;
        }

        private void ProcessSheet2(HSSFWorkbook workbook, int fromYear, int toYear)
        {
            ISheet sheet = workbook.GetSheetAt(2);
            sheet.GetRow(2).GetCell(3).SetCellValue(configurationProvider[ConfigurationKeys.NamaGereja]);
            sheet.GetRow(3).GetCell(3).SetCellValue(configurationProvider[ConfigurationKeys.KotaGereja]);
            sheet.GetRow(4).GetCell(3).SetCellValue(fromYear);
            sheet.GetRow(4).GetCell(5).SetCellValue(toYear);
            sheet.GetRow(5).GetCell(3).SetCellValue(configurationProvider[ConfigurationKeys.KetuaUmum]);
            sheet.GetRow(6).GetCell(3).SetCellValue(configurationProvider[ConfigurationKeys.SekretarisUmum]);
            sheet.GetRow(7).GetCell(3).SetCellValue(configurationProvider[ConfigurationKeys.PICLKKJ]);
            sheet.ForceFormulaRecalculation = true;
        }

        internal string GetKlasisMappingValue(int id, DBAJMappingEnum mappingType)
        {
            switch (mappingType)
            {
                case DBAJMappingEnum.Ethnic:
                    return context.Ethnics.SingleOrDefault(m => m.Id == id)?.KlasisMappingValue ?? String.Empty;
            }

            return String.Empty;
        }

        private DateTime GetStartDatePeriod(int year) => new DateTime(year, 4, 1);
        private DateTime GetEndDatePeriod(int year) => new DateTime(year, 3, DateTime.DaysInMonth(year, 3));
        private async Task<List<Member>> RetrieveProcessedMembers()
        {
            IQueryable<Member> queryMember = from member in context.Members
                                                .Include(m => m.Region)
                                                .Include(m => m.EducationMajor)
                                                .Include(m => m.EducationGrade)
                                                .Include(m => m.Ethnic)
                                             where (member.ChrismationDate.HasValue || member.ChildhoodBaptizedDate.HasValue)
                                             orderby member.Name
                                             select member;
            return await queryMember.ToListAsync();
        }

        internal List<MemberStateHistoryModel> GetMemberStateHistories(IEnumerable<Member> members)
        {
            foreach (Member member in members)
            {
                int memberId = member.Id;           
                if (member != null)
                {
                    // ATL
                    if (member.BirthDate.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATL);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.BirthDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // ATD
                    if (member.ChrismationType == "B" && member.ChrismationDate.HasValue && member.BaptizedReasonId.HasValue)
                    {
                        string baptizedReasonCode = member.BaptizedReason.Code;
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == baptizedReasonCode);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.ChrismationDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // ATIS
                    if (member.ChrismationFlagAtis && member.ChrismationDate.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATIS);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.ChrismationDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // ATIOT
                    if (member.ChildhoodBaptizedFlagAtiot && member.ChildhoodBaptizedDate.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATIOT);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.ChildhoodBaptizedDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // ATPS
                    if (member.JoinFlagAtps && member.JoinDate.HasValue && member.JoinFromShrineId.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATPS);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.JoinDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // ATP
                    if (member.JoinDate.HasValue && member.JoinFromShrineId.HasValue)
                    {
                        if (member.ChildhoodBaptizedDate.HasValue)
                        {
                            MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATP2);
                            var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.JoinDate.Value, String.Empty);
                            stateHistories.Add(stateHistory);
                        }
                        else if (member.ChrismationDate.HasValue)
                        {
                            MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.ATP1);
                            var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.JoinDate.Value, String.Empty);
                            stateHistories.Add(stateHistory);
                        }
                    }

                    // AKK
                    if (member.ResignDate.HasValue && member.ResignReasonId.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == member.ResignReason.Code);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.ResignDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // AKM
                    if (member.DeceasedDate.HasValue && member.ChildhoodBaptizedDate.HasValue && !member.ChrismationDate.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.AKM2);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.DeceasedDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }
                    else if (member.DeceasedDate.HasValue && member.ChrismationDate.HasValue)
                    {
                        MemberMutation memberMutation = MemberMutation.Instance.Single(m => m.Code == MutationCodes.AKM1);
                        var stateHistory = new MemberStateHistoryModel(memberId, member.MemberNo, memberMutation, member.DeceasedDate.Value, String.Empty);
                        stateHistories.Add(stateHistory);
                    }

                    // DKH & ExDKH
                    IList<MemberStateHistoryModel> otherMutations =
                    (from m in context.Members
                     join memberStateHist in context.MemberStateHistories on m.Id equals memberStateHist.MemberId
                     join memberState in context.MemberStates on memberStateHist.MemberStateId equals memberState.Id
                     where m.Id == member.Id
                     select new MemberStateHistoryModel
                     {
                         MemberId = m.Id,
                         MemberNo = m.MemberNo,
                         StateCode = memberState.Code,
                         StateDescription = memberState.Description,
                         EffectiveDate = memberStateHist.EffDate,
                         Notes = memberStateHist.Notes
                     }).ToList();
                    stateHistories.AddRange(otherMutations);
                }
            }

            return stateHistories.OrderBy(state => state.EffectiveDate).ToList();
        }
    }
}
