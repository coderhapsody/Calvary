using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Calvary.Data.ProcedureModels;

namespace Calvary.Data
{
    public partial class CalvaryContext : DbContext, ICalvaryContext
    {
        public async Task<List<ProcReportUlangTahunModel>> ProcReportUlangTahun(DateTime? fromDate, DateTime? toDate)
        {
            var fromDateParam = new SqlParameter { ParameterName = "@FromDate", SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input, Value = fromDate.GetValueOrDefault() };
            if (!fromDate.HasValue)
                fromDateParam.Value = DBNull.Value;

            var toDateParam = new SqlParameter { ParameterName = "@ToDate", SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input, Value = toDate.GetValueOrDefault() };
            if (!toDate.HasValue)
                toDateParam.Value = DBNull.Value;

            var result = Database.SqlQuery<ProcReportUlangTahunModel>("EXEC [dbo].[proc_ReportUlangTahun] @FromDate, @ToDate", fromDateParam, toDateParam);

            return await result.ToListAsync();
        }
    }
}
