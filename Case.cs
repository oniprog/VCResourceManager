using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCResourceManager
{
    public class Case
    {
        private String mRcPath = "";
        private String mResourceHPath = "";
        private String mDataFolder = "";

        public CaseForSave CopyForSave()
        {
            CaseForSave ret = new CaseForSave();
            ret.RcPath = mRcPath;
            ret.ResourceHPath = mResourceHPath;
            ret.DataFolder = mDataFolder;
            return ret;
        }

        public Case Copy() {
            Case ret = new Case();
            ret.mRcPath = mRcPath;
            ret.mResourceHPath = mResourceHPath;
            ret.mDataFolder = mDataFolder;
            return ret;
        }

        // NULLかどうかを判定する
        public bool IsNull()
        {
            return mDataFolder.Length == 0;
        }

        public void SetDataFolder(String strFolder_)
        {
            mDataFolder = strFolder_;
        }
        public String GetDataFolder()
        {
            return mDataFolder;
        }
        public void SetResourcePath(String strPath_)
        {
            mResourceHPath = strPath_;
        } 
        public String GetResourcePath()
        {
            return mResourceHPath;
        }
        public void SetRcPath(String strPath_)
        {
            mRcPath = strPath_;
        }
        public String GetRcPath()
        {
            return mRcPath;
        }
    }

    public class CaseForSave
    {
        public String RcPath = "";
        public String ResourceHPath = "";
        public String DataFolder = "";

        public void SetCase(Case c)
        {
            c.SetRcPath(RcPath);
            c.SetResourcePath(ResourceHPath);
            c.SetDataFolder(DataFolder);
        }
    }
}
