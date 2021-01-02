using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    class Parser {
        private readonly SyntexToken[] _tokens;
        private int _position;
        private List<string> _diagnostics = new List<string> ();
        public IEnumerable<string> Dignostics => _diagnostics;


        public Parser (string text){

            var tokens = new List <SyntexToken>();

            var lexer =  new Lexer(text);

            SyntexToken token;

            do{

                token = lexer.NextToken();

                if (token.Kind != SyntexKind.BadToken &&
                    token.Kind != SyntexKind.WhiteSpaceToken) {

                        tokens.Add(token);
                }



            }while (token.Kind != SyntexKind.EndofFileToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Dignostics);


        }

        private SyntexToken peek(int offset){

            int index = offset + _position;

            if (index >= _tokens.Length){
                return _tokens[_tokens.Length - 1];
            }
            
            return _tokens[index];
        }

        //??
        private SyntexToken Current => peek(0);

        private SyntexToken NextToken (){ 
            var current = Current;

            _position++;

            return current;
        }

        private SyntexToken Match (SyntexKind kind) {
            if (Current.Kind == kind) 
                return NextToken();
            else {

                _diagnostics.Add($"Error: unexpected token <{Current.Kind}>, expected {kind} ");
                return new SyntexToken(kind, Current.Position, null, null);
            }
        }

        private ExpressionSyntex ParseExpression()
        {
            return ParseTerm();
        }

        public SyntexTree Parse()
        {
            var expression =  ParseTerm();
            var endOfFileToken =  Match(SyntexKind.EndofFileToken);
            
            return new SyntexTree(_diagnostics, expression, endOfFileToken);

        }

        private ExpressionSyntex ParseTerm()
        {
            var left = ParseFactor();

            while (Current.Kind == SyntexKind.PlusToken || Current.Kind == SyntexKind.MinusToken)
            {

                // if i want i can just assign current and inc _postion
                var operatorToken = NextToken();

                var right = ParseFactor();

                left = new BinaryExpressionSyntex(left, operatorToken, right);

            }

            return left;
        }

        private ExpressionSyntex ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == SyntexKind.StarToken || Current.Kind == SyntexKind.SlashToken)
            {

                // if i want i can just assign current and inc _postion
                var operatorToken = NextToken();

                var right = ParsePrimaryExpression();

                left = new BinaryExpressionSyntex(left, operatorToken, right);

            }

            return left;
        }

       

        private ExpressionSyntex ParsePrimaryExpression (){

            if (Current.Kind == SyntexKind.OpenParenthesisToken){

                var left = NextToken();
                var expression  = ParseExpression();
                var right = Match(SyntexKind.CloseParenthesisToken);

                return new ParenthesisedExpressionSyntex(left, expression, right);

            }

            var numToken = Match (SyntexKind.NumberToken);
            return new NumberExpresssionSyntex(numToken);


        }

       
    }
   
}
