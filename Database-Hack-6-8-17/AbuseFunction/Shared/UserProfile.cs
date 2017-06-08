using System;
#if NET461
using System.Data.Linq.Mapping;
#endif

namespace Shared
{
#if NET461
    [Table(Name = "UserProfile")]
#endif
    public class UserProfile
    {
#if NET461
        [Column(Name = "Id", IsPrimaryKey = true, CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public Guid Id { get; set; }

#if NET461
        [Column(Name = "PreferredName", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string PreferredName { get; set; }

#if NET461
        [Column(Name = "FirstName", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string FirstName { get; set; }

#if NET461
        [Column(Name = "LastName", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string LastName { get; set; }

#if NET461
        [Column(Name = "Birthdate", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public DateTimeOffset Birthdate { get; set; }

#if NET461
        [Column(Name = "Gender", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string Gender { get; set; }

#if NET461
        [Column(Name = "PhoneNumber", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string PhoneNumber { get; set; }

#if NET461
        [Column(Name = "PhoneType", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string PhoneType { get; set; }

#if NET461
        [Column(Name = "TeacherId", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public Guid TeacherId { get; set; }

#if NET461
        [Column(Name = "CompanyId", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public Guid CompanyId { get; set; }

#if NET461
        [Column(Name = "EmailVerified", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public bool? EmailVerified { get; set; }

#if NET461
        [Column(Name = "Email", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
#endif
        public string Email { get; set; }
    }
}
