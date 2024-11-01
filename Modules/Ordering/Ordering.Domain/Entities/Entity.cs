using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var entity = (Entity)obj;
            return Id == entity.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);
    }

}
