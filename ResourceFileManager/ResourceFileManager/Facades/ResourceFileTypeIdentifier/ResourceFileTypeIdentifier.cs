﻿using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using System;
using System.Reflection;

namespace ResourceFileManager.Facades.ResourceFileTypeIdentifiers
{
    public class ResourceFileTypeIdentifier : IResourceFileTypeIdentifier
    {
        public Type GetInstantiationType<TAttribute>(IdentifierFunc<TAttribute> strategySupportFunc, Assembly assembly)
            where TAttribute : IdentifierAttribute
        {
            if (strategySupportFunc == null) throw new NullReferenceException();

            Type initializerType = null;
            Type[] assemblyTypes = GetAssemblyTypes(assembly);

            foreach (Type type in assemblyTypes)
            {
                TAttribute[] attributes = GetAttributes<TAttribute>(type);
                if (attributes == null || attributes.Length <= 0) continue;

                bool isMeetingContidion = IsMeetingConditions(type, strategySupportFunc, attributes);

                if (isMeetingContidion)
                {
                    initializerType = type;
                    break;
                }
            }

            return initializerType;
        }

        private Type[] GetAssemblyTypes(Assembly assembly)
        {
            Type[] types = null;

            try
            {
                types = assembly?.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine(e.ToString());
            }

            return types;
        }

        private TAttribute[] GetAttributes<TAttribute>(Type type)
            where TAttribute : Attribute
        {
            TAttribute[] attributes = null;

            try
            {
                attributes = (TAttribute[])type?.GetCustomAttributes(typeof(TAttribute));
            }
            catch (Exception e) when (e is ArgumentNullException || e is ArgumentException
                || e is NotSupportedException || e is TypeLoadException)
            {
                Console.WriteLine(e.ToString());
            }

            return attributes;
        }

        private bool IsMeetingConditions<TAttribute>(Type type, IdentifierFunc<TAttribute> strategySupportFunc, TAttribute[] attributes)
            where TAttribute : IdentifierAttribute
        {
            bool condition = false;

            foreach (TAttribute attribute in attributes)
            {
                condition = strategySupportFunc.Invoke(attribute);

                if (condition)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
