using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using YTech.SIS.GJCell.Enums;

namespace YTech.SIS.GJCell.Tasks
{
    public class TWOSPartTasks : ITWOSPartTasks
    {
        private readonly ITWORepository _woRepository;
        private readonly ITWOLogRepository _woLogRepository;
        private readonly ITWOStatusRepository _woStatusRepository;
        private readonly ITWOSPartRepository _woSPartRepository;

        public TWOSPartTasks(ITWORepository woRepository, ITWOLogRepository woLogRepository, ITWOStatusRepository woStatusRepository, ITWOSPartRepository woSPartRepository)
        {
            this._woRepository = woRepository;
            this._woLogRepository = woLogRepository;
            this._woStatusRepository = woStatusRepository;
            this._woSPartRepository = woSPartRepository;
        }

        public void Insert(Domain.TWOSPart woSpart)
        {
            _woSPartRepository.DbContext.BeginTransaction();
            _woSPartRepository.Save(woSpart);
            _woSPartRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.TWOSPart woSpart)
        {
            _woSPartRepository.DbContext.BeginTransaction();
            _woSPartRepository.Update(woSpart);
            _woSPartRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.TWOSPart woSpart)
        {
            _woSPartRepository.DbContext.BeginTransaction();
            _woSPartRepository.Delete(woSpart);
            _woSPartRepository.DbContext.CommitTransaction();
        }

        public TWOSPart One(string woSpartId)
        {
            var woSpart = this._woSPartRepository.Get(woSpartId); ;
            return woSpart;
        }


        public IEnumerable<TWOSPart> GetAll()
        {
            var woSpart = this._woSPartRepository.GetAll(); ;
            return woSpart;
        }


        public IEnumerable<TWOSPart> GetListBySPartDate(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            var woSpart = this._woSPartRepository.GetListBySPartDate(rptDateFrom, rptDateTo); ;
            return woSpart;
        }
    }
}
