using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coding.Exercise
{
    public enum AccessModifier
    {
        Public,
        Private,
        Protected
    }

    public class CodeBuilder
    {
        public CodeBlock Root = new CodeBlock();
        private const string DefaultRootType = "class";
        public class CodeBlock
        {

            // You could just make your data members public without using this shortcut
            // but then if you decided to change the implementation of the data member to have some logic
            // then you would need to break the interface.
            // So in short it is a shortcut to create more flexible code.
            public string Name { get; set; } = string.Empty;

            public string Type { get; set; } = string.Empty;

            public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

            private const int IndentationStep = 2;

            public CodeBlock()
            {
            }

            public CodeBlock(
                string name,
                string type,
                AccessModifier accessModifier = AccessModifier.Public)
            {
                this.Name = name;
                this.Type = type;
                this.AccessModifier = accessModifier;
            }

            public List<CodeBlock> Fields = new List<CodeBlock>();

            public override string ToString()
            {
                return ToStringImpl();
            }

            private string ToStringImpl(int indent = 0)
            {
                var code = new StringBuilder();
                var indentation = new string(' ', indent * IndentationStep);
                code.Append($"{indentation}{AccessModifier.ToString().ToLower()} {Type} {Name}");

                if (Fields.Any())
                {
                    code.AppendLine("\n{");

                    foreach (var field in Fields) // continue here!
                    {
                        code.AppendLine($"{ field.ToStringImpl(indent + 1)};");
                    }

                    code.Append("}");
                }
                return code.ToString();
            }
        }

        public CodeBuilder(
            string name, 
            string type = DefaultRootType,
            AccessModifier accessModifier = AccessModifier.Public)
        {
            Root = new CodeBlock(name, type, accessModifier);
        }

        public CodeBuilder AddField(
            string name, 
            string type, 
            AccessModifier accessModifier = AccessModifier.Public)
        {
            var field = new CodeBlock(name, type, accessModifier);
            Root.Fields.Add(field);
            return this;
        }

        public override string ToString()
        {
            return Root.ToString();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
