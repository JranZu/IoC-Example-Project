using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl
{
    public class ExampleContainer : IContainer
    {

        #region Fields & Properties

        public readonly List<ControlObject> ObjectList = new List<ControlObject>();

        /// <summary>
        /// A string of name separated by HTML breakpoints suitable for quick output
        /// </summary>
        public string ObjectNames
        {
            get
            {
                if (ObjectList == null || ObjectList.Count == 0) { return "No Object Registered"; }

                string objectNames = string.Format("{0} Registered Types:", ObjectList.Count);
                foreach (ControlObject co in ObjectList)
                {
                    objectNames = string.Format(@"{0}<br/> {1}", objectNames, co.TypeRegistered.Name);
                }
                return objectNames;
            }
        }

        #endregion


        #region Interface Implementations

        public void Register<TypeRegistered, TypeImplemented>()
        {
            Register<TypeRegistered, TypeImplemented>(LifestyleType.Transient);
        }
        public void Register<TypeRegistered, TypeImplemented>(LifestyleType lifeStyle)
        {
            // I am thinking that if the object type is already in the list we just want to ignore this.
            // We could also throw an exception if it's already registered but this seems unnecessary
            // and possible undesirable
            if (!IsTypeRegistered<TypeRegistered>())
            {
                ObjectList.Add(new ControlObject(typeof(TypeRegistered), typeof(TypeImplemented), lifeStyle));
            }
        }

        public object Resolve(Type typeRegistered)
        {
            return ResolveObject(typeRegistered);
        }
        public TypeRegistered Resolve<TypeRegistered>()
        {
            return (TypeRegistered)ResolveObject(typeof(TypeRegistered));
        }
        private object ResolveObject(Type typeRegistered)
        {
            if (typeRegistered == null)
            {
                //throw new ArgumentNullException("ResolveObject: typeTypeRegistered cannot be null");
                // I am not sure why, but on rare occasions ASP.Net pages throw the above exception when no resolve request should be called
                // It seems safe to me to just return null and have the other end take care of resolving for null
                return null;
            }

            var thisObject = ObjectList.FirstOrDefault(co => co.TypeRegistered == typeRegistered);
            if (thisObject == null)
            {
                // If this was a larger project I would create/use a custom exceptions class, however for this it seems overkill
                throw new TypeNotRegisteredException(string.Format("ControlObject not registered: {0}", typeRegistered.Name));
            }
            return GetInstance(thisObject);
        }

        #endregion


        #region Helper Methods

        public bool IsTypeRegistered<TypeRegistered>()
        {
            return ObjectList.Any(co => co.IsRegisteredType<TypeRegistered>());
        }

        // The below methods could also be placed in the ControlObject class
        // Is one place better than the other?

        private object GetInstance(ControlObject thisObject)
        {
            if (thisObject.Instance == null || thisObject.Lifestyle == LifestyleType.Transient)
            {
                var parameters = ResolveConstructorParameters(thisObject);
                thisObject.Instantiate(parameters.ToArray());
            }

            return thisObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(ControlObject thisObject)
        {
            var constructorInfo = thisObject.TypeImplemented.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return Resolve(parameter.ParameterType);
            }
        }

        #endregion
    }
}
