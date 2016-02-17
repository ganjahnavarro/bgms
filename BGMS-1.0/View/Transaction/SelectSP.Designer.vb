<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectSP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectSP))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblNoRecords = New System.Windows.Forms.Label()
        Me.itemsGrid = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbTotalCheck = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbTotalPaid = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.btnCheck = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Panel1.Controls.Add(Me.lblNoRecords)
        Me.Panel1.Controls.Add(Me.itemsGrid)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel1.Location = New System.Drawing.Point(17, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel1.Size = New System.Drawing.Size(685, 376)
        Me.Panel1.TabIndex = 97
        '
        'lblNoRecords
        '
        Me.lblNoRecords.AutoSize = True
        Me.lblNoRecords.BackColor = System.Drawing.SystemColors.Window
        Me.lblNoRecords.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblNoRecords.ForeColor = System.Drawing.Color.Black
        Me.lblNoRecords.Location = New System.Drawing.Point(266, 170)
        Me.lblNoRecords.Name = "lblNoRecords"
        Me.lblNoRecords.Size = New System.Drawing.Size(164, 19)
        Me.lblNoRecords.TabIndex = 175
        Me.lblNoRecords.Text = "No records to display."
        Me.lblNoRecords.Visible = False
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
        Me.itemsGrid.Location = New System.Drawing.Point(2, 2)
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
        Me.itemsGrid.Size = New System.Drawing.Size(667, 370)
        Me.itemsGrid.TabIndex = 51
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label4.Location = New System.Drawing.Point(13, 409)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 23)
        Me.Label4.TabIndex = 169
        Me.Label4.Text = "Total Check"
        '
        'tbTotalCheck
        '
        Me.tbTotalCheck.Enabled = False
        Me.tbTotalCheck.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbTotalCheck.Location = New System.Drawing.Point(127, 405)
        Me.tbTotalCheck.Name = "tbTotalCheck"
        Me.tbTotalCheck.Size = New System.Drawing.Size(301, 32)
        Me.tbTotalCheck.TabIndex = 170
        Me.tbTotalCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(13, 447)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 23)
        Me.Label1.TabIndex = 171
        Me.Label1.Text = "Total Paid"
        '
        'tbTotalPaid
        '
        Me.tbTotalPaid.Enabled = False
        Me.tbTotalPaid.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbTotalPaid.Location = New System.Drawing.Point(127, 443)
        Me.tbTotalPaid.Name = "tbTotalPaid"
        Me.tbTotalPaid.Size = New System.Drawing.Size(301, 32)
        Me.tbTotalPaid.TabIndex = 172
        Me.tbTotalPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCancel
        '
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Image = Global.BGMS.My.Resources.Resources.delete
        Me.btnCancel.Location = New System.Drawing.Point(657, 430)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(45, 45)
        Me.btnCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCancel.TabIndex = 173
        Me.btnCancel.TabStop = False
        '
        'btnCheck
        '
        Me.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheck.Image = Global.BGMS.My.Resources.Resources.checkmark
        Me.btnCheck.Location = New System.Drawing.Point(608, 430)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(45, 45)
        Me.btnCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCheck.TabIndex = 174
        Me.btnCheck.TabStop = False
        '
        'SelectSP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(714, 492)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbTotalPaid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbTotalCheck)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectSP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Purchase Orders"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents itemsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbTotalCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbTotalPaid As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheck As System.Windows.Forms.PictureBox
    Friend WithEvents lblNoRecords As System.Windows.Forms.Label
End Class
