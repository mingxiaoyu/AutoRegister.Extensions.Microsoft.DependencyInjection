using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Reflection
{
    internal static class AssemblyExtensions
    {
        public static IEnumerable<TypeInfo> GetAllTypesOfAttribute<T>(this Assembly[] assemblies, bool inherit = true)
           where T : Attribute
        {
            return GetAllTypesOfAttribute(assemblies, typeof(T));

        }
        public static IEnumerable<TypeInfo> GetAllTypesOfAttribute<T>(this Assembly assembly, bool inherit = true)
          where T : Attribute
        {

            return GetAllTypesOfAttribute(assembly, typeof(T));

        }

        public static IEnumerable<TypeInfo> GetAllTypesOfAttribute(this Assembly[] assemblies, Type type, bool inherit = true)
        {
            if (type.BaseType != typeof(Attribute)) throw new NotSupportedException($"The {type} is not Attribute");
            foreach (var assembly in assemblies)
            {
                foreach (var typeInfo in assembly.DefinedTypes)
                {
                    if (typeInfo.IsDefined(type, inherit))
                    {
                        yield return typeInfo;
                    }
                }
            }
        }
        public static IEnumerable<TypeInfo> GetAllTypesOfAttribute(this Assembly assembly, Type type, bool inherit = true)
        {
            if (type.BaseType != typeof(Attribute)) throw new NotSupportedException($"The {type} is not Attribute");
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.IsDefined(type, inherit))
                {
                    yield return typeInfo;
                }
            }

        }

        public static IEnumerable<TypeInfo> GetImplementingTypesOf<T>(this Assembly[] assemblies)
        {
            return GetAllTypesOf<T>(assemblies).Where(typeInfo => !typeInfo.IsAbstract && !typeInfo.IsInterface);
        }
        public static IEnumerable<TypeInfo> GetImplementingTypesOf<T>(this Assembly assembly)
        {
            return GetAllTypesOf<T>(assembly).Where(typeInfo => !typeInfo.IsAbstract && !typeInfo.IsInterface);
        }

        public static IEnumerable<TypeInfo> GetAllTypesOf<T>(this Assembly[] assemblies)
        {
            return GetAllTypesOf(assemblies, typeof(T));
        }
        public static IEnumerable<TypeInfo> GetAllTypesOf<T>(this Assembly assembly)
        {
            return GetAllTypesOf(assembly, typeof(T));
        }

        public static IEnumerable<TypeInfo> GetImplementingTypesOf(this Assembly[] assemblies, Type type)
        {
            return GetAllTypesOf(assemblies, type).Where(typeInfo => !typeInfo.IsAbstract && !typeInfo.IsInterface);
        }
        public static IEnumerable<TypeInfo> GetImplementingTypesOf(this Assembly assembly, Type type)
        {
            return GetAllTypesOf(assembly, type).Where(typeInfo => !typeInfo.IsAbstract && !typeInfo.IsInterface);
        }

        public static IEnumerable<TypeInfo> GetAllTypesOf(this Assembly[] assemblies, Type type)
        {
            foreach (var assembly in assemblies)
            {
                foreach (TypeInfo typeInfo in assembly.DefinedTypes)
                {
                    if (typeInfo.ImplementedInterfaces.Contains(type) ||
                        (typeInfo.ImplementedInterfaces.Any(x => x.IsGenericType && x.IsKindOfGeneric(type)))
                        )
                    {
                        yield return typeInfo;
                    }
                }
            }

        }
        public static IEnumerable<TypeInfo> GetAllTypesOf(this Assembly assembly, Type type)
        {

            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.ImplementedInterfaces.Contains(type) ||
                         (typeInfo.ImplementedInterfaces.Any(x => x.IsGenericType && x.IsKindOfGeneric(type)))
                         )
                {
                    yield return typeInfo;
                }
            }

        }

    }
}
