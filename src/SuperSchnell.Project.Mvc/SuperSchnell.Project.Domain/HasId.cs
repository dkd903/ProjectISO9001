namespace SuperSchnell.Project.Domain
{
    public class HasId : IHasId
    {
        protected HasId(){}
        protected HasId(long id)
        {
            Id = id;
        }

        public virtual long Id { get; private set; }
    }
}