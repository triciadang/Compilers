/* This file was generated by SableCC (http://www.sablecc.org/). */

using System;
using System.Collections;
using System.Text;
using System.IO;
using ToyLanguage.node;

namespace ToyLanguage.lexer {

internal class PushbackReader {
  private TextReader reader;
  private Stack stack = new Stack ();


  internal PushbackReader (TextReader reader)
  {
    this.reader = reader;
  }

  internal int Peek ()
  {
    if ( stack.Count > 0 ) return (int)stack.Peek();
    return reader.Peek();
  }

  internal int Read ()
  {
    if ( stack.Count > 0 ) return (int)stack.Pop();
    return reader.Read();
  }

  internal void Unread (int v)
  {
    stack.Push (v);
  }
}

public class LexerException : ApplicationException
{
    public LexerException(String message) : base (message)
    {
    }
}

public class Lexer
{
    protected Token token;
    protected State currentState = State.INITIAL;

    private PushbackReader input;
    private int line;
    private int pos;
    private bool cr;
    private bool eof;
    private StringBuilder text = new StringBuilder();

    protected virtual void Filter()
    {
    }

    public Lexer(TextReader input)
    {
        this.input = new PushbackReader(input);
    }

    public virtual Token Peek()
    {
        while(token == null)
        {
            token = GetToken();
            Filter();
        }

        return token;
    }

    public virtual Token Next()
    {
        while(token == null)
        {
            token = GetToken();
            Filter();
        }

        Token result = token;
        token = null;
        return result;
    }

    protected virtual Token GetToken()
    {
        int dfa_state = 0;

        int start_pos = pos;
        int start_line = line;

        int accept_state = -1;
        int accept_token = -1;
        int accept_length = -1;
        int accept_pos = -1;
        int accept_line = -1;

        int[][][] gotoTable = Lexer.gotoTable[currentState.id()];
        int[] accept = Lexer.accept[currentState.id()];
        text.Length = 0;

        while(true)
        {
            int c = GetChar();

            if(c != -1)
            {
                switch(c)
                {
                case 10:
                    if(cr)
                    {
                        cr = false;
                    }
                    else
                    {
                        line++;
                        pos = 0;
                    }
                    break;
                case 13:
                    line++;
                    pos = 0;
                    cr = true;
                    break;
                default:
                    pos++;
                    cr = false;
                    break;
                };

                text.Append((char) c);
                do
                {
                    int oldState = (dfa_state < -1) ? (-2 -dfa_state) : dfa_state;

                    dfa_state = -1;

                    int[][] tmp1 =  gotoTable[oldState];
                    int low = 0;
                    int high = tmp1.Length - 1;

                    while(low <= high)
                    {
                        int middle = (low + high) / 2;
                        int[] tmp2 = tmp1[middle];

                        if(c < tmp2[0])
                        {
                            high = middle - 1;
                        }
                        else if(c > tmp2[1])
                        {
                            low = middle + 1;
                        }
                        else
                        {
                            dfa_state = tmp2[2];
                            break;
                        }
                    }
                }while(dfa_state < -1);
            }
            else
            {
                dfa_state = -1;
            }

            if(dfa_state >= 0)
            {
                if(accept[dfa_state] != -1)
                {
                    accept_state = dfa_state;
                    accept_token = accept[dfa_state];
                    accept_length = text.Length;
                    accept_pos = pos;
                    accept_line = line;
                }
            }
            else
            {
                if(accept_state != -1)
                {
                    switch(accept_token)
                    {
                    case 0:
                        {
                            Token token = New0(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 1:
                        {
                            Token token = New1(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 2:
                        {
                            Token token = New2(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 3:
                        {
                            Token token = New3(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 4:
                        {
                            Token token = New4(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 5:
                        {
                            Token token = New5(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 6:
                        {
                            Token token = New6(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 7:
                        {
                            Token token = New7(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 8:
                        {
                            Token token = New8(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 9:
                        {
                            Token token = New9(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 10:
                        {
                            Token token = New10(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 11:
                        {
                            Token token = New11(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 12:
                        {
                            Token token = New12(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 13:
                        {
                            Token token = New13(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 14:
                        {
                            Token token = New14(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 15:
                        {
                            Token token = New15(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 16:
                        {
                            Token token = New16(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 17:
                        {
                            Token token = New17(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 18:
                        {
                            Token token = New18(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 19:
                        {
                            Token token = New19(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 20:
                        {
                            Token token = New20(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 21:
                        {
                            Token token = New21(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 22:
                        {
                            Token token = New22(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 23:
                        {
                            Token token = New23(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 24:
                        {
                            Token token = New24(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 25:
                        {
                            Token token = New25(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 26:
                        {
                            Token token = New26(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 27:
                        {
                            Token token = New27(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 28:
                        {
                            Token token = New28(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 29:
                        {
                            Token token = New29(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 30:
                        {
                            Token token = New30(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 31:
                        {
                            Token token = New31(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 32:
                        {
                            Token token = New32(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    }
                }
                else
                {
                    if(text.Length > 0)
                    {
                        throw new LexerException(
                            "[" + (start_line + 1) + "," + (start_pos + 1) + "]" +
                            " Unknown token: " + text);
                    }
                    else
                    {
                        EOF token = new EOF(
                            start_line + 1,
                            start_pos + 1);
                        return token;
                    }
                }
            }
        }
    }

    private Token New0(String text, int line, int pos) { return new TAssign(text, line, pos); }
    private Token New1(String text, int line, int pos) { return new TPlus(text, line, pos); }
    private Token New2(String text, int line, int pos) { return new TMult(text, line, pos); }
    private Token New3(String text, int line, int pos) { return new TMinus(text, line, pos); }
    private Token New4(String text, int line, int pos) { return new TDivide(text, line, pos); }
    private Token New5(String text, int line, int pos) { return new TExponential(text, line, pos); }
    private Token New6(String text, int line, int pos) { return new TEqual(text, line, pos); }
    private Token New7(String text, int line, int pos) { return new TGreaterEqual(text, line, pos); }
    private Token New8(String text, int line, int pos) { return new TLessEqual(text, line, pos); }
    private Token New9(String text, int line, int pos) { return new TGreater(text, line, pos); }
    private Token New10(String text, int line, int pos) { return new TLess(text, line, pos); }
    private Token New11(String text, int line, int pos) { return new TAnd(text, line, pos); }
    private Token New12(String text, int line, int pos) { return new TOr(text, line, pos); }
    private Token New13(String text, int line, int pos) { return new TNot(text, line, pos); }
    private Token New14(String text, int line, int pos) { return new TConstant(text, line, pos); }
    private Token New15(String text, int line, int pos) { return new TIf(text, line, pos); }
    private Token New16(String text, int line, int pos) { return new TElse(text, line, pos); }
    private Token New17(String text, int line, int pos) { return new TWhile(text, line, pos); }
    private Token New18(String text, int line, int pos) { return new TVoid(text, line, pos); }
    private Token New19(String text, int line, int pos) { return new TLparen(text, line, pos); }
    private Token New20(String text, int line, int pos) { return new TRparen(text, line, pos); }
    private Token New21(String text, int line, int pos) { return new TComma(text, line, pos); }
    private Token New22(String text, int line, int pos) { return new TOpenbracket(text, line, pos); }
    private Token New23(String text, int line, int pos) { return new TClosebracket(text, line, pos); }
    private Token New24(String text, int line, int pos) { return new TMain(text, line, pos); }
    private Token New25(String text, int line, int pos) { return new TEol(text, line, pos); }
    private Token New26(String text, int line, int pos) { return new TId(text, line, pos); }
    private Token New27(String text, int line, int pos) { return new TComment(text, line, pos); }
    private Token New28(String text, int line, int pos) { return new TInteger(text, line, pos); }
    private Token New29(String text, int line, int pos) { return new TFloat(text, line, pos); }
    private Token New30(String text, int line, int pos) { return new TString(text, line, pos); }
    private Token New31(String text, int line, int pos) { return new TBoolean(text, line, pos); }
    private Token New32(String text, int line, int pos) { return new TBlank(text, line, pos); }

    private int GetChar()
    {
        if(eof)
        {
            return -1;
        }

        int result = input.Read();

        if(result == -1)
        {
            eof = true;
        }

        return result;
    }

    private void PushBack(int acceptLength)
    {
        int length = text.Length;
        for(int i = length - 1; i >= acceptLength; i--)
        {
            eof = false;

            input.Unread(text[i]);
        }
    }


    protected virtual void Unread(Token token)
    {
        String text = token.Text;
        int length = text.Length;

        for(int i = length - 1; i >= 0; i--)
        {
            eof = false;

            input.Unread(text[i]);
        }

        pos = token.Pos - 1;
        line = token.Line - 1;
    }

    private string GetText(int acceptLength)
    {
        StringBuilder s = new StringBuilder(acceptLength);
        for(int i = 0; i < acceptLength; i++)
        {
            s.Append(text[i]);
        }

        return s.ToString();
    }

    private static int[][][][] gotoTable = {
      new int[][][] {
        new int[][] {
          new int[] {9, 9, 1},
          new int[] {10, 10, 2},
          new int[] {13, 13, 3},
          new int[] {32, 32, 4},
          new int[] {33, 33, 5},
          new int[] {34, 34, 6},
          new int[] {38, 38, 7},
          new int[] {40, 40, 8},
          new int[] {41, 41, 9},
          new int[] {42, 42, 10},
          new int[] {43, 43, 11},
          new int[] {44, 44, 12},
          new int[] {45, 45, 13},
          new int[] {46, 46, 14},
          new int[] {47, 47, 15},
          new int[] {48, 48, 16},
          new int[] {49, 57, 17},
          new int[] {58, 58, 18},
          new int[] {59, 59, 19},
          new int[] {60, 60, 20},
          new int[] {61, 61, 21},
          new int[] {62, 62, 22},
          new int[] {65, 90, 23},
          new int[] {94, 94, 24},
          new int[] {95, 95, 25},
          new int[] {97, 98, 23},
          new int[] {99, 99, 26},
          new int[] {100, 100, 23},
          new int[] {101, 101, 27},
          new int[] {102, 102, 28},
          new int[] {103, 104, 23},
          new int[] {105, 105, 29},
          new int[] {106, 108, 23},
          new int[] {109, 109, 30},
          new int[] {110, 115, 23},
          new int[] {116, 116, 31},
          new int[] {117, 117, 23},
          new int[] {118, 118, 32},
          new int[] {119, 119, 33},
          new int[] {120, 122, 23},
          new int[] {123, 123, 34},
          new int[] {124, 124, 35},
          new int[] {125, 125, 36},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {0, 33, 37},
          new int[] {34, 34, 38},
          new int[] {35, 91, 37},
          new int[] {92, 92, 39},
          new int[] {93, 65535, 37},
        },
        new int[][] {
          new int[] {38, 38, 40},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {48, 57, 41},
          new int[] {69, 69, 42},
          new int[] {101, 101, 43},
        },
        new int[][] {
          new int[] {47, 47, 44},
        },
        new int[][] {
          new int[] {46, 46, 14},
        },
        new int[][] {
          new int[] {46, 46, 45},
          new int[] {48, 57, 46},
          new int[] {69, 69, 47},
          new int[] {101, 101, 48},
        },
        new int[][] {
          new int[] {61, 61, 49},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {61, 61, 50},
        },
        new int[][] {
          new int[] {61, 61, 51},
        },
        new int[][] {
          new int[] {61, 61, 52},
        },
        new int[][] {
          new int[] {65, 90, 23},
          new int[] {95, 95, 53},
          new int[] {97, 122, 23},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 110, 23},
          new int[] {111, 111, 54},
          new int[] {112, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 107, 23},
          new int[] {108, 108, 55},
          new int[] {109, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 97, 56},
          new int[] {98, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 101, 23},
          new int[] {102, 102, 57},
          new int[] {103, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 97, 58},
          new int[] {98, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 113, 23},
          new int[] {114, 114, 59},
          new int[] {115, 122, 23},
        },
        new int[][] {
          new int[] {65, 110, -28},
          new int[] {111, 111, 60},
          new int[] {112, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 103, 23},
          new int[] {104, 104, 61},
          new int[] {105, 122, 23},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {124, 124, 62},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {0, 65535, -8},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {0, 33, 37},
          new int[] {34, 34, 63},
          new int[] {35, 65535, -8},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {48, 101, -16},
        },
        new int[][] {
          new int[] {45, 45, 64},
          new int[] {49, 57, 65},
        },
        new int[][] {
          new int[] {45, 57, -44},
        },
        new int[][] {
          new int[] {0, 9, 66},
          new int[] {11, 12, 66},
          new int[] {14, 65535, 66},
        },
        new int[][] {
          new int[] {48, 57, 67},
        },
        new int[][] {
          new int[] {46, 101, -19},
        },
        new int[][] {
          new int[] {45, 45, 68},
          new int[] {49, 57, 69},
        },
        new int[][] {
          new int[] {45, 57, -49},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {65, 90, 70},
          new int[] {97, 122, 70},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 109, 23},
          new int[] {110, 110, 71},
          new int[] {111, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 114, 23},
          new int[] {115, 115, 72},
          new int[] {116, 122, 23},
        },
        new int[][] {
          new int[] {65, 107, -29},
          new int[] {108, 108, 73},
          new int[] {109, 122, 23},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 104, 23},
          new int[] {105, 105, 74},
          new int[] {106, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 116, 23},
          new int[] {117, 117, 75},
          new int[] {118, 122, 23},
        },
        new int[][] {
          new int[] {65, 104, -60},
          new int[] {105, 105, 76},
          new int[] {106, 122, 23},
        },
        new int[][] {
          new int[] {65, 104, -60},
          new int[] {105, 105, 77},
          new int[] {106, 122, 23},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {0, 65535, -8},
        },
        new int[][] {
          new int[] {49, 57, 65},
        },
        new int[][] {
          new int[] {48, 57, 78},
        },
        new int[][] {
          new int[] {0, 65535, -46},
        },
        new int[][] {
          new int[] {48, 57, 67},
          new int[] {69, 101, -19},
        },
        new int[][] {
          new int[] {49, 57, 69},
        },
        new int[][] {
          new int[] {48, 57, 79},
        },
        new int[][] {
          new int[] {65, 90, 80},
          new int[] {95, 95, 53},
          new int[] {97, 122, 80},
        },
        new int[][] {
          new int[] {65, 114, -57},
          new int[] {115, 115, 81},
          new int[] {116, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 100, 23},
          new int[] {101, 101, 82},
          new int[] {102, 122, 23},
        },
        new int[][] {
          new int[] {65, 114, -57},
          new int[] {115, 115, 83},
          new int[] {116, 122, 23},
        },
        new int[][] {
          new int[] {65, 109, -56},
          new int[] {110, 110, 84},
          new int[] {111, 122, 23},
        },
        new int[][] {
          new int[] {65, 100, -74},
          new int[] {101, 101, 85},
          new int[] {102, 122, 23},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 99, 23},
          new int[] {100, 100, 86},
          new int[] {101, 122, 23},
        },
        new int[][] {
          new int[] {65, 107, -29},
          new int[] {108, 108, 87},
          new int[] {109, 122, 23},
        },
        new int[][] {
          new int[] {48, 57, 78},
        },
        new int[][] {
          new int[] {48, 57, 79},
        },
        new int[][] {
          new int[] {65, 122, -72},
        },
        new int[][] {
          new int[] {65, 95, -25},
          new int[] {97, 115, 23},
          new int[] {116, 116, 88},
          new int[] {117, 122, 23},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 100, -74},
          new int[] {101, 101, 89},
          new int[] {102, 122, 23},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 100, -74},
          new int[] {101, 101, 90},
          new int[] {102, 122, 23},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
        new int[][] {
          new int[] {65, 122, -25},
        },
      },
    };

    private static int[][] accept = {
      new int[] {
        26, 32, 32, 32, 32, 13, -1, -1, 19, 20, 2, 1, 21, 3, 29, 4, 
        28, 28, -1, 25, 10, -1, 9, 26, 5, 26, 26, 26, 26, 26, 26, 26, 
        26, 26, 22, -1, 23, -1, 30, -1, 11, 29, -1, -1, 27, -1, 28, -1, 
        -1, 0, 8, 6, 7, 26, 26, 26, 26, 15, 26, 26, 26, 26, 12, 30, 
        -1, 29, 27, 29, -1, 29, 26, 26, 26, 26, 26, 26, 26, 26, 29, 29, 
        26, 26, 16, 26, 24, 26, 18, 26, 14, 26, 17, 
      },
    };

    public class State
    {
        public static State INITIAL = new State(0);

        private int _id;

        private State(int _id)
        {
            this._id = _id;
        }

        public int id()
        {
            return _id;
        }
    }
}
}
