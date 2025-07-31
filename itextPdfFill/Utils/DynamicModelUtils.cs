using System.Reflection.Emit;
using System.Reflection;

namespace itextPdfFill.Utils
{
    internal static class DynamicModelUtils
    {
        // Method to dynamically create a class type with properties from the list of strings
        public static Type CreateDynamicType(List<string> propertyNames)
        {
            // Define a dynamic assembly and module
            AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

            // Define a public class named "DynamicClass"
            TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicClass", TypeAttributes.Public);

            // Add properties to the class based on the property names
            foreach (string propertyName in propertyNames)
            {
                // Define the property as a string type for simplicity (you can change this as needed)
                AddProperty(typeBuilder, propertyName, typeof(string));
            }

            // Create the type
            return typeBuilder.CreateTypeInfo().AsType();
        }

        // Helper method to add a property to the dynamic class
        public static void AddProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            // Define the private field
            FieldBuilder fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            // Define the public property
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // Define the getter method for the property
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod(
                "get_" + propertyName,
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                propertyType,
                Type.EmptyTypes);

            ILGenerator getIL = getMethodBuilder.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            // Define the setter method for the property
            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod(
                "set_" + propertyName,
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                null,
                new Type[] { propertyType });

            ILGenerator setIL = setMethodBuilder.GetILGenerator();
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);

            // Assign the getter and setter methods to the property
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }
    }
}
