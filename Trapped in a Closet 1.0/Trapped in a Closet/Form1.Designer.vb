<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnGoForm2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnGoForm2
        '
        Me.btnGoForm2.BackColor = System.Drawing.Color.Silver
        Me.btnGoForm2.Font = New System.Drawing.Font("Impact", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoForm2.Location = New System.Drawing.Point(144, 192)
        Me.btnGoForm2.Name = "btnGoForm2"
        Me.btnGoForm2.Size = New System.Drawing.Size(184, 80)
        Me.btnGoForm2.TabIndex = 0
        Me.btnGoForm2.Text = "BEGIN"
        Me.btnGoForm2.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImage = Global.Trapped_in_a_Closet.My.Resources.Resources.titlescreen2
        Me.ClientSize = New System.Drawing.Size(448, 321)
        Me.Controls.Add(Me.btnGoForm2)
        Me.Name = "Form1"
        Me.Text = "  "
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGoForm2 As System.Windows.Forms.Button

End Class
