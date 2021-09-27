Imports IRISCalculator.DataClasses
Namespace Workers
    Public Class CatalogGroupNameWorker
        ''' <summary>
        ''' Возвращает категорию каталога
        ''' </summary>
        ''' <param name="groupCode"></param>
        ''' <returns></returns>
        Public Shared Function GetItemCategory(groupCode As String) As CatalogItem.ItemCategoryEnum
            'Разбиваем код каталога составляющие разделенные знаком $
            Dim str As String() = groupCode.ToString.Split("$", StringSplitOptions.RemoveEmptyEntries)
            'У правильного кода составляющих 3 (цифррвой код для сортировки, буквенный код соновного раздела и буквенный код подраздела)
            If str.Length = 3 Then
                'Если это правильный код...
                If str(1) = "PAPER" Then
                    '... и его основной раздел бумага, то сразу вохвращаем соответствующее значение
                    Return CatalogItem.ItemCategoryEnum.PAPER
                Else
                    'в противных случаях вычисляем код раздела на основе составного наименование
                    Return [Enum].Parse(GetType(CatalogItem.ItemCategoryEnum), str(1) & str(2))
                End If
            Else
                'Если код был не верным, возврачаем заглушку
                Return CatalogItem.ItemCategoryEnum.NONE
            End If
        End Function
        ''' <summary>
        ''' Возвращает понятное наименование раздела каталога на основе его кода
        ''' !!!! В доработке
        ''' </summary>
        ''' <param name="groupCode"></param>
        ''' <returns></returns>
        Public Shared Function GetItemName(groupCode As String) As String
            Dim str As String() = groupCode.ToString.Split("$", StringSplitOptions.RemoveEmptyEntries)
            If str.Length = 3 Then
                Dim result As String = ""
                Select Case str(1)
                    Case "PAPER"
                        result &= "Бумага "
                    Case "SERVICE"
                        result &= "Услуга "
                End Select

                Select Case str(2)
                    Case "DIZ"
                        result &= "дизайнерская"
                    Case "MAT"
                        result &= "матовая"
                    Case "OFIS"
                        result &= "офисная (офсетная)"
                    Case "SAMOKL"
                        result &= "самоклейка"
                    Case "SINT"
                        result &= "синтетика"
                    '\\\\
                    Case "PRINT"
                        result &= "печать"
                End Select
                Return result
            Else
                Return groupCode
            End If
        End Function
    End Class
End Namespace