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
    public class MCityTasks : IMCityTasks
    {
        private readonly IMCityRepository _cityRepository;

        public MCityTasks(IMCityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }
        
        public void Insert(Domain.MCity city)
        {
            _cityRepository.DbContext.BeginTransaction();
            _cityRepository.Save(city);
            _cityRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.MCity city)
        {
            _cityRepository.DbContext.BeginTransaction();
            _cityRepository.Update(city);
            _cityRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.MCity city)
        {
            _cityRepository.DbContext.BeginTransaction();
            _cityRepository.Delete(city);
            _cityRepository.DbContext.CommitTransaction();
        }

        public MCity One(string cityId)
        {
            var citys = this._cityRepository.Get(cityId); ;
            return citys;
        }

        public IEnumerable<MCity> GetListNotDeleted()
        {
            var citys = this._cityRepository.GetListNotDeleted(); ;
            return citys;
        }
    }
}
