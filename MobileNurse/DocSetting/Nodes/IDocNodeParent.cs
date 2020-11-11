using System.Collections.Generic;

namespace HISPlus
{
    using System;

    public interface IDocNodeParent
    {
        void RelocateChildNodes(int startChildIndex);

        DocNodeCollection1 ChildNodes { get; }        
    }
}

