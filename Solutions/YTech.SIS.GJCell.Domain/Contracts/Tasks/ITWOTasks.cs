﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface ITWOTasks
    {
        IEnumerable<TWO> GetAllWOs();
        TWO Insert(TWO wo);
        TWO Update(TWO wo);
        TWO Delete(TWO wo);
        TWO One(string woId);
        IEnumerable<TWOHaveRead> GetWOByDate(DateTime? dateFrom, DateTime? dateTo);

        IEnumerable<TWOHaveRead> GetListNotDeleted(string userName);

        IEnumerable<VWoByEmp> GetWoByEmp(DateTime? dateFrom, DateTime? dateTo);
    }
}
