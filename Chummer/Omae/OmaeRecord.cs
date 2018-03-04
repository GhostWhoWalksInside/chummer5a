/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */
﻿using System;
using System.Windows.Forms;
using System.Xml;

// OmaeDownloadClicked Event Handler.
public delegate void OmaeDownloadClickedHandler(Object sender);
public delegate void OmaePostUpdateClickedHandler(Object sender);
public delegate void OmaeDeleteClickedHandler(Object sender);

namespace Chummer
{
	public partial class OmaeRecord : UserControl
	{
		// Events.
		public event OmaeDownloadClickedHandler OmaeDownloadClicked;
		public event OmaePostUpdateClickedHandler OmaePostUpdateClicked;
		public event OmaeDeleteClickedHandler OmaeDeleteClicked;

		private readonly int _intCharacterID = 0;
		private readonly string _strCharacterName = "";
		private readonly int _intCharacterType = 0;

		#region Control Events
		public OmaeRecord(XmlNode objNode, int intTypeID, OmaeMode objMode)
		{
			InitializeComponent();
			LanguageManager.Instance.Load(GlobalOptions.Instance.Language, this);

			// Populate the basic information.
			_intCharacterID = Convert.ToInt32(objNode["id"].InnerText);
			_strCharacterName = objNode["name"].InnerText;
			lblCharacterName.Text = objNode["name"].InnerText;
			lblUser.Text = objNode["user"].InnerText;
			if (objNode["description"].InnerText == "")
				lblDescription.Text = LanguageManager.Instance.GetString("Omae_NoDescription");
			else
				lblDescription.Text = objNode["description"].InnerText;
			DateTime datDate = DateTime.Parse(objNode["date"].InnerText, GlobalOptions.Instance.CultureInfo);
			lblDate.Text = LanguageManager.Instance.GetString("Omae_UpdatedDate") + " " + datDate.ToShortDateString();
			lblCount.Text = LanguageManager.Instance.GetString("Omae_DownloadCount").Replace("{0}", objNode["count"].InnerText);

			if (objMode == OmaeMode.Character)
			{
				// Character-specific information.
				string strMetatype = objNode["metatype"].InnerText;
				if (objNode["metavariant"].InnerText != "")
					strMetatype += "(" + objNode["metavariant"].InnerText;
				lblMetatype.Text = LanguageManager.Instance.GetString("Label_Metatype") + " " + strMetatype;
			}
			else if (objMode == OmaeMode.Data)
			{
				// Data-specific information.
				lblMetatype.Text = "";
				string[] strFileList = objNode["filesincluded"].InnerText.Split(',');
				string strOverride = "";
				string strCustom = "";

				for (int i = 0; i <= strFileList.Length - 2; i++)
				{
					string[] strParts = strFileList[i].Split('_');
					if (strFileList[i].StartsWith("override"))
						strOverride += strParts[strParts.Length - 1] + ", ";
					else
						strCustom += strParts[strParts.Length - 1] + ", ";
				}

				// Remove the trailing commas from the strings.
				if (strOverride != string.Empty)
					strOverride = strOverride.Substring(0, strOverride.Length - 2);
				if (strCustom != string.Empty)
					strCustom = strCustom.Substring(0, strCustom.Length - 2);

				if (strCustom != string.Empty)
					lblMetatype.Text += "Custom: " + strCustom;
				if (lblMetatype.Text != string.Empty)
					lblMetatype.Text += ".   ";
				if (strOverride != string.Empty)
					lblMetatype.Text += "Override: " + strOverride;
			}
			else if (objMode == OmaeMode.Sheets)
			{
				lblMetatype.Text = "";
			}
			_intCharacterType = intTypeID;

			// This should check to see if the character exists in the user's Omae save directory.
			// If it does, check the dates. If the last update date is the same or older than the file's current date, don't enable the download button.
			// If the file does not exist, or the last update date is newer than the file's date, enable the download button.
		}

		private void OmaeRecord_Load(object sender, EventArgs e)
		{
			this.Width = cmdDownload.Left + cmdDownload.Width + 6;
		}

		private void cmdDownload_Click(object sender, EventArgs e)
		{
			OmaeDownloadClicked(this);
		}

		private void cmdPostUpdate_Click(object sender, EventArgs e)
		{
			OmaePostUpdateClicked(this);
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			OmaeDeleteClicked(this);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Character ID.
		/// </summary>
		public int CharacterID
		{
			get
			{
				return _intCharacterID;
			}
		}

		/// <summary>
		/// Character name.
		/// </summary>
		public string CharacterName
		{
			get
			{
				return _strCharacterName;
			}
		}

		/// <summary>
		/// User name.
		/// </summary>
		public string UserName
		{
			get
			{
				return lblUser.Text;
			}
		}

		/// <summary>
		/// Description.
		/// </summary>
		public string Description
		{
			get
			{
				return lblDescription.Text;
			}
		}

		/// <summary>
		/// Character type.
		/// </summary>
		public int CharacterType
		{
			get
			{
				return _intCharacterType;
			}
		}

		/// <summary>
		/// Whether or not the Character is owned by the current user which enables the Post Update and Delete buttons.
		/// </summary>
		public bool OwnedByUser
		{
			get
			{
				return cmdPostUpdate.Visible;
			}
			set
			{
				cmdPostUpdate.Visible = value;
				cmdDelete.Visible = value;
			}
		}
		#endregion
	}
}