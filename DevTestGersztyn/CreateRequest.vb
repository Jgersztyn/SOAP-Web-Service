Imports System.IO
Imports System.Net
Imports System.Xml
'Imports System.Xml.Serialization

Public Class CreateRequest

    'holds the POST data for the web request
    Private SoapStr As String
    'holds the generated tracking numbers from the post request
    Private trackingNumbers As List(Of String) = New List(Of String)

    ''' <summary>
    ''' Used to access lists containing all tracking numbers
    ''' </summary>
    ''' <returns> A list containing all tracking numbers </returns>
    Public Function GetTrackingNumbers() As List(Of String)
        Return trackingNumbers
    End Function

    ''' <summary>
    ''' Generate the request stream for this given request
    ''' </summary>
    Private Sub GenerateRequestString()

        SoapStr = "<?xml version=""1.0"" encoding=""utf-8""?>"
        SoapStr = SoapStr & "<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">"
        SoapStr = SoapStr & "<soap12:Body>"
        SoapStr = SoapStr & "<GetOrdersByStatus xmlns = ""http://www.ontimesystem.com/sites/"" >"
        SoapStr = SoapStr & "<SecurityKey>29475d4e-34d8-4883-b6f0-f6836ddd60b4</SecurityKey>"
        SoapStr = SoapStr & "<Status>2</Status>"
        SoapStr = SoapStr & "<StartDate>2013-08-30T09:00:00</StartDate>"
        SoapStr = SoapStr & "<EndDate>2017-05-20T11:00:00</EndDate>"
        SoapStr = SoapStr & "</GetOrdersByStatus>"
        SoapStr = SoapStr & "</soap12:Body>"
        SoapStr = SoapStr & "</soap12:Envelope>"

    End Sub

    ''' <summary>
    ''' Conducts a request to the webpage in order to receive the data from a HttpPOST
    ''' All set-up is conducted, resulting in an XML-based response
    ''' </summary>
    Public Sub Execute()

        'This will generate the XML formmated request to be sent
        GenerateRequestString()

        Dim request As HttpWebRequest = CreateWebRequest()
        Dim soapEnvelopeXml As New XmlDocument()
        soapEnvelopeXml.LoadXml(SoapStr)
        Dim newStream As Stream = request.GetRequestStream()
        soapEnvelopeXml.Save(newStream)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim soapResult As String = reader.ReadToEnd()

        'See what we get...
        'For testing only
        'Console.WriteLine(soapResult)

        'Handles the result and parses the XML to make it useful
        ParseXML(soapResult)

        'Close the necessary connections before exiting
        response.Close()
        reader.Close()

    End Sub


    ''' <summary>
    ''' Used to reate a SOAP webrequest to the given URL
    ''' </summary>
    ''' <returns> A web request with the necessary header </returns>
    Private Shared Function CreateWebRequest() As HttpWebRequest

        Dim request As HttpWebRequest
        request = WebRequest.Create("http://www.vesigo.com/Projects/OnTime/CustomerWebPortal/ws/orders_internal.asmx?op=GetOrdersByStatus")
        request.Host = "www.vesigo.com"
        request.ContentType = "application/soap+xml; charset=utf-8"
        request.Accept = "text/xml"
        request.Method = "POST"
        Return request

    End Function

    ''' <summary>
    ''' Extract the data we need to from the XML request
    ''' The data is then stored into a list
    ''' </summary>
    ''' <param name="resultStr"> The resulting XML request that is going to be parsed </param>
    Private Sub ParseXML(resultStr As String)

        Dim xmlDoc As XmlDocument = New XmlDocument
        xmlDoc.LoadXml(resultStr)

        Dim data As DataSet
        Dim reader As StringReader
        data = New DataSet()

        reader = New StringReader(xmlDoc.InnerXml)
        data.ReadXml(reader)

        For Each t As DataTable In data.Tables
            For Each r As DataRow In t.Rows
                For Each c As DataColumn In t.Columns
                    If (t.TableName = "string" And r(c.ColumnName) <> "0") Then
                        'This is for testing
                        'Console.WriteLine(r(c.ColumnName))

                        'Add item to the list
                        trackingNumbers.Add(r(c.ColumnName))
                    End If
                Next
            Next
        Next
    End Sub

End Class