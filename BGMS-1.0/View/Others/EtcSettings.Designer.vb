<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtcSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EtcSettings))
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.counterSODR = New System.Windows.Forms.TextBox()
        Me.counterSORF = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.counterSOSI = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.counterSOPO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.counterSR = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.counterPR = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.counterCC = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.counterSP = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ButtonsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnEdit = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.btnCheck = New System.Windows.Forms.PictureBox()
        Me.notificationPanel.SuspendLayout()
        Me.ButtonsPanel.SuspendLayout()
        CType(Me.btnEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(29, 4)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(375, 38)
        Me.notificationPanel.TabIndex = 122
        Me.notificationPanel.Visible = False
        '
        'notificationLabel
        '
        Me.notificationLabel.BackColor = System.Drawing.Color.Transparent
        Me.notificationLabel.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.notificationLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.notificationLabel.Location = New System.Drawing.Point(3, 5)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(369, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(356, 11)
        Me.notificationClose.Name = "notificationClose"
        Me.notificationClose.Size = New System.Drawing.Size(16, 16)
        Me.notificationClose.TabIndex = 18
        Me.notificationClose.Text = "X"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(42, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 23)
        Me.Label1.TabIndex = 123
        Me.Label1.Text = "Sales Order (DR)"
        '
        'counterSODR
        '
        Me.counterSODR.Enabled = False
        Me.counterSODR.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSODR.Location = New System.Drawing.Point(239, 78)
        Me.counterSODR.Name = "counterSODR"
        Me.counterSODR.Size = New System.Drawing.Size(152, 32)
        Me.counterSODR.TabIndex = 124
        '
        'counterSORF
        '
        Me.counterSORF.Enabled = False
        Me.counterSORF.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSORF.Location = New System.Drawing.Point(239, 120)
        Me.counterSORF.Name = "counterSORF"
        Me.counterSORF.Size = New System.Drawing.Size(152, 32)
        Me.counterSORF.TabIndex = 126
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(42, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(149, 23)
        Me.Label2.TabIndex = 125
        Me.Label2.Text = "Sales Order (RF)"
        '
        'counterSOSI
        '
        Me.counterSOSI.Enabled = False
        Me.counterSOSI.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSOSI.Location = New System.Drawing.Point(239, 162)
        Me.counterSOSI.Name = "counterSOSI"
        Me.counterSOSI.Size = New System.Drawing.Size(152, 32)
        Me.counterSOSI.TabIndex = 128
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label3.Location = New System.Drawing.Point(42, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(145, 23)
        Me.Label3.TabIndex = 127
        Me.Label3.Text = "Sales Order (SI)"
        '
        'counterSOPO
        '
        Me.counterSOPO.Enabled = False
        Me.counterSOPO.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSOPO.Location = New System.Drawing.Point(239, 204)
        Me.counterSOPO.Name = "counterSOPO"
        Me.counterSOPO.Size = New System.Drawing.Size(152, 32)
        Me.counterSOPO.TabIndex = 130
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label4.Location = New System.Drawing.Point(42, 209)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(150, 23)
        Me.Label4.TabIndex = 129
        Me.Label4.Text = "Sales Order (PO)"
        '
        'counterSR
        '
        Me.counterSR.Enabled = False
        Me.counterSR.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSR.Location = New System.Drawing.Point(239, 246)
        Me.counterSR.Name = "counterSR"
        Me.counterSR.Size = New System.Drawing.Size(152, 32)
        Me.counterSR.TabIndex = 132
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label5.Location = New System.Drawing.Point(42, 251)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 23)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Sales Return"
        '
        'counterPR
        '
        Me.counterPR.Enabled = False
        Me.counterPR.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterPR.Location = New System.Drawing.Point(239, 288)
        Me.counterPR.Name = "counterPR"
        Me.counterPR.Size = New System.Drawing.Size(152, 32)
        Me.counterPR.TabIndex = 136
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label7.Location = New System.Drawing.Point(42, 293)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(149, 23)
        Me.Label7.TabIndex = 135
        Me.Label7.Text = "Purchase Return"
        '
        'counterCC
        '
        Me.counterCC.Enabled = False
        Me.counterCC.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterCC.Location = New System.Drawing.Point(239, 330)
        Me.counterCC.Name = "counterCC"
        Me.counterCC.Size = New System.Drawing.Size(152, 32)
        Me.counterCC.TabIndex = 138
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label8.Location = New System.Drawing.Point(42, 335)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(174, 23)
        Me.Label8.TabIndex = 137
        Me.Label8.Text = "Customer Collection"
        '
        'counterSP
        '
        Me.counterSP.Enabled = False
        Me.counterSP.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.counterSP.Location = New System.Drawing.Point(239, 372)
        Me.counterSP.Name = "counterSP"
        Me.counterSP.Size = New System.Drawing.Size(152, 32)
        Me.counterSP.TabIndex = 140
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label9.Location = New System.Drawing.Point(42, 377)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(179, 23)
        Me.Label9.TabIndex = 139
        Me.Label9.Text = "Payment to Supplier"
        '
        'ButtonsPanel
        '
        Me.ButtonsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonsPanel.Controls.Add(Me.btnEdit)
        Me.ButtonsPanel.Controls.Add(Me.btnCancel)
        Me.ButtonsPanel.Controls.Add(Me.btnCheck)
        Me.ButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.ButtonsPanel.Location = New System.Drawing.Point(227, 448)
        Me.ButtonsPanel.Name = "ButtonsPanel"
        Me.ButtonsPanel.Size = New System.Drawing.Size(190, 46)
        Me.ButtonsPanel.TabIndex = 141
        '
        'btnEdit
        '
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEdit.Image = Global.BGMS.My.Resources.Resources.edit
        Me.btnEdit.Location = New System.Drawing.Point(145, 0)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(45, 45)
        Me.btnEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnEdit.TabIndex = 40
        Me.btnEdit.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Image = Global.BGMS.My.Resources.Resources.delete
        Me.btnCancel.Location = New System.Drawing.Point(96, 0)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(45, 45)
        Me.btnCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCancel.TabIndex = 45
        Me.btnCancel.TabStop = False
        Me.btnCancel.Visible = False
        '
        'btnCheck
        '
        Me.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheck.Image = Global.BGMS.My.Resources.Resources.checkmark
        Me.btnCheck.Location = New System.Drawing.Point(47, 0)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(45, 45)
        Me.btnCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCheck.TabIndex = 46
        Me.btnCheck.TabStop = False
        Me.btnCheck.Visible = False
        '
        'EtcSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(429, 506)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.ButtonsPanel)
        Me.Controls.Add(Me.counterSP)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.counterCC)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.counterPR)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.counterSR)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.counterSOPO)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.counterSOSI)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.counterSORF)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.counterSODR)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EtcSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ButtonsPanel.ResumeLayout(False)
        CType(Me.btnEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents counterSODR As System.Windows.Forms.TextBox
    Friend WithEvents counterSORF As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents counterSOSI As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents counterSOPO As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents counterSR As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents counterPR As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents counterCC As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents counterSP As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ButtonsPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnEdit As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheck As System.Windows.Forms.PictureBox
End Class
