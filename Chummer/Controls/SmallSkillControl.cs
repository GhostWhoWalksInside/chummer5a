﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chummer
{
    public partial class SmallSkillControl : UserControl
    {
        private ISkill _objSkill;
        private readonly Form _objParent;
        
        /// <summary>
        /// Fired when the user decides to roll the dice
        /// </summary>
        public event EventHandler DiceClick
        {
            add { cmdRoll.Click += value; }
            remove { cmdRoll.Click -= value; }
        }

        /// <summary>
        /// The skill information
        /// </summary>
        public ISkill Skill 
        {
            get { return _objSkill; }
            set
            {
                _objSkill = value;
                lblSkillName.Text = Skill.DisplayName + " : " + Skill.TotalRating.ToString();
            }
        }

        public bool ShowSpecialization
        {
            get 
            {
                return chkUseSpecial.Visible;
            }
            set 
            {
                chkUseSpecial.Visible = value;
                if (chkUseSpecial.Visible)
                    Width = chkUseSpecial.Left + chkUseSpecial.Width;
                else if (cmdRoll.Visible)
                    Width = cmdRoll.Left + cmdRoll.Width;
                else
                    Width = lblSkillName.Left + lblSkillName.Width;
            }
        }

        public bool ShowDiceRoller
        {
            get
            {
                return cmdRoll.Visible;
            }
            set
            {
                cmdRoll.Visible = value;
                if (chkUseSpecial.Visible)
                    Width = chkUseSpecial.Left + chkUseSpecial.Width;
                else if (cmdRoll.Visible)
                    Width = cmdRoll.Left + cmdRoll.Width;
                else
                    Width = lblSkillName.Left + lblSkillName.Width;
            }
        }

        public SmallSkillControl(Form parent)
        {
            _objParent = parent;
            InitializeComponent();
            // setup controls
        }

        private void cmdRoll_Click(object sender, EventArgs e)
        {
            // pass the appropriate information onto the dice roller
            if (_objParent is frmGMDashboard)
            {
                frmGMDashboard dash = _objParent as frmGMDashboard;
                dash.DiceRoller.NumberOfDice = chkUseSpecial.Checked ? Skill.TotalRating + 2 : Skill.TotalRating;
                // apply appropriate limit here
                dash.DiceRoller.EdgeUse = DiceRollerControl.EdgeUses.None;
                dash.DiceRoller.NumberOfEdge = Convert.ToInt32(((Attribute)dash.CurrentNPC.EDG).TotalValue);
            }
            else
            {
                // we have the individual player's skill's
                frmPlayerDashboard dash = _objParent as frmPlayerDashboard;
                dash.DiceRoller.NumberOfDice = chkUseSpecial.Checked ? Skill.TotalRating + 2 : Skill.TotalRating;
                dash.DiceRoller.EdgeUse = DiceRollerControl.EdgeUses.None;
                dash.DiceRoller.NumberOfEdge = Convert.ToInt32(dash.CurrentNPC.EDG);
            }
        }

        private void lblSkillName_Click(object sender, EventArgs e)
        {
            string strBook = _objSkill.Source + " " + _objSkill.Page;
            CommonFunctions objCommon = new CommonFunctions();
            objCommon.OpenPDF(strBook);
        }
    }
}
