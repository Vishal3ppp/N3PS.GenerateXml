using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PS.GenerateXml.DataTables
{
    class Tables
    {
        public DataTable GetFieldsTable()
        {
            DataTable dt = new DataTable("Fields");
            dt.Columns.Add(new DataColumn("FieldName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColumnNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("SquenceNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Length", typeof(string)));

            return dt;
        }



        public DataTable GetValidationsTable()
        {
            DataTable dt = new DataTable("ValidationRules");
            dt.Columns.Add(new DataColumn("ValidationId", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidationName", typeof(string)));
            dt.Columns.Add(new DataColumn("ColumnNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidationType", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidationSize", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidationErrorMessage", typeof(string)));

            dt.Columns.Add(new DataColumn("ValidationValues", typeof(string)));
            dt.Columns.Add(new DataColumn("DateFormate", typeof(string)));
            dt.Columns.Add(new DataColumn("DLLName", typeof(string)));
            dt.Columns.Add(new DataColumn("FullyQualifiedClassName", typeof(string)));
            dt.Columns.Add(new DataColumn("RoutineName", typeof(string)));

            return dt;
        }
    }
}
