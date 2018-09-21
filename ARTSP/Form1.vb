Imports System.IO
Imports System.Text.RegularExpressions

'===========================================================================================================================================
'
'                                                       Atomic Red Team Simple Parser
'
' Author: Alfredo Abarca Barajas
' Compiler: Visual Studio 2010
' Languaje: Visual Basic .NET
' Creation Date: 20/09/2018
' Modification Date: --
' 
' This application parse the Att&ck tests framework published by Atomic Red Team (Red Canary) in format YAML to basic readable scripts files
' depending the type and OS system that corresponds. 
'
' This is under Open Source license, please feel free to modify but always mentioning where do you get the source code ;)  
'
'==========================================================================================================================================

Public Class Form1

#Region "Structures"
    Structure Test_Info
        Dim Test_ID As String
        Dim Test_Description As String
        Dim Atom_Tests() As Atom_Test_Info
    End Structure

    Structure Input_Parameters
        Dim Args_ID As String
        Dim Param_Name As String
        Dim Description As String
        Dim Type As String
        Dim Default_Value As String
    End Structure

    Structure Executor_Info
        Dim Exec_Name As String
        Dim Exec_Command As String
    End Structure

    Structure Atom_Test_Info
        Dim Parent_Test_ID As String
        Dim Atom_Test_ID As String
        Dim Nombre As String
        Dim Description As String
        Dim OS_Supported As String
        Dim Input_Arguments() As Input_Parameters
        Dim Exec_Inf() As Executor_Info
        Dim Has_Args As Boolean
    End Structure
#End Region

#Region "Enumerations"
    Enum Script_Type
        command_prompt
        powershell
        sh
        bash
        manual
    End Enum

    Enum Os_Name
        windows
        linux
        macos
        centos
        ubuntu
    End Enum
#End Region

#Region "Global Variables"
    Dim Test_Arr() As Test_Info
    Dim Atom_Test_Arr() As Atom_Test_Info
    Dim Test_Index As Integer = 1
    Dim Source_Directory As String = ""
    Dim Dest_Directory As String = ""
#End Region

    Private Sub btn_source_dir_Click(sender As System.Object, e As System.EventArgs) Handles btn_source_dir.Click
        If Source_Dir.ShowDialog = Windows.Forms.DialogResult.OK Then
            txt_source_dir.Text = Source_Dir.SelectedPath
        End If
    End Sub


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Source_Dir.Description = "Select the folder that contains all the tests folders (atomics folder)."
        Dest_Dir.Description = "Select the folder where you gonna save the output scripts."
    End Sub

    Private Sub btn_dest_dir_Click(sender As System.Object, e As System.EventArgs) Handles btn_dest_dir.Click
        If Dest_Dir.ShowDialog = Windows.Forms.DialogResult.OK Then
            txt_dest_dir.Text = Dest_Dir.SelectedPath
        End If
    End Sub

    Private Sub btn_generate_Click(sender As System.Object, e As System.EventArgs) Handles btn_generate.Click
        If txt_source_dir.Text.Length > 0 Then
            If txt_dest_dir.Text.Length > 0 Then
                If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Or CheckBox5.Checked = True Or CheckBox6.Checked = True Then
                    Parse_Files()
                    MsgBox("Please check the files generated on " & Dest_Directory, MsgBoxStyle.Information, "Finished")
                Else
                    MsgBox("You must select at least one type of output script...", MsgBoxStyle.Critical, AcceptButton)
                End If
            Else
                MsgBox("You must select the directory where the output scripts gonna be saved...", MsgBoxStyle.Critical, AcceptButton)
            End If
        Else
            MsgBox("You must select the directory that contains Atomic tests definitions...", MsgBoxStyle.Critical, AcceptButton)
        End If

    End Sub

#Region "Processing Functions/subs"
    Private Sub Parse_Files()
        Dim SubDir As String
        Dim Dir_arr() As String

        Source_Directory = txt_source_dir.Text
        Dest_Directory = txt_dest_dir.Text

        For Each SubDir In Directory.GetDirectories(Source_Directory)
            If Regex.IsMatch(SubDir, "\\T[0-9]{4}") = True Or Regex.IsMatch(SubDir, "\\RC[0-9]{5}") = True Then
                'This validates that the subdirectories has the normalized name given for Red Canary Atomic Read Team
                Dim FileName As String
                Dir_arr = SubDir.Split("\")
                FileName = Dir_arr(Dir_arr.Length - 1)
                FileName += ".yaml"
                Console.WriteLine("Processing file " & FileName)
                Disect_RC_Yaml_File(SubDir & "\" & FileName)
            End If
        Next

        'Salva las información a un archivo de texto 
        Dim Test_Index As StreamWriter
        Dim Atom_Tests As StreamWriter
        Dim Atom_Args As StreamWriter
        Dim Test_Cursor As Integer = 0
        Dim Atom_Cursor As Integer = 0
        Dim AtomA_Cursor As Integer = 0
        Dim Exec_Cursor As Integer = 0
        Dim HasArgs As Boolean

        Test_Index = My.Computer.FileSystem.OpenTextFileWriter(Dest_Directory & "\Tests.csv", False)
        Atom_Tests = My.Computer.FileSystem.OpenTextFileWriter(Dest_Directory & "\Atoms.csv", False)
        Atom_Args = My.Computer.FileSystem.OpenTextFileWriter(Dest_Directory & "\Args.csv", False)

        For Test_Cursor = 0 To Test_Arr.Length - 2
            'Va a recorrer cada una de las pruebas 
            Test_Index.WriteLine(Test_Arr(Test_Cursor).Test_ID & "," & Test_Arr(Test_Cursor).Test_Description)
            For Atom_Cursor = 0 To Test_Arr(Test_Cursor).Atom_Tests.Length - 2
                HasArgs = Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Has_Args


                For Exec_Cursor = 0 To Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf.Length - 2

                    'Este pedazo de codigo va a reemplazar todas las variables por su valor por defecto (en caso de que tenga), en las lineas de comandos de cada una de las pruebas
                    If Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Has_Args = True Then
                        For AtomA_Cursor = 0 To Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments.Length - 2
                            Dim ParamName As String
                            ParamName = "#{" & Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Param_Name & "}"
                            Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Command = Replace(Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Command, ParamName, Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Default_Value.ToString)

                        Next
                    End If
                    '====Fin de reemplaza las variables 

                    Atom_Tests.WriteLine(Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Parent_Test_ID & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Atom_Test_ID & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Nombre & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Description & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).OS_Supported & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Has_Args.ToString & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Name & "," & _
                                         Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Command)
                Next

                If HasArgs = True Then
                    For AtomA_Cursor = 0 To Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments.Length - 2
                        Atom_Args.WriteLine(Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Args_ID & "," & _
                                            Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Param_Name & "," & _
                                            Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Description & "," & _
                                            Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Type & "," & _
                                            Test_Arr(Test_Cursor).Atom_Tests(Atom_Cursor).Input_Arguments(AtomA_Cursor).Default_Value)
                    Next
                End If

            Next
        Next

        Test_Index.Close()
        Atom_Tests.Close()
        Atom_Args.Close()

        'Create the scripts files depending the options that has been selected
        If CheckBox1.Checked = True Then
            'If the option of create the Bash script for MAC/Linux has been selected...
            Create_Script_File(Script_Type.bash, Os_Name.macos, Test_Arr)
            Create_Script_File(Script_Type.bash, Os_Name.linux, Test_Arr)
            Create_Script_File(Script_Type.bash, Os_Name.centos, Test_Arr)
            Create_Script_File(Script_Type.bash, Os_Name.ubuntu, Test_Arr)
        End If
        If CheckBox2.Checked = True Then
            'If the option of create the SH script for MAC/Linux has been selected...
            Create_Script_File(Script_Type.sh, Os_Name.macos, Test_Arr)
            Create_Script_File(Script_Type.sh, Os_Name.linux, Test_Arr)
            Create_Script_File(Script_Type.sh, Os_Name.centos, Test_Arr)
            Create_Script_File(Script_Type.sh, Os_Name.ubuntu, Test_Arr)
        End If
        If CheckBox3.Checked = True Then
            'If the option is related to create a command prompt script.
            Create_Script_File(Script_Type.command_prompt, Os_Name.windows, Test_Arr)
        End If
        If CheckBox4.Checked = True Then
            'If the option is related to create a powershell script.
            Create_Script_File(Script_Type.powershell, Os_Name.windows, Test_Arr)
        End If
        If CheckBox5.Checked = True Then
            'If the user has selected to create manual instructions for Windows Operating System
            Create_Script_File(Script_Type.manual, Os_Name.windows, Test_Arr)
        End If
        If CheckBox6.Checked = True Then
            'If the user has selected to create manual instructions for *NIX OS
            Create_Script_File(Script_Type.manual, Os_Name.macos, Test_Arr)
            Create_Script_File(Script_Type.manual, Os_Name.linux, Test_Arr)
            Create_Script_File(Script_Type.manual, Os_Name.centos, Test_Arr)
            Create_Script_File(Script_Type.manual, Os_Name.ubuntu, Test_Arr)
        End If

    End Sub

    Private Sub Create_Script_File(Script_Type As Script_Type, OS_Type As Os_Name, Test_Array() As Test_Info)
        'Esta función va a generar un archivo de script para ejecutar las pruebas de manera secuencial de acuerdo al parámetro de su ejecutro 
        Dim FilePath As String
        Dim ScriptWriter As StreamWriter
        Dim Test_Cursor As Integer = 0
        Dim Atom_Cursor As Integer = 0
        Dim AtomA_Cursor As Integer = 0
        Dim Exec_Cursor As Integer = 0
        Dim Executor As String
        Dim OS_Family As String
        Dim Test_ID As String
        Dim Test_Description As String
        Dim Parsed_Command() As String
        Dim x As Integer

        Try
            If Not Test_Array Is Nothing Then

                Select Case OS_Type
                    Case Os_Name.centos
                        OS_Family = "centos"
                    Case Os_Name.linux
                        OS_Family = "linux"
                    Case Os_Name.macos
                        OS_Family = "macos"
                    Case Os_Name.ubuntu
                        OS_Family = "ubuntu"
                    Case Os_Name.windows
                        OS_Family = "windows"
                End Select


                Select Case Script_Type
                    Case Script_Type.bash
                        Executor = "bash"
                        FilePath = Dest_Directory & "\" & OS_Family & "_" & "Attack_Tests_Bash.sh"
                        ScriptWriter = My.Computer.FileSystem.OpenTextFileWriter(FilePath, False)
                        ScriptWriter.WriteLine("#!/bin/bash")
                        ScriptWriter.WriteLine("")
                    Case Script_Type.command_prompt
                        Executor = "command_prompt"
                        FilePath = Dest_Directory & "\" & OS_Family & "_" & "Attack_Tests.bat"
                        ScriptWriter = My.Computer.FileSystem.OpenTextFileWriter(FilePath, False)
                        ScriptWriter.WriteLine("@echo off")
                        ScriptWriter.WriteLine("@echo off")
                        ScriptWriter.WriteLine("")
                    Case Script_Type.powershell
                        Executor = "powershell"
                        FilePath = Dest_Directory & "\" & OS_Family & "_" & "Attack_Tests.ps1"
                        ScriptWriter = My.Computer.FileSystem.OpenTextFileWriter(FilePath, False)
                    Case Script_Type.sh
                        Executor = "sh"
                        FilePath = Dest_Directory & "\" & OS_Family & "_" & "Attack_Tests_Sh.sh"
                        ScriptWriter = My.Computer.FileSystem.OpenTextFileWriter(FilePath, False)
                        ScriptWriter.WriteLine("#!/bin/sh")
                        ScriptWriter.WriteLine("")
                    Case Script_Type.manual
                        Executor = "manual"
                        FilePath = Dest_Directory & "\" & OS_Family & "_" & "Attack_Tests_Manual.txt"
                        ScriptWriter = My.Computer.FileSystem.OpenTextFileWriter(FilePath, False)
                End Select



                For Test_Cursor = 0 To Test_Array.Length - 2
                    Test_ID = Test_Array(Test_Cursor).Test_ID
                    Test_Description = Test_Array(Test_Cursor).Test_Description
                    For Atom_Cursor = 0 To Test_Array(Test_Cursor).Atom_Tests.Length - 2
                        For Exec_Cursor = 0 To Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf.Length - 2
                            If InStr(Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).OS_Supported, OS_Family) > 0 And InStr(Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Name, Executor) > 0 Then
                                'Si tanto la familia del S.O. como el tipo de script coinciden, entonces se procederá a generar el archivo seleccionado 
                                ' Es importante que la prueba de Mitre tenga contemplado el tipo de comando a ejecutar de acuerdo al script seleccionado. 
                                ScriptWriter.WriteLine("echo " & """" & "MITRE TEST ID: " & Test_ID & """")
                                ScriptWriter.WriteLine("echo " & """" & "TEST DESCRIPTION: " & Test_ID & """")
                                ScriptWriter.WriteLine("echo " & """" & "<TAB>ATOM TEST NAME: " & Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).Nombre & """")
                                ScriptWriter.WriteLine("echo " & """" & "<TAB>ATOM TEST DESCRIPTION: " & Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).Description & """")
                                ScriptWriter.WriteLine("echo " & """""")
                                ScriptWriter.WriteLine("echo " & """" & "<TAB>STARTING ATOM TEST: " & """")
                                ScriptWriter.WriteLine("echo " & """""")
                                Parsed_Command = Test_Array(Test_Cursor).Atom_Tests(Atom_Cursor).Exec_Inf(Exec_Cursor).Exec_Command.ToString.Split(New String() {"||"}, StringSplitOptions.RemoveEmptyEntries)
                                For x = 0 To Parsed_Command.Length - 1
                                    'En este segmento de código se hacen las consideraciones adicionales de acuerdo a los tipos de script
                                    If Script_Type = Form1.Script_Type.command_prompt Then
                                        Parsed_Command(x) = Replace(Parsed_Command(x), "%", "%%")
                                        Parsed_Command(x) = Replace(Parsed_Command(x), "cmd.exe", "")
                                    End If
                                    ScriptWriter.WriteLine(Replace(Parsed_Command(x), "@comma@", ","))
                                Next
                                ScriptWriter.WriteLine("echo " & """""")
                            End If
                        Next

                    Next
                Next
                ScriptWriter.Close()

            Else
                MsgBox("No está inicializado el arreglo de pruebas extraído de los archivos")
            End If

        Catch ex As Exception
            MsgBox("Error en la ejecución del código " & ex.Message.ToString)
        End Try
    End Sub

    Sub Disect_RC_Yaml_File(FilePath As String)
        'Esta función va a realizar la interpretación del archivo YAML del repositorio de definiciones de Red Canary 
        'https://github.com/redcanaryco/atomic-red-team/blob/master/atomics
        Dim Temp_Record As String = ""
        Dim Split_Record() As String
        Dim Test_ID As String = ""

        Try
            If File.Exists(FilePath) = True Then
                Dim FReader As StreamReader
                FReader = My.Computer.FileSystem.OpenTextFileReader(FilePath)
                Dim txt_line As String
                txt_line = FReader.ReadLine 'Lee la primer linea del archivo Yaml que debe corresponder a "---" 
                If StrComp(txt_line, "---", CompareMethod.Text) = 0 Then
                    Do While FReader.Peek() > -1
                        txt_line = FReader.ReadLine
                        If InStr(txt_line, "attack_technique:") > 0 Then
                            ReDim Preserve Test_Arr(Test_Index)
                            ReDim Atom_Test_Arr(0)
                            Temp_Record = ""
                            Split_Record = txt_line.Split(": ")

                            Test_ID = Strings.Right(Split_Record(1).ToString, Split_Record(1).Length - 1)
                            Test_Arr(Test_Index - 1).Test_ID = Trim(Split_Record(1))
                            txt_line = FReader.ReadLine
                            Split_Record = txt_line.Split(": ")
                            Temp_Record = Strings.Right(Split_Record(1).ToString, Split_Record(1).Length - 1)
                            Test_Arr(Test_Index - 1).Test_Description = LTrim(Temp_Record) 'Se almacena el registro de la prueba (general en el arreglo principal) 
                            Temp_Record = ""
                            Test_Index += 1
                            'Hasta aqui leerá la tercera linea del archivo

                        End If

                        If txt_line.Length = 0 Then
                            'Si es una linea vacía, no hará ningun procesamiento

                        End If

                        If InStr(txt_line, "atomic_tests:") Then
                            'Si la linea que está leyendo representa el inicio del desgloce de las pruebas atómicas, entonces comenzará a procesar 
                            'cada una de ellas por medio de la siguiente función. 
                            Process_Atomic_Test(Test_ID, FReader)
                            Test_Arr(Test_Index - 2).Atom_Tests = Atom_Test_Arr
                        End If
                    Loop
                Else
                    MsgBox("El archivo " & FilePath & " parece no tener la estructura básica necesaria o no es un archivo válido ")
                End If

                FReader.Close()
            Else
                MsgBox("El archivo " & FilePath & " no existe o no puede ser leído")
            End If
        Catch ex As Exception
            MsgBox("Excepción en la ejecucion " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub Process_Atomic_Test(Test_ID As String, ByRef FReader As StreamReader)
        'Esta función procesará cada una de las pruebas atómicas incluidas en cada uno de los test genericos de cada archivo 
        Dim Temp_Record As String = ""
        Dim Split_Record() As String
        Dim txt_line As String = ""
        Dim Atom_Test_ID As String = ""
        Dim Atom_Counter As Integer = 0
        Dim Inp_Args_Counter As Integer = 0
        Dim Execs_Counter As Integer = 0

        Try
            If Not FReader Is Nothing Then
                txt_line = FReader.ReadLine
                Do While FReader.Peek() > -1

                    If txt_line.Length = 0 Then
                        'Si se trata de una linea en blanco, no será procesada y solo se procederá a la siguiente linea
                        txt_line = FReader.ReadLine
                    End If

                    If (txt_line.Length > 0 And InStr(txt_line, "name:") = 0 And InStr(txt_line, "description:") = 0 And InStr(txt_line, "supported_platforms:") = 0 And InStr(txt_line, "executor:") = 0 And InStr(txt_line, "input_arguments:") = 0) Then
                        'Si se trata de una linea que sale del esquema normal de un archivo YAML, simplemente se ignorará
                        txt_line = FReader.ReadLine
                    End If

                    If InStr(txt_line, "description: |") > 0 Then
                        txt_line = FReader.ReadLine
                        Do While InStr(txt_line, "supported_platforms:") = 0
                            Temp_Record += LTrim(txt_line) & "|"
                            txt_line = FReader.ReadLine 'El indicador anterior, señala que la descripción puede tener mas de una linea
                            ' Y por lo tanto comienza en la linea siguiente. 
                        Loop
                        Atom_Test_Arr(Atom_Counter - 1).Description = LTrim(Temp_Record.Replace(",", "@@"))
                    ElseIf InStr(txt_line, "description: ") > 0 Then
                        Split_Record = txt_line.Split(": ")
                        Temp_Record = Split_Record(1) ' Se guarda la descripcion de la prueba contenida en el archivo
                        Atom_Test_Arr(Atom_Counter - 1).Description = LTrim(Temp_Record.Replace(",", "@@"))
                        txt_line = FReader.ReadLine
                    End If

                    If InStr(txt_line, "- name:") > 0 Then
                        'Se trata de la línea que indica el nombre de una prueba atómica, entonces comenzará el proceso de la misma
                        Inp_Args_Counter = 0
                        Execs_Counter = 0
                        If Atom_Counter = 0 Then
                            Atom_Counter += 1
                            ReDim Preserve Atom_Test_Arr(Atom_Counter)
                        Else
                            If Atom_Test_Arr(Atom_Counter - 1).Nombre.Length > 0 Then
                                Atom_Counter += 1
                                ReDim Preserve Atom_Test_Arr(Atom_Counter)
                                Atom_Test_Arr(Atom_Counter - 1).Nombre = ""
                            End If
                        End If

                        Atom_Test_Arr(Atom_Counter - 1).Has_Args = False
                        Split_Record = txt_line.Split(": ")
                        Atom_Test_Arr(Atom_Counter - 1).Nombre = Trim(Split_Record(1).ToString) ' Nombre de la prueba atomica
                        Atom_Test_Arr(Atom_Counter - 1).Parent_Test_ID = Test_ID
                        Atom_Test_Arr(Atom_Counter - 1).Atom_Test_ID = Test_ID & "_ATT_" & Atom_Counter 'Se genera el ID de la prueba asociado al del test principal 
                        txt_line = FReader.ReadLine
                        Temp_Record = ""
                        If InStr(txt_line, "description: |") > 0 Then
                            txt_line = FReader.ReadLine
                            Do While InStr(txt_line, "supported_platforms:") = 0
                                Temp_Record += LTrim(txt_line) & "|"
                                txt_line = FReader.ReadLine 'El indicador anterior, señala que la descripción puede tener mas de una linea
                                ' Y por lo tanto comienza en la linea siguiente. 
                            Loop
                            Atom_Test_Arr(Atom_Counter - 1).Description = LTrim(Temp_Record.Replace(",", "@@"))
                        ElseIf InStr(txt_line, "description: ") > 0 Then
                            Split_Record = txt_line.Split(": ")
                            Temp_Record = Split_Record(1) ' Se guarda la descripcion de la prueba contenida en el archivo
                            Atom_Test_Arr(Atom_Counter - 1).Description = LTrim(Temp_Record.Replace(",", "@@"))
                        End If

                        'txt_line = FReader.ReadLine
                    End If

                    If InStr(txt_line, "supported_platforms:") > 0 Then
                        'Se comenzará a leer el listado de Sistemas Operativos que estan soportados para esta prueba
                        Temp_Record = ""
                        txt_line = FReader.ReadLine
                        Do While InStr(txt_line, "-") > 0
                            Temp_Record += Strings.Right(Trim(txt_line), Trim(txt_line).Length - 2) & "|"
                            txt_line = FReader.ReadLine
                        Loop
                        Atom_Test_Arr(Atom_Counter - 1).OS_Supported = Temp_Record
                        'txt_line = FReader.ReadLine
                    End If



                    If InStr(txt_line, "executor:") > 0 Then
                        'Se traduce la estructura del ejecutor que se requiere para realizar la prueba en el sistema operativo
                        '============================
                        Execs_Counter += 1
                        ReDim Preserve Atom_Test_Arr(Atom_Counter - 1).Exec_Inf(Execs_Counter)
                        txt_line = FReader.ReadLine
                        Split_Record = txt_line.Split(": ")
                        Atom_Test_Arr(Atom_Counter - 1).Exec_Inf(Execs_Counter - 1).Exec_Name = Trim(Split_Record(1)) 'Extrae el nombre del ejecutor de la prueba (manual, powershell, sh, command...) 
                        txt_line = FReader.ReadLine 'Lee el comienzo de los comandos / pasos a ejecutar 
                        Temp_Record = ""
                        If InStr(txt_line, "|") > 0 Then
                            'Si la fila contiene el caracter pipe, entonces se trata de multiples comandos/pasos 
                            txt_line = FReader.ReadLine
                            Do While (InStr(txt_line, "name:") = 0 And Not txt_line Is Nothing And InStr(txt_line, "executor:") = 0)
                                '==================VALIDAR EL USO DEL #
                                If txt_line.Length = 0 Then
                                    Temp_Record += "||"
                                Else
                                    Temp_Record += LTrim(txt_line) & "||"
                                End If
                                txt_line = FReader.ReadLine
                            Loop
                        Else
                            'Si el comando, está escrito en la misma linea entonces solo se leera esa linea
                            Split_Record = txt_line.Split(": ")
                            Temp_Record += LTrim(Split_Record(1))
                        End If
                        Atom_Test_Arr(Atom_Counter - 1).Exec_Inf(Execs_Counter - 1).Exec_Command = LTrim(Temp_Record.Replace(",", "@comma@"))
                    End If

                    'Falta interpretar los parametros de entrada/salida 

                    If (InStr(txt_line, "input_arguments:") > 0 And Not txt_line Is Nothing) Then
                        'Se tratan los argumentos de entrada 

                        txt_line = FReader.ReadLine  'Lee el nombre del primer parametro
                        Do While (txt_line.Trim.Length <> 0 And txt_line.Length <> 0 And InStr(txt_line, "executor:") = 0 And InStr(txt_line, "- name:") = 0)
                            'Mientras no sea un salto de linea, estará leyendo cada 4 renglones un parametro distinto. 
                            Inp_Args_Counter += 1
                            ReDim Preserve Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter)
                            Atom_Test_Arr(Atom_Counter - 1).Has_Args = True
                            Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Args_ID = Atom_Test_Arr(Atom_Counter - 1).Atom_Test_ID & "_P_" & Inp_Args_Counter
                            Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Param_Name = LTrim(Strings.Left(txt_line, txt_line.Length - 1))
                            txt_line = FReader.ReadLine  'Lee la descripcion del parametro
                            If InStr(txt_line, "|") > 0 Then
                                'Si la descripción está en la siguiente linea (description: |) , entonces se salta una linea mas y se lee el contenido de la misma
                                txt_line = FReader.ReadLine
                                Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Description = LTrim(txt_line.Replace(",", "@comma@"))
                            Else
                                'Si la descripción del parámetro está en la misma linea, entonces simplemente se hace un split de la cadena para obtener la misma.
                                Split_Record = txt_line.Split(": ")
                                Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Description = LTrim(Split_Record(1).Replace(",", "@comma@"))
                            End If
                            txt_line = FReader.ReadLine  'Lee el tipo de parametro
                            Split_Record = txt_line.Split(": ")
                            Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Type = LTrim(Split_Record(1))
                            txt_line = FReader.ReadLine  'Lee el valor por defecto de este parametro
                            If InStr(txt_line, "default") > 0 Then
                                'Si existe un valor por defecto para ese parámetro, entonces se procesará como tal, de otra forma
                                'se inicializa el valor del registro a NONE  y se continúa con el análisis
                                Split_Record = Regex.Split(Trim(txt_line), "\W+: ")
                                Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Default_Value = LTrim(Split_Record(0).Replace("default:", ""))
                                txt_line = FReader.ReadLine
                            Else
                                Atom_Test_Arr(Atom_Counter - 1).Input_Arguments(Inp_Args_Counter - 1).Default_Value = "NONE"
                            End If

                        Loop
                    End If

                Loop

            End If
        Catch ex As Exception
            MsgBox("Excepción en la ejecucion " & ex.Message.ToString)
        End Try

    End Sub
#End Region


    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            LinkLabel1.LinkVisited = True
            System.Diagnostics.Process.Start(LinkLabel1.Text)
        Catch ex As Exception
            MsgBox("The website " & LinkLabel1.Text & " cannot be opened in the default browser.")
        End Try
    End Sub


End Class
