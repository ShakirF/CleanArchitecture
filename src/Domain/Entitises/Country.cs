using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entitises
{
    public class Country : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? PhoneAreaCode { get; set; }
    }
}
