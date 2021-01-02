using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    sealed class NumberExpresssionSyntex : ExpressionSyntex
    {
        public NumberExpresssionSyntex (SyntexToken numberToken)
        {
            NumberToken = numberToken;
        }
        public override SyntexKind Kind => SyntexKind.NumberExpression;

        public SyntexToken NumberToken { get; }

        public override IEnumerable<SyntexNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
   
}
