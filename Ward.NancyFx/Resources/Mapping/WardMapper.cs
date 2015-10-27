using IceLib.Core.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.NancyFx.Resources.Mapping
{
    public class WardMapper<TSource, TTarget> : IMapper<TSource, TTarget>
        where TSource : new()
        where TTarget : new()
    {
        public WardMapper() { }

        public TTarget Map(TSource source)
        {
            return AutoMapper.Mapper.Map<TTarget>(source);
        }
    }
}
