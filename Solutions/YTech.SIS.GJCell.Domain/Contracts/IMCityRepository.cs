﻿using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public interface IMCityRepository : INHibernateRepositoryWithTypedId<MCity, string>
    {
        IEnumerable<MCity> GetListNotDeleted();
    }
}
