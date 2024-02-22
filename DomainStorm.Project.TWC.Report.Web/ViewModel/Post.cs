namespace DomainStorm.Project.TWC.Report.Web.ViewModel
{
    public class Post
    {
        public Guid PostId { get; set; }

        public string Title { get; set; }

        public PostUser User { get; set; }

        public Department Department { get; set; }

        public List<Role> Roles { get; set; }

        public bool Enabled { get; set; }
    }

    public class PostDetail
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public string UserDn { get; set; }
        public string Account { get; set; }
        public string DisplayName { get; set; }
        public string UserCode { get; set; }

        public List<Role> Roles { get; set; }

        public bool Enabled { get; set; }
    }


    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IReadOnlyList<PostDetail>? PostDetails { get; set; }

        /// <summary>
        /// 子單位清單, 用在查詢組織樹時
        /// </summary>
        public IReadOnlyList<Department>? Departments { get; set; }

        public string AnotherCode { get; set; }
        public int? Level { get; set; }

    }

    public class PostUser
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }

        public string Code { get; set; }
    }

    public class Role
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 不適用於組織樹中的物件 (沒有同步更新) 
        /// </summary>
        public bool Agent { get; set; }

        /// <summary>
        /// 不適用於組織樹中的物件 (沒有同步更新) 
        /// </summary>
        public Guid? AgentPostId { get; set; }
    }
}
