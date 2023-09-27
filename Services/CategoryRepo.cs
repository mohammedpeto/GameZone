using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Services
{
    public class CategoryRepo : ICategoryRepo
    {
        public AppDbContext context { get; }

        public CategoryRepo(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            return context.Categories.Select(c =>
               new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name })
               .OrderBy(c => c.Text)
               .ToList();
        }
    }
}
