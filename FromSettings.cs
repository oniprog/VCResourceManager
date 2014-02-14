using System;
using System.Windows.Forms;
using System.IO;
using VCResourceManager.ResourceFilter.ResourceFilter.Properties;

namespace VCResourceManager
{
    public partial class FromSettings : Form
    {
        private readonly Case _mCase;

        public Case GetCase()
        {
            return _mCase;
        }


        public FromSettings(Case case_)
        {
            InitializeComponent();
            _mCase = case_;
        }

        private void FromSettings_Load(object sender, EventArgs e)
        {
            txtDataFolder.Text = _mCase.GetDataFolder();
            txtRcPath.Text = _mCase.GetRcPath();
            txtResourceHPath.Text = _mCase.GetResourcePath();
        }

        private void btnRefDataFolder_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                CheckFileExists = false,
                Filter = Resources.FromSettings_btnRefDataFolder_Click_data_folder____,
                FileName = "dummy file name"
            };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            txtDataFolder.Text = new FileInfo(dlg.FileName).DirectoryName;
        }

        private void btnRefRcPath_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = Resources.FromSettings_btnRefRcPath_Click_rc_file___rc};
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            txtRcPath.Text = dlg.FileName;
        }

        private void btnRefResourceHPath_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "resource.h|*.h"};
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            txtResourceHPath.Text = dlg.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            _mCase.SetResourcePath(txtResourceHPath.Text);
            _mCase.SetDataFolder(txtDataFolder.Text);
            _mCase.SetRcPath(txtRcPath.Text);

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
