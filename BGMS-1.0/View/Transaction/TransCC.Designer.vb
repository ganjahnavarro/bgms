<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransCC
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TransCC))
        Me.ButtonsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnSearch = New System.Windows.Forms.PictureBox()
        Me.btnNext = New System.Windows.Forms.PictureBox()
        Me.btnPrev = New System.Windows.Forms.PictureBox()
        Me.btnAdd = New System.Windows.Forms.PictureBox()
        Me.btnEdit = New System.Windows.Forms.PictureBox()
        Me.btnDelete = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.btnCheck = New System.Windows.Forms.PictureBox()
        Me.lblPostedOn = New System.Windows.Forms.Label()
        Me.lblOn = New System.Windows.Forms.Label()
        Me.lblBy = New System.Windows.Forms.Label()
        Me.lblPostedDate = New System.Windows.Forms.Label()
        Me.tbDocNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbBank = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.enterGridOrders = New BGMS.EnterDataGridView()
        Me.enterGridChecks = New BGMS.EnterDataGridView()
        Me.tbTotalPaid = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbTotalCheck = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.docDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUnitName = New System.Windows.Forms.Label()
        Me.tbCustomer = New System.Windows.Forms.TextBox()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.addItems = New System.Windows.Forms.PictureBox()
        Me.ButtonsPanel.SuspendLayout()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.enterGridOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.enterGridChecks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.notificationPanel.SuspendLayout()
        CType(Me.addItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonsPanel
        '
        Me.ButtonsPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonsPanel.Controls.Add(Me.btnSearch)
        Me.ButtonsPanel.Controls.Add(Me.btnNext)
        Me.ButtonsPanel.Controls.Add(Me.btnPrev)
        Me.ButtonsPanel.Controls.Add(Me.btnAdd)
        Me.ButtonsPanel.Controls.Add(Me.btnEdit)
        Me.ButtonsPanel.Controls.Add(Me.btnDelete)
        Me.ButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.ButtonsPanel.Location = New System.Drawing.Point(554, 684)
        Me.ButtonsPanel.Name = "ButtonsPanel"
        Me.ButtonsPanel.Size = New System.Drawing.Size(392, 46)
        Me.ButtonsPanel.TabIndex = 126
        '
        'btnSearch
        '
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.Image = Global.BGMS.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(347, 0)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(45, 45)
        Me.btnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnSearch.TabIndex = 47
        Me.btnSearch.TabStop = False
        '
        'btnNext
        '
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.Image = Global.BGMS.My.Resources.Resources.right_arrow
        Me.btnNext.Location = New System.Drawing.Point(298, 0)
        Me.btnNext.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(45, 45)
        Me.btnNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnNext.TabIndex = 43
        Me.btnNext.TabStop = False
        '
        'btnPrev
        '
        Me.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrev.Image = Global.BGMS.My.Resources.Resources.left_arrow
        Me.btnPrev.Location = New System.Drawing.Point(249, 0)
        Me.btnPrev.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(45, 45)
        Me.btnPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnPrev.TabIndex = 44
        Me.btnPrev.TabStop = False
        '
        'btnAdd
        '
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAdd.Image = Global.BGMS.My.Resources.Resources.plusx
        Me.btnAdd.Location = New System.Drawing.Point(190, 0)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 0, 10, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(45, 45)
        Me.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnAdd.TabIndex = 41
        Me.btnAdd.TabStop = False
        '
        'btnEdit
        '
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEdit.Image = Global.BGMS.My.Resources.Resources.edit
        Me.btnEdit.Location = New System.Drawing.Point(141, 0)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(45, 45)
        Me.btnEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnEdit.TabIndex = 40
        Me.btnEdit.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.Image = Global.BGMS.My.Resources.Resources.bin
        Me.btnDelete.Location = New System.Drawing.Point(92, 0)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(45, 45)
        Me.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnDelete.TabIndex = 42
        Me.btnDelete.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Image = Global.BGMS.My.Resources.Resources.delete
        Me.btnCancel.Location = New System.Drawing.Point(901, 684)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(45, 45)
        Me.btnCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCancel.TabIndex = 45
        Me.btnCancel.TabStop = False
        '
        'btnCheck
        '
        Me.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheck.Image = Global.BGMS.My.Resources.Resources.checkmark
        Me.btnCheck.Location = New System.Drawing.Point(852, 684)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(45, 45)
        Me.btnCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCheck.TabIndex = 46
        Me.btnCheck.TabStop = False
        '
        'lblPostedOn
        '
        Me.lblPostedOn.AutoSize = True
        Me.lblPostedOn.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblPostedOn.ForeColor = System.Drawing.Color.Green
        Me.lblPostedOn.Location = New System.Drawing.Point(143, 707)
        Me.lblPostedOn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPostedOn.Name = "lblPostedOn"
        Me.lblPostedOn.Size = New System.Drawing.Size(92, 23)
        Me.lblPostedOn.TabIndex = 133
        Me.lblPostedOn.Text = "Posted on"
        '
        'lblOn
        '
        Me.lblOn.AutoSize = True
        Me.lblOn.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblOn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblOn.Location = New System.Drawing.Point(12, 711)
        Me.lblOn.Name = "lblOn"
        Me.lblOn.Size = New System.Drawing.Size(107, 19)
        Me.lblOn.TabIndex = 132
        Me.lblOn.Text = "On: 12/03/14"
        '
        'lblBy
        '
        Me.lblBy.AutoSize = True
        Me.lblBy.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblBy.Location = New System.Drawing.Point(12, 692)
        Me.lblBy.Name = "lblBy"
        Me.lblBy.Size = New System.Drawing.Size(61, 19)
        Me.lblBy.TabIndex = 131
        Me.lblBy.Text = "By: Mel"
        '
        'lblPostedDate
        '
        Me.lblPostedDate.AutoSize = True
        Me.lblPostedDate.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblPostedDate.ForeColor = System.Drawing.Color.Green
        Me.lblPostedDate.Location = New System.Drawing.Point(232, 707)
        Me.lblPostedDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPostedDate.Name = "lblPostedDate"
        Me.lblPostedDate.Size = New System.Drawing.Size(104, 23)
        Me.lblPostedDate.TabIndex = 134
        Me.lblPostedDate.Text = "12/10/2014"
        '
        'tbDocNo
        '
        Me.tbDocNo.Enabled = False
        Me.tbDocNo.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDocNo.Location = New System.Drawing.Point(147, 47)
        Me.tbDocNo.Name = "tbDocNo"
        Me.tbDocNo.Size = New System.Drawing.Size(420, 32)
        Me.tbDocNo.TabIndex = 157
        Me.tbDocNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label6.Location = New System.Drawing.Point(575, 115)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 23)
        Me.Label6.TabIndex = 167
        Me.Label6.Text = "Bank"
        '
        'tbBank
        '
        Me.tbBank.Enabled = False
        Me.tbBank.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbBank.Location = New System.Drawing.Point(579, 141)
        Me.tbBank.Name = "tbBank"
        Me.tbBank.Size = New System.Drawing.Size(154, 32)
        Me.tbBank.TabIndex = 161
        Me.tbBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Panel2.Controls.Add(Me.enterGridOrders)
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel2.Location = New System.Drawing.Point(42, 450)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel2.Size = New System.Drawing.Size(869, 222)
        Me.Panel2.TabIndex = 165
        '
        'enterGridOrders
        '
        Me.enterGridOrders.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.enterGridOrders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.enterGridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.enterGridOrders.BackgroundColor = System.Drawing.SystemColors.Window
        Me.enterGridOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.enterGridOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.enterGridOrders.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.enterGridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.enterGridOrders.DefaultCellStyle = DataGridViewCellStyle3
        Me.enterGridOrders.EnableHeadersVisualStyles = False
        Me.enterGridOrders.GridColor = System.Drawing.Color.Gainsboro
        Me.enterGridOrders.Location = New System.Drawing.Point(4, 4)
        Me.enterGridOrders.MultiSelect = False
        Me.enterGridOrders.Name = "enterGridOrders"
        Me.enterGridOrders.ReadOnly = True
        Me.enterGridOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.enterGridOrders.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.enterGridOrders.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.enterGridOrders.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.enterGridOrders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.enterGridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.enterGridOrders.Size = New System.Drawing.Size(851, 214)
        Me.enterGridOrders.TabIndex = 166
        '
        'enterGridChecks
        '
        Me.enterGridChecks.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.enterGridChecks.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.enterGridChecks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.enterGridChecks.BackgroundColor = System.Drawing.SystemColors.Window
        Me.enterGridChecks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.enterGridChecks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.enterGridChecks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.enterGridChecks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.enterGridChecks.DefaultCellStyle = DataGridViewCellStyle8
        Me.enterGridChecks.EnableHeadersVisualStyles = False
        Me.enterGridChecks.GridColor = System.Drawing.Color.Gainsboro
        Me.enterGridChecks.Location = New System.Drawing.Point(4, 4)
        Me.enterGridChecks.MultiSelect = False
        Me.enterGridChecks.Name = "enterGridChecks"
        Me.enterGridChecks.ReadOnly = True
        Me.enterGridChecks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 13.0!)
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.MediumSeaGreen
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.enterGridChecks.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.enterGridChecks.RowHeadersVisible = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.enterGridChecks.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.enterGridChecks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.enterGridChecks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.enterGridChecks.Size = New System.Drawing.Size(847, 210)
        Me.enterGridChecks.TabIndex = 169
        '
        'tbTotalPaid
        '
        Me.tbTotalPaid.Enabled = False
        Me.tbTotalPaid.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbTotalPaid.Location = New System.Drawing.Point(744, 141)
        Me.tbTotalPaid.Name = "tbTotalPaid"
        Me.tbTotalPaid.Size = New System.Drawing.Size(167, 32)
        Me.tbTotalPaid.TabIndex = 163
        Me.tbTotalPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Panel1.Controls.Add(Me.enterGridChecks)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel1.Location = New System.Drawing.Point(46, 195)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel1.Size = New System.Drawing.Size(865, 218)
        Me.Panel1.TabIndex = 163
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label5.Location = New System.Drawing.Point(46, 145)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 23)
        Me.Label5.TabIndex = 159
        Me.Label5.Text = "Remarks"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label4.Location = New System.Drawing.Point(740, 47)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 23)
        Me.Label4.TabIndex = 156
        Me.Label4.Text = "Total Check"
        '
        'tbTotalCheck
        '
        Me.tbTotalCheck.Enabled = False
        Me.tbTotalCheck.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbTotalCheck.Location = New System.Drawing.Point(744, 75)
        Me.tbTotalCheck.Name = "tbTotalCheck"
        Me.tbTotalCheck.Size = New System.Drawing.Size(167, 32)
        Me.tbTotalCheck.TabIndex = 162
        Me.tbTotalCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label3.Location = New System.Drawing.Point(740, 115)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 23)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "Total Paid"
        '
        'tbRemarks
        '
        Me.tbRemarks.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbRemarks.Location = New System.Drawing.Point(146, 141)
        Me.tbRemarks.Multiline = True
        Me.tbRemarks.Name = "tbRemarks"
        Me.tbRemarks.Size = New System.Drawing.Size(421, 32)
        Me.tbRemarks.TabIndex = 159
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.Location = New System.Drawing.Point(577, 47)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 23)
        Me.Label2.TabIndex = 154
        Me.Label2.Text = "Date"
        '
        'docDate
        '
        Me.docDate.CustomFormat = ""
        Me.docDate.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.docDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.docDate.Location = New System.Drawing.Point(579, 77)
        Me.docDate.MaxDate = New Date(2999, 12, 31, 0, 0, 0, 0)
        Me.docDate.MinDate = New Date(1990, 1, 1, 0, 0, 0, 0)
        Me.docDate.Name = "docDate"
        Me.docDate.Size = New System.Drawing.Size(154, 32)
        Me.docDate.TabIndex = 160
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.Location = New System.Drawing.Point(46, 97)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 23)
        Me.Label1.TabIndex = 153
        Me.Label1.Text = "Customer"
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = True
        Me.lblUnitName.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblUnitName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblUnitName.Location = New System.Drawing.Point(46, 51)
        Me.lblUnitName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(74, 23)
        Me.lblUnitName.TabIndex = 152
        Me.lblUnitName.Text = "Coll No."
        '
        'tbCustomer
        '
        Me.tbCustomer.Enabled = False
        Me.tbCustomer.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbCustomer.Location = New System.Drawing.Point(147, 93)
        Me.tbCustomer.Name = "tbCustomer"
        Me.tbCustomer.Size = New System.Drawing.Size(420, 32)
        Me.tbCustomer.TabIndex = 158
        Me.tbCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(199, 5)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(558, 38)
        Me.notificationPanel.TabIndex = 168
        Me.notificationPanel.Visible = False
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(533, 10)
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
        Me.notificationLabel.Location = New System.Drawing.Point(10, 4)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(520, 28)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'addItems
        '
        Me.addItems.BackColor = System.Drawing.Color.Transparent
        Me.addItems.Cursor = System.Windows.Forms.Cursors.Hand
        Me.addItems.Image = Global.BGMS.My.Resources.Resources.plusx
        Me.addItems.Location = New System.Drawing.Point(901, 422)
        Me.addItems.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.addItems.Name = "addItems"
        Me.addItems.Size = New System.Drawing.Size(35, 35)
        Me.addItems.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.addItems.TabIndex = 169
        Me.addItems.TabStop = False
        Me.addItems.Visible = False
        '
        'TransCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(964, 742)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.tbCustomer)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.tbDocNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbBank)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.tbTotalPaid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbTotalCheck)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbRemarks)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.docDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblUnitName)
        Me.Controls.Add(Me.lblPostedOn)
        Me.Controls.Add(Me.lblOn)
        Me.Controls.Add(Me.lblBy)
        Me.Controls.Add(Me.lblPostedDate)
        Me.Controls.Add(Me.ButtonsPanel)
        Me.Controls.Add(Me.addItems)
        Me.Font = New System.Drawing.Font("Lucida Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TransCC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customer Collection"
        Me.ButtonsPanel.ResumeLayout(False)
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.enterGridOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.enterGridChecks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        CType(Me.addItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonsPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAdd As System.Windows.Forms.PictureBox
    Friend WithEvents btnEdit As System.Windows.Forms.PictureBox
    Friend WithEvents btnDelete As System.Windows.Forms.PictureBox
    Friend WithEvents btnNext As System.Windows.Forms.PictureBox
    Friend WithEvents btnPrev As System.Windows.Forms.PictureBox
    Friend WithEvents btnSearch As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheck As System.Windows.Forms.PictureBox
    Friend WithEvents lblPostedOn As System.Windows.Forms.Label
    Friend WithEvents lblOn As System.Windows.Forms.Label
    Friend WithEvents lblBy As System.Windows.Forms.Label
    Friend WithEvents lblPostedDate As System.Windows.Forms.Label
    Friend WithEvents tbDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbBank As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tbTotalPaid As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbTotalCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents docDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUnitName As System.Windows.Forms.Label
    Friend WithEvents tbCustomer As System.Windows.Forms.TextBox
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
    Friend WithEvents enterGridChecks As BGMS.EnterDataGridView
    Friend WithEvents enterGridOrders As BGMS.EnterDataGridView
    Friend WithEvents addItems As System.Windows.Forms.PictureBox
End Class
