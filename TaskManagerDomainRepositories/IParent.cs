﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsc.TaskManager.DomainRepositories
{
    public interface IParent
    {
        string ParentTypeId { get; }
        long ParentRecordId { get; }
    }
}