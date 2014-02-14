using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // RCファイルより抽出するフィルタ
    public class ResourceExtractFilter : ResourceFilterBase
    {
        private String mLang;
        private StreamWriter mSW;
        private ResourceFileMaster.EMode mMode;
        private String mOutputFolder;
        private HashSet<string> mSetExtractName = new HashSet<string>();

        public ResourceExtractFilter(String strOutputFolder, HashSet<string> setExtractName)
        {
            mSetExtractName = setExtractName;
            mOutputFolder = strOutputFolder;
            if (!Directory.Exists(strOutputFolder))
            {
                Directory.CreateDirectory(strOutputFolder);
            }
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (mSW != null)
            {
                if (mMode != mode)
                {
                    mSW.Close();
                    mSW = null;
                }
                else
                {
                    mSW.WriteLine(strLine);
                }
            }
        }
        public override void BeginLang(String strLang)
        {
            Close();
            mLang = strLang;
        }

        public override void EndProcess()
        {
            Close();
        }

        private void Close()
        {
            if (mSW != null)
            {
                mSW.Close();
                mSW = null;
            }
        }

        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (!mSetExtractName.Contains(strOutputName))
            {
                Close();
                return;
            }

            String strFileName = null;
            if (mode == ResourceFileMaster.EMode.InDialogIn1)
            {
                strFileName = strOutputName + ".1." + mLang + ".txt";
            }
            else if (mode == ResourceFileMaster.EMode.InDialogInfoIn1)
            {
                strFileName = strOutputName + ".2." + mLang + ".txt";
            }
            else if (mode == ResourceFileMaster.EMode.InDesignInfoIn1)
            {
                strFileName = strOutputName + ".3." + mLang + ".txt";
            }

            if (strFileName == null)
                return;

            mMode = mode;

            Close();

            // すでに出力したファイルには追加書き込みとする
            bool bAppend = mSetExtractName.Contains(strFileName);
            mSW = new StreamWriter(mOutputFolder + @"\\" + strFileName, bAppend, Encoding.UTF8);
            if (bAppend)
                mSW.WriteLine();    // 空行を入れておく
            mSetExtractName.Add(strFileName);
        }
    }
}
