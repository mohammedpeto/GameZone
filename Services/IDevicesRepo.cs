using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Services
{
   public  interface IDevicesRepo
    {
        IEnumerable<SelectListItem> GetSelectLists();
    }
}
