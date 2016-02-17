<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtcAddItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EtcAddItem))
        Me.tbDescription = New System.Windows.Forms.TextBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.tbStock = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.itemsGrid = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbCategory = New System.Windows.Forms.TextBox()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbPrice = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbOnHand = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbAvailable = New System.Windows.Forms.TextBox()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.tbDisc1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbDisc2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbDisc3 = New System.Windows.Forms.TextBox()
        Me.lblDisc3 = New System.Windows.Forms.Label()
        Me.btnCheckAdd = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.btnCheck = New System.Windows.Forms.PictureBox()
        Me.delayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.notificationPanel = New System.Windows.Forms.Panel()
        Me.notificationClose = New System.Windows.Forms.Label()
        Me.notificationLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.notificationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbDescription
        '
        Me.tbDescription.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDescription.Location = New System.Drawing.Point(167, 392)
        Me.tbDescription.Multiline = True
        Me.tbDescription.Name = "tbDescription"
        Me.tbDescription.Size = New System.Drawing.Size(314, 55)
        Me.tbDescription.TabIndex = 198
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblFilter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilter.Location = New System.Drawing.Point(31, 392)
        Me.lblFilter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(129, 23)
        Me.lblFilter.TabIndex = 199
        Me.lblFilter.Text = "By Description"
        '
        'tbStock
        '
        Me.tbStock.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbStock.Location = New System.Drawing.Point(167, 350)
        Me.tbStock.Name = "tbStock"
        Me.tbStock.Size = New System.Drawing.Size(314, 32)
        Me.tbStock.TabIndex = 197
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(31, 350)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 23)
        Me.Label2.TabIndex = 196
        Me.Label2.Text = "By Name"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.Panel1.Controls.Add(Me.itemsGrid)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.Panel1.Location = New System.Drawing.Point(12, 23)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1)
        Me.Panel1.Size = New System.Drawing.Size(489, 304)
        Me.Panel1.TabIndex = 193
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
        Me.itemsGrid.Size = New System.Drawing.Size(467, 295)
        Me.itemsGrid.StandardTab = True
        Me.itemsGrid.TabIndex = 193
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(31, 458)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 23)
        Me.Label1.TabIndex = 200
        Me.Label1.Text = "By Category"
        '
        'tbCategory
        '
        Me.tbCategory.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbCategory.Location = New System.Drawing.Point(167, 458)
        Me.tbCategory.Name = "tbCategory"
        Me.tbCategory.Size = New System.Drawing.Size(314, 32)
        Me.tbCategory.TabIndex = 199
        '
        'lblStock
        '
        Me.lblStock.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.lblStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStock.Location = New System.Drawing.Point(508, 23)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(293, 40)
        Me.lblStock.TabIndex = 202
        Me.lblStock.Text = "01463-SR3-000J"
        Me.lblStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbQty
        '
        Me.tbQty.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbQty.Location = New System.Drawing.Point(3, 5)
        Me.tbQty.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(162, 32)
        Me.tbQty.TabIndex = 200
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 23)
        Me.Label4.TabIndex = 204
        Me.Label4.Text = "Quantity"
        '
        'tbPrice
        '
        Me.tbPrice.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbPrice.Location = New System.Drawing.Point(3, 45)
        Me.tbPrice.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbPrice.Name = "tbPrice"
        Me.tbPrice.Size = New System.Drawing.Size(162, 32)
        Me.tbPrice.TabIndex = 201
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 23)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(50, 40)
        Me.Label3.TabIndex = 206
        Me.Label3.Text = "Price"
        '
        'tbOnHand
        '
        Me.tbOnHand.Enabled = False
        Me.tbOnHand.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbOnHand.Location = New System.Drawing.Point(3, 85)
        Me.tbOnHand.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbOnHand.Name = "tbOnHand"
        Me.tbOnHand.Size = New System.Drawing.Size(162, 32)
        Me.tbOnHand.TabIndex = 202
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 63)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(85, 40)
        Me.Label5.TabIndex = 208
        Me.Label5.Text = "On Hand"
        '
        'tbAvailable
        '
        Me.tbAvailable.Enabled = False
        Me.tbAvailable.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbAvailable.Location = New System.Drawing.Point(3, 125)
        Me.tbAvailable.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbAvailable.Name = "tbAvailable"
        Me.tbAvailable.Size = New System.Drawing.Size(162, 32)
        Me.tbAvailable.TabIndex = 203
        '
        'lblAvailable
        '
        Me.lblAvailable.AutoSize = True
        Me.lblAvailable.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvailable.Location = New System.Drawing.Point(4, 103)
        Me.lblAvailable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.lblAvailable.Size = New System.Drawing.Size(83, 40)
        Me.lblAvailable.TabIndex = 210
        Me.lblAvailable.Text = "Available"
        '
        'tbDisc1
        '
        Me.tbDisc1.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDisc1.Location = New System.Drawing.Point(3, 165)
        Me.tbDisc1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbDisc1.Name = "tbDisc1"
        Me.tbDisc1.Size = New System.Drawing.Size(162, 32)
        Me.tbDisc1.TabIndex = 204
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 143)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(92, 40)
        Me.Label7.TabIndex = 212
        Me.Label7.Text = "Discount1"
        '
        'tbDisc2
        '
        Me.tbDisc2.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDisc2.Location = New System.Drawing.Point(3, 205)
        Me.tbDisc2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbDisc2.Name = "tbDisc2"
        Me.tbDisc2.Size = New System.Drawing.Size(162, 32)
        Me.tbDisc2.TabIndex = 205
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 183)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(92, 40)
        Me.Label8.TabIndex = 214
        Me.Label8.Text = "Discount2"
        '
        'tbAmount
        '
        Me.tbAmount.Enabled = False
        Me.tbAmount.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbAmount.Location = New System.Drawing.Point(3, 285)
        Me.tbAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbAmount.Name = "tbAmount"
        Me.tbAmount.Size = New System.Drawing.Size(162, 32)
        Me.tbAmount.TabIndex = 207
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 263)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.Label9.Size = New System.Drawing.Size(75, 40)
        Me.Label9.TabIndex = 218
        Me.Label9.Text = "Amount"
        '
        'tbDisc3
        '
        Me.tbDisc3.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.tbDisc3.Location = New System.Drawing.Point(3, 245)
        Me.tbDisc3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbDisc3.Name = "tbDisc3"
        Me.tbDisc3.Size = New System.Drawing.Size(162, 32)
        Me.tbDisc3.TabIndex = 206
        '
        'lblDisc3
        '
        Me.lblDisc3.AutoSize = True
        Me.lblDisc3.Font = New System.Drawing.Font("Tahoma", 14.0!)
        Me.lblDisc3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDisc3.Location = New System.Drawing.Point(4, 223)
        Me.lblDisc3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDisc3.Name = "lblDisc3"
        Me.lblDisc3.Padding = New System.Windows.Forms.Padding(0, 17, 0, 0)
        Me.lblDisc3.Size = New System.Drawing.Size(92, 40)
        Me.lblDisc3.TabIndex = 216
        Me.lblDisc3.Text = "Discount3"
        '
        'btnCheckAdd
        '
        Me.btnCheckAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheckAdd.Image = CType(resources.GetObject("btnCheckAdd.Image"), System.Drawing.Image)
        Me.btnCheckAdd.Location = New System.Drawing.Point(654, 445)
        Me.btnCheckAdd.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCheckAdd.Name = "btnCheckAdd"
        Me.btnCheckAdd.Size = New System.Drawing.Size(45, 45)
        Me.btnCheckAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCheckAdd.TabIndex = 222
        Me.btnCheckAdd.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Image = Global.BGMS.My.Resources.Resources.delete
        Me.btnCancel.Location = New System.Drawing.Point(752, 445)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(45, 45)
        Me.btnCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCancel.TabIndex = 220
        Me.btnCancel.TabStop = False
        '
        'btnCheck
        '
        Me.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheck.Image = Global.BGMS.My.Resources.Resources.checkmark
        Me.btnCheck.Location = New System.Drawing.Point(703, 445)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(45, 45)
        Me.btnCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCheck.TabIndex = 221
        Me.btnCheck.TabStop = False
        '
        'delayTimer
        '
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Label4)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label5)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblAvailable)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label7)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label8)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblDisc3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label9)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(518, 81)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(109, 338)
        Me.FlowLayoutPanel1.TabIndex = 223
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.tbQty)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbPrice)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbOnHand)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbAvailable)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbDisc1)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbDisc2)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbDisc3)
        Me.FlowLayoutPanel2.Controls.Add(Me.tbAmount)
        Me.FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(632, 74)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(169, 345)
        Me.FlowLayoutPanel2.TabIndex = 224
        '
        'notificationPanel
        '
        Me.notificationPanel.BackColor = System.Drawing.Color.LightSlateGray
        Me.notificationPanel.Controls.Add(Me.notificationClose)
        Me.notificationPanel.Controls.Add(Me.notificationLabel)
        Me.notificationPanel.Location = New System.Drawing.Point(130, 4)
        Me.notificationPanel.Name = "notificationPanel"
        Me.notificationPanel.Size = New System.Drawing.Size(558, 35)
        Me.notificationPanel.TabIndex = 225
        Me.notificationPanel.Visible = False
        '
        'notificationClose
        '
        Me.notificationClose.AutoSize = True
        Me.notificationClose.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notificationClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.notificationClose.Location = New System.Drawing.Point(539, 11)
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
        Me.notificationLabel.Location = New System.Drawing.Point(7, 0)
        Me.notificationLabel.Name = "notificationLabel"
        Me.notificationLabel.Size = New System.Drawing.Size(536, 35)
        Me.notificationLabel.TabIndex = 0
        Me.notificationLabel.Text = "Notification"
        Me.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'EtcAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 511)
        Me.Controls.Add(Me.notificationPanel)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.btnCheckAdd)
        Me.Controls.Add(Me.FlowLayoutPanel2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.lblStock)
        Me.Controls.Add(Me.tbCategory)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbDescription)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.tbStock)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EtcAddItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add"
        Me.Panel1.ResumeLayout(False)
        CType(Me.itemsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.notificationPanel.ResumeLayout(False)
        Me.notificationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents tbStock As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents itemsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbCategory As System.Windows.Forms.TextBox
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents tbQty As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbOnHand As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbAvailable As System.Windows.Forms.TextBox
    Friend WithEvents lblAvailable As System.Windows.Forms.Label
    Friend WithEvents tbDisc1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbDisc2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbDisc3 As System.Windows.Forms.TextBox
    Friend WithEvents lblDisc3 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheck As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheckAdd As System.Windows.Forms.PictureBox
    Friend WithEvents delayTimer As System.Windows.Forms.Timer
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents notificationPanel As System.Windows.Forms.Panel
    Friend WithEvents notificationClose As System.Windows.Forms.Label
    Friend WithEvents notificationLabel As System.Windows.Forms.Label
End Class
