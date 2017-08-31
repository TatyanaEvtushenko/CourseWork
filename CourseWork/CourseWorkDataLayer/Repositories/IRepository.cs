namespace CourseWork.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        T AddItem(string id);
    }
}
