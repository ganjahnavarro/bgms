<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PostSO
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PostSO))
        Me.docDate = New System.Windows.Forms.DateTimePicker()
        Me.itemsGrid = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblNoRecords = New System.Windows.Forms.Label()
        Me.btnMark = New System.Windows.Forms.Button()
        Me.btnUnmark = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.PictureBox()
        Me.lblPostedOn = New System.Windows.Forms.Label()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btn3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'docDate
        '
        Me.docDate.CustomFormat = ""
        Me.docDate.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.docDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.docDate.Location = New System.Drawing.Point(627, 57)
        Me.docDate.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.docDate.MinDate = New Date(1990, 1, 1, 0, 0, 0, 0)
        Me.docDate.Name = "docDate"
        Me.docDate.Size = New System.Drawing.Size(160, 32)
        Me.docDate.TabIndex = 84
        '
        'itemsGrid
        '
        Me.itemsGrid.AllowUserToAddRows = False
        Me.itemsGrid.AllowUserToDeleteRows = False
        Me.itemsGrid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.NullValue = Nothing
        Me.itemsGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.itemsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.itemsGrid.BackgroundColor = System.Drawing.Color.White
        Me.itemsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.itemsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.itemsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.itemsGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.itemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(167, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.itemsGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.itemsGrid.EnableHeadersVisualStyles = False
        Me.itemsGrid.GridColor = System.Drawing.Color.Gainsboro
        Me.itemsGrid.Location = New System.Drawing.Point(4, 4)
        Me.itemsGrid.Name = "itemsGrid"
        Me.itemsGrid.ReadOnly = True
        Me.itemsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSeaGreen
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MediumAquamarine
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
        Me.itemsGrid.Size = New System.Drawing.Size(718, 356)
        Me.itemsGrid.TabIndex = 49
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblNoRecords)
        Me.Panel1.Controls.Add(Me.itemsGrid)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel1.Location = New System.Drawing.Point(45, 106)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel1.Size = New System.Drawing.Size(742, 364)
        Me.Panel1.TabIndex = 89
        '
        'lblNoRecords
        '
        Me.lblNoRecords.AutoSize = True
        Me.lblNoRecords.BackColor = System.Drawing.SystemColors.Window
        Me.lblNoRecords.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.lblNoRecords.ForeColor = System.Drawing.Color.Black
        Me.lblNoRecords.Location = New System.Drawing.Point(289, 163)
        Me.lblNoRecords.Name = "lblNoRecords"
        Me.lblNoRecords.Size = New System.Drawing.Size(182, 22)
        Me.lblNoRecords.TabIndex = 60
        Me.lblNoRecords.Text = "No records to display."
        Me.lblNoRecords.Visible = False
        '
        'btnMark
        '
        Me.btnMark.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMark.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack
        Me.btnMark.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnMark.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMark.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.btnMark.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnMark.Location = New System.Drawing.Point(46, 492)
        Me.btnMark.Name = "btnMark"
        Me.btnMark.Size = New System.Drawing.Size(118, 45)
        Me.btnMark.TabIndex = 88
        Me.btnMark.Text = "Mark All"
        Me.btnMark.UseVisualStyleBackColor = False
        '
        'btnUnmark
        '
        Me.btnUnmark.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnUnmark.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack
        Me.btnUnmark.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnUnmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnmark.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.btnUnmark.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnUnmark.Location = New System.Drawing.Point(170, 492)
        Me.btnUnmark.Name = "btnUnmark"
        Me.btnUnmark.Size = New System.Drawing.Size(118, 45)
        Me.btnUnmark.TabIndex = 87
        Me.btnUnmark.Text = "Unmark All"
        Me.btnUnmark.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn3.Image = Global.BGMS.My.Resources.Resources.post
        Me.btn3.Location = New System.Drawing.Point(742, 482)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(45, 45)
        Me.btn3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btn3.TabIndex = 86
        Me.btn3.TabStop = False
        '
        'lblPostedOn
        '
        Me.lblPostedOn.AutoSize = True
        Me.lblPostedOn.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblPostedOn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPostedOn.Location = New System.Drawing.Point(524, 62)
        Me.lblPostedOn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPostedOn.Name = "lblPostedOn"
        Me.lblPostedOn.Size = New System.Drawing.Size(92, 23)
        Me.lblPostedOn.TabIndex = 85
        Me.lblPostedOn.Text = "Posted on"
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(140, 5)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(558, 38)
        Me.notificationPanel.TabIndex = 122
        Me.notificationPanel.Visible = False
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(528, 11)
        Me.notificationClose.Name = "notificationClose"
        Me.notificationClose.Size = New System.Drawing.Size(16, 16)
        Me.notificationClose.TabIndex = 18
        Me.notificationClose.Text = "X"
        '
        'notificationLabel
        '
        Me.notificationLabel.BackColor = System.Drawing.Color.Transparent
        Me.notificationLabel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.notificationLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.notificationLabel.Location = New System.Drawing.Point(7, 4)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(526, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PostSO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(834, 562)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.docDate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnMark)
        Me.Controls.Add(Me.btnUnmark)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.lblPostedOn)
        Me.Font = New System.Drawing.Font("Lucida Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PostSO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sales Order"
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btn3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents docDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents itemsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblNoRecords As System.Windows.Forms.Label
    Friend WithEvents btnMark As System.Windows.Forms.Button
    Friend WithEvents btnUnmark As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPostedOn As System.Windows.Forms.Label
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
End Class
