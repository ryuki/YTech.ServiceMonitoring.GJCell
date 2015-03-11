using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Enums;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface ITReferenceTasks
    {
        TReference GetByReferenceType(EnumReferenceType referenceType);
        TReference Update(Domain.TReference reference);
    }
}
