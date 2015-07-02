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
        void Register<TypeRegistered, TypeImplemented>();
        void Register<TypeRegistered, TypeImplemented>(LifestyleType lifestyle);
        TypeRegistered Resolve<TypeRegistered>();
        object Resolve(Type TypeRegistered);
    }
}
