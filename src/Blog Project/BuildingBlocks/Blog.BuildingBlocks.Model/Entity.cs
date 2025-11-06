using Blog.BuildingBlocks.Model.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BuildingBlocks.Model
{
    public class Entity
    {
        private List<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEventes => _domainEvents.ToList();

        public void ClearDomainEvent()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents ??= [];
            _domainEvents.Add(domainEvent);
        }


        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
