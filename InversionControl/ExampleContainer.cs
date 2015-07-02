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

        public string ObjectNames
        {
            get
            {
                if (ObjectList == null || ObjectList.Count == 0) { return "No Object Registered"; }

                string objectNames = string.Format("Registered Objects: {0}", ObjectList.Count);
                foreach (ControlObject co in ObjectList)
                {
                    objectNames = string.Format(@"{0}<br/>  {1}", objectNames, co.TypeOfIRepository.Name);
                }
                return objectNames;
            }
        }

        #endregion


        #region Interface Implementations

        public void Register<IRepository, Repository>()
        {
            Register<IRepository, Repository>(LifestyleType.Transient);
        }
        public void Register<IRepository, Repository>(LifestyleType lifeStyle)
        {
            // I am thinking that if the object type is already in the list we just want to ignore this.
            // We could also throw an exception if it's already registered but this seems unnecessary.
            if (!IsTypeRegistered<IRepository>())
            {
                ObjectList.Add(new ControlObject(typeof(IRepository), typeof(Repository), lifeStyle));
            }
        }

        public object Resolve(Type IRepository)
        {
           return ResolveObject(IRepository);
        }
        public IRepository Resolve<IRepository>()
        {
            return (IRepository)ResolveObject(typeof(IRepository));
        }
        private object ResolveObject(Type typeIRepository)
        {
            if (typeIRepository == null) { throw new ArgumentNullException("ResolveObject: typeIRepository cannot be null"); }

            var thisObject = ObjectList.FirstOrDefault(co => co.TypeOfIRepository == typeIRepository);
            if (thisObject == null)
            {
                // If this was a larger project I would create/use a custom exceptions class, however for this it seems overkill
                throw new Exception(string.Format("ControlObject not registered: {0}", typeIRepository.Name));
            }
            return GetInstance(thisObject);
        }

        #endregion


        #region Helper Methods

        public bool IsTypeRegistered<IRepository>()
        {
            return ObjectList.Any(co => co.IsType<IRepository>());
        }

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
            var constructorInfo = thisObject.TypeOfRepository.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return Resolve(parameter.ParameterType);
            }
        }

        #endregion
    }
}
