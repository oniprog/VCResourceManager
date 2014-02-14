using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VCResourceManager
{
    // resource.hファイルからリソース名を抽出する
    public class ResHExtractor
    {
        private readonly HashSet<string> _mSetId = new HashSet<string>();
        private StreamReader _mSr;

        // 取得したIDリスト
        public HashSet<string>  GetSetId() {
            return _mSetId;
        }

        // ファイルからリソース名を抽出する
        public bool ParseFile(string strPath)
        {
            var match1 = new Regex("[#]define");

            // ファイルを開く
            _mSr = DecideFileLang(strPath);
            if (_mSr == null)
                return false;

            int nMax = 0;

            using (_mSr)
            {
                while (true)
                {
                    string strLine = _mSr.ReadLine();
                    if (strLine == null)
                        break;

                    if (match1.IsMatch(strLine))
                    {
                        string strId;
                        int nNum;
                        if( !ParseLine(out strId, out nNum, strLine) )
                            continue;

                        if ( nMax < nNum )
                            nMax = nNum;

                        _mSetId.Add(strId);
                    }
                }
            } 
            return true;
        }

        // 行を解析する
        private bool ParseLine(out string strId, out int nNum, string strLine)
        {
            strId = "";
            nNum = 0;

            var spliter = new Regex("\\s+");
            var split = spliter.Split(strLine);
            
            if ( split.Length < 3 )
                return false;

            strId = split[1];
            if ( !int.TryParse( split[2], out nNum) )
                return false;

            return true;
        }

        // 言語を判定しつつファイルを開く
        public static StreamReader DecideFileLang(string strPath)
        {
            var match1 = new Regex("Microsoft Visual C");
            const int n = 5;

            try
            {
                using (var sr = new StreamReader(strPath, Encoding.GetEncoding("SJIS")))
                {
                    for (int it = 0; it < n; ++it)
                    {
                        string strLine = sr.ReadLine();
                        if (strLine == null) break;
                        if (match1.IsMatch(strLine))
                        {
                            return new StreamReader(strPath, Encoding.GetEncoding("SJIS"));
                        }
                    }
                }
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            try
            {
                using (var sr = new StreamReader(strPath, Encoding.UTF8))
                {
                    for (int it = 0; it < n; ++it)
                    {
                        string strLine = sr.ReadLine();
                        if (strLine == null) break;
                        if (match1.IsMatch(strLine))
                        {
                            return new StreamReader(strPath, Encoding.UTF8);
                        }
                    }
                }
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            return null;
        }
    }

    // resource.hファイルからリソース名を抽出する
    public class ResHAppender
    {
        private StreamReader _mSr;
        private HashSet<string> _mSetAddId;
        
        // 追加するIDリスト
        public void SetAddId(HashSet<string> setAddId)
        {
            _mSetAddId = setAddId;
        }

        // ファイルからリソース名を抽出する
        public bool AppendFile(string strOuptutPath, string strPath)
        {
            if (_mSetAddId == null)
                return true;

            var match1 = new Regex("[#]define");

            using (var sw = new StreamWriter(strOuptutPath, false, Encoding.Unicode)) { 

                // ファイルを開く
                _mSr = ResHExtractor.DecideFileLang(strPath);
                if (_mSr == null)
                    return false;

                int nMax = 0;
                bool bOutputed = false;

                using (_mSr)
                {
                    while (true)
                    {
                        string strLine = _mSr.ReadLine();
                        if (strLine == null)
                            break;

                        if (match1.IsMatch(strLine))
                        {
                            string strId;
                            int nNum;
                            if (!ParseLine(out strId, out nNum, strLine))
                                continue;

                            if (nMax < nNum)
                                nMax = nNum;
                        }
                        else if (strLine.Length == 0 && bOutputed == false)
                        {
                            bOutputed = true;

                            // IDを出力する
                            int nId = ++nMax;
                            foreach (var id1 in _mSetAddId)
                            {
                                sw.Write("#define " + id1);
                                for (int it = id1.Length; it < 31; ++it)
                                {
                                    sw.Write(" ");
                                }
                                sw.Write(" ");
                                sw.WriteLine("" + nId++);
                            }
                        }

                        sw.WriteLine(strLine);
                    }
                }
            }
            return true;
        }

        // 行を解析する
        private bool ParseLine(out string strId, out int nNum, string strLine)
        {
            strId = "";
            nNum = 0;

            var spliter = new Regex("\\s+");
            var split = spliter.Split(strLine);

            if (split.Length < 3)
                return false;

            strId = split[1];
            if (!int.TryParse(split[2], out nNum))
                return false;

            return true;
        }
    }
}
