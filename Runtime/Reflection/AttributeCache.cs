using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Syrinj.Attributes;

namespace Syrinj.Reflection
{
    public class AttributeCache
    {
        private readonly Dictionary<Type, List<MemberInfo>> cachedTypes;
        private readonly HashSet<MemberInfo> cachedMembers;
        private readonly Dictionary<MemberInfo, List<UnityInjectorAttribute>> injectorAttributes;
        private readonly Dictionary<MemberInfo, List<UnityProviderAttribute>> providerAttributes;
        private readonly Dictionary<MemberInfo, List<UnityDependencyAttribute>> nonLazySingletonAttributes;

        public AttributeCache()
        {
            cachedTypes = new Dictionary<Type, List<MemberInfo>>();
            cachedMembers = new HashSet<MemberInfo>();
            injectorAttributes = new Dictionary<MemberInfo, List<UnityInjectorAttribute>>();
            providerAttributes = new Dictionary<MemberInfo, List<UnityProviderAttribute>>();
            nonLazySingletonAttributes = new Dictionary<MemberInfo, List<UnityDependencyAttribute>>();
        }

        public List<UnityInjectorAttribute> GetInjectorAttributesForMember(MemberInfo info)
        {
            //TryCacheMember(info);

            if (injectorAttributes.ContainsKey(info))
            {
                return injectorAttributes[info];
            }
            else
            {
                return null;
            }
        }

        public List<UnityProviderAttribute> GetProviderAttributesForMember(MemberInfo info)
        {
            //TryCacheMember(info);

            if (providerAttributes.ContainsKey(info))
            {
                return providerAttributes[info];
            }
            else
            {
                return null;
            }
        }
        
        public List<UnityDependencyAttribute> GetNonLazySingletonAttributesForMember(MemberInfo info)
        {
            //TryCacheMember(info);

            if (nonLazySingletonAttributes.ContainsKey(info))
            {
                return nonLazySingletonAttributes[info];
            }
            else
            {
                return null;
            }
        }

        public List<MemberInfo> GetMembersForType(Type type)
        {
            TryCacheType(type);

            return cachedTypes[type];
        }

        private void TryCacheType(Type type)
        {
            if (!IsCachedType(type))
            {
                CacheType(type);
            }
        }

        private bool IsCachedType(Type type)
        {
            return cachedTypes.ContainsKey(type);
        }

        private void CacheType(Type type)
        {
            var members = type.FindMembers(ValidMemberTypes(), ValidBindingFlags(), Filter,
                typeof (UnityDependencyAttribute));

            for (int i = 0; i < members.Length; i++)
            {
                CacheMember(members[i]);
            }

            cachedTypes.Add(type, members.ToList());
        }

        private static MemberTypes ValidMemberTypes()
        {
            return MemberTypes.Field | MemberTypes.Property;
        }

        private static BindingFlags ValidBindingFlags()
        {
            return BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        }

        private static bool Filter(MemberInfo m, object filtercriteria)
        {
            return m.IsDefined((Type)filtercriteria, false);
        }

        private void TryCacheMember(MemberInfo info)
        {
            if (IsValidMember(info) && !IsCachedMember(info))
            {
                CacheMember(info);
            }
        }

        private static bool IsValidMember(MemberInfo info)
        {
            return (info.MemberType & ValidMemberTypes()) != 0;
        }

        private bool IsCachedMember(MemberInfo info)
        {
            return cachedMembers.Contains(info);
        }

        private void CacheMember(MemberInfo info)
        {
            var attributes = info.GetCustomAttributes(typeof(UnityDependencyAttribute), false);

            for (int i = 0; i < attributes.Length; i++)
            {
                CacheAttribute(info, (UnityDependencyAttribute)attributes[i]);
            }

            cachedMembers.Add(info);
        }

        private void CacheAttribute(MemberInfo info, UnityDependencyAttribute attribute)
        {
            if (IsInjectorAttribute(attribute))
            {
                CacheInjectorAttribute(info, (UnityInjectorAttribute) attribute);
            }
            else if (IsProviderAttribute(attribute))
            {
                CacheProviderAttribute(info, (UnityProviderAttribute) attribute);
                if (IsNonLazySingletonAttribute(attribute))
                {
                    CacheNonLazySingletonAttribute(info, (SingletonAttribute) attribute);
                }
            }
        }

        private static bool IsInjectorAttribute(Attribute attribute)
        {
            return attribute.GetType().IsSubclassOf(typeof(UnityInjectorAttribute));
        }

        private void CacheInjectorAttribute(MemberInfo info, UnityInjectorAttribute attribute)
        {
            if (!injectorAttributes.ContainsKey(info))
            {
                injectorAttributes.Add(info, new List<UnityInjectorAttribute>());
            }
            injectorAttributes[info].Add(attribute);
        }

        private static bool IsProviderAttribute(Attribute attribute)
        {
            return attribute.GetType().IsSubclassOf(typeof(UnityProviderAttribute));
        }

        private void CacheProviderAttribute(MemberInfo info, UnityProviderAttribute attribute)
        {
            if (!providerAttributes.ContainsKey(info))
            {
                providerAttributes.Add(info, new List<UnityProviderAttribute>());
            }

            providerAttributes[info].Add(attribute);
        }

        private static bool IsNonLazySingletonAttribute(Attribute attribute)
        {
            return attribute.GetType().IsAssignableFrom(typeof(SingletonAttribute)) &&
                   ((SingletonAttribute)attribute).SingletonOptions == SingletonOptions.NonLazy;
        }

        private void CacheNonLazySingletonAttribute(MemberInfo info, SingletonAttribute attribute)
        {
            if (!nonLazySingletonAttributes.ContainsKey(info))
            {
                nonLazySingletonAttributes.Add(info, new List<UnityDependencyAttribute>());
            }
            nonLazySingletonAttributes[info].Add(attribute);
        }
    }
}
