<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockSelection
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.stockTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.stockGrid = New System.Windows.Forms.DataGridView()
        Me.stockInput = New System.Windows.Forms.TextBox()
        Me.stockTableLayout.SuspendLayout()
        CType(Me.stockGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'stockTableLayout
        '
        Me.stockTableLayout.ColumnCount = 1
        Me.stockTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.stockTableLayout.Controls.Add(Me.stockGrid, 0, 1)
        Me.stockTableLayout.Controls.Add(Me.stockInput, 0, 0)
        Me.stockTableLayout.Dock = System.Windows.Forms.DockStyle.Top
        Me.stockTableLayout.Location = New System.Drawing.Point(0, 0)
        Me.stockTableLayout.Name = "stockTableLayout"
        Me.stockTableLayout.RowCount = 2
        Me.stockTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.49296!))
        Me.stockTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.50704!))
        Me.stockTableLayout.Size = New System.Drawing.Size(531, 258)
        Me.stockTableLayout.TabIndex = 0
        '
        'stockGrid
        '
        Me.stockGrid.AllowUserToAddRows = False
        Me.stockGrid.AllowUserToDeleteRows = False
        Me.stockGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.stockGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.stockGrid.ColumnHeadersVisible = False
        Me.stockGrid.Location = New System.Drawing.Point(3, 42)
        Me.stockGrid.Name = "stockGrid"
        Me.stockGrid.ReadOnly = True
        Me.stockGrid.RowHeadersVisible = False
        Me.stockGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.stockGrid.ShowEditingIcon = False
        Me.stockGrid.ShowRowErrors = False
        Me.stockGrid.Size = New System.Drawing.Size(442, 174)
        Me.stockGrid.TabIndex = 0
        '
        'stockInput
        '
        Me.stockInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.stockInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.stockInput.Location = New System.Drawing.Point(3, 3)
        Me.stockInput.Multiline = True
        Me.stockInput.Name = "stockInput"
        Me.stockInput.Size = New System.Drawing.Size(525, 33)
        Me.stockInput.TabIndex = 1
        '
        'StockSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(531, 284)
        Me.Controls.Add(Me.stockTableLayout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "StockSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "StockSelection"
        Me.stockTableLayout.ResumeLayout(False)
        Me.stockTableLayout.PerformLayout()
        CType(Me.stockGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents stockTableLayout As TableLayoutPanel
    Friend WithEvents stockGrid As DataGridView
    Friend WithEvents stockInput As TextBox
End Class
