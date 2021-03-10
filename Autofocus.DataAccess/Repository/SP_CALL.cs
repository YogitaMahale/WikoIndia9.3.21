using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Autofocus.DataAccess.Repository
{
    public class SP_CALL : ISP_CALL
    {
        private readonly ApplicationDbContext _db;
        private static string ConnenctioString = "";
        public SP_CALL(ApplicationDbContext db)
        {
            _db = db;
            ConnenctioString = _db.Database.GetDbConnection().ConnectionString;

        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Execute(string ProcedureName, DynamicParameters param = null)
        {
            using(SqlConnection con=new  SqlConnection(ConnenctioString))
            {
                con.Open();
                con.Execute(ProcedureName,commandType:System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(ConnenctioString))
            {
                con.Open();
                //return  con.Query<T>(ProcedureName, commandType: System.Data.CommandType.StoredProcedure);
                return con.Query<T>(ProcedureName,param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(ConnenctioString))
            {
                con.Open();
                var res = SqlMapper.QueryMultiple(con, ProcedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = res.Read<T1>().ToList();
                var item2 = res.Read<T2>().ToList();

                if(item1!=null&&item2!=null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                }
                // con.Execute(ProcedureName, commandType: System.Data.CommandType.StoredProcedure, CommandType: System.Data.CommandType.StoredProcedure));
            }
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(),new List<T2>());

        }

        public T OneRecord<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(ConnenctioString))
            {
                con.Open();
                var val= con.Query<T>(ProcedureName, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(val.FirstOrDefault(), typeof(T));

            }
        }

        public T Single<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(ConnenctioString))
            {
                con.Open();
                return (T)Convert.ChangeType( con.ExecuteScalar<T>(ProcedureName, commandType: System.Data.CommandType.StoredProcedure),typeof(T));
            }
        }
    }
}
