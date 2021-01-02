using System.Collections.Generic;
using System.Linq;

namespace tc.CodeAnalysis
{
    class SyntexToken : SyntexNode
    {
        public SyntexToken (SyntexKind kind, int position, string text, object value){

            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntexKind Kind { get; }


        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntexNode> GetChildren()
        {
            return Enumerable.Empty<SyntexNode>();
        }
    }
   
}
