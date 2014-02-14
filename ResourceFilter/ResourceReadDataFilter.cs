using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VCResourceManager.ResourceFilter
{
    // ファイルからデータを読み込む
    public class ResourceReadDataFilter : ResourceFilterBase
    {
        private HeaderDataList mListHeaderData = new HeaderDataList();
        private List<string> mBody;
        private String mLang;
        private String mDataName;
        private ResourceFileMaster.EMode mMode;
        private HeaderData mHeaderData;

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (mode != mMode)
            {
                if (mBody != null)
                {
                    mHeaderData.SetBody(mBody.ToArray());
                    mBody = null;
                }
            }
            if (mBody != null)
                mBody.Add(strLine);
        }
        public override void BeginLang(String strLang)
        {
            if (mBody != null)
            {
                mHeaderData.SetBody(mBody.ToArray());
                mBody = null;
            }
            mLang = strLang;
        }
        public override void EndProcess()
        {
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            string strNumber;
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
            else {
                return;
            }
            
            if (mBody != null)
            {
                mHeaderData.SetBody(mBody.ToArray());
                mBody = null;
            }

            mDataName = strOutputName + "." + strNumber + "." + mLang;
            mHeaderData = mListHeaderData.GetHeaderData(mLang, mDataName);
            if (mHeaderData.GetBody() == null)
                mBody = new List<string>();
            else 
                mBody = new List<string>(mHeaderData.GetBody());
            mMode = mode;

        }

        // 読み込んだデータを返す
        public HeaderDataList GetHeaderDataList()
        {
            return mListHeaderData;
        }
    }

    // データ保持クラス
    public class HeaderData
    {
        private String mHeader;
        private String[] mListBody;

        public HeaderData(String strHeader)
        {
            mHeader = strHeader;
        }

        public void SetBody( string[] listBody) {
            mListBody = listBody;
        }

        public String GetHeader()
        {
            return mHeader;
        }
        public String[] GetBody()
        {
            return mListBody;
        }
    }
    public class HeaderDataList
    {
        private Dictionary<String, List<HeaderData>> mDicHeaderData = new Dictionary<String, List<HeaderData>>();

        public void Add(string strLang, HeaderData data)
        {
            if (!mDicHeaderData.ContainsKey(strLang))
            {
                mDicHeaderData.Add(strLang, new List<HeaderData>());
            }
            mDicHeaderData[strLang].Add(data);
        }

        // HeaderDataを作成して返す
        public HeaderData GetHeaderData(string strLang, string strHeader)
        {
            if (!mDicHeaderData.ContainsKey(strLang))
            {
                HeaderData data = new HeaderData(strHeader);
                Add(strLang, data);
                return data;
            }
            else
            {
                var listHeaderData = mDicHeaderData[strLang];
                foreach (var headerdata in listHeaderData)
                {
                    if (headerdata.GetHeader() == strHeader)
                        return headerdata;
                }

                HeaderData data = new HeaderData(strHeader);
                mDicHeaderData[strLang].Add(data);
                return data;
            }
        }

        // 2つのヘッダーリストを比較する
        // otherだけにあるデータは見逃す
        public HashSet<string> CompareHeaderDataList(HeaderDataList other)
        {
            // すべてのヘッダーを得る
            HashSet<string> setDiff = new HashSet<string>();

            // 比較していく
            foreach (var listHeaderData in mDicHeaderData.Values)
            {
                foreach (var headerdata in listHeaderData)
                {
                    var header1 = headerdata.GetHeader();
                    var body1 = headerdata.GetBody();

                    if (body1 == null)
                        continue;

                    bool bFind = false;

                    foreach (var listHeaderData2 in other.mDicHeaderData.Values)
                    {
                        foreach( var headerdata2 in listHeaderData2) {
                            var header2 = headerdata2.GetHeader();
                            if (header1 == header2)
                            {
                                var body2 = headerdata2.GetBody();

                                if (body2 == null)
                                    continue;

                                if ( body1.Length == body2.Length ) {

                                    bFind = true;
                                    for( int it=0; it<body1.Length; ++it ) {
                                        if( body1[it] != body2[it] ) {
                                            bFind = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (bFind)
                            break;
                    }
                    if (bFind)
                        continue;

                    var split = header1.Split('.');
                    if (setDiff.Contains(split[0]))
                        continue;

                    setDiff.Add(split[0]);
                }
            }

            return setDiff;
        }
    }
}


