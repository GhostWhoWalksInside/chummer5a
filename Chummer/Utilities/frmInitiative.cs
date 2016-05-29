using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chummer
{
    public partial class frmInitiative : Form
    {
        #region Properties
        /// <summary>
        /// The initiative controler for this form
        /// </summary>
        public InitiativeUserControl InitUC
        {
            get { return this.ucInit; }
        }
        #endregion

        public frmInitiative()
        {
            InitializeComponent();
            CenterToParent();
            if (DesignerUtil.InDesignMode())
            {
                return;
            }
            LanguageManager.Instance.Load(GlobalOptions.Instance.Language, this);
        }
    }
}

