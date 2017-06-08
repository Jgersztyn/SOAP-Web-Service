<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTrackingDisplay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DisplayButton = New System.Windows.Forms.Button()
        Me.TrackingDataGridView = New System.Windows.Forms.DataGridView()
        Me.ResultDatabaseDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ResultDatabaseDataSet = New DevTestGersztyn.ResultDatabaseDataSet()
        Me.TrackingIdColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TrackingNumColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.TrackingDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResultDatabaseDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResultDatabaseDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DisplayButton
        '
        Me.DisplayButton.Location = New System.Drawing.Point(12, 12)
        Me.DisplayButton.Name = "DisplayButton"
        Me.DisplayButton.Size = New System.Drawing.Size(75, 23)
        Me.DisplayButton.TabIndex = 0
        Me.DisplayButton.Text = "Display Data"
        Me.DisplayButton.UseVisualStyleBackColor = True
        '
        'TrackingDataGridView
        '
        Me.TrackingDataGridView.AllowUserToAddRows = False
        Me.TrackingDataGridView.AllowUserToDeleteRows = False
        Me.TrackingDataGridView.AutoGenerateColumns = False
        Me.TrackingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TrackingDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TrackingIdColumn, Me.TrackingNumColumn})
        Me.TrackingDataGridView.DataSource = Me.ResultDatabaseDataSetBindingSource
        Me.TrackingDataGridView.Location = New System.Drawing.Point(12, 54)
        Me.TrackingDataGridView.Name = "TrackingDataGridView"
        Me.TrackingDataGridView.ReadOnly = True
        Me.TrackingDataGridView.Size = New System.Drawing.Size(240, 150)
        Me.TrackingDataGridView.TabIndex = 1
        '
        'ResultDatabaseDataSetBindingSource
        '
        Me.ResultDatabaseDataSetBindingSource.DataSource = Me.ResultDatabaseDataSet
        Me.ResultDatabaseDataSetBindingSource.Position = 0
        '
        'ResultDatabaseDataSet
        '
        Me.ResultDatabaseDataSet.DataSetName = "ResultDatabaseDataSet"
        Me.ResultDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TrackingIdColumn
        '
        Me.TrackingIdColumn.HeaderText = "ID"
        Me.TrackingIdColumn.Name = "TrackingIdColumn"
        Me.TrackingIdColumn.ReadOnly = True
        '
        'TrackingNumColumn
        '
        Me.TrackingNumColumn.HeaderText = "Tracking Number"
        Me.TrackingNumColumn.Name = "TrackingNumColumn"
        Me.TrackingNumColumn.ReadOnly = True
        '
        'FormTrackingDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.TrackingDataGridView)
        Me.Controls.Add(Me.DisplayButton)
        Me.Name = "FormTrackingDisplay"
        Me.Text = "Tracking Information"
        CType(Me.TrackingDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResultDatabaseDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResultDatabaseDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DisplayButton As Button
    Friend WithEvents TrackingDataGridView As DataGridView
    Friend WithEvents ResultDatabaseDataSetBindingSource As BindingSource
    Friend WithEvents ResultDatabaseDataSet As ResultDatabaseDataSet
    Friend WithEvents TrackingIdColumn As DataGridViewTextBoxColumn
    Friend WithEvents TrackingNumColumn As DataGridViewTextBoxColumn
End Class
