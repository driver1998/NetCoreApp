using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace NetCoreApp
{
    public partial class NetCoreAppXamlMember : IXamlMember
    {
        public bool IsAttachable => false;

        public bool IsDependencyProperty => _propertyInfo.PropertyType.IsAssignableTo(typeof(DependencyProperty));

        public bool IsReadOnly => _propertyInfo.CanRead && !_propertyInfo.CanWrite;

        public string Name => _propertyInfo.Name;

        public IXamlType TargetType => _targetType;

        public IXamlType Type => NetCoreAppXamlTypeDictionary.GetXamlType(_propertyInfo.PropertyType) ?? new NetCoreAppXamlType(typeof(object));

        public object? GetValue(object instance)
        {
            if (_propertyInfo.CanRead && instance.GetType().IsAssignableTo(_targetType.UnderlyingType))
            {
                return _propertyInfo.GetValue(instance);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void SetValue(object instance, object value)
        {
            if (_propertyInfo.CanWrite && instance.GetType().IsAssignableTo(_targetType.UnderlyingType))
            {
                _propertyInfo.SetValue(instance, value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private IXamlType _targetType;
        private PropertyInfo _propertyInfo;

        public NetCoreAppXamlMember(IXamlType targetType, PropertyInfo propertyInfo)
        {
            _targetType = targetType;
            _propertyInfo = propertyInfo;
        }
    }
    public partial class NetCoreAppXamlType : IXamlType
    {
        public IXamlType? BaseType => null;

        public IXamlMember? ContentProperty => null;

        public string? FullName => _type.FullName;

        public bool IsArray => _type.IsArray;

        public bool IsBindable => true;

        public bool IsCollection => _type.IsAssignableTo(typeof(ICollection));

        public bool IsConstructible => true;

        public bool IsDictionary => _type.IsAssignableTo(typeof(IDictionary));

        public bool IsMarkupExtension => false;

        public IXamlType? ItemType => (IsArray || IsCollection || IsDictionary) ? new NetCoreAppXamlType(typeof(object)) : null;

        public IXamlType? KeyType => IsDictionary ? new NetCoreAppXamlType(typeof(object)) : null;

        public Type UnderlyingType => _type;

        public object? ActivateInstance()
        {
            if (_type == null)
            {
                return null;
            }
            return Activator.CreateInstance(_type);
        }

        public void AddToMap(object instance, object key, object value)
        {
            if (this.IsDictionary)
            {
                if (instance is IDictionary dict)
                {
                    dict[key] = value;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void AddToVector(object instance, object value)
        {
            if (instance is IList list)
            {
                list.Add(value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public object CreateFromString(string value)
        {
            throw new NotImplementedException();
        }

        public IXamlMember GetMember(string name)
        {
            return new NetCoreAppXamlMember(this, _type?.GetProperty(name)!);
        }

        public void RunInitializer() {}

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)]
        private readonly Type _type;

        public NetCoreAppXamlType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties)] Type t)
        {
            _type = t;
        }
    }
    
    public static class NetCoreAppXamlTypeDictionary
    {
        private static readonly Dictionary<string, IXamlType> XamlTypeDictionary = new() {
            { typeof(MainPage).FullName!, new NetCoreAppXamlType(typeof(MainPage)) },
            { typeof(TodoPage).FullName!, new NetCoreAppXamlType(typeof(TodoPage)) },
            { typeof(TodoPageViewModel).FullName!, new NetCoreAppXamlType(typeof(TodoPageViewModel)) },
            { typeof(ObservableCollection<TodoListItem>).FullName!, new NetCoreAppXamlType(typeof(ObservableCollection<TodoListItem>)) },
        };

        public static IXamlType? GetXamlType(Type type)
        {
            if (XamlTypeDictionary.TryGetValue(type.FullName ?? "", out var xamlType))
            {
                return xamlType;
            }
            return null;
        }

        public static IXamlType? GetXamlType(string fullName)
        {
            if (XamlTypeDictionary.TryGetValue(fullName, out var xamlType))
            {
                return xamlType;
            }
            return null;
        }
    }

    public partial class NetCoreAppXamlMetadataProvider : IXamlMetadataProvider
    {
        public IXamlType? GetXamlType(Type type)
        {
            return NetCoreAppXamlTypeDictionary.GetXamlType(type);
        }

        public IXamlType? GetXamlType(string fullName)
        {
            return NetCoreAppXamlTypeDictionary.GetXamlType(fullName);
        }

        public XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return Array.Empty<XmlnsDefinition>();
        }
    }

}
