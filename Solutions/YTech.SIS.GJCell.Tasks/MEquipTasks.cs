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
    public class MEquipTasks : IMEquipTasks
    {
        private readonly IMEquipRepository _equipRepository;

        public MEquipTasks(IMEquipRepository equipRepository)
        {
            this._equipRepository = equipRepository;
        }
        
        public void Insert(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Save(equip);
            _equipRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Update(equip);
            _equipRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Delete(equip);
            _equipRepository.DbContext.CommitTransaction();
        }

        public MEquip One(string equipId)
        {
            var equips = this._equipRepository.Get(equipId); ;
            return equips;
        }

        public IEnumerable<MEquip> GetListNotDeleted()
        {
            var equips = this._equipRepository.GetListNotDeleted(); ;
            return equips;
        }
    }
}
