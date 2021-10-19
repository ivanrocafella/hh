using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Interfaces
{
    public interface IAccountService<T, V, L, S, M, R>
    {
        Task<T> MakeUserForReg(R r, V v);
        Task<T> MakeUserForEdit(M m, V v);
        Task<S> TakeLoginEmail(L l);
        Task<M> GetUserbyName(string name);
    }
}
