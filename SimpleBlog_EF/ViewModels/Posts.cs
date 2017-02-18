using SimpleBlog_EF.Infrastructure;
using SimpleBlog_EF.Models.DataBase;

namespace SimpleBlog_EF.ViewModels
{
    public class PostsIndex
    {
        public PageData<Post> Posts { get; set; }
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }
        public PageData<Post> Posts { get; set; }
    }
}
