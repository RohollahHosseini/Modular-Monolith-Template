
using Blog.BuildingBlocks.Model.BusinessRule;

namespace Blog.BuildingBlocks.Model
{
    public abstract class BaseEntity<TKey> :Entity, IEntity
    {

        public TKey Id { get; protected set; }
        public bool IsDeleted { get; } = false;
        public DateTime CreatedTime { get; } = DateTime.UtcNow;
        public DateTime? ModifiedTime { get; set; }

     
        public override bool Equals(object? obj)
        {
            if (!(obj is BaseEntity<TKey> other))
                return false;

            if (ReferenceEquals(this, other))
               return true;

            if (GetType() != other.GetType())
                return false;


            if (EqualityComparer<TKey>.Default.Equals(Id, default) ||
               EqualityComparer<TKey>.Default.Equals(other.Id, default))
                return false;

            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public static bool operator !=(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            return !(a == b);
        }

        public static bool operator ==(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if(a is null || b is null)
                return false;   

            return a.Equals(b);

        }
       
        public override int GetHashCode()
        {
            return (GetType().ToString()+Id).GetHashCode();
        }

    }
}
