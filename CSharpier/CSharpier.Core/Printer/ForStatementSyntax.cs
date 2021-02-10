using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core
{
    public partial class Printer
    {
        private Doc PrintForStatementSyntax(ForStatementSyntax node)
        {
            var parts = new Parts(node.ForKeyword.Text, " (");
            if (node.Declaration != null)
            {
                parts.Push(
                    this.PrintVariableDeclarationSyntax(node.Declaration),
                    "; "
                );
            }
            else
            {
                parts.Add(";");
            }

            if (node.Condition != null)
            {
                parts.Push(this.Print(node.Condition), "; ");
            }
            else
            {
                parts.Add(";");
            }

            parts.Push(Join(", ", node.Incrementors.Select(this.Print)), ")");
            var statement = this.Print(node.Statement);
            if (node.Statement is BlockSyntax)
            {
                parts.Add(statement);
            }
            else
            {
                parts.Add(Indent(Concat(HardLine, statement)));
            }

            return Concat(parts);
        }
    }
}