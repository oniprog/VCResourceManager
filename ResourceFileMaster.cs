using System;
using System.Text;
using System.Text.RegularExpressions;
using VCResourceManager.ResourceFilter;

namespace VCResourceManager
{
    /*
     * リソースを解析するためのクラス
     */
    public class ResourceFileMaster
    {
        private ReadFileCache _mFile;

//        private Regex patternBegin = new Regex("AFX_RESOURCE_DLL");
//        private Regex patternExtractLang = new Regex("(?<=AFX_TARG_)[^)]+(?=[)])");
        private readonly Regex _patternBegin = new Regex("^LANGUAGE");
        private readonly Regex _patternExtractLang = new Regex("(?<=LANGUAGE )[^,]+, [^,]+");

        private readonly Regex _patternBeginDialog1 = new Regex("^// Dialog$");
        private readonly Regex _patternBeginDialog2 = new Regex("^// ダイアログ$");
        private readonly Regex _patternBeginDialogInfo = new Regex("^// Dialog Info$");
        private readonly Regex _patternBeginDesignInfo = new Regex("^// DESIGNINFO$");

        private readonly Regex _patternEndDialog = new Regex("^///////////////////////////////////////////////////////");
        private readonly Regex _patternEndDialogInfo = new Regex("^///////////////////////////////////////////////////////");

//        private Regex patternBeginInDialog1 = new Regex("^#if defined[(]APSTUDIO_INVOKED");
//        private Regex patternEndInDialog1 = new Regex("^#endif$"); // 次の行が空行であること

        private readonly Regex _patternExtractDialogName = new Regex("^[^ $]+(?=[ $])");
        private readonly Regex _patternEndInDialog1 = new Regex("^END$");

//        private Regex patternBeginInDialogInfo1 = new Regex("^#if defined[(]APSTUDIO_INVOKED");
//        private Regex patternEndInDialogInfo1 = new Regex("^#endif$");  // 次の行が空行であること

        private readonly Regex _patternBeginInDesignInfo1 = new Regex("(?<=    [\"])[^,$]+(?=[^,]*,)");
        private readonly Regex _patternEndInDesignInfo1 = new Regex("^    END");

        public enum EMode
        {
            Normal,             // 何もないとき
            InDialog,           // ダイアログリソース内
            InDialogIn1,        // ダイアログリソース内の1つ内
            InDialogInfo,       // ダイアログ情報リソース内
            InDialogInfoIn1,    // ダイアログ情報リソース内の1つ内
            InDesiginInfo,      // デザイン情報リソース内
            InDesignInfoIn1     // デザイン情報リソース内の1つ内
        }

		// ファイルのエンコーディングを判定しつつファイルを開く
        private bool DecideFileLang(string strPath)
        {
			// ファイルのエンコードを調べる
			// 試し読みして決定する
            var patternTest = new Regex("Microsoft Visual C");
            {
                if (!_mFile.OpenFile(strPath, Encoding.Unicode))
                    return false;

                bool bFind = false;
                int nCnt = 0;
                for (; nCnt < 10; ++nCnt)
                {
                    string strLine = _mFile.ReadLine();
                    if (strLine == null)
                        break;
                    if (patternTest.IsMatch(strLine))
                    {
                        bFind = true;
                        break;
                    }
                }
                if (bFind)
                {
					// 読み進めた分を戻す
                    _mFile.GoBackward(nCnt);
                }
                else
                {
					// ダメならばSJISで読むことにする
                    if (!_mFile.OpenFile(strPath, Encoding.GetEncoding("SJIS")))
                        return false;
                }
            }
            return true;
        }

		// rcファイルの解析
        public void ParseRcFile(String strPath, ResourceFilterBase filter)
        {
            _mFile = new ReadFileCache();

			// エンコーディングを見つつファイルを開く
            if (!DecideFileLang(strPath))
                return;

            try
            {
                var mode = EMode.Normal;
                String strLang = "";
                String strOutputName = "";

                while (true)
                {
                    String strLine = _mFile.ReadLine();
                    if (strLine == null)
                        break;

                    switch (mode)
                    {
                        case EMode.Normal:
                            ProcessNormal(filter, ref mode, ref strLang, strLine);
                            break;
                        case EMode.InDialog:
                            ProcessInDialog(filter, ref mode, ref strOutputName, strLine);
                            break;
                        case EMode.InDialogIn1:
                            ProcessInDialogIn1(filter, ref mode, strLine);
                            break;
                        case EMode.InDialogInfo:
                            ProcessDialogInfo(filter, ref mode, ref strOutputName, strLine);
                            break;
                        case EMode.InDialogInfoIn1:
                            ProcessDialogInfoIn1(filter, ref mode, strLine);
                            break;
                        case EMode.InDesiginInfo:
                            ProcessDesignInfo(filter, ref mode, strLine);
                            break;
                        case EMode.InDesignInfoIn1:
                            ProcessDesignInfoIn1(filter, ref mode, strLine);
                            break;
                    }
                }
            }
            finally
            {
                _mFile.Close();
            }
            filter.EndProcess();
        }

        // 何もモードに入ってないときの処理
        private void ProcessNormal(ResourceFilterBase filter, ref EMode mode, ref string strLang, String strLine)
        {
            if (_patternBegin.IsMatch(strLine))
            {
                // 言語を抽出する
                var match = _patternExtractLang.Match(strLine);
                strLang = ExtractLangExt(match.Value);
                filter.BeginLang(strLang);
            }
            else if (_patternBeginDialog1.IsMatch(strLine) || _patternBeginDialog2.IsMatch(strLine))
            {
                mode = EMode.InDialog;
            }
            else if (_patternBeginDialogInfo.IsMatch(strLine))
            {
                mode = EMode.InDialogInfo;
            }
            else if (_patternBeginDesignInfo.IsMatch(strLine))
            {
                mode = EMode.InDesiginInfo;
            }

            filter.Process(strLine, mode);
        }


        // 拡張子を抽出する
        private static string ExtractLangExt(String strLang)
        {
            strLang = strLang.Replace(",", "").Replace("SUBLANG_", "").Replace("LANG_", "").Replace(" ", "_");
            if (strLang == "17_1") strLang = "JAPANESE_DEFAULT";
            return strLang;
        }

        // ダイアログ内の処理
        private void ProcessInDialog(ResourceFilterBase filter, ref EMode mode, ref String strOutputName, String strLine)
        {
            // Dialog内
            if (strLine.Length == 0 )
            {
                mode = EMode.InDialogIn1;
                strOutputName = ParseOutputName(filter, mode);

                if ( strOutputName.Length  == 0 )
                    mode = EMode.InDialog; // 戻す
            }           
            else if (_patternEndDialog.IsMatch(strLine))
            {
                mode = EMode.Normal;
            }
            filter.Process(strLine, mode);
        }

        private void ProcessInDialogIn1(ResourceFilterBase filter, ref EMode mode, String strLine)
        {
            // Dialog一つ内
            if (strLine.Length == 0)
            {
                mode = EMode.InDialog;
                _mFile.GoBackward(1);
            }
            else
            {
                filter.Process(strLine, mode);
            }
        }

        // OutputNameを検索する
        private string ParseOutputName(ResourceFilterBase filter, EMode mode)
        {
            string strOutputName = "";
            int nCnt = 1;
            for (; ; ++nCnt)
            {
                String strLine = _mFile.ReadLine();
                if (strLine.Length > 0 && strLine[0] != '#')
                {
                    strOutputName = _patternExtractDialogName.Match(strLine).Value;
                    if ( strOutputName.Length > 0 )
                        filter.BeginOutputName(mode, strOutputName);
                    break;
                }
                if (_patternEndDialog.IsMatch(strLine))
                    break;
            }
			// 読み進めた分を戻す
            _mFile.GoBackward(nCnt);
            return strOutputName;
        }


        // Dialog Info内
        private void ProcessDialogInfo(ResourceFilterBase filter, ref EMode mode, ref String strOutputName, String strLine)
        {
            // Dialgo Info内
            if (strLine.Length == 0)
            {
                mode = EMode.InDialogInfoIn1;
                strOutputName = ParseOutputName(filter, mode);
                if (strOutputName.Length == 0)
                {
                    mode = EMode.InDialogInfo; // 戻す
                }
                else
                {
                    filter.DialogInfoEndFlag = false;
                }
            }
            else if (_patternEndDialogInfo.IsMatch(strLine))
            {
                mode = EMode.Normal;
            }
            filter.Process(strLine, mode);
        }

        // Dialog Info内の1つ内
        private void ProcessDialogInfoIn1(ResourceFilterBase filter, ref EMode mode, String strLine)
        {

            // DialogInfo一つ内
            if (_patternEndInDialog1.IsMatch(strLine))
            {
                filter.DialogInfoEndFlag = true;
                filter.Process(strLine, mode);
            }
            else if (filter.DialogInfoEndFlag && strLine.Length == 0)
            {
                mode = EMode.InDialogInfo;
                _mFile.GoBackward(1);
            }
            else
            {
                filter.Process(strLine, mode);
            }
        }

        // Design Info内
        private void ProcessDesignInfoIn1(ResourceFilterBase filter, ref EMode mode, String strLine)
        {
            filter.Process(strLine, mode);
            if (_patternEndInDesignInfo1.IsMatch(strLine))
            {
                mode = EMode.InDesiginInfo;
            }
        }

        // Design Info内の1つ内
        private void ProcessDesignInfo(ResourceFilterBase filter, ref EMode mode, String strLine)
        {
            var match = _patternBeginInDesignInfo1.Match(strLine);
            if (match.Length > 0)
            {
                mode = EMode.InDesignInfoIn1;
                filter.BeginOutputName(mode, match.Value);
            }
            else if (_patternEndDialogInfo.IsMatch(strLine))
            {
                mode = EMode.Normal;
            }
            filter.Process(strLine, mode);
        }
    }

    // IDC_等を検出する
}
