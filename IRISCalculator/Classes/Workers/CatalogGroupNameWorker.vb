Imports IRISCalculator.DataClasses
Namespace Workers
    Public Class CatalogGroupNameWorker

        Public Shared Function GetItemCategory(groupCode As String) As CatalogItem.ItemCategoryEnum
            Dim str As String() = groupCode.ToString.Split("$", StringSplitOptions.RemoveEmptyEntries)
            If str.Length = 3 Then
                Dim result As CatalogItem.ItemCategoryEnum = CatalogItem.ItemCategoryEnum.None
                Select Case str(1)
                    Case "PAPER"
                        result = CatalogItem.ItemCategoryEnum.Paper
                    Case "SERVICE"
                        result = CatalogItem.ItemCategoryEnum.Service
                End Select
                Return result
            Else
                Return CatalogItem.ItemCategoryEnum.None
            End If
        End Function
    End Class



End Namespace