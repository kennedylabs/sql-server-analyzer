using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public abstract class ParentSmoWrapper<TSmoObject, TSmoChildren> : SmoWrapper<TSmoObject>
        where TSmoObject : SmoObjectBase where TSmoChildren : NamedSmoObject
    {
        private readonly Lazy<List<TSmoChildren>> _children;

        internal List<TSmoChildren> Children => _children.Value;

        internal ParentSmoWrapper(TSmoObject smoObject,
            Func<TSmoObject, IEnumerable> childrenSelector) : base(smoObject)
        {
            _children = new Lazy<List<TSmoChildren>>(
                () => childrenSelector(smoObject).Cast<TSmoChildren>().ToList());
        }

        internal protected Task<List<TSmoChildren>> GetChildrenAsync()
        {
            return Task.FromResult(_children.Value);
        }
        
        internal protected Task<IList<TWrapper>> GetChildrenWrappersAsync<TWrapper>(
            Func<TSmoChildren, TWrapper> constructWrapperFunc)
        {
            return Task.FromResult((IList<TWrapper>)_children.Value.Select(
                constructWrapperFunc).ToList());
        }
    }
}
