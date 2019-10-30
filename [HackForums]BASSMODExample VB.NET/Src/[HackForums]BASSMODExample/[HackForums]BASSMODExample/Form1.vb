Imports System.IO
Imports Un4seen.BassMOD.BassMOD

Public Class Form1

    Dim XmFileData As Byte() = Nothing

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'stoped dll events, for closed app
        BASSMOD_MusicPause()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim Inicializacion As Boolean = BASSMOD_Init(-1, 44100, 0)

            If Inicializacion = False Then
                MsgBox("Error; Failed to Start BassMod")
                End
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DirXMfile As String = openFile()
        If Not DirXMfile = "Error" Then
            TextBox1.Text = DirXMfile
            XmFileData = File.ReadAllBytes(DirXMfile)
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not TextBox1.Text = "" Then
            BASSMOD_MusicLoad(XmFileData, 0, CUInt(XmFileData.Length), BASSMOD_CONSTANTS.BASS_MUSIC_LOOP Or BASSMOD_CONSTANTS.BASS_MUSIC_RAMPS Or BASSMOD_CONSTANTS.BASS_MUSIC_SURROUND Or BASSMOD_CONSTANTS.BASS_MUSIC_CALCLEN)
            BASSMOD_MusicPlay()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BASSMOD_MusicPause()
    End Sub


#Region " BassMod Constanst Structure "

    Friend Structure BASSMOD_CONSTANTS
        Public Const BASS_MUSIC_LOOP As Integer = 4
        Public Const BASS_MUSIC_RAMPS As Integer = 2
        Public Const BASS_MUSIC_SURROUND As Integer = 512
        Public Const BASS_MUSIC_CALCLEN As Integer = 8192
        Public Const BASS_SYNC_END As Integer = 2
    End Structure

#End Region

#Region " Funcs "

    Public Shared Function openFile(Optional ByVal Custom_Filter As String = "Executables (*.xm)|*.xm") As String
        Using ofd As New OpenFileDialog
            With ofd
                .AddExtension = True
                .AutoUpgradeEnabled = True
                .CheckPathExists = True
                .Title = "Selec File"
                .Filter = Custom_Filter
                .FileName = ""
                .RestoreDirectory = True
            End With
            If ofd.ShowDialog() = DialogResult.OK Then
                Return ofd.FileName
            End If
        End Using
        Return "Error"
    End Function

#End Region

End Class
