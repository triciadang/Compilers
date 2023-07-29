using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ToyLanguage.node;

namespace ToyLanguage.analysis
{
    class CodeGenerator : DepthFirstAdapter
    {
        StreamWriter _output;
        int label_counter;
        int temp_label_counter;


        public CodeGenerator(StreamWriter argOutputStream) 
            {
                _output = argOutputStream;
            }

        public override void InAProg(AProg node)
        {
            _output.WriteLine(".assembly extern mscorlib {}");
            _output.WriteLine(".assembly Test");
            _output.WriteLine("{");
            _output.WriteLine("\t.ver 1:0:1:0");
            _output.WriteLine("}");

        }

        public override void InASubfunction(ASubfunction node)
        {
            _output.WriteLine(".method public static void " + node.GetId().Text + "() cil managed ");
            _output.WriteLine("{");
            _output.WriteLine("\t.maxstack 128");
        }

        public override void OutASubfunction(ASubfunction node)
        {
            _output.WriteLine("\tret");
            _output.WriteLine("}");
        }

        public override void InAMainstmt(AMainstmt node)
        {
            _output.WriteLine(".method static void main() cil managed");
            _output.WriteLine("{");
            _output.WriteLine("\t.maxstack 128");
            _output.WriteLine("\t.entrypoint");
        }

        public override void OutAMainstmt(AMainstmt node)
        {
            _output.WriteLine("\tret");
            _output.WriteLine("}");
        }


        public override void OutAProg(AProg node)
        {
            _output.Close();
        }



        public override void OutAFunctioncall(AFunctioncall node)
        {
            if (node.GetId().Text.Equals("new_line"))
            {
                _output.WriteLine("\tldstr \"\\n\"");
                _output.WriteLine("\tcall void [mscorlib]System.Console::Write(string)");
            }
            else if (node.GetId().Text.Equals("put_int"))
            {
                _output.WriteLine("\tcall void [mscorlib]System.Console::WriteLine(int32)");

            }
            else if (node.GetId().Text.Equals("put_float"))
            {
                _output.WriteLine("\tcall void [mscorlib]System.Console::WriteLine(float32)");

            }
            else if (node.GetId().Text.Equals("put_string"))
            {
                _output.WriteLine("\tcall void [mscorlib]System.Console::Write(string)");

            }
            else
            {
                _output.WriteLine("\tcall void " + node.GetId().Text + "()");
            }
        }



        public override void CaseAWhilestmt(AWhilestmt node)
        {
            int local_label = label_counter;

            label_counter += 1;
            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            InAWhilestmt(node);
            if (node.GetWhile() != null)
            {
                node.GetWhile().Apply(this);
            }
            if (node.GetLparen() != null)
            {
                node.GetLparen().Apply(this);
            }
            if (node.GetOrcond() != null)
            {
                node.GetOrcond().Apply(this);
            }
            
            if (node.GetRparen() != null)
            {
                node.GetRparen().Apply(this);
            }
            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;

            _output.WriteLine("\tldc.i4 0");
            _output.WriteLine("\tbeq L" + local_label);


            if (node.GetOpenbracket() != null)
            {
                node.GetOpenbracket().Apply(this);
            }
            if (node.GetStmts() != null)
            {
                node.GetStmts().Apply(this);
            }
            if (node.GetClosebracket() != null)
            {
                node.GetClosebracket().Apply(this);
            }
            OutAWhilestmt(node);
            _output.WriteLine("\tbr L" + (local_label + 1));
            _output.WriteLine("L" + local_label + ":");
            local_label += 1;
        }

        public override void CaseACondifstmt(ACondifstmt node)
        {
            int local_label = label_counter;

            //allocate two for if and else stmt
            label_counter += 2;
            InACondifstmt(node);
            if (node.GetIf() != null)
            {
                node.GetIf().Apply(this);
            }
            if (node.GetLparen() != null)
            {
                node.GetLparen().Apply(this);
            }
            if (node.GetOrcond() != null)
            {
                node.GetOrcond().Apply(this);
            }
            if (node.GetRparen() != null)
            {
                node.GetRparen().Apply(this);
            }
            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;

            _output.WriteLine("\tldc.i4 0");
            _output.WriteLine("\tbeq L" + local_label);
            if (node.GetIfbracketopen() != null)
            {
                node.GetIfbracketopen().Apply(this);
            }
            if (node.GetResultone() != null)
            {
                node.GetResultone().Apply(this);
            }
            if (node.GetIfbracketclose() != null)
            {
                node.GetIfbracketclose().Apply(this);
            }
            if (node.GetCondelsestmt() != null)
            {
                _output.WriteLine("\tbr L" + (local_label + 1));
                _output.WriteLine("L" + local_label + ":");
                local_label += 1;
                node.GetCondelsestmt().Apply(this);
            }
            OutACondifstmt(node);
            _output.WriteLine("L" + local_label + ":");
            local_label += 1;
        }





        public override void OutAOrcondoneOrcond(AOrcondoneOrcond node)
        {
            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tor");
        }
        public override void OutAAndcondoneAndcond(AAndcondoneAndcond node)
        {
            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tand");
        }



        public override void OutAEqualitycomparisonCondition(AEqualitycomparisonCondition node)
        {
            _output.WriteLine("\tbeq L" + label_counter);
            _output.WriteLine("\tldc.i4 0");
            _output.WriteLine("\tbr L" + (label_counter + 1));

            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tldc.i4 1");


        }

        public override void OutAGreatercondCondition(AGreatercondCondition node)
        {
            _output.WriteLine("\tbgt L" + label_counter);
            _output.WriteLine("\tldc.i4 0");
            _output.WriteLine("\tbr L" + (label_counter+1));

            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tldc.i4 1");

        }



        public override void OutAGreatereqcondCondition(AGreatereqcondCondition node)
        {
            _output.WriteLine("\tblt L" + label_counter);
            _output.WriteLine("\tldc.i4 1");
            _output.WriteLine("\tbr L" + (label_counter + 1));

            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tldc.i4 0");


        }

        public override void OutALesscondCondition(ALesscondCondition node)
        {
            _output.WriteLine("\tblt L" + label_counter);
            _output.WriteLine("\tldc.i4 0");
            _output.WriteLine("\tbr L" + (label_counter + 1));

            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tldc.i4 1");


        }


        public override void OutALesseqcondCondition(ALesseqcondCondition node)
        {
            _output.WriteLine("\tbgt L" + label_counter);
            _output.WriteLine("\tldc.i4 1");
            _output.WriteLine("\tbr L" + (label_counter + 1));

            _output.WriteLine("L" + label_counter + ":");
            label_counter += 1;
            _output.WriteLine("\tldc.i4 0");


        }

        public override void OutAPlusExpr(APlusExpr node)
        {
            _output.WriteLine("\tadd");
        }
        public override void OutAMinusExpr(AMinusExpr node)
        {
            _output.WriteLine("\tsub");
        }

        public override void OutAMultExpr2(AMultExpr2 node)
        {
            _output.WriteLine("\tmul");
        }
        public override void OutADivideExpr2(ADivideExpr2 node)
        {
            _output.WriteLine("\tdiv");
        }

        public override void OutANegationNegation(ANegationNegation node)
        {
            _output.WriteLine("\tldc.r4 -1");
            _output.WriteLine("\tmul");
         
        }

        public override void OutANotcondoneNotcond(ANotcondoneNotcond node)
        {
            _output.WriteLine("\tnot");
        }

        public override void OutABooloperandOperand(ABooloperandOperand node)
        {
            _output.WriteLine("\tldstr " + node.GetBoolean().Text);
        }
        public override void OutAVariableoperandOperand(AVariableoperandOperand node)
        {
            _output.WriteLine("\tldloc " + node.GetId().Text);
        }
        public override void OutAStringoperandOperand(AStringoperandOperand node)
        {
            _output.WriteLine("\tldstr " + node.GetString().Text);
        }
        public override void OutAIntoperandOperand(AIntoperandOperand node)
        {
            _output.WriteLine("\tldc.i4 " + node.GetInteger().Text);
        }

        public override void OutAFloatoperandOperand(AFloatoperandOperand node)
        {
            _output.WriteLine("\tldc.r4 " + node.GetFloat().Text);
        }

        public override void InADeclarestmt(ADeclarestmt node)
        {
            _output.Write("\t.locals init(");

            if (node.GetType().Text == "int" || node.GetType().Text == "float")
            {
                _output.WriteLine(node.GetType().Text + "32 " + node.GetVarname().Text + ")");
            }
            else if (node.GetType().Text == "string")
            {
                _output.WriteLine(node.GetType().Text +" "+ node.GetVarname().Text + ")");
            }
        }
        public override void OutAAssignstmt(AAssignstmt node)
        {
            _output.WriteLine("\tstloc " + node.GetId().Text);
        }


    }
}
