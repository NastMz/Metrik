using System.Reflection;

namespace Metrik.Mapping.Mapping
{
    /// <summary>
    /// Represents a type mapping configuration between source and destination types.
    /// </summary>
    internal class TypeMap : ITypeMap
    {
        /// <summary>
        /// Dictionary to hold member mappings from destination to source.
        /// </summary>
        private readonly Dictionary<string, string> _memberMappings = new Dictionary<string, string>();

        /// <summary>
        /// Set to hold ignored members.
        /// </summary>
        private readonly HashSet<string> _ignoredMembers = new HashSet<string>();

        /// <summary>
        /// Dictionary to hold constant value mappings for destination members.
        /// </summary>
        private readonly Dictionary<string, object> _valueMappings = new Dictionary<string, object>();

        /// <summary>
        /// Dictionary to hold function mappings for destination members.
        /// </summary>
        private readonly Dictionary<string, Func<object, object>> _functionMappings = new Dictionary<string, Func<object, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMap"/> class.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <exception cref="ArgumentNullException">If sourceType or destinationType is null.</exception>
        public TypeMap(Type sourceType, Type destinationType)
        {
            SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            DestinationType = destinationType ?? throw new ArgumentNullException(nameof(destinationType));

            // By default, map properties with matching names
            foreach (var destProp in DestinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var sourceProp = SourceType.GetProperty(destProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp != null && destProp.CanWrite)
                {
                    _memberMappings[destProp.Name] = sourceProp.Name;
                }
            }
        }

        /// <summary>
        /// The source type for the mapping.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// The destination type for the mapping.
        /// </summary>
        public Type DestinationType { get; }

        /// <inheritdoc />
        public object Map(object source)
        {
            if (source == null)
                return null;

            var destination = Activator.CreateInstance(DestinationType);
            return Map(source, destination);
        }

        /// <inheritdoc />
        public object Map(object source, object destination)
        {
            if (source == null)
                return destination;

            foreach (var destProp in DestinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (_ignoredMembers.Contains(destProp.Name) || !destProp.CanWrite)
                    continue;

                if (_valueMappings.TryGetValue(destProp.Name, out var value))
                {
                    // Use configured constant value
                    destProp.SetValue(destination, value);
                    continue;
                }

                if (_functionMappings.TryGetValue(destProp.Name, out var mapFunc))
                {
                    // Use function mapping
                    destProp.SetValue(destination, mapFunc(source));
                    continue;
                }

                if (_memberMappings.TryGetValue(destProp.Name, out var sourceMemberName))
                {
                    // Map from configured source member
                    var sourceProp = SourceType.GetProperty(sourceMemberName);
                    if (sourceProp != null)
                    {
                        var sourceValue = sourceProp.GetValue(source);
                        try
                        {
                            destProp.SetValue(destination, sourceValue);
                        }
                        catch (ArgumentException)
                        {
                            // Handle type conversion if needed
                            if (sourceValue != null && destProp.PropertyType != sourceProp.PropertyType)
                            {
                                try
                                {
                                    var convertedValue = Convert.ChangeType(sourceValue, destProp.PropertyType);
                                    destProp.SetValue(destination, convertedValue);
                                }
                                catch
                                {
                                    // Silently fail if conversion is not possible
                                }
                            }
                        }
                    }
                }
            }

            return destination;
        }

        /// <summary>
        /// Adds a member mapping from destination to source.
        /// </summary>
        /// <param name="destinationMemberName">The name of the destination member.</param>
        /// <param name="sourceMemberName">The name of the source member.</param>
        public void AddMemberMapping(string destinationMemberName, string sourceMemberName)
        {
            _memberMappings[destinationMemberName] = sourceMemberName;
        }

        /// <summary>
        /// Ignores a member during mapping.
        /// </summary>
        /// <param name="memberName">The name of the member to ignore.</param>
        public void IgnoreMember(string memberName)
        {
            _ignoredMembers.Add(memberName);
        }

        /// <summary>
        /// Adds a constant value mapping for a destination member.
        /// </summary>
        /// <param name="memberName">The name of the destination member.</param>
        /// <param name="value">The constant value to set.</param>
        public void AddValueMapping(string memberName, object value)
        {
            _valueMappings[memberName] = value;
        }

        /// <summary>
        /// Adds a function mapping for a destination member.
        /// </summary>
        /// <param name="memberName">The name of the destination member.</param>
        /// <param name="mapFunction">The function to map the source object to the destination member.</param>
        public void AddFunctionMapping(string memberName, Func<object, object> mapFunction)
        {
            _functionMappings[memberName] = mapFunction;
        }
    }
}