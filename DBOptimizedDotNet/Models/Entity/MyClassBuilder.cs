using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using PropertyAttributes = System.Reflection.PropertyAttributes;

namespace ClassGenerator
{
    public interface IKoopObject
    {

    }

    //public interface IKoopQuery<T>
    //{
    //    public DbCommand Command { get; set; }
    //    public string Query { get; set; }

    //    public KeyValuePair<string, object> Parameters { get; set; }
    //}

    //public class KoopQuery<T> : IKoopQuery<T>
    //{
    //    public DbCommand Command { get; set; }
    //    public string Query { get; set; }
    //    public KeyValuePair<string, object> Parameters { get; set; }
    //}


    
    public class MyClassBuilder
    {
        readonly AssemblyName _asemblyName;

        public MyClassBuilder(string className)
        {
            _asemblyName = new AssemblyName(className);
        }

        public object CreateObject(Dictionary<string, object> item)
        {
            if (item.Count == 0) return null;

            var dynamicClass = CreateClass();
            CreateConstructor(dynamicClass);

            foreach (var field in item)
            {
                CreateProperty(dynamicClass, field.Key, typeof(object));

            }

            Type type = dynamicClass.CreateTypeInfo();
 
 

            return Activator.CreateInstance(type);
        }

        private TypeBuilder CreateClass()
        {
            AssemblyBuilder assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(_asemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType(_asemblyName.FullName
                , TypeAttributes.Public |
                  TypeAttributes.Class |
                  TypeAttributes.AutoClass |
                  TypeAttributes.AnsiClass |
                  TypeAttributes.BeforeFieldInit |
                  TypeAttributes.AutoLayout
                , null);
             
            typeBuilder.AddInterfaceImplementation(typeof(IKoopObject));

            return typeBuilder;
        }

        private void CreateConstructor(TypeBuilder typeBuilder)
        {
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName |
                                                 MethodAttributes.RTSpecialName);
        }

        private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder =
                typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder =
                typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName,
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType,
                Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,
                null, new[] {propertyType});

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}
