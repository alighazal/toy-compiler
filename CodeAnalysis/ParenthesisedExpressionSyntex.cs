using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    sealed class ParenthesisedExpressionSyntex : ExpressionSyntex {

        public ParenthesisedExpressionSyntex (SyntexToken openParenthesisToken, ExpressionSyntex expression, SyntexToken closeParenthesisToken){
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }
        public override SyntexKind Kind => SyntexKind.ParenthesisedExpression;

        public SyntexToken OpenParenthesisToken { get; }
        public ExpressionSyntex Expression { get; }
        public SyntexToken CloseParenthesisToken { get; }


        public override IEnumerable<SyntexNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;

        }
    }
   
}
