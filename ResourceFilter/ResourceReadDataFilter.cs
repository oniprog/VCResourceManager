using System;
using System.Collections.Generic;
using System.Linq;

namespace VCResourceManager.ResourceFilter
{
    // ファイルからデータを読み込む
    public class ResourceReadDataFilter : ResourceFilterBase
    {
        private readonly HeaderDataList _mListHeaderData = new HeaderDataList();
        private List<string> _mBody;
        private String _mLang;
        private String _mDataName;
        private ResourceFileMaster.EMode _mMode;
        private HeaderData _mHeaderData;

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            if (mode != _mMode)
            {
                if (_mBody != null)
                {
                    _mHeaderData.SetBody(_mBody.ToArray());
                    _mBody = null;
                }
            }
            if (_mBody != null)
                _mBody.Add(strLine);
        }
        public override void BeginLang(String strLang)
        {
            if (_mBody != null)
            {
                _mHeaderData.SetBody(_mBody.ToArray());
                _mBody = null;
            }
            _mLang = strLang;
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
            
            if (_mBody != null)
            {
                _mHeaderData.SetBody(_mBody.ToArray());
                _mBody = null;
            }

            _mDataName = strOutputName + "." + strNumber + "." + _mLang;
            _mHeaderData = _mListHeaderData.GetHeaderData(_mLang, _mDataName);
            _mBody = _mHeaderData.GetBody() == null ? new List<string>() : new List<string>(_mHeaderData.GetBody());
            _mMode = mode;

        }

        // 読み込んだデータを返す
        public HeaderDataList GetHeaderDataList()
        {
            return _mListHeaderData;
        }
    }

    // データ保持クラス
    public class HeaderData
    {
        private readonly String _mHeader;
        private String[] _mListBody;

        public HeaderData(String strHeader)
        {
            _mHeader = strHeader;
        }

        public void SetBody( string[] listBody) {
            _mListBody = listBody;
        }

        public String GetHeader()
        {
            return _mHeader;
        }
        public String[] GetBody()
        {
            return _mListBody;
        }
    }
    public class HeaderDataList
    {
        private readonly Dictionary<String, List<HeaderData>> _mDicHeaderData = new Dictionary<String, List<HeaderData>>();

        public void Add(string strLang, HeaderData data)
        {
            if (!_mDicHeaderData.ContainsKey(strLang))
            {
                _mDicHeaderData.Add(strLang, new List<HeaderData>());
            }
            _mDicHeaderData[strLang].Add(data);
        }

        // HeaderDataを作成して返す
        public HeaderData GetHeaderData(string strLang, string strHeader)
        {
            if (!_mDicHeaderData.ContainsKey(strLang))
            {
                var data = new HeaderData(strHeader);
                Add(strLang, data);
                return data;
            }
            else
            {
                var listHeaderData = _mDicHeaderData[strLang];
                foreach (var headerdata in listHeaderData)
                {
                    if (headerdata.GetHeader() == strHeader)
                        return headerdata;
                }

                var data = new HeaderData(strHeader);
                _mDicHeaderData[strLang].Add(data);
                return data;
            }
        }

        // 2つのヘッダーリストを比較する
        // otherだけにあるデータは見逃す
        public HashSet<string> CompareHeaderDataList(HeaderDataList other)
        {
            // すべてのヘッダーを得る
            var setDiff = new HashSet<string>();

            // 比較していく
            foreach (var listHeaderData in _mDicHeaderData.Values)
            {
                foreach (var headerdata in listHeaderData)
                {
                    var header1 = headerdata.GetHeader();
                    var body1 = headerdata.GetBody();

                    if (body1 == null)
                        continue;

                    bool bFind = false;

                    foreach (var listHeaderData2 in other._mDicHeaderData.Values)
                    {
                        foreach( var headerdata2 in listHeaderData2) {
                            var header2 = headerdata2.GetHeader();
                            if (header1 == header2)
                            {
                                var body2 = headerdata2.GetBody();

                                if (body2 == null)
                                    continue;

                                if ( body1.Length == body2.Length ) {
                                    bFind = !body1.Where((t, it) => t != body2[it]).Any();
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


