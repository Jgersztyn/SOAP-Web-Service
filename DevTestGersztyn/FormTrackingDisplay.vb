Imports System.Data.DataSet
Imports System.Data.SqlClient
Imports DevTestGersztyn.CreateRequest

Public Class FormTrackingDisplay

    'Sets the connection for the local database
    Private connectionString As String =
        "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ResultDatabase.mdf;Integrated Security=True"

    'Initialize this object so a request can be generated
    Dim soapRequest As CreateRequest

    'Helper function used to create and validate the connection
    'May not be necessary, used for testing
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

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'make TEST call to insert data before displaying the form
        'Remove this once the real data comes in
        'InsertTempData()

        soapRequest = New CreateRequest()
        'soapRequest.GenerateSoapRequest()
        soapRequest.Execute()

    End Sub

    Private Sub TrackingDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TrackingDataGridView.CellContentClick

    End Sub

    'Inserts test data into the local DB
    Private Sub InsertTempData()
        Dim sqlConnection1 As New _
            SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ResultDatabase.mdf;Integrated Security=True")

        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "INSERT TrackingNumbers (TrackingId, TrackingNumber) VALUES (1, '23496yjhg76'), (2, '63gnnfg3646'), (3, '19mw576myio1')"
        cmd.Connection = sqlConnection1
        sqlConnection1.Open()
        cmd.ExecuteNonQuery()
        sqlConnection1.Close()
    End Sub

    Private Sub DisplayButton_Click(sender As Object, e As EventArgs) Handles DisplayButton.Click

        'Call to connect with the database
        'ConnectionAttempt()

        Dim con As New SqlConnection(connectionString)
        Dim com As String = "SELECT TrackingId, TrackingNumber FROM TrackingNumbers"
        Dim dataAdapter As New SqlDataAdapter(com, connectionString)
        Dim commandBuilder = New SqlCommandBuilder(dataAdapter)
        Dim ds As New DataSet()
        dataAdapter.Fill(ds, "TrackingNumbers") ' Try <--- , "TrackingNumbers"

        TrackingDataGridView.ReadOnly = True 'Ensure displayed tracking data cannot be modified
        TrackingDataGridView.DataSource = ds.Tables(0)

    End Sub
End Class

