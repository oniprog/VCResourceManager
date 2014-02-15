using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // RCファイルより抽出するフィルタ
    public class ResourceExtractFilter : ResourceFilterBase
    {
        private String _mLang;
        private StreamWriter _mSw;
        private ResourceFileMaster.EMode _mMode;
        private readonly String _mOutputFolder;
        private readonly HashSet<string> _mSetExtractName = new HashSet<string>();

        public ResourceExtractFilter(String strOutputFolder, HashSet<string> setExtractName)
        {
            _mSetExtractName = setExtractName;
            _mOutputFolder = strOutputFolder;
            if (!Directory.Exists(strOutputFolder))
            {
                Directory.CreateDirectory(strOutputFolder);
            }
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (_mSw != null)
            {
                if (_mMode != mode)
                {
                    _mSw.Close();
                    _mSw = null;
                }
                else
                {
                    _mSw.WriteLine(strLine);
                }
            }
        }
        public override void BeginLang(String strLang)
        {
            Close();
            _mLang = strLang;
        }

        public override void EndProcess()
        {
            Close();
        }

        private void Close()
        {
            if (_mSw != null)
            {
                _mSw.Close();
                _mSw = null;
            }
        }

        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (!_mSetExtractName.Contains(strOutputName))
            {
                Close();
                return;
            }

            String strFileName = null;
            if (mode == ResourceFileMaster.EMode.InDialogIn1)
            {
                strFileName = strOutputName + ".1." + _mLang + ".txt";
            }
            else if (mode == ResourceFileMaster.EMode.InDialogInfoIn1)
            {
                strFileName = strOutputName + ".2." + _mLang + ".txt";
            }
            else if (mode == ResourceFileMaster.EMode.InDesignInfoIn1)
            {
                strFileName = strOutputName + ".3." + _mLang + ".txt";
            }

            if (strFileName == null)
                return;

            _mMode = mode;

            Close();

            // すでに出力したファイルには追加書き込みとする
            bool bAppend = _mSetExtractName.Contains(strFileName);
            _mSw = new StreamWriter(_mOutputFolder + @"\\" + strFileName, bAppend, Encoding.UTF8);
            if (bAppend)
                _mSw.WriteLine();    // 空行を入れておく
            _mSetExtractName.Add(strFileName);
        }
    }
}
