using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // .rcファイルを追加する
    public class ResourceAddRcFilter : ResourceFilterBase
    {
        private StreamWriter mSW = null;
        private String mCommonFolder;
        private String mLang;
        private bool mOutputFlag;
        private ResourceFileMaster.EMode mMode;

        private FileInfo[] mListCommonFile;

        private HashSet<string> mSetSelected;
        bool[] bFirstFlag = new bool[3];
        private String strBeforeLine = "";

        public ResourceAddRcFilter(String strOutputPath, String strCommonFolder, HashSet<string> setSelected)
        {
            mSW = new StreamWriter(strOutputPath, false, Encoding.Unicode);
            mCommonFolder = strCommonFolder;
            mOutputFlag = true;
            mSetSelected = setSelected;

            mListCommonFile = new DirectoryInfo(mCommonFolder).GetFiles("*.txt");
            bFirstFlag[0] = bFirstFlag[1] = bFirstFlag[2] = true;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (!mOutputFlag)
            {
                if (mMode == mode)
                    return;
                mOutputFlag = true;
            }

            // 連続する空行は出力しない
            if (strBeforeLine.Length == 0 && strLine.Length == 0)
                return;

            mSW.WriteLine(strLine);
            strBeforeLine = strLine;
        }
        public override void BeginLang(String strLang)
        {
            mLang = strLang;
            bFirstFlag[0] = bFirstFlag[1] = bFirstFlag[2] = true;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (mSetSelected.Contains(strOutputName))
            {
                // 選択されたファイルは追加されるのでコピーはしない
                mOutputFlag = false;
                mMode = mode;
            }

            // 追加するかをチェックする
            String strNumber = "";
            if (mode == ResourceFileMaster.EMode.InDialogIn1)
            {
                if (!bFirstFlag[0])
                    return;
                bFirstFlag[0] = false;
                strNumber = "1";

            }
            else if (mode == ResourceFileMaster.EMode.InDialogInfoIn1)
            {
                if (!bFirstFlag[1])
                    return;
                bFirstFlag[1] = false;
                strNumber = "2";
            }
            else if (mode == ResourceFileMaster.EMode.InDesignInfoIn1)
            {
                if (!bFirstFlag[2])
                    return;
                bFirstFlag[2] = false;
                strNumber = "3";
            }
            else
                return;

            // 指定ファイルをすべて追加する
            foreach (var strFilename in mSetSelected)
            {

                if (!IsExistFile(strFilename))
                    continue;

                String strCheckName = mLang + "." + strNumber + "." + strFilename;
                OutputExistFile(strFilename, strNumber);
                mSW.WriteLine();
            }
        }

        // 対応ファイルの存在確認
        private bool IsExistFile(String strOutputName)
        {
            foreach (var strFile in mListCommonFile)
            {
                var split = strFile.Name.Split('.');
                if (split.Length != 4)
                    continue;

                var strHead = split[0];
                if (strHead == strOutputName)
                    return true;
            }

            return false;
        }

        // 対応ファイルの出力
        private void OutputExistFile(String strOutputName, String strNumber)
        {
            var strSearchName = mCommonFolder + @"\" + strOutputName + "." + strNumber + "." + mLang + ".txt";
            if (!File.Exists(strSearchName))
                return;

            using (StreamReader sr = new StreamReader(strSearchName, Encoding.UTF8))
            {
                while (true)
                {
                    String strLine = sr.ReadLine();
                    if (strLine == null)
                        break;
                    mSW.WriteLine(strLine);
                }
            }
        }

        public override void EndProcess()
        {
            this.mSW.Close();
        }
    }
}


