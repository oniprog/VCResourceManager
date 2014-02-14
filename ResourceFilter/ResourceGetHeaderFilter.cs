using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // ヘッダーを集めるフィルタ
    public class ResourceGetHeaderFilter : ResourceFilterBase
    {
        private String mCurLang;
        private List<HeaderList> mListHeader = new List<HeaderList>();
        private HeaderList mCurHeaderList;

        public List<HeaderList> GetHeaderListList()
        {
            return mListHeader;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {

        }
        public override void BeginLang(String strLang)
        {
            mCurLang = strLang;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (mCurHeaderList == null || mCurHeaderList.GetLang() != mCurLang)
            {
                mCurHeaderList = null;
                for (int it = 0; it < mListHeader.Count; ++it)
                {
                    if (mListHeader[it].GetLang() == mCurLang)
                    {
                        mCurHeaderList = mListHeader[it];
                        break;
                    }
                }
                if (mCurHeaderList == null)
                {
                    HeaderList pListNew = new HeaderList(mCurLang);
                    mListHeader.Add(pListNew);
                    mCurHeaderList = pListNew;
                }
            }

            mCurHeaderList.AddHeader(strOutputName);
        }
    }

    public class HeaderList
    {
        private String mLang;

        private HashSet<string> mSetHeader = new HashSet<string>();
        private List<string> mListHeader = new List<string>();

        public HeaderList(String lang_)
        {
            mLang = lang_;
        }
        public List<string> GetHeaderList()
        {
            return mListHeader;
        }
        public String GetLang()
        {
            return mLang;
        }
        public void AddHeader(String strHeader)
        {
            if (mSetHeader.Contains(strHeader))
                return;

            mSetHeader.Add(strHeader);
            mListHeader.Add(strHeader);
        }
    }
}
