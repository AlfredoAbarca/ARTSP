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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Source_Dir = New System.Windows.Forms.FolderBrowserDialog()
        Me.txt_source_dir = New System.Windows.Forms.TextBox()
        Me.btn_source_dir = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Dest_Dir = New System.Windows.Forms.FolderBrowserDialog()
        Me.txt_dest_dir = New System.Windows.Forms.TextBox()
        Me.btn_dest_dir = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(408, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ARTSP (Atomic Read Team Simple Parser) is a tool to convert the Att&&&ck framewor" & _
    "k tests published by Red Canary Co. to a simple scripts that you can modify and " & _
    "run depending the Operating System."
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(448, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To use, only follow this instrunctions."
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(448, 24)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "1.- Clone or download the following GitHub repository."
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(15, 110)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(239, 13)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "https://github.com/redcanaryco/atomic-red-team"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(448, 30)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "2.- Once you have downloaded the repo, please select the directory the tests fold" & _
    "ers/files." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      This is commonly named (atomics)."
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ARTSP.My.Resources.Resources.nuclear
        Me.PictureBox1.Location = New System.Drawing.Point(401, 41)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 67)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Source_Dir
        '
        Me.Source_Dir.ShowNewFolderButton = False
        '
        'txt_source_dir
        '
        Me.txt_source_dir.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txt_source_dir.Enabled = False
        Me.txt_source_dir.Location = New System.Drawing.Point(15, 177)
        Me.txt_source_dir.Name = "txt_source_dir"
        Me.txt_source_dir.ReadOnly = True
        Me.txt_source_dir.Size = New System.Drawing.Size(381, 20)
        Me.txt_source_dir.TabIndex = 6
        '
        'btn_source_dir
        '
        Me.btn_source_dir.Location = New System.Drawing.Point(401, 175)
        Me.btn_source_dir.Name = "btn_source_dir"
        Me.btn_source_dir.Size = New System.Drawing.Size(45, 23)
        Me.btn_source_dir.TabIndex = 7
        Me.btn_source_dir.Text = "..."
        Me.btn_source_dir.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(448, 30)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "3.- Select the directory where you gonna save the generated scripts."
        '
        'txt_dest_dir
        '
        Me.txt_dest_dir.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txt_dest_dir.Enabled = False
        Me.txt_dest_dir.Location = New System.Drawing.Point(15, 229)
        Me.txt_dest_dir.Name = "txt_dest_dir"
        Me.txt_dest_dir.ReadOnly = True
        Me.txt_dest_dir.Size = New System.Drawing.Size(381, 20)
        Me.txt_dest_dir.TabIndex = 9
        '
        'btn_dest_dir
        '
        Me.btn_dest_dir.Location = New System.Drawing.Point(401, 226)
        Me.btn_dest_dir.Name = "btn_dest_dir"
        Me.btn_dest_dir.Size = New System.Drawing.Size(45, 23)
        Me.btn_dest_dir.TabIndex = 10
        Me.btn_dest_dir.Text = "..."
        Me.btn_dest_dir.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 261)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(434, 30)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "3.- Select the scripts that you want to generate based on the ""Executor"" param co" & _
    "ntained on YAML definition"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(18, 294)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(110, 17)
        Me.CheckBox1.TabIndex = 12
        Me.CheckBox1.Text = "Bash (Mac/Linux)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(18, 317)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(99, 17)
        Me.CheckBox2.TabIndex = 13
        Me.CheckBox2.Text = "Sh (Mac/Linux)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(144, 294)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(162, 17)
        Me.CheckBox3.TabIndex = 14
        Me.CheckBox3.Text = "Command Prompt (Windows)"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(144, 317)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(130, 17)
        Me.CheckBox4.TabIndex = 15
        Me.CheckBox4.Text = "Powershell (Windows)"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(312, 294)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(114, 17)
        Me.CheckBox5.TabIndex = 16
        Me.CheckBox5.Text = "Manual (Windows)"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 346)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(434, 18)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "4.- Finally"
        '
        'btn_generate
        '
        Me.btn_generate.Location = New System.Drawing.Point(166, 360)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(127, 23)
        Me.btn_generate.TabIndex = 19
        Me.btn_generate.Text = "Generate Scripts"
        Me.btn_generate.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(312, 317)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox6.TabIndex = 20
        Me.CheckBox6.Text = "Manual (Mac/Linux)"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 395)
        Me.Controls.Add(Me.CheckBox6)
        Me.Controls.Add(Me.btn_generate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CheckBox5)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_dest_dir)
        Me.Controls.Add(Me.txt_dest_dir)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btn_source_dir)
        Me.Controls.Add(Me.txt_source_dir)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "ARTSP (By Bl4sph3m 2018)"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Source_Dir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txt_source_dir As System.Windows.Forms.TextBox
    Friend WithEvents btn_source_dir As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Dest_Dir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txt_dest_dir As System.Windows.Forms.TextBox
    Friend WithEvents btn_dest_dir As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btn_generate As System.Windows.Forms.Button
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox

End Class
