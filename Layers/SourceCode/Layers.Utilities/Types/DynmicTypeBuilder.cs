using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.Types
{
    public static class DynmicTypeBuilder
    {
        public static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain
                .DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run)
                .DefineDynamicModule(moduleName)
                .DefineType(typeName, TypeAttributes.Public);

            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            return typeBuilder;
        }

        public static void CreateAutoImplementedProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string privateFieldPrefix = "m_";
            const string getterPrefix = "get_";
            const string setterPrefix = "set_";

            // Generate the field
            FieldBuilder fieldBuilder = builder.DefineField(string.Concat(privateFieldPrefix, propertyName), propertyType, FieldAttributes.Private);

            // Generate property
            PropertyBuilder propertyBuilder = builder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attribute
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define getter Method
            MethodBuilder getterMethod = builder.DefineMethod(string.Concat(getterPrefix, propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code
            // ldarg.0
            // ldfld,_field
            //ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method
            MethodBuilder setterMethod = builder.DefineMethod(string.Concat(setterPrefix, propertyName), propertyMethodAttributes, null, new Type[] { propertyType });

            // Emit the IL code
            // ldarg.0
            // ldarg.1
            // ldfld,_field
            //ret
            ILGenerator setterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldarg_1);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }


    }
}
