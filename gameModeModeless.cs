using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlevgaardA_Lab02_Missiles
{
    public delegate void delVoidBool(bool value);

    public partial class gameModeModeless : Form
    {
        public delVoidVoid _dformClosing = null;
        public delVoidBool _dBounceChanged = null;
        public delVoidBool _dReloadChanged = null;
        public delVoidBool _dShockChanged = null;

        public gameModeModeless()
        {
            InitializeComponent();
        }

        private void gameModeModeless_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if ( _dformClosing != null)
                {
                    _dformClosing();
                }
                e.Cancel = true;
                Hide();
            }
        }

        private void missileBounceCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (_dBounceChanged != null)
            {
                _dBounceChanged.Invoke(missileBounceCheck.Checked);
            }
        }

        private void reloadRequiredCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (_dReloadChanged != null)
            {
                _dReloadChanged.Invoke(reloadRequiredCheck.Checked);
            }
        }

        private void shockAndAweCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (_dShockChanged != null)
            {
                _dShockChanged.Invoke(shockAndAweCheck.Checked);
            }
        }
    }
}
