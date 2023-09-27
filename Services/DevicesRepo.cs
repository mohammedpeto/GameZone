using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Services
{
    public class DevicesRepo : IDevicesRepo
    {
        public AppDbContext context { get; }

        public DevicesRepo(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            return context.Devices.Select(d =>
                 new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                  .ToList();
        }
    }
}
