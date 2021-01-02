using System;
using System.Collections.Generic;
using System.Linq;
using tc.CodeAnalysis;

namespace tc
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showtree = false; 

            while(true) {


                Console.Write("> ");
                var line = Console.ReadLine();  
                    if (string.IsNullOrWhiteSpace(line)){
                        return;
                    }

                    if (line == "#showtree"){

                        showtree = !showtree;

                        Console.WriteLine(showtree? "Showning tree": "not showing tree");

                        continue;

                    }

                    var syntextree = SyntexTree.Parse(line);

                    if (showtree){


                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        prettyPrint(syntextree.Root);
                        
                        Console.ResetColor();

                    }



                    if (!syntextree.Diagnostics.Any())
                    {

                        var evaluate = new Evaluator(syntextree.Root);
                        var result = evaluate.Evaluate();
                        Console.WriteLine(result);


                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        foreach (var error in syntextree.Diagnostics)
                            Console.WriteLine(error);

                        Console.ResetColor();
                    }

            }
        }

        static void prettyPrint (SyntexNode node, string indent = "", bool isLast = true) {

            //├── 
            //└── 
            //│

            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntexToken t && t.Value != null) {
                Console.Write(" ");
                Console.Write(t.Value);
            }


            Console.WriteLine();

            indent += isLast? "    " :  "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach(var child in node.GetChildren()){ 
                prettyPrint(child, indent, child == lastChild);
            }

        }
    }
}   

