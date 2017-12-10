<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.rtxtGameLog = New System.Windows.Forms.RichTextBox()
        Me.txtUserAction = New System.Windows.Forms.TextBox()
        Me.picRoom = New System.Windows.Forms.PictureBox()
        Me.btnEnterAction = New System.Windows.Forms.Button()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInstructions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRestart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEndProgram = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picRoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtxtGameLog
        '
        Me.rtxtGameLog.BackColor = System.Drawing.Color.Snow
        Me.rtxtGameLog.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtGameLog.ForeColor = System.Drawing.Color.Maroon
        Me.rtxtGameLog.Location = New System.Drawing.Point(16, 40)
        Me.rtxtGameLog.Name = "rtxtGameLog"
        Me.rtxtGameLog.ReadOnly = True
        Me.rtxtGameLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.rtxtGameLog.Size = New System.Drawing.Size(416, 256)
        Me.rtxtGameLog.TabIndex = 0
        Me.rtxtGameLog.Text = ""
        '
        'txtUserAction
        '
        Me.txtUserAction.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.txtUserAction.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserAction.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtUserAction.Location = New System.Drawing.Point(16, 304)
        Me.txtUserAction.Name = "txtUserAction"
        Me.txtUserAction.Size = New System.Drawing.Size(416, 20)
        Me.txtUserAction.TabIndex = 1
        '
        'picRoom
        '
        Me.picRoom.Location = New System.Drawing.Point(448, 40)
        Me.picRoom.Name = "picRoom"
        Me.picRoom.Size = New System.Drawing.Size(260, 260)
        Me.picRoom.TabIndex = 2
        Me.picRoom.TabStop = False
        '
        'btnEnterAction
        '
        Me.btnEnterAction.BackColor = System.Drawing.Color.Crimson
        Me.btnEnterAction.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnterAction.Location = New System.Drawing.Point(16, 336)
        Me.btnEnterAction.Name = "btnEnterAction"
        Me.btnEnterAction.Size = New System.Drawing.Size(56, 32)
        Me.btnEnterAction.TabIndex = 3
        Me.btnEnterAction.Text = "Enter"
        Me.btnEnterAction.UseVisualStyleBackColor = False
        '
        'btnClearLog
        '
        Me.btnClearLog.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnClearLog.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearLog.Location = New System.Drawing.Point(88, 336)
        Me.btnClearLog.Name = "btnClearLog"
        Me.btnClearLog.Size = New System.Drawing.Size(88, 32)
        Me.btnClearLog.TabIndex = 4
        Me.btnClearLog.Text = "Clear Log"
        Me.btnClearLog.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMenu})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(722, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuMenu
        '
        Me.mnuMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuInstructions, Me.mnuRestart, Me.mnuEndProgram})
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(50, 20)
        Me.mnuMenu.Text = "Menu"
        '
        'mnuInstructions
        '
        Me.mnuInstructions.Name = "mnuInstructions"
        Me.mnuInstructions.Size = New System.Drawing.Size(152, 22)
        Me.mnuInstructions.Text = "Instructions"
        '
        'mnuRestart
        '
        Me.mnuRestart.Name = "mnuRestart"
        Me.mnuRestart.Size = New System.Drawing.Size(152, 22)
        Me.mnuRestart.Text = "Restart"
        '
        'mnuEndProgram
        '
        Me.mnuEndProgram.Name = "mnuEndProgram"
        Me.mnuEndProgram.Size = New System.Drawing.Size(152, 22)
        Me.mnuEndProgram.Text = "End Program"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(722, 381)
        Me.Controls.Add(Me.btnClearLog)
        Me.Controls.Add(Me.btnEnterAction)
        Me.Controls.Add(Me.picRoom)
        Me.Controls.Add(Me.txtUserAction)
        Me.Controls.Add(Me.rtxtGameLog)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form2"
        Me.Text = "Trapped in a Closet"
        CType(Me.picRoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtxtGameLog As System.Windows.Forms.RichTextBox
    Friend WithEvents txtUserAction As System.Windows.Forms.TextBox
    Friend WithEvents picRoom As System.Windows.Forms.PictureBox
    Friend WithEvents btnEnterAction As System.Windows.Forms.Button
    Friend WithEvents btnClearLog As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInstructions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEndProgram As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRestart As System.Windows.Forms.ToolStripMenuItem
End Class
