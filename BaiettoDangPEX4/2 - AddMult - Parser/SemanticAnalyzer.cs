using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyLanguage.node;


namespace ToyLanguage.analysis
{
    class SemanticAnalyzer : DepthFirstAdapter
    {
        // Symbol Tables
        LinkedList<Dictionary<string, Definition>> _previousSymbolTables = new LinkedList<Dictionary<string, Definition>>();
        Dictionary<string, Definition> _currentSymbolTable = new Dictionary<string, Definition>();
        Dictionary<string, Definition> _globalSymbolTable = new Dictionary<string, Definition>();

        // List of params into functions
        List<TypeDefinition> _parameterList = new List<TypeDefinition>();
        List<TypeDefinition> _argList = new List<TypeDefinition>();

        // ParseTree Decoration
        Dictionary<Node, Definition> _decoratedParseTree = new Dictionary<Node, Definition>();

        public override void InAProg(AProg node)
        {
            // Build definitions for allowed types according to grammar. ToyLanguage only allows int and string
            BasicTypeDefinition intType;
            intType = new BasicTypeDefinition();
            intType.name = "int";

            BasicTypeDefinition floatType;
            floatType = new BasicTypeDefinition();
            floatType.name = "float";

            StringTypeDefinition stringType = new StringTypeDefinition();
            stringType.name = "string";

            BoolTypeDefinition boolType = new BoolTypeDefinition();
            boolType.name = "bool";

            BoolTypeDefinition trueType = new BoolTypeDefinition();
            boolType.name = "true";

            BoolTypeDefinition falseType = new BoolTypeDefinition();
            boolType.name = "false";

            //Create and seed the _globalSymbolTable
            _globalSymbolTable.Add("int", intType);
            _globalSymbolTable.Add("float", floatType);
            _globalSymbolTable.Add("string", stringType);
            _globalSymbolTable.Add("bool", boolType);
            _globalSymbolTable.Add("true", boolType);
            _globalSymbolTable.Add("false", boolType);
        }

        public override void OutASomeconstantsConstants(ASomeconstantsConstants node)
        {
            Definition typeDef, exprDef, varDef;
            String typeName = node.GetType().Text;
            String varName = node.GetVarname().Text;

            // Check that the type name is a type definition(int, bool, float, string) and in the global symbol table
            if(!_globalSymbolTable.TryGetValue(typeName, out typeDef))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not defined.");
                // make sure typeDef is a TypeDefinition
            }
            else if (!(typeDef is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not a valid type.");
                // is orcond decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out exprDef))
            {
                Console.WriteLine("[" + node.GetVarname().Line + "] : right hand side was not decorated.");

                // Ensure that type ID and orcond have the same types
            }
            else if (!exprDef.name.Equals(typeDef.name))
            {
                Console.WriteLine("[" + node.GetVarname().Line + "] : Invalid assignment. Can not assign " + exprDef.name + " to " +
                    typeDef.name + ".");
                // make sure variable is not already in global symbol table
            }
            else if(_globalSymbolTable.TryGetValue(varName, out varDef))
            {
                Console.WriteLine("[" + node.GetVarname().Line + "] : " + varName + " is already defined.");
            }
            else
            {
                VariableDefinition newDef = new VariableDefinition();
                newDef.name = varName;
                newDef.vartype = (TypeDefinition)typeDef;
                _globalSymbolTable.Add(varName, newDef); // add the constant to the global symbol table
            }
        }

        public override void InAMainstmt(AMainstmt node)
        {
            setupSymboltable();
        }

        private void setupSymboltable()
        {
            // Build definitions for allowed types according to grammar. ToyLanguage only allows int and string
            BasicTypeDefinition intType;
            intType = new BasicTypeDefinition();
            intType.name = "int";

            BasicTypeDefinition floatType;
            floatType = new BasicTypeDefinition();
            floatType.name = "float";

            StringTypeDefinition stringType = new StringTypeDefinition();
            stringType.name = "string";

            BoolTypeDefinition boolType = new BoolTypeDefinition();
            boolType.name = "bool";

            BoolTypeDefinition trueType = new BoolTypeDefinition();
            boolType.name = "true";

            BoolTypeDefinition falseType = new BoolTypeDefinition();
            boolType.name = "false";

            // Create and seed the symbolTable
            _currentSymbolTable = new Dictionary<string, Definition>();
            _currentSymbolTable.Add("int", intType);
            _currentSymbolTable.Add("float", floatType);
            _currentSymbolTable.Add("string", stringType);
            _currentSymbolTable.Add("bool", boolType);
            _currentSymbolTable.Add("true", boolType);
            _currentSymbolTable.Add("false", boolType);
        }

        public override void InASubfunctionorsubfunctionsSubfunctions(ASubfunctionorsubfunctionsSubfunctions node)
        {
            // Save current symbol table.
            _previousSymbolTables.AddFirst(_currentSymbolTable);
            _currentSymbolTable = new Dictionary<string, Definition>();

            // Create new list of parameters
            //_parameterList = new List<TypeDefinition>();

            // Build definitions for allowed types according to grammar. ToyLanguage only allows int and string
            setupSymboltable();
        }


        public override void OutASubfunction(ASubfunction node)
        {
            // Restore previous symbol table
            _currentSymbolTable = _previousSymbolTables.First();
            _previousSymbolTables.RemoveFirst();

            Definition def;
            String methodName = node.GetId().Text;

            // Ensure submethod name isn't used
            if (_currentSymbolTable.TryGetValue(methodName, out def) || _globalSymbolTable.TryGetValue(methodName, out def))
            {
                Console.WriteLine("[" + node.GetLparen().Line + "] : " + methodName + " is already declared.");

                // Build function definition and add to symbol table 
            }
            else
            {
                def = new MethodDefinition();
                def.name = methodName;
                ((MethodDefinition)def).paramList = _parameterList;
                _parameterList = new List<TypeDefinition>();
                _globalSymbolTable.Add(methodName, def);
            }
        }

        public override void OutADefinearg(ADefinearg node)
        {
            Definition typeDef, varDef;
            String typeName = node.GetType().Text;
            String varName = node.GetVarname().Text;

            // Check that the type name is defined
            if (!_currentSymbolTable.TryGetValue(typeName, out typeDef))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not defined.");

                // Check that the type name is defined as a type
            }
            else if (!(typeDef is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not a valid type.");

                // check that the var name is not defined
            }
            else if (_currentSymbolTable.TryGetValue(varName, out varDef) || _globalSymbolTable.TryGetValue(varName, out varDef))
            {
                Console.WriteLine("[" + node.GetVarname().Line + "] : " + varName + " is already defined.");

                // Add variable name and definition to symbol table
            }
            else
            {
                VariableDefinition newDef = new VariableDefinition();
                newDef.name = varName;
                newDef.vartype = (TypeDefinition)typeDef;
                _currentSymbolTable.Add(varName, newDef);
                _parameterList.Add((TypeDefinition)typeDef);
            }
        }

        public override void OutAArgument(AArgument node)
        {
            Definition typeDef;

            // Check that the type name is defined
            if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out typeDef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree 
            }
            else
            {
                _argList.Add((TypeDefinition)typeDef);
            }
        }

        public override void OutACondifstmt(ACondifstmt node)
        {
            Definition orcondExpr;

            //Ensure orcond node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out orcondExpr))
            {

                
            }
            //ensure node is a boolean
            else if (!(orcondExpr is BoolTypeDefinition))
            {
                Console.WriteLine("[" + node.GetLparen().Line + "] : Invalid Type.  Cannot have conditional of type:  " + orcondExpr.name + 
                    "s. Must have boolean conditional");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutACondelsestmtCondelsestmt(ACondelsestmtCondelsestmt node)
        {
            base.OutACondelsestmtCondelsestmt(node);
        }

        public override void OutAWhilestmt(AWhilestmt node)
        {
            Definition orcondExpr;

            //Ensure orcond node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out orcondExpr))
            {


            }
            //ensure node is a boolean
            else if (!(orcondExpr is BoolTypeDefinition))
            {
                Console.WriteLine("[" + node.GetLparen().Line + "] : Invalid Type.  Cannot have conditional of type:  " + orcondExpr.name +
                    "s. Must have boolean conditional");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAOrcondoneOrcond(AOrcondoneOrcond node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out lhs))
            {
                Console.WriteLine("[" + node.GetOr().Line + "] : left hand side of '||' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetAndcond(), out rhs))
            {
                Console.WriteLine("[" + node.GetOr().Line + "] : right hand side of '||' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetOr().Line + "] : Type mismatch.  Cannot or " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BoolTypeDefinition))
            {
                Console.WriteLine("[" + node.GetOr().Line + "] : Invalid Type.  Cannot or " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }
        public override void OutAPassandcondOrcond(APassandcondOrcond node)
        {
            Definition andcondDef;

            //Ensure andcond node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetAndcond(), out andcondDef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, andcondDef);
            }
        }

        public override void OutAAndcondoneAndcond(AAndcondoneAndcond node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetAndcond(), out lhs))
            {
                Console.WriteLine("[" + node.GetAnd().Line + "] : left hand side of '&&' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetCondition(), out rhs))
            {
                Console.WriteLine("[" + node.GetAnd().Line + "] : right hand side of '&&' was not decorated.");

            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetAnd().Line + "] : Type mismatch.  Cannot and " + lhs.name + " with " +
                    rhs.name + ".");
            }
            else if (!(lhs is BoolTypeDefinition))
            {
                Console.WriteLine("[" + node.GetAnd().Line + "] : Invalid Type.  Cannot and " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasscondAndcond(APasscondAndcond node)
        {
            Definition conditionDef;

            //Ensure condition node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetCondition(), out conditionDef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, conditionDef);
            }
        }

        public override void OutAEqualitycomparisonCondition(AEqualitycomparisonCondition node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetFirstexpr(), out lhs))
            {
                //Throw error
                Console.WriteLine("[" + node.GetEqual().Line + "] : left hand side of '==' was not decorated.");

            }
            //Ensure rhs of the or is deconrated
            else if (!_decoratedParseTree.TryGetValue(node.GetSecondexpr(), out rhs))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetEqual().Line + "] : right hand side of '==' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetEqual().Line + "] : Type mismatch.  Cannot == " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetEqual().Line + "] : Invalid Type.  Cannot == " + lhs.name + "types.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutALesscondCondition(ALesscondCondition node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetFirstexpr(), out lhs))
            {
                //Throw error
                Console.WriteLine("[" + node.GetLess().Line + "] : left hand side of '==' was not decorated.");
            }
            //Ensure rhs of the or is deconrated
            else if (!_decoratedParseTree.TryGetValue(node.GetSecondexpr(), out rhs))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetLess().Line + "] : right hand side of '<' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetLess().Line + "] : Type mismatch.  Cannot < " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetLess().Line + "] : Invalid Type.  Cannot < " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAGreatercondCondition(AGreatercondCondition node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetFirstexpr(), out lhs))
            {
                //Throw error
                Console.WriteLine("[" + node.GetGreater().Line + "] : left hand side of '>' was not decorated.");
            }
            //Ensure rhs of the or is deconrated
            else if (!_decoratedParseTree.TryGetValue(node.GetSecondexpr(), out rhs))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetGreater().Line + "] : right hand side of '>' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetGreater().Line + "] : Type mismatch.  Cannot > " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetGreater().Line + "] : Invalid Type.  Cannot > " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutALesseqcondCondition(ALesseqcondCondition node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetFirstexpr(), out lhs))
            {
                //Throw error
                Console.WriteLine("[" + node.GetLessEqual().Line + "] : left hand side of '<=' was not decorated.");
            }
            //Ensure rhs of the or is deconrated
            else if (!_decoratedParseTree.TryGetValue(node.GetSecondexpr(), out rhs))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetLessEqual().Line + "] : right hand side of '<=' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetLessEqual().Line + "] : Type mismatch.  Cannot <= " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetLessEqual().Line + "] : Invalid Type.  Cannot <= " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAGreatereqcondCondition(AGreatereqcondCondition node)
        {
            Definition lhs, rhs;

            //Ensure lhs of the or is deconrated
            if (!_decoratedParseTree.TryGetValue(node.GetFirstexpr(), out lhs))
            {
                //Throw error
                Console.WriteLine("[" + node.GetGreaterEqual().Line + "] : left hand side of '>=' was not decorated.");
            }
            //Ensure rhs of the or is deconrated
            else if (!_decoratedParseTree.TryGetValue(node.GetSecondexpr(), out rhs))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetGreaterEqual().Line + "] : right hand side of '>=' was not decorated.");
            }

            //Ensure sides are the same type
            else if (!lhs.name.Equals(rhs.name))
            {
                //Throw Error
                Console.WriteLine("[" + node.GetGreaterEqual().Line + "] : Type mismatch.  Cannot >= " + lhs.name + " to " +
                    rhs.name + ".");
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetGreaterEqual().Line + "] : Invalid Type.  Cannot >= " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasstoexprCondition(APasstoexprCondition node)
        {
            Definition exprDef;

            //Ensure expr node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr(), out exprDef))
            {
                //There is no token here so we can't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, exprDef);
            }
        }

        public override void OutAPlusExpr(APlusExpr node)
        {
            Definition lhs, rhs;

            // Ensure lhs of the plus is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr(), out lhs))
            {
                Console.WriteLine("[" + node.GetPlus().Line + "] : left hand side of '+' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetExpr2(), out rhs))
            {
                Console.WriteLine("[" + node.GetPlus().Line + "] : right hand side of '+' was not decorated.");

                // Ensure sides are the same type
            }
            else if (!lhs.name.Equals(rhs.name))
            {
                Console.WriteLine("[" + node.GetPlus().Line + "] : Type mismatch.  Cannot add " + lhs.name + " to " +
                    rhs.name + ".");

                // Ensure that lhs and rhs are basic types
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetPlus().Line + "] : Invalid Type.  Cannot add " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAMinusExpr(AMinusExpr node)
        {
            Definition lhs, rhs;

            // Ensure lhs of the plus is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr(), out lhs))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : left hand side of '-' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetExpr2(), out rhs))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : right hand side of '-' was not decorated.");

                // Ensure sides are the same type
            }
            else if (!lhs.name.Equals(rhs.name))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : Type mismatch.  Cannot subtract " + lhs.name + " to " +
                    rhs.name + ".");

                // Ensure that lhs and rhs are basic types
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : Invalid Type.  Cannot subtract " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasstoexprtwoExpr(APasstoexprtwoExpr node)
        {
            Definition expr2Def;

            //Ensure expr2 node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr2(), out expr2Def))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, expr2Def);
            }
        }

        public override void OutAMultExpr2(AMultExpr2 node)
        {
            Definition lhs, rhs;

            // Ensure lhs of the plus is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr2(), out lhs))
            {
                Console.WriteLine("[" + node.GetMult().Line + "] : left hand side of '*' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetExpr3(), out rhs))
            {
                Console.WriteLine("[" + node.GetMult().Line + "] : right hand side of '*' was not decorated.");

                // Ensure sides are the same type
            }
            else if (!lhs.name.Equals(rhs.name))
            {
                Console.WriteLine("[" + node.GetMult().Line + "] : Type mismatch.  Cannot multiply " + lhs.name + " to " +
                    rhs.name + ".");

                // Ensure that lhs and rhs are basic types
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetMult().Line + "] : Invalid Type.  Cannot multiply " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutADivideExpr2(ADivideExpr2 node)
        {
            Definition lhs, rhs;

            // Ensure lhs of the plus is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr2(), out lhs))
            {
                Console.WriteLine("[" + node.GetDivide().Line + "] : left hand side of '/' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetExpr3(), out rhs))
            {
                Console.WriteLine("[" + node.GetDivide().Line + "] : right hand side of '/' was not decorated.");

                // Ensure sides are the same type
            }
            else if (!lhs.name.Equals(rhs.name))
            {
                Console.WriteLine("[" + node.GetDivide().Line + "] : Type mismatch.  Cannot divide " + lhs.name + " to " +
                    rhs.name + ".");

                // Ensure that lhs and rhs are basic types
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetDivide().Line + "] : Invalid Type.  Cannot divide " + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasstoexprthreeExpr2(APasstoexprthreeExpr2 node)
        {
            Definition exprthreeDef;

            // Ensure expr3 node is decorated.
            if (!_decoratedParseTree.TryGetValue(node.GetExpr3(), out exprthreeDef))
            {
                //  There is no token here, so we can't output an error with meaning.  
                //    However, it will propagate up the parse tree

                // Decorate this node
            }
            else
            {
                _decoratedParseTree.Add(node, exprthreeDef);
            }
        }

        public override void OutAExponentExpr3(AExponentExpr3 node)
        {
            Definition lhs, rhs;

            // Ensure lhs of the plus is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr3(), out lhs))
            {
                Console.WriteLine("[" + node.GetExponential().Line + "] : left hand side of '^' was not decorated.");

                // Ensure rhs of the plus is decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetExpr4(), out rhs))
            {
                Console.WriteLine("[" + node.GetExponential().Line + "] : right hand side of '^' was not decorated.");

                // Ensure sides are the same type
            }
            else if (!lhs.name.Equals(rhs.name))
            {
                Console.WriteLine("[" + node.GetExponential().Line + "] : Type mismatch.  Cannot ^ " + lhs.name + " to " +
                    rhs.name + ".");

                // Ensure that lhs and rhs are basic types
            }
            else if (!(lhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetExponential().Line + "] : Invalid Type.  Cannot ^" + lhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = lhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasstoexprfourExpr3(APasstoexprfourExpr3 node)
        {
            Definition negationDef;

            //Ensure exprfour node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetNegation(), out negationDef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, negationDef);
            }
        }

        public override void OutANegationNegation(ANegationNegation node)
        {
            Definition rhs;
            if (!_decoratedParseTree.TryGetValue(node.GetNotcond(), out rhs))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : right hand side of '-' was not decorated.");

            }
            else if (!(rhs is BasicTypeDefinition))
            {
                Console.WriteLine("[" + node.GetMinus().Line + "] : Invalid Type.  Cannot negate " + rhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BasicTypeDefinition currNodeType = new BasicTypeDefinition();
                currNodeType.name = rhs.name;
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPasstonotcondNegation(APasstonotcondNegation node)
        {
            Definition notcondDef;

            //Ensure notcond node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetNotcond(), out notcondDef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, notcondDef);
            }
        }

        public override void OutANotcondoneNotcond(ANotcondoneNotcond node)
        {
            Definition rhs;
            if (!_decoratedParseTree.TryGetValue(node.GetNotcond(), out rhs))
            {
                Console.WriteLine("[" + node.GetNot().Line + "] : right hand side of 'not' was not decorated.");

            }
            else if (!(rhs is BoolTypeDefinition))
            {
                Console.WriteLine("[" + node.GetNot().Line + "] : Invalid Type.  Cannot 'not' " + rhs.name + "s.");

                // Decorate this node
            }
            else
            {
                BoolTypeDefinition currNodeType = new BoolTypeDefinition();
                currNodeType.name = "bool";
                _decoratedParseTree.Add(node, currNodeType);
            }
        }

        public override void OutAPassequalitycondNotcond(APassequalitycondNotcond node)
        {
            Definition exprfourdef;

            //Ensure expr2 node is decorated
            if (!_decoratedParseTree.TryGetValue(node.GetExpr4(), out exprfourdef))
            {
                //There is no token here so we cna't output an error with meaning.
                //However it will propagate up the parse tree
            }
            //Decorate this node
            else
            {
                _decoratedParseTree.Add(node, exprfourdef);
            }
        }

        public override void OutAParentExpr4(AParentExpr4 node)
        {
            Definition operDef;

            // check type of operand
            if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out operDef))
            {
                //There is no token here so we can't output an error with meaning.
                //However it will propagate up the parse tree
            }

            // decorate this node
            else
            {
                _decoratedParseTree.Add(node, operDef);
            }
        }

        public override void OutASingularExpr4(ASingularExpr4 node)
        {
            Definition operDef;

            // check type of operand
            if (!_decoratedParseTree.TryGetValue(node.GetOperand(), out operDef))
            {
                //There is no token here so we can't output an error with meaning.
                //However it will propagate up the parse tree
            }

            // decorate this node
            else
            {
                _decoratedParseTree.Add(node, operDef);
            }
        }

        public override void OutAVariableoperandOperand(AVariableoperandOperand node)
        {
            Definition varDef;
            String varName = node.GetId().Text;
            bool flag = false;

            // check if varname is declared
            if (_currentSymbolTable.TryGetValue(varName, out varDef))
            {
                flag = true;
            }
            else if (_globalSymbolTable.TryGetValue(varName, out varDef))
            {
                flag = true;
            }

            if (!flag)
            { 
                Console.WriteLine("[" + node.GetId().Line + "] : " + varName + " is not defined.");

                // check that it is a variable definition
            }
            else if (!(varDef is VariableDefinition))
            {
                if(varName != "true" && varName != "false")
                {
                    Console.WriteLine("[" + node.GetId().Line + "] : " + varName + " is not a valid variable.");
                }
                else
                {
                    varDef.name = "bool";
                    _decoratedParseTree.Add(node, varDef);
                }
                // decorate this node
            }
            else
            {
                _decoratedParseTree.Add(node, ((VariableDefinition)varDef).vartype);
            }
        }

        public override void OutAStringoperandOperand(AStringoperandOperand node)
        {
            // Decorate this node
            StringTypeDefinition strDef = new StringTypeDefinition();
            strDef.name = "string";
            _decoratedParseTree.Add(node, strDef);
        }

        public override void OutABooloperandOperand(ABooloperandOperand node)
        {
            // Decorate this node
            BoolTypeDefinition boolDef = new BoolTypeDefinition();
            boolDef.name = "bool";
            _decoratedParseTree.Add(node, boolDef);
        }

        public override void OutAIntoperandOperand(AIntoperandOperand node)
        {
            // Decorate this node
            BasicTypeDefinition intDef = new BasicTypeDefinition();
            intDef.name = "int";
            _decoratedParseTree.Add(node, intDef);
        }

        public override void OutAFloatoperandOperand(AFloatoperandOperand node)
        {
            // Decorate this node
            BasicTypeDefinition floatDef = new BasicTypeDefinition();
            floatDef.name = "float";
            _decoratedParseTree.Add(node, floatDef);
        }

        public override void CaseEOF(EOF node)
        {
            base.CaseEOF(node);
            Console.WriteLine("Semantic Analyzation complete.");
        }


        public override void OutADeclarestmt(ADeclarestmt node)
        {
            Definition typeDef, varDef;
            String typeName = node.GetType().Text;
            String varName = node.GetVarname().Text;

            // Check that the type name is defined
            if (!_currentSymbolTable.TryGetValue(typeName, out typeDef))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not defined.");

                // Check that the type name is defined as a type
            }
            else if (!(typeDef is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetType().Line + "] : " + typeName + " is not a valid type.");

                // check that the var name is not defined
            }
            else if (_currentSymbolTable.TryGetValue(varName, out varDef) || _globalSymbolTable.TryGetValue(varName, out varDef))
            {
                Console.WriteLine("[" + node.GetVarname().Line + "] : " + varName + " is already defined.");

                // Add variable name and definition to symbol table
            }
            else
            {
                VariableDefinition newDef = new VariableDefinition();
                newDef.name = varName;
                newDef.vartype = (TypeDefinition)typeDef;
                _currentSymbolTable.Add(varName, newDef);
            }
        }

        public override void OutAAssignstmt(AAssignstmt node)
        {
            Definition idDef, exprDef;
            String varName = node.GetId().Text;

            // Ensure that ID is not in the global symbol table
            if (_globalSymbolTable.TryGetValue(varName, out idDef))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : " + varName + " is unchangeable.");
                // Ensure that ID is in the current symbol table
            }
            else if (!_currentSymbolTable.TryGetValue(varName, out idDef))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : " + varName + " is not defined.");
                // Ensure that ID is a variable
            }
            else if (!(idDef is VariableDefinition))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : " + varName + " is not a valid variable.");

                // Ensure that expr node has been decorated
            }
            else if (!_decoratedParseTree.TryGetValue(node.GetOrcond(), out exprDef))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : right hand side was not decorated.");

                // Ensure that expr and ID have the same types
            }
            else if (!exprDef.name.Equals(((VariableDefinition)idDef).vartype.name))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : Invalid assignment. Can not assign " + exprDef.name + " to " +
                    idDef.name + ".");

            }
        }

        public override void OutAFunctioncall(AFunctioncall node)
        {
            Definition idDef, exprDef;
            String funcName = node.GetId().Text;

            // Ensure that ID has been declared
            if (!_globalSymbolTable.TryGetValue(funcName, out idDef))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : " + funcName + " is not defined.");

                // Ensure that ID is a method
            }
            else if (!(idDef is MethodDefinition))
            {
                Console.WriteLine("[" + node.GetId().Line + "] : " + funcName + " is not a method.");

                // Ensure that argument has been decorated
            }
            else
            {
                // loop through lists and make sure they are the same.  If so you are good to go
                bool flag1 = true; // assume good until proven otherwise
                bool flag2 = true;

                List<TypeDefinition> parmList = ((MethodDefinition)idDef).paramList;

                int lenParamList = parmList.Count;
                int lenArgList = _argList.Count;
                
                if(lenArgList != lenParamList)
                {
                    flag1 = false;
                }

                if (flag1)
                {
                    for (int i = 0; i < lenParamList; i++)
                    {
                        TypeDefinition paramType = parmList[i];
                        TypeDefinition argType = _argList[i];
                        if (!paramType.name.Equals(argType.name))
                        {
                            flag2 = false;
                        }
                    }
                }

                if (!flag1)
                {
                    Console.WriteLine("[" + node.GetId().Line + "] : " + funcName + " has an incorrect number of arguments");
                }
                else if(!flag2)
                {
                    Console.WriteLine("[" + node.GetId().Line + "] : " + funcName + " has arguments of incorrect type(s)");
                }

                _argList = new List<TypeDefinition>();



            }
        }


    }
}

