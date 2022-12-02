using AutoMapper;
using System.Reflection;

namespace Task4.Application.Common.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappings(GetImplementedTypesFromAssembly(typeof(IMapWith<>), assembly));

        private void ApplyMappings(List<Type> types)
        {
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] {this});
            }
        }

        public static List<Type> GetImplementedTypesFromAssembly(Type implementedType, Assembly assembly) =>
            assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == implementedType))
                .ToList();
    }
}
