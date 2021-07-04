using BoggleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleApi.Services
{
    public interface IBoggleBoxService
    {
        BoggleBox GetBoggleBox();
        BoggleBox GetBoggleBox(Guid guid);
    }
}
