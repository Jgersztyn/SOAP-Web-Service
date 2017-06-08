Imports System.IO
Imports System.Net
Imports System.Xml

Public Class CreateRequest

    'Some variables to determine if we need...
    'Dim Request As WebRequest
    'Dim Response As WebResponse
    'Dim DataStream As Stream
    'Dim Reader As StreamReader
    'Dim SoapByte() As Byte
    'Dim pSuccess As Boolean = True


    'holds the POST data for the web request
    Dim SoapStr As String


    'This holds the final result from the web request
    Dim Results() As String

    ''' <summary>
    ''' Generate the request stream for this given request
    ''' </summary>
    Sub GenerateRequestString()

        SoapStr = "<?xml version=""1.0"" encoding=""utf-8""?>"
        SoapStr = SoapStr & "<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">"
        SoapStr = SoapStr & "<soap12:Body>"

        'SoapStr = SoapStr & "<ns:getAuth> <delisId>id</delisId> <password>pass</password> <messageLanguage>de_DE</messageLanguage> </ns:getAuth>"
        SoapStr = SoapStr & "<GetOrdersByStatus xmlns = ""http://www.ontimesystem.com/sites/"" >"

        SoapStr = SoapStr & "<SecurityKey>29475d4e-34d8-4883-b6f0-f6836ddd60b4</SecurityKey>"
        SoapStr = SoapStr & "<Status>2</Status>"
        SoapStr = SoapStr & "<StartDate>2016-08-30T09:00:00</StartDate>"
        SoapStr = SoapStr & "<EndDate>2016-09-20T11:00:00</EndDate>"
        SoapStr = SoapStr & "</GetOrdersByStatus>"
        SoapStr = SoapStr & "</soap12:Body>"
        SoapStr = SoapStr & "</soap12:Envelope>"

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub Execute()

        GenerateRequestString()

        Dim request As HttpWebRequest = CreateWebRequest() 'is this right?
        Dim soapEnvelopeXml As New XmlDocument()
        soapEnvelopeXml.LoadXml(SoapStr)

        Dim newStream As Stream = request.GetRequestStream()

        soapEnvelopeXml.Save(newStream)


        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim soapResult As String = reader.ReadToEnd()

        'See what we get...
        Console.WriteLine(soapResult)

        'Need to write to the database somewhere also

        'Clean up
        response.Close()
        reader.Close()

    End Sub


    ''' <summary>
    ''' Create a soap webrequest to the given URL
    ''' </summary>
    Public Shared Function CreateWebRequest() As HttpWebRequest

        Dim request As HttpWebRequest
        request = WebRequest.Create("http://www.vesigo.com/Projects/OnTime/CustomerWebPortal/ws/orders_internal.asmx?op=GetOrdersByStatus")

        request.ContentType = "application/soap+xml; charset=utf-8"
        request.Accept = "text/xml"
        request.Method = "GET"
        Return request

    End Function

End Class
