<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptSalesMn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptSalesMn))
        Me.printBtn = New System.Windows.Forms.PictureBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dateTo = New System.Windows.Forms.DateTimePicker()
        Me.dateFrom = New System.Windows.Forms.DateTimePicker()
        Me.tabAgent = New System.Windows.Forms.Button()
        Me.tabStock = New System.Windows.Forms.Button()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.tbCustomer = New System.Windows.Forms.TextBox()
        Me.tbStock = New System.Windows.Forms.TextBox()
        Me.tbAgent = New System.Windows.Forms.TextBox()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.tabCustomer = New System.Windows.Forms.Button()
        CType(Me.printBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'printBtn
        '
        Me.printBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.printBtn.Image = Global.BGMS.My.Resources.Resources.print1
        Me.printBtn.Location = New System.Drawing.Point(387, 135)
        Me.printBtn.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.printBtn.Name = "printBtn"
        Me.printBtn.Size = New System.Drawing.Size(45, 45)
        Me.printBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.printBtn.TabIndex = 64
        Me.printBtn.TabStop = False
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.Location = New System.Drawing.Point(75, 173)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(106, 23)
        Me.lblFilter.TabIndex = 63
        Me.lblFilter.Text = "CUSTOMER"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(408, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 23)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "TO"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(75, 131)
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
        Me.dateTo.Location = New System.Drawing.Point(459, 124)
        Me.dateTo.Name = "dateTo"
        Me.dateTo.Size = New System.Drawing.Size(156, 32)
        Me.dateTo.TabIndex = 60
        '
        'dateFrom
        '
        Me.dateFrom.CustomFormat = ""
        Me.dateFrom.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateFrom.Location = New System.Drawing.Point(194, 124)
        Me.dateFrom.Name = "dateFrom"
        Me.dateFrom.Size = New System.Drawing.Size(162, 32)
        Me.dateFrom.TabIndex = 59
        '
        'tabAgent
        '
        Me.tabAgent.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabAgent.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabAgent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabAgent.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabAgent.ForeColor = System.Drawing.Color.Chocolate
        Me.tabAgent.Location = New System.Drawing.Point(412, 81)
        Me.tabAgent.Name = "tabAgent"
        Me.tabAgent.Size = New System.Drawing.Size(139, 30)
        Me.tabAgent.TabIndex = 58
        Me.tabAgent.Text = "Agent"
        Me.tabAgent.UseVisualStyleBackColor = False
        '
        'tabStock
        '
        Me.tabStock.BackColor = System.Drawing.Color.Chocolate
        Me.tabStock.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabStock.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabStock.ForeColor = System.Drawing.Color.White
        Me.tabStock.Location = New System.Drawing.Point(118, 81)
        Me.tabStock.Name = "tabStock"
        Me.tabStock.Size = New System.Drawing.Size(139, 30)
        Me.tabStock.TabIndex = 57
        Me.tabStock.Text = "Stock"
        Me.tabStock.UseVisualStyleBackColor = False
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblStock.Location = New System.Drawing.Point(75, 213)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(67, 23)
        Me.lblStock.TabIndex = 66
        Me.lblStock.Text = "STOCK"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.tbCustomer)
        Me.FlowLayoutPanel1.Controls.Add(Me.tbStock)
        Me.FlowLayoutPanel1.Controls.Add(Me.tbAgent)
        Me.FlowLayoutPanel1.Controls.Add(Me.printBtn)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(183, 167)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(432, 192)
        Me.FlowLayoutPanel1.TabIndex = 68
        '
        'tbCustomer
        '
        Me.tbCustomer.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbCustomer.Location = New System.Drawing.Point(11, 3)
        Me.tbCustomer.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.tbCustomer.Name = "tbCustomer"
        Me.tbCustomer.Size = New System.Drawing.Size(418, 32)
        Me.tbCustomer.TabIndex = 69
        '
        'tbStock
        '
        Me.tbStock.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbStock.Location = New System.Drawing.Point(11, 48)
        Me.tbStock.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.tbStock.Name = "tbStock"
        Me.tbStock.Size = New System.Drawing.Size(418, 32)
        Me.tbStock.TabIndex = 70
        '
        'tbAgent
        '
        Me.tbAgent.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbAgent.Location = New System.Drawing.Point(11, 93)
        Me.tbAgent.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.tbAgent.Name = "tbAgent"
        Me.tbAgent.Size = New System.Drawing.Size(418, 32)
        Me.tbAgent.TabIndex = 71
        Me.tbAgent.Visible = False
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(145, 7)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(380, 35)
        Me.notificationPanel.TabIndex = 122
        Me.notificationPanel.Visible = False
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(355, 10)
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
        Me.notificationLabel.Location = New System.Drawing.Point(7, 3)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(370, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabCustomer
        '
        Me.tabCustomer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabCustomer.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabCustomer.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabCustomer.ForeColor = System.Drawing.Color.Chocolate
        Me.tabCustomer.Location = New System.Drawing.Point(266, 81)
        Me.tabCustomer.Name = "tabCustomer"
        Me.tabCustomer.Size = New System.Drawing.Size(139, 30)
        Me.tabCustomer.TabIndex = 123
        Me.tabCustomer.Text = "Customer"
        Me.tabCustomer.UseVisualStyleBackColor = False
        '
        'RptSalesMn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(671, 367)
        Me.Controls.Add(Me.tabCustomer)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.lblStock)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTo)
        Me.Controls.Add(Me.dateFrom)
        Me.Controls.Add(Me.tabAgent)
        Me.Controls.Add(Me.tabStock)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RptSalesMn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sales Monitoring"
        CType(Me.printBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents printBtn As System.Windows.Forms.PictureBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents tabAgent As System.Windows.Forms.Button
    Friend WithEvents tabStock As System.Windows.Forms.Button
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents tbCustomer As System.Windows.Forms.TextBox
    Friend WithEvents tbStock As System.Windows.Forms.TextBox
    Friend WithEvents tbAgent As System.Windows.Forms.TextBox
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents tabCustomer As System.Windows.Forms.Button
End Class
