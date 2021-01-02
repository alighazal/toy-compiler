using System.Collections.Generic;

namespace tc.CodeAnalysis
{
    class Lexer {

        private readonly string _text;

        private int _position;

        private List<string> _diagnostics = new List<string> ();

        public Lexer(string text) {
            _text = text;
        }

        public IEnumerable<string> Dignostics => _diagnostics;

        private char Current {

            get {
                if (_position >= _text.Length)
                    return '\0';
                else {
                    return _text[_position];
                }

            }

        }

        private void Next () {
            _position++;
        }

        public SyntexToken NextToken(){

            if (_position >= _text.Length)
                return new SyntexToken(SyntexKind.EndofFileToken, _position, "\0", null);
            
            if (char.IsDigit(Current)){

                var start = _position;

                while(char.IsDigit(Current))
                    Next();
                
                var Length =  _position - start ;
                var text = _text.Substring(start, Length);
                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"the number {text} is in valid int32");


                return new SyntexToken(SyntexKind.NumberToken, start, text, value);

            } 

            if (char.IsWhiteSpace(Current)) {

                 var start = _position;

                while(char.IsWhiteSpace(Current))
                    Next();
                
                var Length =  _position - start;
                var text = _text.Substring(start, Length);
                int.TryParse(text, out var value);
                return new SyntexToken(SyntexKind.WhiteSpaceToken, start, text, null);

            } 
            
            if (Current == '+'){
                return new SyntexToken(SyntexKind.PlusToken, _position++, "+", null);
            }
            else if (Current == '-'){
                return new SyntexToken(SyntexKind.MinusToken, _position++, "-", null);
            }
            else if (Current == '*'){
                return new SyntexToken(SyntexKind.StarToken, _position++, "*", null);
            }
            else if (Current == '/'){
                return new SyntexToken(SyntexKind.SlashToken, _position++, "/", null);
            }
            else if (Current == '('){
                return new SyntexToken(SyntexKind.OpenParenthesisToken, _position++, "(", null);
            }
            else if (Current == ')'){
                return new SyntexToken(SyntexKind.CloseParenthesisToken, _position++, ")", null);
            }
            
            _diagnostics.Add($"Error: Bad Character input  {Current} ");
            return new SyntexToken(SyntexKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }


    }
   
}
