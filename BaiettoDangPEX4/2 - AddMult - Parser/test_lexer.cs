/* This file was generated by SableCC (http://www.sablecc.org/). */

using System;
using System.Collections;
using System.Text;
using System.IO;
using ToyLanguage.lexer;
using ToyLanguage.node;

class TestLexer
{
  public static void Main(String[] args)
  {
    Lexer l = new Lexer(Console.In);
    while (true)
    {
        Token token = l.Next();
        Console.WriteLine ("Read token '" + token.GetType().Name +
            "', Text = [" + token.Text + "] at [" + token.Line +
            "," + token.Pos + "]");
        if ( token is EOF ) break;
    }
  }
}
