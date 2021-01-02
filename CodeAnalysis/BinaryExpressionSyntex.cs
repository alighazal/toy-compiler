using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    sealed class BinaryExpressionSyntex : ExpressionSyntex
    {

        public BinaryExpressionSyntex (ExpressionSyntex left, SyntexToken operatorToken, ExpressionSyntex right){
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
        public override SyntexKind Kind => SyntexKind.BinaryExpressionSyntex;

        public ExpressionSyntex Left { get; }
        public SyntexToken OperatorToken { get; }
        public ExpressionSyntex Right { get; }

        public override IEnumerable<SyntexNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;

        }
    }
   
}
