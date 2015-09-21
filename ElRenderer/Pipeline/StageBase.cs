using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElRenderer.Pipeline
{
    public abstract class StageBase<TInput, TOutput>
    {
        public abstract TOutput Process(TInput input);
    }
}
