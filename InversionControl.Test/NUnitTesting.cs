using System;
using NUnit.Framework;
using InversionControl;

namespace InversionControl.Test
{
    [TestFixture]
    public class NUnitTesting
    {
        [Test]
        public void CheckObjectsRegistered()
        {
            ExampleContainer container = new ExampleContainer();
            container.Register<IRepository, Repository>();
            Assert.That(container.IsTypeRegistered<IRepository>());
        }

        [Test]
        public void CheckResolveObject()
        {
            ExampleContainer container = new ExampleContainer();
            container.Register<IRepository, Repository>();
            var instance = container.Resolve<IRepository>();

            Assert.That(instance, Is.InstanceOf<Repository>());
        }

        [Test]
        public void CheckExceptionThrownTypeNotRegistered()
        {
            ExampleContainer container = new ExampleContainer();

            Exception exception = null;
            try { container.Resolve<IRepository>(); }
            catch (Exception ex) { exception = ex; }
            Assert.That(exception, Is.InstanceOf<TypeNotRegisteredException>());
        }

        [Test]
        public void CheckObjectWithParametersResolves()
        {
            ExampleContainer container = new ExampleContainer();
            container.Register<IRepository, Repository>();
            container.Register<IRepositoryWithParams, RepositoryWithParams>();
            var instance = container.Resolve<IRepositoryWithParams>();

            Assert.That(instance, Is.InstanceOf<IRepositoryWithParams>());
        }

        [Test]
        public void CheckTransientIsDefault()
        {
            ExampleContainer container = new ExampleContainer();
            container.Register<IRepository, Repository>();
            var instance = container.Resolve<IRepository>();

            Assert.That(container.Resolve<IRepository>(), Is.Not.SameAs(instance));
        }

        [Test]
        public void CheckSingltonIsCreated()
        {
            ExampleContainer container = new ExampleContainer();
            container.Register<IRepository, Repository>(LifestyleType.Singleton);
            var instance = container.Resolve<IRepository>();

            Assert.That(container.Resolve<IRepository>(), Is.SameAs(instance));
        }

    }
}
