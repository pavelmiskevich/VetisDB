using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using VetisDB.Classes.Services;
using VetisDB.EnterpriseServiceProd;

namespace VetisDB
{
    class Program
    {
        private static int _count = 1000;
        private static int _offset = 0;
        private static long _total = 0;
        private static readonly string lostTime = "Затраченное время";

        static void Main(string[] args)
        {
            GetBusinessEntities();
            Console.ReadLine();
        }

        private static void GetBusinessEntities()
        {
            EnterpriseServicePortTypeClient soap = new EnterpriseServicePortTypeClient();
            soap.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings.Get("UserName");
            soap.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings.Get("Password");

            getBusinessEntityListRequest getBusinessEntityListRequest = new getBusinessEntityListRequest();
            getBusinessEntityListResponse getBusinessEntityListResponse = new getBusinessEntityListResponse();

            getBusinessEntityListRequest.listOptions = new ListOptions
            {
                count = _count.ToString(),
                offset = _offset.ToString()
            };

            try
            {
                //BusinessEntity[] students = {
                //                new BusinessEntity { active = true, createDate = DateTime.Now, guid = Guid.NewGuid().ToString() },
                //                new BusinessEntity { active = true, createDate = DateTime.Now, guid = Guid.NewGuid().ToString() },
                //                new BusinessEntity { active = true, createDate = DateTime.Now, guid = Guid.NewGuid().ToString() }
                //               };
                ////ParallelQuery<int>

                //DataTable dtTmp = students.ToDataTable();
                getBusinessEntityListResponse = soap.GetBusinessEntityList(getBusinessEntityListRequest);
                _total = getBusinessEntityListResponse.businessEntityList.total;
                DataTable dtTmp = getBusinessEntityListResponse.businessEntityList.businessEntity.ToDataTable();
                int c = BusinessEntityInsertUdt(dtTmp);
                for (int j = _count; j < _total + _count; j += _count)
                {
                    getBusinessEntityListRequest.listOptions = new ListOptions
                    {
                        count = _count.ToString(),
                        offset = j.ToString() //_offset.ToString()
                    };
                    Console.WriteLine($"{_count} {j}");
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    getBusinessEntityListResponse = soap.GetBusinessEntityList(getBusinessEntityListRequest);
                    watch.Stop();
                    Console.WriteLine($"GET {lostTime} : {watch.ElapsedMilliseconds}");
                    watch = System.Diagnostics.Stopwatch.StartNew();
                    dtTmp = getBusinessEntityListResponse.businessEntityList.businessEntity.ToDataTable();
                    watch.Stop();
                    Console.WriteLine($"ToDataTable() {lostTime} : {watch.ElapsedMilliseconds}");
                    watch = System.Diagnostics.Stopwatch.StartNew();
                    c = BusinessEntityInsertUdt(dtTmp);
                    watch.Stop();
                    Console.WriteLine($"BusinessEntityInsertUdt(dtTmp) {lostTime} : {watch.ElapsedMilliseconds}");
                }
                //getBusinessEntityListResponse.businessEntityList.total //2931499
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// BusinessEntityInsert with UDT
        /// </summary>
        /// <param name="dt">business entity data table</param>
        /// <returns>affected rows count</returns>
        static int BusinessEntityInsertUdt(DataTable dt)
        {
            int count = 0;
            //SqlConnection sqlConnection = new SqlConnection(Const.cs);
            using (var sqlConnection = new SqlConnection(Const.cs))
            {
                using (var cmd = new SqlCommand("BusinessEntityInsertUDT", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //https://progi.pro/tip-tablici-t-sql-ukazanniy-tip-ne-zaregistrirovan-1559491
                    SqlParameter tvparam = cmd.Parameters.AddWithValue("@table", dt);
                    tvparam.SqlDbType = SqlDbType.Structured;
                    //var rootParam = new SqlParameter();
                    //rootParam.ParameterName = "@table";
                    //rootParam.SqlDbType = SqlDbType.Udt;
                    //rootParam.UdtTypeName = "BusinessEntitiesUDT";
                    //rootParam.Value = dt;
                    //cmd.Parameters.Add(rootParam);

                    try
                    {
                        sqlConnection.Open();
                        //cmd.ExecuteNonQuery();
                        count = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        //Указанный тип не зарегистрирован на сервере назначения
                        //TODO: Проверить типы в DataTable
                        Console.WriteLine("{0} Exception caught.", ex);
                    }
                }
            }
            return count;
        }
    }
}
