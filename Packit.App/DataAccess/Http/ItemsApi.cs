﻿using Newtonsoft.Json;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class ItemsApi : BasicDataAccessHttp<Item>, IItems
    {
        //Implement methods that are not possible to make generic here.
    }
}