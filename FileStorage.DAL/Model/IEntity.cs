using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Model
{
    public interface IEntity<T>
    {
        T Id { set; get; }
    }
}
