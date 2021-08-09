using ObjectsComparer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectsComparer.Resolvers
{
    public class ListResolver : AbstractObjectResolver
    {
        private readonly Type _resolvedType;

        public ListResolver(IResolverFinder resolverFinder, Type resolvedType) : base(resolverFinder) 
            => _resolvedType = resolvedType;
        
        public override IComparisonResult Compare(object obj1, object obj2)
        {
            var collectionGenericType = _resolvedType.GenericTypeArguments[0]; //refatorar e passar o tipo pronto ao inves de extrair aqui

            ICollection listItems1 = obj1 as ICollection;
            ICollection listItems2 = obj2 as ICollection;

            if (listItems1.Count != listItems2.Count)
                return new ComparisonResult(true);
            else
            {
                List<object> testedReferences = new List<object>();

                foreach (var item1 in listItems1)
                {
                    bool equalityFound = false;

                    foreach (var item2 in listItems2)
                    {
                        if (!testedReferences.Contains(item2))
                        {
                            if (!_resolverFinder.FindResolver(collectionGenericType).Compare(item1, item2).IsDifferent)
                            {
                                testedReferences.Add(item2);
                                equalityFound = true;
                                break;
                            }
                        }
                    }

                    if (!equalityFound)
                        return new ComparisonResult(true);
                }
            }

            return new ComparisonResult(false);
        }
    }
}
