using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog_EF.ViewModels
{

    public class SideBarTag
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public int PostCount { get; private set; }

        public SideBarTag(int id, string name, string slug, int postCount)
        {
            Id = id;
            Name = name;
            Slug = slug;
            PostCount = postCount;
        }
    }


    public class LayoutSideBar
    {
        public bool IsLoggedIn { get; set; }
        public string  UserName { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<SideBarTag> Tags { get; set; }
    }
}
