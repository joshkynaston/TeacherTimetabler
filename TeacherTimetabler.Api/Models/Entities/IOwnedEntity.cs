using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherTimetabler.Api.Models.Entities;

public interface IOwnedEntity
{
    public string UserEntityId { get; set; }
    public AppUserEntity UserEntity { get; set; }
}
