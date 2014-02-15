using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // .rcファイルを追加する
    public class ResourceAddRcFilter : ResourceFilterBase
    {
        private readonly StreamWriter _mSw;
        private readonly String _mCommonFolder;
        private String _mLang;
        private bool _mOutputFlag;
        private ResourceFileMaster.EMode _mMode;

        private readonly FileInfo[] _mListCommonFile;

        private readonly HashSet<string> _mSetSelected;
        private readonly bool[] _bFirstFlag = new bool[3];
        private String _strBeforeLine = "";

        public ResourceAddRcFilter(String strOutputPath, String strCommonFolder, HashSet<string> setSelected)
        {
            _mSw = new StreamWriter(strOutputPath, false, Encoding.Unicode);
            _mCommonFolder = strCommonFolder;
            _mOutputFlag = true;
            _mSetSelected = setSelected;

            _mListCommonFile = new DirectoryInfo(_mCommonFolder).GetFiles("*.txt");
            _bFirstFlag[0] = _bFirstFlag[1] = _bFirstFlag[2] = true;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (!_mOutputFlag)
            {
                if (_mMode == mode)
                    return;
                _mOutputFlag = true;
            }

            // 連続する空行は出力しない
            if (_strBeforeLine.Length == 0 && strLine.Length == 0)
                return;

            _mSw.WriteLine(strLine);
            _strBeforeLine = strLine;
        }
        public override void BeginLang(String strLang)
        {
            _mLang = strLang;
            _bFirstFlag[0] = _bFirstFlag[1] = _bFirstFlag[2] = true;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (_mSetSelected.Contains(strOutputName))
            {
                // 選択されたファイルは追加されるのでコピーはしない
                _mOutputFlag = false;
                _mMode = mode;
            }

            // 追加するかをチェックする
            String strNumber;
            if (mode == ResourceFileMaster.EMode.InDialogIn1)
            {
                if (!_bFirstFlag[0])
                    return;
                _bFirstFlag[0] = false;
                strNumber = "1";

            }
            else if (mode == ResourceFileMaster.EMode.InDialogInfoIn1)
            {
                if (!_bFirstFlag[1])
                    return;
                _bFirstFlag[1] = false;
                strNumber = "2";
            }
            else if (mode == ResourceFileMaster.EMode.InDesignInfoIn1)
            {
                if (!_bFirstFlag[2])
                    return;
                _bFirstFlag[2] = false;
                strNumber = "3";
            }
            else
                return;

            // 指定ファイルをすべて追加する
            foreach (var strFilename in _mSetSelected)
            {

                if (!IsExistFile(strFilename))
                    continue;

                OutputExistFile(strFilename, strNumber);
                _mSw.WriteLine();
            }
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


