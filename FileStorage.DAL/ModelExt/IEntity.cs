using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.ModelExt
{
    public interface IEntity<T>
    {
        T Id { set; get; }
    }
}
