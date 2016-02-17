<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtcSearch
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EtcSearch))
        Me.dateFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblPostedOn = New System.Windows.Forms.Label()
        Me.dateTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbDoc = New System.Windows.Forms.TextBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.itemsGrid = New System.Windows.Forms.DataGridView()
        Me.delayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dateFrom
        '
        Me.dateFrom.CustomFormat = ""
        Me.dateFrom.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateFrom.Location = New System.Drawing.Point(603, 510)
        Me.dateFrom.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.dateFrom.MinDate = New Date(1990, 1, 1, 0, 0, 0, 0)
        Me.dateFrom.Name = "dateFrom"
        Me.dateFrom.Size = New System.Drawing.Size(165, 32)
        Me.dateFrom.TabIndex = 191
        '
        'lblPostedOn
        '
        Me.lblPostedOn.AutoSize = True
        Me.lblPostedOn.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblPostedOn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPostedOn.Location = New System.Drawing.Point(541, 517)
        Me.lblPostedOn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPostedOn.Name = "lblPostedOn"
        Me.lblPostedOn.Size = New System.Drawing.Size(53, 23)
        Me.lblPostedOn.TabIndex = 185
        Me.lblPostedOn.Text = "From"
        '
        'dateTo
        '
        Me.dateTo.CustomFormat = ""
        Me.dateTo.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTo.Location = New System.Drawing.Point(603, 547)
        Me.dateTo.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.dateTo.MinDate = New Date(1990, 1, 1, 0, 0, 0, 0)
        Me.dateTo.Name = "dateTo"
        Me.dateTo.Size = New System.Drawing.Size(165, 32)
        Me.dateTo.TabIndex = 192
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(557, 554)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 23)
        Me.Label1.TabIndex = 187
        Me.Label1.Text = "To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(29, 514)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 23)
        Me.Label2.TabIndex = 188
        Me.Label2.Text = "Doc No."
        '
        'tbDoc
        '
        Me.tbDoc.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDoc.Location = New System.Drawing.Point(113, 510)
        Me.tbDoc.Name = "tbDoc"
        Me.tbDoc.Size = New System.Drawing.Size(409, 32)
        Me.tbDoc.TabIndex = 189
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilter.Location = New System.Drawing.Point(29, 551)
        Me.lblFilter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(70, 23)
        Me.lblFilter.TabIndex = 190
        Me.lblFilter.Text = "Lookup"
        '
        'tbFilter
        '
        Me.tbFilter.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbFilter.Location = New System.Drawing.Point(113, 547)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(409, 32)
        Me.tbFilter.TabIndex = 190
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Panel1.Controls.Add(Me.itemsGrid)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel1.Location = New System.Drawing.Point(12, 13)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel1.Size = New System.Drawing.Size(790, 486)
        Me.Panel1.TabIndex = 183
        '
        'itemsGrid
        '
        Me.itemsGrid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.NullValue = Nothing
        Me.itemsGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.itemsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.itemsGrid.BackgroundColor = System.Drawing.SystemColors.Window
        Me.itemsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.itemsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.itemsGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.itemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.itemsGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.itemsGrid.EnableHeadersVisualStyles = False
        Me.itemsGrid.GridColor = System.Drawing.Color.Gainsboro
        Me.itemsGrid.Location = New System.Drawing.Point(4, 4)
        Me.itemsGrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.itemsGrid.MultiSelect = False
        Me.itemsGrid.Name = "itemsGrid"
        Me.itemsGrid.ReadOnly = True
        Me.itemsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.itemsGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.itemsGrid.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.itemsGrid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.itemsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.itemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.itemsGrid.Size = New System.Drawing.Size(773, 477)
        Me.itemsGrid.StandardTab = True
        Me.itemsGrid.TabIndex = 193
        '
        'delayTimer
        '
        '
        'EtcSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(814, 592)
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.tbDoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dateTo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateFrom)
        Me.Controls.Add(Me.lblPostedOn)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EtcSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Search"
        Me.Panel1.ResumeLayout(False)
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblPostedOn As System.Windows.Forms.Label
    Friend WithEvents dateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbDoc As System.Windows.Forms.TextBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents tbFilter As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents itemsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents delayTimer As System.Windows.Forms.Timer
End Class
