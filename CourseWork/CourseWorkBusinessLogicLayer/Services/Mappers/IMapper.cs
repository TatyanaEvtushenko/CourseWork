namespace CourseWork.BusinessLogicLayer.Services.Mappers
{
    public interface IMapper<T, TV>
    {
        TV ConvertTo(T item);

        T ConvertFrom(TV item);
    }
}
