Imports System.Data.DataSet
Imports System.Data.SqlClient
Imports DevTestGersztyn.CreateRequest

Public Class FormTrackingDisplay

    'Sets the connection for the local database
    Private connectionString As String =
        "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ResultDatabase.mdf;Integrated Security=True"
    'This object is used to generate a SOAP request
    Private soapRequest As CreateRequest

    ''' <summary>
    ''' Helper function used to create and validate the connection
    ''' May not be necessary but used for testing
    ''' </summary>
    Private Sub ConnectionAttempt()
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'close the connection after data has been processed
            connection.Close()
        End Using
    End Sub

    ''' <summary>
    ''' Handles the initial loading of the form
    ''' The main entry point of the application
    ''' Note that the form is generated only after the SOAP reqeust is completed
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'make TEST call to insert test data before displaying the form
        'Remove this once the real data comes in
        'InsertData()

        soapRequest = New CreateRequest()
        'soapRequest.GenerateSoapRequest()
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
                'testing to see values in DB
                'Debug.Write(element)

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
        Dim sqlConnection1 As New _
            SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ResultDatabase.mdf;Integrated Security=True")

        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text

        'THIS LINE INSERTS THE TEMP DATA
        'cmd.CommandText = "INSERT TrackingNumbers (TrackingId, TrackingNumber) VALUES (1, '23496yjhg76'), (2, '63gnnfg3646'), (3, '19mw576myio1')"

        'THIS LINE INSERTS THE PERMANENT DATA
        cmd.CommandText = "INSERT TrackingNumbers (TrackingId, TrackingNumber) VALUES (" _
            & id & ", '" & trackingNum & "')"

        cmd.Connection = sqlConnection1
        sqlConnection1.Open()
        cmd.ExecuteNonQuery()
        sqlConnection1.Close()
    End Sub

    ''' <summary>
    ''' Handles the Display Data Button on the form
    ''' </summary>
    Private Sub DisplayButton_Click(sender As Object, e As EventArgs) Handles DisplayButton.Click

        'Call to connect with the database
        ConnectionAttempt()

        'Dim con As New SqlConnection(connectionString)
        'Dim com As String = "SELECT TrackingId, TrackingNumber FROM TrackingNumbers"
        'Dim dataAdapter As New SqlDataAdapter(com, connectionString)
        'Dim commandBuilder = New SqlCommandBuilder(dataAdapter)
        'Dim ds As New DataSet()
        'dataAdapter.Fill(ds, "TrackingNumbers") ' Try <--- , "TrackingNumbers"
        'TrackingDataGridView.ReadOnly = True 'Ensure displayed tracking data cannot be modified
        'TrackingDataGridView.DataSource = ds.Tables(0)

        Dim query As String = "SELECT TrackingNumber FROM TrackingNumbers"
        Dim connect As New SqlConnection(connectionString)
        Dim command As New SqlCommand(query, connect)
        Dim dataAdapter As New SqlDataAdapter(query, connectionString)
        Dim table As New DataTable
        dataAdapter.Fill(table)
        TrackingDataGridView.AutoGenerateColumns = True
        TrackingDataGridView.DataSource = table

    End Sub
End Class

