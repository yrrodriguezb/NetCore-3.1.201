using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection
{
    public class Container
    {
        Dictionary<Type, Type> _map = new Dictionary<Type, Type>();
        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(this, sourceType);
        }

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if(_map.ContainsKey(sourceType))
            {
                var destinationType = _map[sourceType];
                return CreateInstance(destinationType);
            } 
            else if (sourceType.IsGenericType && _map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = _map[sourceType.GetGenericTypeDefinition()];
                var closedDestination = destination.MakeGenericType(sourceType.GenericTypeArguments);
                return CreateInstance(closedDestination);
            }
            else if (!sourceType.IsAbstract)
            {
                return CreateInstance(sourceType);
            }

            throw new InvalidOperationException("Could not resolve" + sourceType.FullName);
        }

        private object CreateInstance(Type destinationType)
        {
            var parameters = destinationType.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Count())
                .First()
                .GetParameters()
                .Select(parameter => Resolve(parameter.ParameterType))
                .ToArray();

            return Activator.CreateInstance(destinationType, parameters);
        }

        public class ContainerBuilder
        {
            Container _container;
            Type _sourceType;

            public ContainerBuilder(Container container, Type sourceType)
            {
                _container = container;
                _sourceType = sourceType;    
            }

            public ContainerBuilder Use<TDestination>()
            {
                return Use(typeof(TDestination));
            }

            public ContainerBuilder Use(Type destinationType)
            {
                _container._map.Add(_sourceType, destinationType);
                return this;
            }
        }
    }
}