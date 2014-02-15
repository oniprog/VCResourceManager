using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // .rcファイルを生成する(更新)
    public class ResourceGenerateRcFilter : ResourceFilterBase
    {
        private readonly StreamWriter _mSw;
        private readonly String _mCommonFolder;
        private String _mLang;
        private bool _mOutputFlag;
        private ResourceFileMaster.EMode _mMode;

        private readonly FileInfo[] _mListCommonFile;
        private readonly HashSet<string> _mSetOutputFile = new HashSet<string>();

        private readonly HashSet<string> _mSetSelected;

        public ResourceGenerateRcFilter(String strOutputPath, String strCommonFolder, HashSet<string> setSelected)
        {
            _mSw = new StreamWriter(strOutputPath, false, Encoding.Unicode);
            _mCommonFolder = strCommonFolder;
            _mOutputFlag = true;
            _mSetSelected = setSelected;

            _mListCommonFile = new DirectoryInfo(_mCommonFolder).GetFiles("*.txt");
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (!_mOutputFlag)
            {
                if (_mMode == mode)
                    return;
                _mOutputFlag = true;
            }
            _mSw.WriteLine(strLine);
        }
        public override void BeginLang(String strLang)
        {
            _mLang = strLang;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (!_mSetSelected.Contains(strOutputName))
            {
                _mOutputFlag = true;
                return;
            }

            String strNumber;
            if (mode == ResourceFileMaster.EMode.InDialogIn1)
            {
                strNumber = "1";
            }
            else if (mode == ResourceFileMaster.EMode.InDialogInfoIn1)
            {
                strNumber = "2";
            }
            else if (mode == ResourceFileMaster.EMode.InDesignInfoIn1)
            {
                strNumber = "3";
            }
            else
                return;

            if (!IsExistFile(strOutputName))
            {
                _mOutputFlag = true;
                return;
            }

            _mOutputFlag = false;
            _mMode = mode;

            String strCheckName = _mLang + "." + strNumber + "." + strOutputName;
            if (_mSetOutputFile.Contains(strCheckName))
                return;
            _mSetOutputFile.Add(strCheckName);

            OutputExistFile(strOutputName, strNumber);
        }

        // 対応ファイルの存在確認
        private bool IsExistFile(String strOutputName)
        {
            return (from strFile in _mListCommonFile select strFile.Name.Split('.') into split where split.Length == 4 select split[0]).Any(strHead => strHead == strOutputName);
        }

        // 対応ファイルの出力
        private void OutputExistFile(String strOutputName, String strNumber)
        {
            var strSearchName = _mCommonFolder + @"\" + strOutputName + "." + strNumber + "." + _mLang + ".txt";
            if (!File.Exists(strSearchName))
                return;

            using (var sr = new StreamReader(strSearchName, Encoding.UTF8))
            {
                while (true)
                {
                    String strLine = sr.ReadLine();
                    if (strLine == null)
                        break;
                    _mSw.WriteLine(strLine);
                }
            }
        }

        public override void EndProcess()
        {
            _mSw.Close();
        }
    }
}


