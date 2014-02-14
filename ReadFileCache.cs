using System;
using System.Text;
using System.IO;

namespace VCResourceManager
{
    /*
     * ファイルを読み込むときにキャッシュして読み戻したりができるクラス
     */
    public class ReadFileCache : IDisposable
    {
        private StreamReader _sr;
        private int _mBackwardCount;
        private int _mBufferIndex;
        private const int MaxBuffer = 100;
        private readonly String[] _mBuffer = new String[MaxBuffer];

        public bool OpenFile(String strPath, Encoding enc)
        {
            if ( _sr != null ) 
                Close();

            try
            {
                _sr = new StreamReader(strPath, enc);
            }
            catch (IOException)
            {
                return false; 
            }
            _mBackwardCount = 0;
            _mBufferIndex = 0;
            return true;
        }

        public void Close() {
            if ( _sr != null) {
                _sr.Close();
                _sr.Dispose();
                _sr = null;
            }
        }

        public String ReadLine() {
            if ( _mBackwardCount == 0) {
                String strRead = _sr.ReadLine();
                _mBufferIndex = (_mBufferIndex+1) % MaxBuffer;
                _mBuffer[_mBufferIndex] = strRead;
                return strRead;
            }
            var nIndex = (_mBufferIndex + MaxBuffer - _mBackwardCount+1) % MaxBuffer;
            _mBackwardCount--;
            return _mBuffer[nIndex];
        }

        public void GoForward(int nCnt) {
            for( int ic=0; ic<nCnt; ++ic)
                ReadLine();
        }

        public void GoBackward() {
            ++_mBackwardCount;
            if ( _mBackwardCount >= MaxBuffer )
                throw new InternalBufferOverflowException("バッファが足りません");
        }

        public void GoBackward(int nCnt) {
            for( int ic=0; ic<nCnt; ++ic ) 
                GoBackward();
        }
    
        public void Dispose()
        {
            Close();
        }
    }
}
