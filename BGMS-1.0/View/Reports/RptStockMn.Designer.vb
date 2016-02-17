<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptStockMn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptStockMn))
        Me.shOnHand = New System.Windows.Forms.CheckBox()
        Me.shPrice = New System.Windows.Forms.CheckBox()
        Me.shCost = New System.Windows.Forms.CheckBox()
        Me.tabNegative = New System.Windows.Forms.Button()
        Me.tabPositive = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tabZero = New System.Windows.Forms.Button()
        Me.tabAll = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.PictureBox()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.cbSort = New System.Windows.Forms.CheckBox()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'shOnHand
        '
        Me.shOnHand.AutoSize = True
        Me.shOnHand.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.shOnHand.Location = New System.Drawing.Point(362, 74)
        Me.shOnHand.Name = "shOnHand"
        Me.shOnHand.Size = New System.Drawing.Size(92, 22)
        Me.shOnHand.TabIndex = 82
        Me.shOnHand.Text = "ON HAND"
        Me.shOnHand.UseVisualStyleBackColor = True
        '
        'shPrice
        '
        Me.shPrice.AutoSize = True
        Me.shPrice.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.shPrice.Location = New System.Drawing.Point(228, 74)
        Me.shPrice.Name = "shPrice"
        Me.shPrice.Size = New System.Drawing.Size(128, 22)
        Me.shPrice.TabIndex = 81
        Me.shPrice.Text = "SELLING PRICE"
        Me.shPrice.UseVisualStyleBackColor = True
        '
        'shCost
        '
        Me.shCost.AutoSize = True
        Me.shCost.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.shCost.Location = New System.Drawing.Point(112, 74)
        Me.shCost.Name = "shCost"
        Me.shCost.Size = New System.Drawing.Size(108, 22)
        Me.shCost.TabIndex = 80
        Me.shCost.Text = "UNIT PRICE"
        Me.shCost.UseVisualStyleBackColor = True
        '
        'tabNegative
        '
        Me.tabNegative.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabNegative.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabNegative.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabNegative.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabNegative.ForeColor = System.Drawing.Color.Chocolate
        Me.tabNegative.Location = New System.Drawing.Point(433, 111)
        Me.tabNegative.Name = "tabNegative"
        Me.tabNegative.Size = New System.Drawing.Size(101, 30)
        Me.tabNegative.TabIndex = 79
        Me.tabNegative.Text = "Negative"
        Me.tabNegative.UseVisualStyleBackColor = False
        '
        'tabPositive
        '
        Me.tabPositive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabPositive.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabPositive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabPositive.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPositive.ForeColor = System.Drawing.Color.Chocolate
        Me.tabPositive.Location = New System.Drawing.Point(326, 111)
        Me.tabPositive.Name = "tabPositive"
        Me.tabPositive.Size = New System.Drawing.Size(101, 30)
        Me.tabPositive.TabIndex = 78
        Me.tabPositive.Text = "Positive"
        Me.tabPositive.UseVisualStyleBackColor = False
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.Location = New System.Drawing.Point(45, 160)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(103, 23)
        Me.lblFilter.TabIndex = 75
        Me.lblFilter.Text = "CATEGORY"
        '
        'tabZero
        '
        Me.tabZero.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tabZero.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabZero.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabZero.ForeColor = System.Drawing.Color.Chocolate
        Me.tabZero.Location = New System.Drawing.Point(219, 111)
        Me.tabZero.Name = "tabZero"
        Me.tabZero.Size = New System.Drawing.Size(101, 30)
        Me.tabZero.TabIndex = 74
        Me.tabZero.Text = "Zero"
        Me.tabZero.UseVisualStyleBackColor = False
        '
        'tabAll
        '
        Me.tabAll.BackColor = System.Drawing.Color.Chocolate
        Me.tabAll.FlatAppearance.BorderColor = System.Drawing.Color.Chocolate
        Me.tabAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tabAll.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabAll.ForeColor = System.Drawing.Color.White
        Me.tabAll.Location = New System.Drawing.Point(112, 111)
        Me.tabAll.Name = "tabAll"
        Me.tabAll.Size = New System.Drawing.Size(101, 30)
        Me.tabAll.TabIndex = 73
        Me.tabAll.Text = "All"
        Me.tabAll.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.Image = Global.BGMS.My.Resources.Resources.print1
        Me.btnPrint.Location = New System.Drawing.Point(557, 201)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(45, 45)
        Me.btnPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnPrint.TabIndex = 76
        Me.btnPrint.TabStop = False
        '
        'tbFilter
        '
        Me.tbFilter.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbFilter.Location = New System.Drawing.Point(169, 156)
        Me.tbFilter.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(433, 32)
        Me.tbFilter.TabIndex = 79
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(141, 9)
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
        Me.notificationClose.Location = New System.Drawing.Point(358, 7)
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
        Me.notificationLabel.Size = New System.Drawing.Size(367, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbSort
        '
        Me.cbSort.AutoSize = True
        Me.cbSort.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.cbSort.Location = New System.Drawing.Point(169, 201)
        Me.cbSort.Name = "cbSort"
        Me.cbSort.Size = New System.Drawing.Size(170, 22)
        Me.cbSort.TabIndex = 123
        Me.cbSort.Text = "SORT BY CATEGORY"
        Me.cbSort.UseVisualStyleBackColor = True
        '
        'RptStockMn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(652, 303)
        Me.Controls.Add(Me.cbSort)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.shOnHand)
        Me.Controls.Add(Me.shPrice)
        Me.Controls.Add(Me.shCost)
        Me.Controls.Add(Me.tabNegative)
        Me.Controls.Add(Me.tabPositive)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.tabZero)
        Me.Controls.Add(Me.tabAll)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RptStockMn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Stock Monitoring"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents shOnHand As System.Windows.Forms.CheckBox
    Friend WithEvents shPrice As System.Windows.Forms.CheckBox
    Friend WithEvents shCost As System.Windows.Forms.CheckBox
    Friend WithEvents tabNegative As System.Windows.Forms.Button
    Friend WithEvents tabPositive As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.PictureBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents tabZero As System.Windows.Forms.Button
    Friend WithEvents tabAll As System.Windows.Forms.Button
    Friend WithEvents tbFilter As System.Windows.Forms.TextBox
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents cbSort As System.Windows.Forms.CheckBox
End Class
