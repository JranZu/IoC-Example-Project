using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl
{
    public class ControlObject
    {
        internal object Instance;

        public Type TypeOfIRepository { get; private set; }
        public Type TypeOfRepository { get; private set; }
        public LifestyleType Lifestyle { get; private set; }

        public ControlObject(Type typeOfIRepository, Type typeOfRepository, LifestyleType lifestyle)
        {
            TypeOfIRepository = typeOfIRepository;
            TypeOfRepository = typeOfRepository;
            Lifestyle = lifestyle;
        }

        public bool IsType<T>()
        {
            return TypeOfIRepository == typeof(T);
        }

        internal void Instantiate(params object[] args)
        {
            Instance = Activator.CreateInstance(this.TypeOfRepository, args);
        }

    }
}
