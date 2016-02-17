<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPurchaseMn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptPurchaseMn))
        Me.btnPrint = New System.Windows.Forms.PictureBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dateTo = New System.Windows.Forms.DateTimePicker()
        Me.dateFrom = New System.Windows.Forms.DateTimePicker()
        Me.tabSupplier = New System.Windows.Forms.Button()
        Me.tabStock = New System.Windows.Forms.Button()
        Me.tbSupplier = New System.Windows.Forms.TextBox()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.tbStock = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.Image = Global.BGMS.My.Resources.Resources.print1
        Me.btnPrint.Location = New System.Drawing.Point(543, 299)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(45, 45)
        Me.btnPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnPrint.TabIndex = 64
        Me.btnPrint.TabStop = False
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.Location = New System.Drawing.Point(55, 191)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(92, 23)
        Me.lblFilter.TabIndex = 63
        Me.lblFilter.Text = "SUPPLIER"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(388, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 23)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "TO"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(55, 147)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 23)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "DATE FROM"
        '
        'dateTo
        '
        Me.dateTo.CustomFormat = ""
        Me.dateTo.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTo.Location = New System.Drawing.Point(428, 140)
        Me.dateTo.Name = "dateTo"
        Me.dateTo.Size = New System.Drawing.Size(160, 32)
        Me.dateTo.TabIndex = 60
        '
        'dateFrom
        '
        Me.dateFrom.CustomFormat = ""
        Me.dateFrom.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateFrom.Location = New System.Drawing.Point(173, 143)
        Me.dateFrom.Name = "dateFrom"
        Me.dateFrom.Size = New System.Drawing.Size(160, 32)
        Me.dateFrom.TabIndex = 59
        '
        'tabSupplier
        '
        Me.tabSupplier.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabSupplier.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabSupplier.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSupplier.ForeColor = System.Drawing.Color.Chocolate
        Me.tabSupplier.Location = New System.Drawing.Point(329, 90)
        Me.tabSupplier.Name = "tabSupplier"
        Me.tabSupplier.Size = New System.Drawing.Size(212, 30)
        Me.tabSupplier.TabIndex = 58
        Me.tabSupplier.Text = "Supplier"
        Me.tabSupplier.UseVisualStyleBackColor = False
        '
        'tabStock
        '
        Me.tabStock.BackColor = System.Drawing.Color.Chocolate
        Me.tabStock.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabStock.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabStock.ForeColor = System.Drawing.Color.White
        Me.tabStock.Location = New System.Drawing.Point(110, 90)
        Me.tabStock.Name = "tabStock"
        Me.tabStock.Size = New System.Drawing.Size(212, 30)
        Me.tabStock.TabIndex = 57
        Me.tabStock.Text = "Stock"
        Me.tabStock.UseVisualStyleBackColor = False
        '
        'tbSupplier
        '
        Me.tbSupplier.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbSupplier.Location = New System.Drawing.Point(173, 188)
        Me.tbSupplier.Name = "tbSupplier"
        Me.tbSupplier.Size = New System.Drawing.Size(415, 32)
        Me.tbSupplier.TabIndex = 66
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(122, 10)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(400, 35)
        Me.notificationPanel.TabIndex = 122
        Me.notificationPanel.Visible = False
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(379, 9)
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
        Me.notificationLabel.Location = New System.Drawing.Point(3, 4)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(392, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbStock
        '
        Me.tbStock.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbStock.Location = New System.Drawing.Point(173, 232)
        Me.tbStock.Name = "tbStock"
        Me.tbStock.Size = New System.Drawing.Size(415, 32)
        Me.tbStock.TabIndex = 124
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label3.Location = New System.Drawing.Point(55, 235)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 23)
        Me.Label3.TabIndex = 123
        Me.Label3.Text = "STOCK"
        '
        'RptPurchaseMn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(653, 369)
        Me.Controls.Add(Me.tbStock)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.tbSupplier)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTo)
        Me.Controls.Add(Me.dateFrom)
        Me.Controls.Add(Me.tabSupplier)
        Me.Controls.Add(Me.tabStock)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RptPurchaseMn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Purchase Monitoring"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.PictureBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents tabSupplier As System.Windows.Forms.Button
    Friend WithEvents tabStock As System.Windows.Forms.Button
    Friend WithEvents tbSupplier As System.Windows.Forms.TextBox
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents tbStock As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
