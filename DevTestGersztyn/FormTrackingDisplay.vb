Imports System.Data.SqlClient

''' <summary>
''' A basic application used to generate a SOAP web request
''' The application generates the request in the background and informs the user that data has been loaded
''' The user can request the data to be populated inside of the form by clicking the provided button
''' </summary>
Public Class FormTrackingDisplay

    'Hard codes the connection for the local database
    'Private connectionString As String =
    '    "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ResultDatabase.mdf;Integrated Security=True"

    'Sets the connection to the local database by pulling it from App.config
    Private connectionStr As New _
        SqlConnection(Configuration.ConfigurationManager.ConnectionStrings("ResultDatabaseConnectionString").ConnectionString)
    'This object is used to generate a SOAP request
    Private soapRequest As CreateRequest

    '''' <summary>
    '''' Helper function used to create and validate the connection
    '''' May not be necessary but used for testing
    '''' </summary>
    'Private Sub ConnectionAttempt()
    '    Using connection As New SqlConnection(connectionStr.ConnectionString)
    '        Try
    '            connection.Open()
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        'close the connection after data has been processed
    '        connection.Close()
    '    End Using
    'End Sub

    ''' <summary>
    ''' Handles the initial loading of the form
    ''' The main entry point of the application
    ''' Note that the form is generated only after the SOAP reqeust is completed
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        soapRequest = New CreateRequest()
        soapRequest.Execute()
        LoadFormData()
    End Sub

    ''' <summary>
    ''' Populates the database with the data based on the SOAP request
    ''' </summary>
    Private Sub LoadFormData()

        'Retrieve all tracking numbers from the SOAP result
        Dim dataStrings = soapRequest.GetTrackingNumbers()

        If (dataStrings.Count > 0) Then
            Dim counter As Integer = 0
            For Each element As String In dataStrings
                counter += 1
                InsertData(counter, element)
            Next
        Else
            'There is no data to add to database in this case
            MessageBox.Show("Data was not succfully added to the database")
            Exit Sub
        End If

        MessageBox.Show("Data has been successfully added to database.")
    End Sub

    ''' <summary>
    ''' Generates the Data Grid to display tracking numbers
    ''' </summary>
    Private Sub TrackingDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TrackingDataGridView.CellContentClick

    End Sub

    ''' <summary>
    ''' Inserts test data into the local DB
    ''' </summary>
    Private Sub InsertData(id As Integer, trackingNum As String)

        Dim sqlConnection As New SqlConnection(connectionStr.ConnectionString)
        Dim cmd As New SqlCommand

        cmd.CommandType = CommandType.Text
        'Create a simple SQL command to insert the data into the local database
        cmd.CommandText = "INSERT TrackingNumbers (TrackingId, TrackingNumber) VALUES (" _
            & id & ", '" & trackingNum & "')"

        cmd.Connection = sqlConnection

        Try
            sqlConnection.Open()
            cmd.ExecuteNonQuery()
            sqlConnection.Close()
        Catch ex As Exception
            MsgBox("Failed to connect to the database.")
        End Try
    End Sub

    ''' <summary>
    ''' Handles the Display Data Button on the form
    ''' </summary>
    Private Sub DisplayButton_Click(sender As Object, e As EventArgs) Handles DisplayButton.Click

        Dim query As String = "SELECT TrackingNumber FROM TrackingNumbers"
        Dim connect As New SqlConnection(connectionStr.ConnectionString)
        Dim command As New SqlCommand(query, connect)
        Dim dataAdapter As New SqlDataAdapter(query, connectionStr)
        Dim table As New DataTable
        dataAdapter.Fill(table)
        TrackingDataGridView.DataSource = table
    End Sub
End Class

