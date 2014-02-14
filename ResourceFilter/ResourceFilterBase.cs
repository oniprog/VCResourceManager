using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VCResourceManager.ResourceFilter
{
    // Base抽象クラス
    public abstract class ResourceFilterBase
    {
        public abstract void Process( String strLine, ResourceFileMaster.EMode mode );
        public abstract void BeginLang(String strLang);
        public abstract void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName);
        public virtual void EndProcess() { }

        public bool bDialogInfoEndFlag;
    }


}
