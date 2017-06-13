Imports System.Data.SqlClient

''' <summary>
''' A basic application used to generate a SOAP web request.
''' The application generates the request in the background and informs the user that data has been loaded.
''' The user can request the data to be populated inside of the form by clicking the provided button.
''' </summary>
Public Class FormTrackingDisplay

    'Sets the connection for the local database
    Private connectionStr As New _
        SqlConnection(Configuration.ConfigurationManager.ConnectionStrings("ResultDatabaseConnectionString").ConnectionString)
    'This object is used to generate a SOAP request
    Private soapRequest As CreateRequest = New CreateRequest()

    ''' <summary>
    ''' Handles the initial loading of the form.
    ''' The main entry point of the application.
    ''' Note that the form is generated only after the SOAP reqeust is completed.
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        soapRequest.Execute()
    End Sub

    ''' <summary>
    ''' Generates the Data Grid to display tracking numbers.
    ''' </summary>
    Private Sub TrackingDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TrackingDataGridView.CellContentClick

    End Sub

    ''' <summary>
    ''' Handles the Display Data Button on the form.
    ''' Once the button is clicked the data grid will be notified.
    ''' Data from the database will then be populated.
    ''' </summary>
    Private Sub DisplayButton_Click(sender As Object, e As EventArgs) Handles DisplayButton.Click
        Dim query As String = "SELECT TrackingNumber FROM TrackingNumbers"
        Dim table As New DataTable
        'Note the connection string is retrieved from App.config
        Using connection As New SqlConnection(connectionStr.ConnectionString)
            'Set up the SQL command to query the database and add the data to the grid
            Using dataAdapter As New SqlDataAdapter(query, connectionStr)
                Using command As New SqlCommand(query, connection)
                    'Populate the data grid with the values from the query
                    dataAdapter.Fill(table)
                    TrackingDataGridView.DataSource = table
                End Using
            End Using
        End Using
    End Sub
End Class