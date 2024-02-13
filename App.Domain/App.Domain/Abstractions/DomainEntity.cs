namespace App.Domain.Abstractions
{
    public abstract class DomainEntity
    {
        private readonly List<IDomainEvent> _domainEvent = new();

        protected DomainEntity(Guid id) => Id = id;


        public Guid Id { get; init; }


        public List<IDomainEvent> GetDomainEvents()
        {
            return _domainEvent.ToList();
        }
        public void ClearDomainEvents()
        {
            _domainEvent.Clear();
        }

        protected void PublishDomainEvent(IDomainEvent domainEvent) 
        { 
            _domainEvent.Add(domainEvent);
        }
    }
}
