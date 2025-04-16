using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.LocalData.Entity
{
    public class UserLocalEntity : IEquatable<UserLocalEntity>
    {
        public required string FIO { get; set; }
        public Guid Guid { get; set; }
        public required int GroupId { get; set; }

        public bool Equals(UserLocalEntity? other)
        {
            if (other == null) return false;
            return Guid.Equals(other.Guid);
        }
    }
}