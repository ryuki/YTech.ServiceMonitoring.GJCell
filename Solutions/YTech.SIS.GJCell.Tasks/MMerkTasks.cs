using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using SharpArch.Domain;
using YTech.SIS.GJCell.Infrastructure.Repository;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Tasks
{
    public class MMerkTasks : IMMerkTasks
    {
        private readonly IMMerkRepository _merkRepository;

        public MMerkTasks(IMMerkRepository merkRepository)
        {
            this._merkRepository = merkRepository;
        }
        
        public void Insert(Domain.MMerk merk)
        {
            _merkRepository.DbContext.BeginTransaction();
            _merkRepository.Save(merk);
            _merkRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.MMerk merk)
        {
            _merkRepository.DbContext.BeginTransaction();
            _merkRepository.Update(merk);
            _merkRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.MMerk merk)
        {
            _merkRepository.DbContext.BeginTransaction();
            _merkRepository.Delete(merk);
            _merkRepository.DbContext.CommitTransaction();
        }

        public MMerk One(string merkId)
        {
            var merkomers = this._merkRepository.Get(merkId); ;
            return merkomers;
        }

        public IEnumerable<MMerk> GetListNotDeleted()
        {
            var merks = this._merkRepository.GetListNotDeleted(); ;
            return merks;
        }
    }
}
