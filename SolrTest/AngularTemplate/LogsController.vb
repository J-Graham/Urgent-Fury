Imports System.Net
Imports System.Net.WebRequestMethods.Http
Imports System.Web.Http
Imports System.Data.Entity

Public Class LogsController
    Inherits ApiController


    'GET api/<controller>
    <HttpGet>
    Public Function GetLogs(Optional ByVal RequestURiBase As String = "") As DataLibrary.Log()
        Using ctx As New DataLibrary.ModelEntities
            Dim objs = (From x In ctx.Logs
                        Where x.RequestURIBase.Contains(RequestURiBase)
                        Take 10
                        Select x).ToArray
            Return objs
        End Using
    End Function

    ' GET api/<controller>/5
    <HttpGet>
    Public Function GetValue(Optional ByVal id As Integer = 0) As DataLibrary.Log
        Using ctx As New DataLibrary.ModelEntities
            Dim objs = (From x In ctx.Logs
                        Where x.LogID = id
                        Select x).FirstOrDefault
            Return objs
        End Using
    End Function

    '' POST api/<controller>
    'Public Sub PostValue(<FromBody()> ByVal value As String)

    'End Sub

    '' PUT api/<controller>/5
    'Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    'End Sub

    '' DELETE api/<controller>/5
    'Public Sub DeleteValue(ByVal id As Integer)

    'End Sub
End Class
