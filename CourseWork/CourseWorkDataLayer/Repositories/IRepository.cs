using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWorkDataLayer.Repositories
{
    public interface IRepository<T>
    {
        T AddItem(string id);
    }
}
