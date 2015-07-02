using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl.Test
{
    public interface IRepository
    {
    }

    public class Repository : IRepository
    {
    }

    public interface IRepositoryWithParams
    {
    }

    public class RepositoryWithParams : IRepositoryWithParams
    {
        public RepositoryWithParams(IRepository typeToResolve)
        {
        }
    }
}
