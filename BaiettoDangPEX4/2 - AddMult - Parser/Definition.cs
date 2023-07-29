using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyLanguage
{
    public abstract class Definition
    {
        public string name;
    }
    public abstract class TypeDefinition : Definition
    {
    }
    public class BasicTypeDefinition: TypeDefinition
    {
    }
    public class StringTypeDefinition : TypeDefinition
    {
    }

    public class BoolTypeDefinition : TypeDefinition
    {
    }

    public class VariableDefinition : Definition
    {
        public TypeDefinition vartype;
    }
    public class MethodDefinition : Definition
    {
        public List<TypeDefinition> paramList;

    }
}
