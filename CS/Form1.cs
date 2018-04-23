using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler;
using DevExpress.Services;
using DevExpress.XtraScheduler.Services.Implementation;
using DevExpress.XtraScheduler.Commands;

namespace HowTo {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        void Form1_Load(object sender, EventArgs e) {
            SchedulerKeyboardHandlerService oldService = (SchedulerKeyboardHandlerService)schedulerControl1.Services.GetService(typeof(IKeyboardHandlerService));
            schedulerControl1.Services.RemoveService(typeof(IKeyboardHandlerService));
            MySchedulerKeyboardHandlerService myKeyboardService = new MySchedulerKeyboardHandlerService(oldService);
            schedulerControl1.Services.AddService(typeof(IKeyboardHandlerService), myKeyboardService);
        }
    }
    public class MySchedulerKeyboardHandlerService : IKeyboardHandlerService {
        SchedulerKeyboardHandlerService oldService;
        public MySchedulerKeyboardHandlerService(SchedulerKeyboardHandlerService oldService) {
            this.oldService = oldService;
        }
        public SchedulerKeyboardHandlerService OldService { get { return oldService; } }
        #region IKeyboardHandlerService Members
        public void OnKeyDown(KeyEventArgs e) {
            OldService.OnKeyDown(e);
        }
        public void OnKeyPress(KeyPressEventArgs e) {
            Keys modifier = Form1.ModifierKeys;
            if ((modifier & Keys.Alt) == 0 && (modifier & Keys.Control) == 0) {
                SchedulerControl control = oldService.Control;
                SchedulerCommand command = null;
                if (control.SelectedAppointments.Count <= 0)
                    command = new NewAppointmentCommand(control, control);
                else if (control.SelectedAppointments.Count == 1)
                    command = new EditAppointmentWinCommand(control, control);
                if (command != null) {
                    e.Handled = true;
                    command.Execute();
                }
            } else
                OldService.OnKeyPress(e);
        }
        public void OnKeyUp(KeyEventArgs e) {
            OldService.OnKeyUp(e);
        }
        #endregion
    }
}
