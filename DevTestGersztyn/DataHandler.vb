Imports System.Data.SqlClient

''' <summary>
''' A simple class used to handle connections to the local database.
''' </summary>
Public Class DataHandler

    'Sets the connection to the local database by retrieving it from App.config
    Private connectionStr As New _
        SqlConnection(Configuration.ConfigurationManager.ConnectionStrings("ResultDatabaseConnectionString").ConnectionString)

    ''' <summary>
    ''' Populates the database with the data gathered from the SOAP request.
    ''' </summary>
    Public Sub LoadData(ByVal trackingNumbers As List(Of String))
        'Retrieve all tracking numbers from the SOAP result
        Dim dataResult As List(Of String) = trackingNumbers
        'Connect to the database only if data exists
        If (dataResult.Count > 0) Then
            'Note the connection string is retrieved from App.config
            Using connection As New SqlConnection(connectionStr.ConnectionString)
                'Open the database connection
                connection.Open()
                'Loop through all elements of the array
                For index = 0 To dataResult.Count - 1
                    'Format the query for the insert, using SQL parameters to avoid possible injection
                    Dim query As String =
                        "INSERT INTO TrackingNumbers(TrackingId, TrackingNumber) VALUES(@ID, @TRACKING)"
                    'Create a simple SQL command to insert the data into the local database
                    Using cmd As New SqlCommand(query, connection)
                        'Supply the value of each variable within the query
                        'Add 1 to the index, since the list index is always 1 less than the database id
                        cmd.Parameters.AddWithValue("@ID", index + 1)
                        cmd.Parameters.AddWithValue("@TRACKING", dataResult.Item(index))
                        'Insert the next tracking number into the database
                        cmd.ExecuteNonQuery()
                    End Using
                Next
                'Clean up and close database connection
                connection.Close()
            End Using
        Else
            'Display this if there was no data to be added to the database
            MessageBox.Show("No data was processed during your request. Database is empty.")
            Exit Sub
        End If
        MessageBox.Show("Data has been successfully added to database.")
    End Sub
End Class