Imports System.CodeDom.Compiler
Imports System.Reflection

Public Class OldFunction
    ''' <summary>
    ''' Преобразует текст математическорй формулы в результат
    ''' </summary>
    ''' <param name="formula"></param>
    ''' <returns></returns>
    Public Shared Function GetFormulaRezult(formula As String) As Double
        Dim codetext As String = "Public Class LambdaCreator" & vbCr & vbLf & "Public Shared Function Calc() As Double" & vbCr & vbLf & "Return " & formula & vbCr & vbLf & "End Function" & vbCr & vbLf & "End Class"
        Dim provider As New VBCodeProvider
        Dim parameters As New CompilerParameters
        parameters.GenerateInMemory = True
        parameters.ReferencedAssemblies.Add("System.dll")

        Dim results As CompilerResults = provider.CompileAssemblyFromSource(parameters, codetext)
        Dim cls As Type = results.CompiledAssembly.GetType("LambdaCreator")
        Dim method As MethodInfo = cls.GetMethod("Calc", BindingFlags.Static Or BindingFlags.Public)
        Return method.Invoke(Nothing, Nothing)
    End Function
End Class