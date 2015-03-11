using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Enums;

namespace YTech.SIS.GJCell.Infrastructure.Repository
{
    public class TWORepository : NHibernateRepositoryWithTypedId<TWO, string>, ITWORepository
    {
        public IEnumerable<TWOHaveRead> GetWOByDate(DateTime? dateFrom, DateTime? dateTo)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            //criteria.Add(Expression.Between("WODate", dateFrom, dateTo));
            //criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            //criteria.AddOrder(Order.Asc("WODate"));
            //return criteria.List<TWO>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   EXEC [dbo].[SP_GET_LIST_WO_READ]
                        		@User_Name = :UserName,
		                        @Date_From = :dateFrom,
		                        @Date_To = :dateTo ");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));
            q.SetString("UserName", "");
            q.SetDateTime("dateFrom", dateFrom.Value);
            q.SetDateTime("dateTo", dateTo.Value);

            return q.List<TWOHaveRead>();
        }

        public IEnumerable<TWOHaveRead> GetListNotDeleted(string userName)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            //criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            //return criteria.List<TWO>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   EXEC [dbo].[SP_GET_LIST_WO_READ]
                        		@User_Name = :UserName ");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));
            q.SetString("UserName", userName);
            
            return q.List<TWOHaveRead>();
            //TWO
            //List<TWO> result = new List<TWO>(q.List());
            //return result;
        }

        public IEnumerable<VWoByEmp> GetWoByEmp(DateTime? dateFrom, DateTime? dateTo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(VWoByEmp));
            return criteria.List<VWoByEmp>();
        }
    }
}
