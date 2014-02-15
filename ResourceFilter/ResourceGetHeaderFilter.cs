using System;
using System.Collections.Generic;

namespace VCResourceManager.ResourceFilter
{
    // ヘッダーを集めるフィルタ
    public class ResourceGetHeaderFilter : ResourceFilterBase
    {
        private String _mCurLang;
        private readonly List<HeaderList> _mListHeader = new List<HeaderList>();
        private HeaderList _mCurHeaderList;

        public List<HeaderList> GetHeaderListList()
        {
            return _mListHeader;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {

        }
        public override void BeginLang(String strLang)
        {
            _mCurLang = strLang;
        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {
            if (_mCurHeaderList == null || _mCurHeaderList.GetLang() != _mCurLang)
            {
                _mCurHeaderList = null;
                foreach (HeaderList t in _mListHeader)
                {
                    if (t.GetLang() == _mCurLang)
                    {
                        _mCurHeaderList = t;
                        break;
                    }
                }
                if (_mCurHeaderList == null)
                {
                    var pListNew = new HeaderList(_mCurLang);
                    _mListHeader.Add(pListNew);
                    _mCurHeaderList = pListNew;
                }
            }

            _mCurHeaderList.AddHeader(strOutputName);
        }
    }

    public class HeaderList
    {
        private readonly String _mLang;

        private readonly HashSet<string> _mSetHeader = new HashSet<string>();
        private readonly List<string> _mListHeader = new List<string>();

        public HeaderList(String lang)
        {
            _mLang = lang;
        }
        public List<string> GetHeaderList()
        {
            return _mListHeader;
        }
        public String GetLang()
        {
            return _mLang;
        }
        public void AddHeader(String strHeader)
        {
            if (_mSetHeader.Contains(strHeader))
                return;

            _mSetHeader.Add(strHeader);
            _mListHeader.Add(strHeader);
        }
    }
}
