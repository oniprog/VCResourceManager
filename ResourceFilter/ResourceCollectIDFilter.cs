using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VCResourceManager.ResourceFilter
{
    // IDを集める
    public class ResourceCollectIDFilter : ResourceFilterBase
    {
        private Regex mMatchID = new Regex(@"ID[^$\s(,]+");
        private HashSet<string> mSetID = new HashSet<string>();

        public HashSet<string> GetSetID()
        {
            return mSetID;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            var match = mMatchID.Match(strLine);
            if (!match.Success)
                return;

            mSetID.Add(match.Value);
        }
        public override void BeginLang(String strLang)
        {

        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {

        }
        public override void EndProcess() { }
    }
}
