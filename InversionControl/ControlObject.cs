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

        public Type TypeRegistered { get; private set; }
        public Type TypeImplemented { get; private set; }
        public LifestyleType Lifestyle { get; private set; }

        public ControlObject(Type typeToRegister, Type typeToImplement, LifestyleType lifestyle)
        {
            TypeRegistered = typeToRegister;
            TypeImplemented = typeToImplement;
            Lifestyle = lifestyle;
        }

        public bool IsRegisteredType<T>()
        {
            return TypeRegistered == typeof(T);
        }

        internal void Instantiate(params object[] args)
        {
            Instance = Activator.CreateInstance(this.TypeImplemented, args);
        }

    }
}
