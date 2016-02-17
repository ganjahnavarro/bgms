<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptTransactions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptTransactions))
        Me.cbDetail = New System.Windows.Forms.CheckBox()
        Me.tbFilterDoc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tabCC = New System.Windows.Forms.Button()
        Me.tabPR = New System.Windows.Forms.Button()
        Me.tabSP = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tabSO = New System.Windows.Forms.Button()
        Me.tabSR = New System.Windows.Forms.Button()
        Me.tabPO = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dateTo = New System.Windows.Forms.DateTimePicker()
        Me.dateFrom = New System.Windows.Forms.DateTimePicker()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.cbOrder = New System.Windows.Forms.CheckBox()
        Me.cbSpecial = New System.Windows.Forms.CheckBox()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbDetail
        '
        Me.cbDetail.AutoSize = True
        Me.cbDetail.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.cbDetail.Location = New System.Drawing.Point(194, 274)
        Me.cbDetail.Name = "cbDetail"
        Me.cbDetail.Size = New System.Drawing.Size(77, 22)
        Me.cbDetail.TabIndex = 90
        Me.cbDetail.Text = "DETAIL"
        Me.cbDetail.UseVisualStyleBackColor = True
        '
        'tbFilterDoc
        '
        Me.tbFilterDoc.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbFilterDoc.Location = New System.Drawing.Point(194, 236)
        Me.tbFilterDoc.Name = "tbFilterDoc"
        Me.tbFilterDoc.Size = New System.Drawing.Size(395, 32)
        Me.tbFilterDoc.TabIndex = 94
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label3.Location = New System.Drawing.Point(76, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 23)
        Me.Label3.TabIndex = 88
        Me.Label3.Text = "DOC. NO"
        '
        'tabCC
        '
        Me.tabCC.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabCC.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabCC.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabCC.ForeColor = System.Drawing.Color.Chocolate
        Me.tabCC.Location = New System.Drawing.Point(412, 108)
        Me.tabCC.Name = "tabCC"
        Me.tabCC.Size = New System.Drawing.Size(150, 30)
        Me.tabCC.TabIndex = 87
        Me.tabCC.Text = "Cust. Collection"
        Me.tabCC.UseVisualStyleBackColor = False
        '
        'tabPR
        '
        Me.tabPR.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabPR.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabPR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabPR.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabPR.ForeColor = System.Drawing.Color.Chocolate
        Me.tabPR.Location = New System.Drawing.Point(256, 72)
        Me.tabPR.Name = "tabPR"
        Me.tabPR.Size = New System.Drawing.Size(150, 30)
        Me.tabPR.TabIndex = 86
        Me.tabPR.Text = "Purchase Return"
        Me.tabPR.UseVisualStyleBackColor = False
        '
        'tabSP
        '
        Me.tabSP.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabSP.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabSP.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabSP.ForeColor = System.Drawing.Color.Chocolate
        Me.tabSP.Location = New System.Drawing.Point(412, 72)
        Me.tabSP.Name = "tabSP"
        Me.tabSP.Size = New System.Drawing.Size(150, 30)
        Me.tabSP.TabIndex = 85
        Me.tabSP.Text = "Supplier Payment"
        Me.tabSP.UseVisualStyleBackColor = False
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.Location = New System.Drawing.Point(76, 199)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(92, 23)
        Me.lblFilter.TabIndex = 82
        Me.lblFilter.Text = "SUPPLIER"
        '
        'tabSO
        '
        Me.tabSO.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabSO.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabSO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabSO.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabSO.ForeColor = System.Drawing.Color.Chocolate
        Me.tabSO.Location = New System.Drawing.Point(100, 108)
        Me.tabSO.Name = "tabSO"
        Me.tabSO.Size = New System.Drawing.Size(150, 30)
        Me.tabSO.TabIndex = 77
        Me.tabSO.Text = "Sales Order"
        Me.tabSO.UseVisualStyleBackColor = False
        '
        'tabSR
        '
        Me.tabSR.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabSR.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabSR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabSR.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabSR.ForeColor = System.Drawing.Color.Chocolate
        Me.tabSR.Location = New System.Drawing.Point(256, 108)
        Me.tabSR.Name = "tabSR"
        Me.tabSR.Size = New System.Drawing.Size(150, 30)
        Me.tabSR.TabIndex = 76
        Me.tabSR.Text = "Sales Return"
        Me.tabSR.UseVisualStyleBackColor = False
        '
        'tabPO
        '
        Me.tabPO.BackColor = System.Drawing.Color.Chocolate
        Me.tabPO.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabPO.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.tabPO.ForeColor = System.Drawing.Color.White
        Me.tabPO.Location = New System.Drawing.Point(100, 72)
        Me.tabPO.Name = "tabPO"
        Me.tabPO.Size = New System.Drawing.Size(150, 30)
        Me.tabPO.TabIndex = 75
        Me.tabPO.Text = "Purchase Order"
        Me.tabPO.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.Image = Global.BGMS.My.Resources.Resources.print1
        Me.btnPrint.Location = New System.Drawing.Point(544, 281)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(45, 45)
        Me.btnPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnPrint.TabIndex = 83
        Me.btnPrint.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(369, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 23)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "TO"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(76, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 23)
        Me.Label1.TabIndex = 93
        Me.Label1.Text = "DATE FROM"
        '
        'dateTo
        '
        Me.dateTo.CustomFormat = ""
        Me.dateTo.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTo.Location = New System.Drawing.Point(430, 154)
        Me.dateTo.Name = "dateTo"
        Me.dateTo.Size = New System.Drawing.Size(159, 32)
        Me.dateTo.TabIndex = 92
        '
        'dateFrom
        '
        Me.dateFrom.CustomFormat = ""
        Me.dateFrom.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateFrom.Location = New System.Drawing.Point(194, 154)
        Me.dateFrom.Name = "dateFrom"
        Me.dateFrom.Size = New System.Drawing.Size(164, 32)
        Me.dateFrom.TabIndex = 91
        '
        'tbFilter
        '
        Me.tbFilter.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbFilter.Location = New System.Drawing.Point(194, 196)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(395, 32)
        Me.tbFilter.TabIndex = 93
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(130, 6)
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
        Me.notificationClose.Location = New System.Drawing.Point(373, 11)
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
        Me.notificationLabel.Location = New System.Drawing.Point(22, 3)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(345, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbOrder
        '
        Me.cbOrder.AutoSize = True
        Me.cbOrder.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.cbOrder.Location = New System.Drawing.Point(277, 274)
        Me.cbOrder.Name = "cbOrder"
        Me.cbOrder.Size = New System.Drawing.Size(74, 22)
        Me.cbOrder.TabIndex = 123
        Me.cbOrder.Text = "ORDER"
        Me.cbOrder.UseVisualStyleBackColor = True
        Me.cbOrder.Visible = False
        '
        'cbSpecial
        '
        Me.cbSpecial.AutoSize = True
        Me.cbSpecial.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.cbSpecial.Location = New System.Drawing.Point(357, 274)
        Me.cbSpecial.Name = "cbSpecial"
        Me.cbSpecial.Size = New System.Drawing.Size(82, 22)
        Me.cbSpecial.TabIndex = 124
        Me.cbSpecial.Text = "SPECIAL"
        Me.cbSpecial.UseVisualStyleBackColor = True
        Me.cbSpecial.Visible = False
        '
        'RptTransactions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(669, 362)
        Me.Controls.Add(Me.cbSpecial)
        Me.Controls.Add(Me.cbOrder)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTo)
        Me.Controls.Add(Me.dateFrom)
        Me.Controls.Add(Me.cbDetail)
        Me.Controls.Add(Me.tbFilterDoc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tabCC)
        Me.Controls.Add(Me.tabPR)
        Me.Controls.Add(Me.tabSP)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.tabSO)
        Me.Controls.Add(Me.tabSR)
        Me.Controls.Add(Me.tabPO)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RptTransactions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Transaction List"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbDetail As System.Windows.Forms.CheckBox
    Friend WithEvents tbFilterDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tabCC As System.Windows.Forms.Button
    Friend WithEvents tabPR As System.Windows.Forms.Button
    Friend WithEvents tabSP As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.PictureBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents tabSO As System.Windows.Forms.Button
    Friend WithEvents tabSR As System.Windows.Forms.Button
    Friend WithEvents tabPO As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbFilter As System.Windows.Forms.TextBox
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents cbOrder As System.Windows.Forms.CheckBox
    Friend WithEvents cbSpecial As System.Windows.Forms.CheckBox
End Class
