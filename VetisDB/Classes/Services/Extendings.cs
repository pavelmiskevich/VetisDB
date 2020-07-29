using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using VetisDB.EnterpriseServiceProd;

namespace VetisDB.Classes.Services
{
    public static class Extendings
    {
        //https://stackoverflow.com/questions/11331388/from-array-to-datatable       

        //public static DataTable ToDataTable<T>(this T[] array)
        //{
        //    #region https://stackoverflow.com/questions/11331388/from-array-to-datatable
        //    //TODO: не возвращает tmp.GetType().GetFields()
        //    if (array == null || array.Length == 0) return null;
        //    DataTable table = new DataTable();
        //    var tmp = array[0];
        //    table.Columns.AddRange(tmp.GetType().GetProperties().Select(field => new DataColumn(field.Name, field.PropertyType)).ToArray());
        //    int fieldCount = tmp.GetType().GetProperties().Count();
        //    array.All(a =>
        //    {
        //        table.Rows.Add(Enumerable.Range(0, fieldCount).Select(index => a.GetType().GetProperties()[index].GetValue(a)).ToArray());
        //        return true;
        //    });
        //    return table;
        //    #endregion https://stackoverflow.com/questions/11331388/from-array-to-datatable
        //}

        public static DataTable ToDataTable(this BusinessEntity[] array)
        {
            if (array == null || array.Length == 0) return null;
            DataTable table = new DataTable();
            var tmp = array[0];
            table.Columns.Add("uuid", typeof(string)); //typeof(Guid)
            table.Columns.Add("guid", typeof(string)); //typeof(Guid)
            table.Columns.Add("active", typeof(bool));
            table.Columns.Add("last", typeof(bool));
            table.Columns.Add("status", typeof(string));
            table.Columns.Add("createDate", typeof(DateTime));
            table.Columns.Add("updateDate", typeof(DateTime));
            table.Columns.Add("previous", typeof(string)); //typeof(Guid)
            table.Columns.Add("next", typeof(string)); //typeof(Guid)
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            foreach (var item in array)
            {
                DataRow dr = table.NewRow();
                dr[0] = item.uuid;
                dr[1] = item.guid;
                dr[2] = item.active;
                dr[3] = item.last;
                dr[4] = item.status;
                dr[5] = item.createDate;
                dr[6] = item.updateDate;
                if (item.previous != null)
                    dr[7] = item.previous;
                else
                    dr[7] = DBNull.Value;
                if (item.next != null)
                    dr[8] = item.next;
                else
                    dr[8] = DBNull.Value;
                dr[9] = item.type;
                if (item.name != null)
                    dr[10] = item.name;
                else
                    dr[10] = DBNull.Value;;
                table.Rows.Add(dr);
            }
            return table;
        }
    }
}
