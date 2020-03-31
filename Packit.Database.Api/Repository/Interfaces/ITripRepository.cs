﻿using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Generic;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    public interface ITripRepository : IGenericRepository<Trip>, IGenericManyToManyRepository<Trip>
    {
        //Declare methods that are possible to make generic here.
    }
}
