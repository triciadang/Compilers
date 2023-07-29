.assembly extern mscorlib {}
.assembly Test
{
	.ver 1:0:1:0
}
.method public static void subprogram() cil managed 
{
	.maxstack 128
	ldstr "in subprogram 1"
	call void [mscorlib]System.Console::Write(string)
	ret
}
.method static void main() cil managed
{
	.maxstack 128
	.entrypoint
	.locals init(int32 x)
	ldc.i4 5
	ldc.i4 5
	add
	stloc x
	ldstr "xIsLoadedWith "
	call void [mscorlib]System.Console::Write(string)
	ldloc x
	call void [mscorlib]System.Console::WriteLine(int32)
	ldstr "\n"
	call void [mscorlib]System.Console::Write(string)
	call void subprogram()
	ret
}
