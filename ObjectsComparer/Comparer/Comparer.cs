using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using testes.Comparador.Atributos;

namespace testes.Comparador
{
    public class Comparer
    {
        private static readonly string DEFAULT_COMPARABLE_PROPERTY = "ID";

        public static bool IsDifferent(object object1, object object2) => GetDifferences(object1, object2).Any();

        public static IEnumerable<string> GetDifferences(object object1, object object2)
        {
            if (object1.GetType() != object2.GetType())
                throw new Exception("Cannot compare different type objects.");

            var comparableProps = GetTypeComparableProperties(object1.GetType());

            return GetDifferences(object1, object2, comparableProps);
        }

        private static IEnumerable<string> GetDifferences(object object1, object object2, IEnumerable<PropertyInfo> comparableProps)
        {
            return CompareProperties(comparableProps, object1, object2);
        }

        private static IEnumerable<PropertyInfo> GetTypeComparableProperties(Type type)
        {
            var comparableProps = type.GetProperties().Where(w => w.GetCustomAttributes(true).Where(w => w.GetType().Equals(typeof(Comparable))).Any());

            if (!comparableProps.Any())
                comparableProps = type.GetProperties().Where(w => w.Name == DEFAULT_COMPARABLE_PROPERTY);

            if (!comparableProps.Any())
                throw new Exception("Comparable properties not found.");

            return comparableProps;
        }

        private static IEnumerable<string> CompareProperties(IEnumerable<PropertyInfo> comparableProps, object object1, object object2)
        {
            var differentProps = new List<string>();

            foreach (var prop in comparableProps)
            {
                if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(bool))
                {
                    if (prop.GetValue(object1).ToString() != prop.GetValue(object2).ToString())
                        differentProps.Add(prop.Name);
                }
                else if (typeof(ICollection).IsAssignableFrom(prop.PropertyType))
                {
                    var enumerableGenericType = prop.PropertyType.GenericTypeArguments[0];

                    var enumerableComparableProps = GetTypeComparableProperties(enumerableGenericType);

                    ICollection listItems1 = prop.GetValue(object1) as ICollection;
                    ICollection listItems2 = prop.GetValue(object2) as ICollection;

                    if (listItems1.Count != listItems2.Count)
                        differentProps.Add(prop.Name);
                    else
                    {
                        List<object> testedObjects = new List<object>();

                        foreach (var item1 in listItems1)
                        {
                            bool equalityFound = false;

                            foreach (var item2 in listItems2)
                            {
                                if (!GetDifferences(item1, item2).Any() && !testedObjects.Contains(item2))
                                {
                                    equalityFound = true;
                                    break;
                                }
                            }

                            if (!equalityFound)
                            {
                                differentProps.Add(prop.Name);
                                break;
                            }
                        }
                    }
                }
                else if (prop.PropertyType.IsClass)
                {
                    if (GetDifferences(prop.GetValue(object1), prop.GetValue(object2)).Any())
                        differentProps.Add(prop.Name);
                }
            }

            return differentProps;
        }
    }
}
