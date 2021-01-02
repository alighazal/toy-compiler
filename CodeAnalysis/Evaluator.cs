using System;

namespace tc.CodeAnalysis
{
    class Evaluator {

        private readonly ExpressionSyntex _root;
        public Evaluator (ExpressionSyntex root){

                _root = root;             

        }

        public int Evaluate(){

            return EvaluateExpression(_root);
        }

        private int EvaluateExpression (ExpressionSyntex node){


            if (node is NumberExpresssionSyntex n){
                return (int) n.NumberToken.Value;
            }

            if (node is BinaryExpressionSyntex b) {
                
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                switch (b.OperatorToken.Kind)
                {
                    case SyntexKind.PlusToken:
                        return left + right;
                    case SyntexKind.MinusToken:
                        return left - right;
                    case SyntexKind.StarToken:
                        return left * right;
                    case SyntexKind.SlashToken:
                        return left / right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                    
                }                
            }

            if (node is ParenthesisedExpressionSyntex p){
                return EvaluateExpression(p.Expression);
            }    

            throw new Exception($"Unexpected node {node.Kind} ");
        }

    }
   
}
