﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;

namespace YTech.SIS.GJCell.Tasks
{
    public class TWOStatusTasks : ITWOStatusTasks
    {
         private readonly ITWORepository _woRepository;
        private readonly ITWOLogRepository _woLogRepository;
        private readonly ITWOStatusRepository _woStatusRepository;

        public TWOStatusTasks(ITWORepository woRepository, ITWOLogRepository woLogRepository, ITWOStatusRepository woStatusRepository)
        {
            this._woRepository = woRepository;
            this._woLogRepository = woLogRepository;
            this._woStatusRepository = woStatusRepository;
        }

        public IEnumerable<Domain.TWOStatus> GetWOStatus(string woId)
        {
            IEnumerable<Domain.TWOStatus> wos = this._woStatusRepository.GetWOStatus(woId);
            return wos;
        }
    }
}