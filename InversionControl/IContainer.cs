using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl
{
    public enum LifestyleType { Transient, Singleton };

    // This is completely unnecessary (an interface for the container), But it seemed like a good idea at the time.
    public interface IContainer
    {
        void Register<IRepository, Repository>();
        void Register<IRepository, Repository>(LifestyleType lifestyle);
        IRepository Resolve<IRepository>();
        object Resolve(Type IRepository);
    }
}
