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
    public class TWOTrackTasks : ITWOTrackTasks
    {
        private readonly ITWOTrackRepository _woTrackRepository;
        private readonly ITWORepository _woRepository;

        public TWOTrackTasks(ITWOTrackRepository woTrackRepository, ITWORepository woRepository)
        {
            this._woTrackRepository = woTrackRepository;
            this._woRepository = woRepository;
        }
        
        public void Insert(Domain.TWOTrack woTrack)
        {
            _woTrackRepository.DbContext.BeginTransaction();
            _woTrackRepository.Save(woTrack);
            _woTrackRepository.DbContext.CommitTransaction();
        }

        public void Update(Domain.TWOTrack woTrack)
        {
            _woTrackRepository.DbContext.BeginTransaction();
            _woTrackRepository.Update(woTrack);
            _woTrackRepository.DbContext.CommitTransaction();
        }

        public void Delete(Domain.TWOTrack woTrack)
        {
            _woTrackRepository.DbContext.BeginTransaction();
            _woTrackRepository.Delete(woTrack);
            _woTrackRepository.DbContext.CommitTransaction();
        }

        public Domain.TWOTrack One(string woTrackId)
        {
            var woTrack = this._woTrackRepository.Get(woTrackId); ;
            return woTrack;
        }

        public IEnumerable<TWOTrack> GetListByWOId(string woId)
        {
            var woTracks = this._woTrackRepository.GetListByWOId(woId); ;
            return woTracks;
        }

        public void ConfirmTrack(TWOTrack woTrack)
        {
            _woTrackRepository.DbContext.BeginTransaction();
            _woTrackRepository.Update(woTrack);
            
            //update wo last track
            TWO wo = woTrack.WOId;
            wo.WOUnitLastTrack = woTrack.WOTrackTo;
            _woRepository.Update(wo);

            _woTrackRepository.DbContext.CommitTransaction();
        }
    }
}
