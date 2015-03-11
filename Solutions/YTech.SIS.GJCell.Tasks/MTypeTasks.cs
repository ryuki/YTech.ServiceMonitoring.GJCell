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
    public class MTypeTasks : IMTypeTasks
    {
        private readonly IMTypeRepository _typeRepository;

        public MTypeTasks(IMTypeRepository typeRepository)
        {
            this._typeRepository = typeRepository;
        }
        
        public void Insert(Domain.MType type)
        {
            _typeRepository.DbContext.BeginTransaction();
            _typeRepository.Save(type);
            _typeRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.MType type)
        {
            _typeRepository.DbContext.BeginTransaction();
            _typeRepository.Update(type);
            _typeRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.MType type)
        {
            _typeRepository.DbContext.BeginTransaction();
            _typeRepository.Delete(type);
            _typeRepository.DbContext.CommitTransaction();
        }

        public MType One(string typeId)
        {
            var typeomers = this._typeRepository.Get(typeId); ;
            return typeomers;
        }

        public IEnumerable<MType> GetListNotDeleted()
        {
            var types = this._typeRepository.GetListNotDeleted(); ;
            return types;
        }
    }
}
