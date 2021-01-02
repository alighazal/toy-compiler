using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    abstract class SyntexNode {
        public abstract SyntexKind Kind {get;}

        public abstract IEnumerable<SyntexNode> GetChildren();
    }
   
}
