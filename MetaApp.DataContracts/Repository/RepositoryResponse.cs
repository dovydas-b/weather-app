namespace MetaApp.DataContracts.Repository
{
    public class RepositoryResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }
    }
}