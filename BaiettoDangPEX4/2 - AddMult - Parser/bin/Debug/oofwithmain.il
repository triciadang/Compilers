.assembly extern mscorlib {}
.assembly Test
{
	.ver 1:0:1:0
}
.method public static void subprogram_uno() cil managed 
{
	.maxstack 128
	.locals init(int32 a)
	ldc.i4 71
	stloc a
	ldloc a
	ldc.i4 100
	bgt L2
	ldc.i4 1
	br L3
L2:
	ldc.i4 0
L3:
	ldc.i4 0
	beq L0
	ldloc a
	ldc.i4 2
	sub
	stloc a
	ldstr "a should be 69: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(int32)
	br L1
L0:
L1:
	ret
}
.method public static void subprogram_dos() cil managed 
{
	.maxstack 128
	ldstr " oof"
	call void [mscorlib]System.Console::Write(string)
	ret
}
.method static void main() cil managed
{
	.maxstack 128
	.entrypoint
	.locals init(string answer)
	.locals init(string vara)
	.locals init(string varb)
	.locals init(string varc)
	.locals init(float32 a)
	ldc.r4 42.0
	stloc a
	ldstr "a should be 42: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	ldstr "big oof"
	call void [mscorlib]System.Console::Write(string)
	ldstr "Documentation: Hitchhikers's guide"
	stloc vara
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr " to the galaxy"
	stloc varb
	ldloc vara
	call void [mscorlib]System.Console::Write(string)
	ldloc varb
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	ldc.r4 42.0
	beq L6
	ldc.i4 0
	br L7
L6:
	ldc.i4 1
L7:
	ldc.i4 0
	beq L4
	ldstr "correct"
	stloc answer
	br L5
L4:
	ldstr "nein, try again"
	stloc answer
L5:
	ldloc answer
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "pink fluffy unicorns"
	stloc varc
L9:
	ldloc answer
	ldstr "correct"
	beq L10
	ldc.i4 0
	br L11
L10:
	ldc.i4 1
L11:
	ldc.i4 0
	beq L8
	.locals init(string sing)
	ldstr "na na na c'mon"
	stloc sing
	ldstr "sing is: "
	call void [mscorlib]System.Console::Write(string)
	ldloc sing
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "correcto"
	stloc answer
	br L9
L8:
	ldloc answer
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	call void subprogram_uno()
	ldloc a
	ldc.r4 100.0
	bgt L14
	ldc.i4 1
	br L15
L14:
	ldc.i4 0
L15:
	ldc.i4 0
	beq L12
	ldloc a
	ldc.r4 2.0
	sub
	stloc a
	ldstr "a should be 40.0: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	br L13
L12:
L13:
L17:
	ldloc a
	ldc.r4 0.0
	bgt L18
	ldc.i4 0
	br L19
L18:
	ldc.i4 1
	ldloc a
	ldc.r4 100.0
	blt L19
	ldc.i4 0
	br L20
L19:
	ldc.i4 1
L20:
	and
L21:
	ldc.i4 0
	beq L16
	ldstr "dancing on rainbows"
	stloc varc
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldstr "pink fluffy unicorns "
	call void [mscorlib]System.Console::Write(string)
	ldloc varc
	call void [mscorlib]System.Console::Write(string)
	call void subprogram_dos()
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	ldc.r4 10.0
	sub
	stloc a
	ldstr "a should go down by 10: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	br L17
L16:
	ldloc a
	ldc.r4 0.0
	blt L24
	ldc.i4 1
	br L25
L24:
	ldc.i4 0
L25:
	ldc.i4 0
	beq L22
	.locals init(string sing)
	ldstr "la la la"
	stloc sing
	ldloc a
	ldc.r4 1.0
	add
	stloc a
	ldstr "a should be 1.0: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	br L23
L22:
L23:
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	ldc.r4 0.0
	blt L28
	ldc.i4 0
	br L29
L28:
	ldc.i4 1
L29:
	ldc.i4 0
	beq L26
	ldloc a
	ldc.r4 1.0
	add
	stloc a
	ldstr "nothing should print"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	br L27
L26:
	ldstr "a should still be 1: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	ldloc a
	ldc.r4 2.0
	add
	stloc a
	ldstr "a should be 3.0: "
	call void [mscorlib]System.Console::Write(string)
	ldloc a
	call void [mscorlib]System.Console::WriteLine(float32)
	.locals init(int32 b)
	ldc.i4 5
	stloc b
	ldloc b
	call void [mscorlib]System.Console::WriteLine(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
L31:
	ldloc b
	ldc.i4 1
	bgt L32
	ldc.i4 0
	br L33
L32:
	ldc.i4 1
L33:
	ldc.i4 0
	beq L30
	ldloc b
	ldc.i4 1
	sub
	stloc b
	ldloc b
	call void [mscorlib]System.Console::WriteLine(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	br L31
L30:
	ldstr "Blast off!"
	call void [mscorlib]System.Console::Write(string)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
L27:
	.locals init(float32 u)
	ldc.r4 6.2
	ldc.r4 -1
	mul
	ldc.r4 7.0
	mul
	stloc u
	ldstr "u should be -43.4: "
	call void [mscorlib]System.Console::Write(string)
	ldloc u
	call void [mscorlib]System.Console::WriteLine(float32)
	ldc.r4 6.3
	ldc.r4 -1
	mul
	stloc u
	ldstr "u should be -6.3: "
	call void [mscorlib]System.Console::Write(string)
	ldloc u
	call void [mscorlib]System.Console::WriteLine(float32)
	.locals init(int32 nick)
	ldc.i4 5
	ldc.i4 4
	mul
	ldc.i4 6
	ldc.i4 2
	div
	sub
	ldc.i4 3
	add
	stloc nick
	ldstr "nick should be bob(20): "
	call void [mscorlib]System.Console::Write(string)
	ldloc nick
	call void [mscorlib]System.Console::WriteLine(int32)
	ret
}
