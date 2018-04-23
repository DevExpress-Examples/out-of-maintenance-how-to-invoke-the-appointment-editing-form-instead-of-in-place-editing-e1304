Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler.UI
Imports DevExpress.XtraScheduler
Imports DevExpress.Services
Imports DevExpress.XtraScheduler.Services.Implementation
Imports DevExpress.XtraScheduler.Commands

Namespace HowTo
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim oldService As SchedulerKeyboardHandlerService = CType(schedulerControl1.Services.GetService(GetType(IKeyboardHandlerService)), SchedulerKeyboardHandlerService)
			schedulerControl1.Services.RemoveService(GetType(IKeyboardHandlerService))
			Dim myKeyboardService As New MySchedulerKeyboardHandlerService(oldService)
			schedulerControl1.Services.AddService(GetType(IKeyboardHandlerService), myKeyboardService)
		End Sub
	End Class
	Public Class MySchedulerKeyboardHandlerService
		Implements IKeyboardHandlerService
		Private oldService_Renamed As SchedulerKeyboardHandlerService
		Public Sub New(ByVal oldService As SchedulerKeyboardHandlerService)
			Me.oldService_Renamed = oldService
		End Sub
		Public ReadOnly Property OldService() As SchedulerKeyboardHandlerService
			Get
				Return oldService_Renamed
			End Get
		End Property
		#Region "IKeyboardHandlerService Members"
		Public Sub OnKeyDown(ByVal e As KeyEventArgs) Implements IKeyboardHandlerService.OnKeyDown
			OldService.OnKeyDown(e)
		End Sub
		Public Sub OnKeyPress(ByVal e As KeyPressEventArgs) Implements IKeyboardHandlerService.OnKeyPress
			Dim modifier As Keys = Form1.ModifierKeys
			If (modifier And Keys.Alt) = 0 AndAlso (modifier And Keys.Control) = 0 Then
				Dim control As SchedulerControl = oldService_Renamed.Control
				Dim command As SchedulerCommand = Nothing
				If control.SelectedAppointments.Count <= 0 Then
					command = New NewAppointmentCommand(control, control)
				ElseIf control.SelectedAppointments.Count = 1 Then
					command = New EditAppointmentWinCommand(control, control)
				End If
				If command IsNot Nothing Then
					e.Handled = True
					command.Execute()
				End If
			Else
				OldService.OnKeyPress(e)
			End If
		End Sub
		Public Sub OnKeyUp(ByVal e As KeyEventArgs) Implements IKeyboardHandlerService.OnKeyUp
			OldService.OnKeyUp(e)
		End Sub
		#End Region
	End Class
End Namespace
