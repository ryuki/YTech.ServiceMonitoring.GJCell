using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface ITWOTrackTasks
    {
        void Insert(TWOTrack woTrack);
        void Update(TWOTrack woTrack);
        void Delete(TWOTrack woTrack);
        TWOTrack One(string woTrackId);
        IEnumerable<TWOTrack> GetListByWOId(string woId);
        void ConfirmTrack(TWOTrack woTrack);
    }
}
