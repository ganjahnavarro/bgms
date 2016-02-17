<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtcLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EtcLogin))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.tbPassword = New System.Windows.Forms.TextBox()
        Me.tbUsername = New System.Windows.Forms.TextBox()
        Me.lblLoginPassword = New System.Windows.Forms.Label()
        Me.lblLoginUsername = New System.Windows.Forms.Label()
        Me.lblNoti = New System.Windows.Forms.Label()
        Me.loginLogo = New System.Windows.Forms.PictureBox()
        CType(Me.loginLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnCancel.Location = New System.Drawing.Point(269, 234)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 39)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Exit"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.SystemColors.HotTrack
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.btnLogin.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.btnLogin.Location = New System.Drawing.Point(158, 234)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(105, 39)
        Me.btnLogin.TabIndex = 15
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'tbPassword
        '
        Me.tbPassword.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbPassword.Location = New System.Drawing.Point(230, 137)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.tbPassword.Size = New System.Drawing.Size(273, 32)
        Me.tbPassword.TabIndex = 14
        Me.tbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbUsername
        '
        Me.tbUsername.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbUsername.Location = New System.Drawing.Point(230, 73)
        Me.tbUsername.Name = "tbUsername"
        Me.tbUsername.Size = New System.Drawing.Size(273, 32)
        Me.tbUsername.TabIndex = 13
        Me.tbUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblLoginPassword
        '
        Me.lblLoginPassword.AutoSize = True
        Me.lblLoginPassword.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblLoginPassword.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblLoginPassword.Location = New System.Drawing.Point(226, 111)
        Me.lblLoginPassword.Name = "lblLoginPassword"
        Me.lblLoginPassword.Size = New System.Drawing.Size(88, 23)
        Me.lblLoginPassword.TabIndex = 12
        Me.lblLoginPassword.Text = "Password"
        '
        'lblLoginUsername
        '
        Me.lblLoginUsername.AutoSize = True
        Me.lblLoginUsername.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblLoginUsername.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblLoginUsername.Location = New System.Drawing.Point(227, 47)
        Me.lblLoginUsername.Name = "lblLoginUsername"
        Me.lblLoginUsername.Size = New System.Drawing.Size(94, 23)
        Me.lblLoginUsername.TabIndex = 11
        Me.lblLoginUsername.Text = "Username"
        '
        'lblNoti
        '
        Me.lblNoti.AutoSize = True
        Me.lblNoti.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblNoti.ForeColor = System.Drawing.Color.Crimson
        Me.lblNoti.Location = New System.Drawing.Point(227, 174)
        Me.lblNoti.Name = "lblNoti"
        Me.lblNoti.Size = New System.Drawing.Size(206, 19)
        Me.lblNoti.TabIndex = 17
        Me.lblNoti.Text = "Invalid Username/Password"
        Me.lblNoti.Visible = False
        '
        'loginLogo
        '
        Me.loginLogo.Image = Global.BGMS.My.Resources.Resources.bgms
        Me.loginLogo.Location = New System.Drawing.Point(24, 40)
        Me.loginLogo.Name = "loginLogo"
        Me.loginLogo.Size = New System.Drawing.Size(190, 166)
        Me.loginLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.loginLogo.TabIndex = 10
        Me.loginLogo.TabStop = False
        '
        'EtcLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(534, 300)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblNoti)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.tbPassword)
        Me.Controls.Add(Me.tbUsername)
        Me.Controls.Add(Me.lblLoginPassword)
        Me.Controls.Add(Me.lblLoginUsername)
        Me.Controls.Add(Me.loginLogo)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EtcLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Login"
        CType(Me.loginLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents tbUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblLoginPassword As System.Windows.Forms.Label
    Friend WithEvents lblLoginUsername As System.Windows.Forms.Label
    Friend WithEvents loginLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblNoti As System.Windows.Forms.Label
End Class
