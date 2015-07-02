using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl
{
    public enum LifestyleType { Transient, Singleton };

    public interface IContainer
    {
        void Register<Itype, Repository>();
        void Register<Itype, Repository>(LifestyleType lifestyle);
        Itype Resolve<Itype>();
        object Resolve(Type Itype);
    }
}
