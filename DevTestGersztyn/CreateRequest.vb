Imports System.IO
Imports System.Net
Imports System.Xml

''' <summary>
''' The purpose of this class is to generate a SOAP request and pass it to the desired web service.
''' Provides formatting and parsing for the XML.
''' The resulting data from the request will be stored onto the machine's local database.
''' </summary>
Public Class CreateRequest

    'Holds the generated tracking numbers from the post request
    Private trackingNumbers As List(Of String)
    'Generates a data model to access the database
    Private handler As DataHandler
    'Holds the POST data for the web request
    Private SoapStr As String

    ''' <summary>
    ''' Manages the SOAP request and data processed within it.
    ''' </summary>
    Sub New()
        trackingNumbers = New List(Of String)
        handler = New DataHandler()
        SoapStr = ""
    End Sub

    ''' <summary>
    ''' Generate the request stream for this given request.
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
    ''' Conducts a request to the webpage in order to receive the data from a HttpPOST.
    ''' All set-up is conducted, resulting in an XML-based response.
    ''' </summary>
    Public Sub Execute()
        'This will generate the XML formmated request to be sent
        GenerateRequestString()
        'Attempt to make the connection to the web server and extract the XML data
        Try
            'Generate a web response object to handle the request to the server
            Dim request As HttpWebRequest = CreateWebRequest()
            'Create an XML formmated document to hold the response
            Dim soapEnvelopeXml As New XmlDocument()
            soapEnvelopeXml.LoadXml(SoapStr)
            Using newStream As Stream = request.GetRequestStream()
                soapEnvelopeXml.Save(newStream)
                Using response As WebResponse = request.GetResponse()
                    Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                        'Handles the resulting XML and parses it to be added into the database
                        ParseXML(reader.ReadToEnd())
                    End Using
                End Using
            End Using
        Catch Ex As Exception
            MsgBox("Unable to connect with the web server. Cannot fulfill request at this time.")
        End Try
    End Sub

    ''' <summary>
    ''' Used to reate a SOAP webrequest to the given URL.
    ''' </summary>
    ''' <returns> A web request with the necessary header. </returns>
    Private Function CreateWebRequest() As HttpWebRequest
        Dim request As HttpWebRequest _
            = WebRequest.Create("http://www.vesigo.com/Projects/OnTime/CustomerWebPortal/ws/orders_internal.asmx?op=GetOrdersByStatus")
        request.Host = "www.vesigo.com"
        request.ContentType = "application/soap+xml; charset=utf-8"
        request.Accept = "text/xml"
        request.Method = "POST"
        Return request
    End Function

    ''' <summary>
    ''' Extract the data we need to from the XML request.
    ''' The data is then stored into a list.
    ''' </summary>
    ''' <param name="resultStr"> The resulting XML request that is going to be parsed. </param>
    Private Sub ParseXML(resultStr As String)
        Dim xmlDoc As XmlDocument = New XmlDocument()
        Dim data As DataSet = New DataSet()
        xmlDoc.LoadXml(resultStr)
        Using reader As StringReader = New StringReader(xmlDoc.InnerXml)
            data.ReadXml(reader)
            For Each t As DataTable In data.Tables
                For Each r As DataRow In t.Rows
                    For Each c As DataColumn In t.Columns
                        If (t.TableName = "string" And r(c.ColumnName) <> "0") Then
                            'Add item to the list
                            trackingNumbers.Add(r(c.ColumnName))
                        End If
                    Next
                Next
            Next
        End Using
        'Adds each tracking number from the request to the local database
        handler.LoadData(trackingNumbers)
    End Sub
End Class