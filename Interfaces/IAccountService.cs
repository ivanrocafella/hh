using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Interfaces
{
    public interface IAccountService<T, V, I, L, S>
    {
        Task<T> MakeUser(V v, I i);
        Task<S> TakeLoginEmail(L l);
    }
}
