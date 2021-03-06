﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Helper
{
    public class ReflectionHelper
    {
        ///<summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;
            Type o = Type.GetType(path);
            object obj = Activator.CreateInstance(o, true);
            return (T)obj;//类型转换并返回
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;
                Assembly asm = Assembly.Load(assemblyName);
                object ect = asm.CreateInstance(fullName);
                return (T)ect;
            }
            catch(Exception ex)
            {
                //发生异常，返回类型的默认值
                LogHelper.Error(ex);
                return default(T);
            }
        }

        public static AssemblyInfos GetAssemblyInfo(string path)
        {
            AssemblyInfos info = new AssemblyInfos();
            Assembly assembly = Assembly.LoadFrom(path);
            AssemblyName asmName = assembly.GetName();
            Type[] public_types = assembly.GetExportedTypes();
            foreach(Type type in public_types)
            {
                if (type.BaseType.Name == "ConnectInfo")
                {
                    info.AssemblyName = asmName.Name;
                    info.NameSpace = type.Namespace;
                    info.ClassName = type.Name;
                }
            }
            return info;
        }
    }

    public class AssemblyInfos
    {
        public string AssemblyName;
        public string NameSpace;
        public string ClassName;
    }
}
