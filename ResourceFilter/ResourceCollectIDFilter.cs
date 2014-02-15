using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VCResourceManager.ResourceFilter
{
    // IDを集める
    public class ResourceCollectIdFilter : ResourceFilterBase
    {
        private readonly Regex _mMatchId = new Regex(@"ID[^$\s(,]+");
        private readonly HashSet<string> _mSetId = new HashSet<string>();

        public HashSet<string> GetSetId()
        {
            return _mSetId;
        }

        public override void Process(String strLine, ResourceFileMaster.EMode mode)
        {
            var match = _mMatchId.Match(strLine);
            if (!match.Success)
                return;

            _mSetId.Add(match.Value);
        }
        public override void BeginLang(String strLang)
        {

        }
        public override void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName)
        {

        }
        public override void EndProcess() { }
    }

    class ResourceCollectIdFilterImpl : ResourceCollectIdFilter
    {
    }
}
