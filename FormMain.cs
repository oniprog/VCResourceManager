using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VCResourceManager.ResourceFilter;
using VCResourceManager.ResourceFilter.ResourceFilter.Properties;

namespace VCResourceManager
{
    public partial class FormMain : Form
    {
        ///  無視するダイアログリソース名
        private HashSet<string> _mSetIgnoreDialog = new HashSet<string>();

        private Case _mCase = new Case();

        public FormMain()
        {
            InitializeComponent();
        }

		/*
        private void btnSettings_Click(object sender, EventArgs e)
        {
            FromSettings settings = new FromSettings(_mCase.Copy());
            if ( settings.ShowDialog() == DialogResult.OK ) {
                _mCase = settings.getCase();
                SetEnable();
            }
        }*/

        private void SetEnable()
        {
            bool bEnable = !_mCase.IsNull();
            //btnSave.Enabled = bEnable;
            listBoxCommon.Enabled = bEnable;
            listBoxRes.Enabled = bEnable;
            btnToCommon.Enabled = bEnable;
            btnToResUpdate.Enabled = bEnable;
            btnRefreshCommon.Enabled = bEnable;
            btnRefreshRes.Enabled = bEnable;
            btnSelectAllRes.Enabled = bEnable;
            btnSelectAllCommon.Enabled = bEnable;
            btnToResAdd.Enabled = bEnable;
        }

        // 無視するダイアログリソースIDの読み込み
        private void LoadIgnoreDialogFile()
        {
            _mSetIgnoreDialog = new HashSet<string>();

            String strPath = Application.StartupPath + @"\ignore_dialog.txt";
            if ( !File.Exists(strPath))
                return;

            using( var sr = new StreamReader(strPath, Encoding.UTF8)) {

                while(true) {
                    string strLine = sr.ReadLine();
                    if (strLine == null)
                        break;
                    if (strLine.Length == 0)
                        continue;
                    _mSetIgnoreDialog.Add(strLine);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetEnable();
        }

		/*
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "resman file|*.resman";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            ReadConfigFromFile(dlg.FileName);
            LoadFromResource();
            LoadFromCommon();
        }*/

        private void ReadConfigFromFile( String strPath ) {

            _mCase = new Case();

            var serialize = new System.Xml.Serialization.XmlSerializer(typeof(CaseForSave));
            using (var fs = new FileStream(strPath, FileMode.Open))
            {
                var obj = (CaseForSave)serialize.Deserialize(fs);
                obj.SetCase(_mCase);
            }

            SetEnable();
        }

		/*
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "resman file|*.resman";
            dlg.DefaultExt = "resman";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            SaveConfigToFile(dlg.FileName);
        }*/

        private void SaveConfigToFile(String strPath)
        {
            var serialize = new System.Xml.Serialization.XmlSerializer(typeof(CaseForSave));
            using (var fs = new FileStream(strPath, FileMode.Create))
            {
                var caseForSave = _mCase.CopyForSave();
                serialize.Serialize(fs, caseForSave);
            }
        }

        // リソースファイルを読み込む
        private void LoadFromResource()
        {
            if (_mCase.GetRcPath().Length == 0)
                return;
            if (!File.Exists(_mCase.GetRcPath()))
            {
                MessageBox.Show(Resources.FormMain_LoadFromResource_RCファイルがありません);
                return;
            }

            // 無視するダイアログリソース名を読み込む
            LoadIgnoreDialogFile();

            // リソースファイルを読み込む
            var filter = new ResourceGetHeaderFilter();
            var master = new ResourceFileMaster();
            master.ParseRcFile(_mCase.GetRcPath(), filter);

            var setResourceName = new HashSet<string>();
            listBoxRes.Items.Clear();
            var listHeader = filter.GetHeaderListList();
            foreach (var eachLang in listHeader )
            {
                foreach (var strHeader in eachLang.GetHeaderList() )
                {
                    if (setResourceName.Contains(strHeader))
                        continue;

                    // 無視するダイアログリソースのチェック
                    if (_mSetIgnoreDialog.Contains(strHeader))
                        continue;

                    setResourceName.Add(strHeader);
                    listBoxRes.Items.Add(strHeader);
                }
            }
        }

        private void btnRefreshRes_Click(object sender, EventArgs e)
        {
            LoadFromResource();
        }

        // ListBoxRes内で選択されているものすべてを得る
        private HashSet<string> GetSelectedAtListBoxRes()
        {
            var setCopy = new HashSet<string>();
            for (int it = 0; it < listBoxRes.Items.Count; ++it)
            {
                if (listBoxRes.GetSelected(it))
                {
                    setCopy.Add(listBoxRes.Items[it].ToString());
                }
            }
            return setCopy;
        }

        private void btnToCommon_Click(object sender, EventArgs e)
        {
            // 選択されている名前一覧を得る
            var setCopy = GetSelectedAtListBoxRes();

            // 出力フィルタをかます
            var filter = new ResourceExtractFilter(_mCase.GetDataFolder(), setCopy);
            var master = new ResourceFileMaster();
            master.ParseRcFile(_mCase.GetRcPath(), filter);

            LoadFromCommon();

            MessageBox.Show(Resources.FormMain_btnToCommon_Click_抽出しました);
            // 終了
        }

        private void btnRefreshCommon_Click(object sender, EventArgs e)
        {
            LoadFromCommon();
        }

        private void LoadFromCommon() {

            if (_mCase.GetDataFolder().Length < 4)
                return;

            // 無視するダイアログリソース名を読み込む
            LoadIgnoreDialogFile();

            // リソース一覧更新
            var setFiles = new HashSet<string>();

            var filelist = new DirectoryInfo(_mCase.GetDataFolder()).GetFiles("*.txt");

            foreach( var strFile in filelist ) {
                
                var split = strFile.Name.Split('.');
                if ( split.Length != 4 )
                    continue;

                var filename = split[0];
                if (setFiles.Contains(filename))
                    continue;

                // 無視するダイアログリソースのチェック
                if (_mSetIgnoreDialog.Contains(filename))
                    continue;

                setFiles.Add(filename);
            }

            listBoxCommon.Items.Clear();

            foreach (var strfile in setFiles)
            {
                listBoxCommon.Items.Add(strfile);
            }
        }

        // ListBoxCommon内で選択されているものすべてを得る
        private HashSet<string> GetSelectedAtListBoxCommon()
        {
            var setCopy = new HashSet<string>();
            for (int it = 0; it < listBoxCommon.Items.Count; ++it)
            {
                if (listBoxCommon.GetSelected(it))
                {
                    setCopy.Add(listBoxCommon.Items[it].ToString());
                }
            }
            return setCopy;
        }

        // rcファイルのバックアップを取る
        private string MakeRcBackup(out int nBackupCnt)
        {
            String strBackupPath;
            for (int it = 1; ; ++it)
            {
                strBackupPath = _mCase.GetRcPath() + "." + ("" + it);
                if (!File.Exists(strBackupPath))
                {
                    File.Copy(_mCase.GetRcPath(), strBackupPath);
                    nBackupCnt = it;
                    break;
                }
            }

            return strBackupPath;
        }

        private void btnToRes_Click(object sender, EventArgs e)
        {
            // 無視するダイアログリソース名を読み込む
            LoadIgnoreDialogFile();

            // 選択一覧を得る
            var setSelected = GetSelectedAtListBoxCommon();
            if (setSelected.Count == 0)
            {
                MessageBox.Show(Resources.FormMain_btnToRes_Click_共通領域のリソースファイルが一つも選択されていません);
                return;
            }

            if (MessageBox.Show(Resources.FormMain_btnToRes_Click_, Resources.FormMain_btnToRes_Click_警告, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            // バックアップを作成する
            int nBackupCnt;
            String strBackupPath = MakeRcBackup(out nBackupCnt);

            // バックアップを参照しながら書き換える
            var master = new ResourceFileMaster();
            var filter = new ResourceGenerateRcFilter(_mCase.GetRcPath(), _mCase.GetDataFolder(), setSelected);
            master.ParseRcFile(strBackupPath, filter);

            // データの比較用バックアップ
            String strCompPath = _mCase.GetRcPath() + ".backup.txt";
            if ( !File.Exists(strCompPath))
                File.Copy(strBackupPath, strCompPath, true);

            // 比較用バックアップも更新する
            String strCompPathTmp = _mCase.GetRcPath() + ".backup.txt.tmp";
            var master2 = new ResourceFileMaster();
            var filter2 = new ResourceGenerateRcFilter(strCompPathTmp, _mCase.GetDataFolder(), setSelected);
            master2.ParseRcFile(strCompPath, filter2);

            // 上書きコピーする
            File.Copy(strCompPathTmp, strCompPath, true);
            File.Delete(strCompPathTmp);

            // Resource.hを書き換える
            AddResourceH(nBackupCnt);

            MessageBox.Show(Resources.FormMain_btnToRes_Click_rcファイルを書き換えました);
        }

        // 無視するIDのリストを読む
        private IEnumerable<string> ReadIgnoreIDList()
        {
            var setRet = new HashSet<string>();
            string strIgnoreIdListPath = Application.StartupPath + @"\ignore_id.txt";
            if (!File.Exists(strIgnoreIdListPath))
                return setRet;

            using (var sr = new StreamReader(strIgnoreIdListPath, Encoding.UTF8))
            {
                while (true)
                {
                    string strLine = sr.ReadLine();
                    if (strLine == null)
                        break;
                    setRet.Add(strLine);
                }
            }
            return setRet;
        }

        // Resource.hのバックアップを取る
        private void AddResourceH(int nBackupCnt)
        {
            // すでにあるIDを抽出する
            var master2 = new ResHExtractor();
            master2.ParseFile(_mCase.GetResourcePath());
            var setId1 = master2.GetSetId();

            // ファイルよりIDを抽出する
            var master = new ResourceFileMaster();
            var filter = new ResourceCollectIdFilter();
            master.ParseRcFile(_mCase.GetRcPath(), filter);
            var setId2 = filter.GetSetId();

            // setID2 - setID1 を計算する
            foreach (var id1 in setId1 ) 
            {
                if (setId2.Contains(id1))
                    setId2.Remove(id1);
            }

            // 無視リストを反映する
            var setIgnoreId = ReadIgnoreIDList();
            foreach (var id1 in setIgnoreId)
            {
                if (setId2.Contains(id1))
                    setId2.Remove(id1);
            }

#if false
            // ファイルに残ったIDを出力する
            using (StreamWriter sw = new StreamWriter("c:\\tmp\\ignore_id.txt", true, Encoding.UTF8))
            {
                foreach (var id in setID2)
                {
                    sw.WriteLine(id);
                }
            }
#endif
            // バックアップファイル作成
            String strBackupPath = _mCase.GetResourcePath() + "." + (""+nBackupCnt);
            File.Copy( _mCase.GetResourcePath(), strBackupPath, true );

            // バックアップファイルから新規作成
            var master3 = new ResHAppender();
            master3.SetAddId(setId2);
            master3.AppendFile(_mCase.GetResourcePath(), strBackupPath);
        }


        private void chkShowDiffrence_CheckedChanged(object sender, EventArgs e)
        {
            LoadFromResource();
            if (!chkShowDiffrence.Checked)
                return;

            var strBackupPath = _mCase.GetRcPath() + ".backup.txt";
            if (!File.Exists(strBackupPath))
            {
                MessageBox.Show(Resources.FormMain_chkShowDiffrence_CheckedChanged_バックアップファイルが存在しないのでチェックできません);
                chkShowDiffrence.Checked = false;
                return;
            }

            // まずファイルを読み込む
            var master = new ResourceFileMaster();
            var filter1 = new ResourceReadDataFilter();
            master.ParseRcFile(strBackupPath, filter1);
            var filter2 = new ResourceReadDataFilter();
            master.ParseRcFile(_mCase.GetRcPath(), filter2);

            var listHeaderData1 = filter1.GetHeaderDataList();
            var listHeaderData2 = filter2.GetHeaderDataList();

            // データを比較する
            var setDiff = listHeaderData2.CompareHeaderDataList(listHeaderData1);

            listBoxRes.Items.Clear();

            foreach (var strOutputName in setDiff)
            {
                // 無視ファイルは無視する
                if (_mSetIgnoreDialog.Contains(strOutputName))
                    continue;

                listBoxRes.Items.Add(strOutputName);
            }
        }

        private void btnSelectAllRes_Click(object sender, EventArgs e)
        {
            for (int it = 0; it < listBoxRes.Items.Count; ++it)
            {
                listBoxRes.SetSelected(it, true);
            }
        }

        private void btnSelectAllCommon_Click(object sender, EventArgs e)
        {
            for (int it = 0; it < listBoxRes.Items.Count; ++it)
            {
                listBoxCommon.SetSelected(it, true);
            }
        }

        private void btnToResAdd_Click(object sender, EventArgs e)
        {
            // バックアップを取る
            int nBackupCnt;
            string strBackupPath = MakeRcBackup(out nBackupCnt);

            // 無視するダイアログリソース名を読み込む
            LoadIgnoreDialogFile();

            // 選択されたファイル一覧を得る
            var setSelected = GetSelectedAtListBoxCommon();
            if (setSelected.Count == 0)
            {
                MessageBox.Show(Resources.FormMain_btnToResAdd_Click_共通領域のリソースファイルが一つも選択されていません);
                return;
            }

            // 新規リソースを追加する
            var master = new ResourceFileMaster();
            var filter = new ResourceAddRcFilter(_mCase.GetRcPath(), _mCase.GetDataFolder(), setSelected);
            master.ParseRcFile(strBackupPath, filter);

            // 比較用バックアップも更新する
            String strCompPath = _mCase.GetRcPath() + ".backup.txt";
            String strCompPathTmp = _mCase.GetRcPath() + ".backup.txt.tmp";
            var master2 = new ResourceFileMaster();
            var filter2 = new ResourceAddRcFilter(strCompPathTmp, _mCase.GetDataFolder(), setSelected);
            master2.ParseRcFile(strCompPath, filter2);

            // 上書きコピーする
            File.Copy(strCompPathTmp, strCompPath, true);
            File.Delete(strCompPathTmp);

            // Resource.hの修正
            AddResourceH(nBackupCnt);

            MessageBox.Show(Resources.FormMain_btnToRes_Click_rcファイルを書き換えました);

            LoadFromResource();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = Resources.FormMain_toolStripMenuItem2_Click_resman_file___resman};
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            ReadConfigFromFile(dlg.FileName);
            LoadFromResource();
            LoadFromCommon();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = Resources.FormMain_toolStripMenuItem2_Click_resman_file___resman,
                DefaultExt = "resman"
            };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            SaveConfigToFile(dlg.FileName);
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new FromSettings(_mCase.Copy());
            if (settings.ShowDialog() == DialogResult.OK)
            {
                _mCase = settings.GetCase();
                SetEnable();
            }
        }
    }
}
