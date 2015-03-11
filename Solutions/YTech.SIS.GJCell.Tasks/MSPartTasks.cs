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
    public class MSPartTasks : IMSPartTasks
{
        private readonly IMSPartRepository _spartRepository;

        public MSPartTasks(IMSPartRepository spartRepository)
        {
            this._spartRepository = spartRepository;
        }

        public IEnumerable<Domain.MSPart> GetAllSParts()
        {
            var sparts = this._spartRepository.GetAll(); ;
            return sparts;
        }

        public MSPart Insert(Domain.MSPart spart)
        {
            _spartRepository.DbContext.BeginTransaction();
            _spartRepository.Save(spart);
            _spartRepository.DbContext.CommitTransaction();
            return spart;
        }

        public MSPart Update(Domain.MSPart spart)
        {
            _spartRepository.DbContext.BeginTransaction();
            _spartRepository.Update(spart);
            _spartRepository.DbContext.CommitTransaction();
            return spart;
        }

        public MSPart Delete(Domain.MSPart spart)
        {
            _spartRepository.DbContext.BeginTransaction();
            _spartRepository.Delete(spart);
            _spartRepository.DbContext.CommitTransaction();
            return spart;
        }

        public MSPart One(string spartId)
        {
            var sparts = this._spartRepository.Get(spartId); ;
            return sparts;
        }

        public IEnumerable<MSPart> GetListNotDeleted()
        {
            var sparts = this._spartRepository.GetListNotDeleted();
            return sparts;
        }
    }
}
