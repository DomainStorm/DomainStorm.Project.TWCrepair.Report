using System.ComponentModel.DataAnnotations;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWC.Report.Web.CommandModel
{
    public static class User
    {
        public static class V1
        {
            public class CreateUser
            {
                public Guid UserId { get; set; }

                [Required]
                public string Code { get; set; }

                [Required]
                public string FirstName { get; set; }

                [Required]
                public string LastName { get; set; }

                [Required]
                public string Account { get; set; }

                [Required]
                public string Password { get; set; }

                [Required]
                public string Email { get; set; }

                [Required]
                public string TelephoneNumber { get; set; }
            }

            public class UpdateFullName : IUpdate
            {
                public Guid Id { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }

            public class UpdateDisplayName : IUpdate
            {
                public Guid Id { get; set; }
                public string DisplayName { get; set; }
            }

            public class DeleteUser : IUpdate
            {
                public Guid Id { get; set; }
            }

            public class SearchUserCommand : IQuery
            {

                public Guid? UserId { get; set; }

                public string? Code { get; set; }

                public string? FirstName { get; set; }
                public string? LastName { get; set; }
                public string? DisplayName { get; set; }
                public string? Account { get; set; }

                public string? Email { get; set; }

                public string? TelephoneNumber { get; set; }

                public int PageIndex { get; set; } = 0;
                public int PageRows { get; set; } = 10;

                public string? OrderingFieldName { get; set; }
                public OrderType OrderType { get; set; }
            }

            public class KeyWordSearchUserCommand : IQuery
            {
                public string KeyWord { get; set; }

                public int PageIndex { get; set; } = 0;
                public int PageRows { get; set; } = 10;

                public string? OrderingFiledName { get; set; }
                public OrderType OrderType { get; set; }
            }
        }
    }
}
