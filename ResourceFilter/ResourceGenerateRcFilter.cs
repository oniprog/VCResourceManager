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
        private StreamWriter mSW = null;
        private String mCommonFolder;
        private String mLang;
        private bool mOutputFlag;
        private ResourceFileMaster.EMode mMode;

        private FileInfo[] mListCommonFile;
        private HashSet<string> mSetOutputFile = new HashSet<string>();

        private HashSet<string> mSetSelected;

        public ResourceGenerateRcFilter(String strOutputPath, String strCommonFolder, HashSet<string> setSelected)
        {
            mSW = new StreamWriter(strOutputPath, false, Encoding.Unicode);
            mCommonFolder = strCommonFolder;
            mOutputFlag = true;
            mSetSelected = setSelected;

            mListCommonFile = new DirectoryInfo(mCommonFolder).GetFiles("*.txt");
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (!mOutputFlag)
            {
                if (mMode == mode)
                    return;
                mOutputFlag = true;
            }
            mSW.WriteLine(strLine);
        }
        public override void BeginLang(String strLang)
        {
            mLang = strLang;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (!mSetSelected.Contains(strOutputName))
            {
                mOutputFlag = true;
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
                mOutputFlag = true;
                return;
            }

            mOutputFlag = false;
            mMode = mode;

            String strCheckName = mLang + "." + strNumber + "." + strOutputName;
            if (mSetOutputFile.Contains(strCheckName))
                return;
            mSetOutputFile.Add(strCheckName);

            OutputExistFile(strOutputName, strNumber);
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


