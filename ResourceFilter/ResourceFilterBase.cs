using System;

namespace VCResourceManager.ResourceFilter
{
    // Base抽象クラス
    public abstract class ResourceFilterBase
    {
        public abstract void Process( String strLine, ResourceFileMaster.EMode mode );
        public abstract void BeginLang(String strLang);
        public abstract void BeginOutputName(ResourceFileMaster.EMode mode, String strOutputName);
        public virtual void EndProcess() { }

        public bool DialogInfoEndFlag;
    }


}
