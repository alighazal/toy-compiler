using System.Collections.Generic;
using System.Linq;

namespace tc.CodeAnalysis
{
    sealed class SyntexTree{

        public SyntexTree (IEnumerable<string> diagnostics, ExpressionSyntex root, SyntexToken endOfFileToken){
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntex Root { get; }
        public SyntexToken EndOfFileToken { get; }

        public static SyntexTree Parse(string text) {

            var parser = new Parser(text);
            return parser.Parse();

        }

    }
   
}
