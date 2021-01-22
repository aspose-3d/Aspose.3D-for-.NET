using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aspose.App.Api.Utils
{
    /// <summary>
    /// File stream decorator with limited output size.
    /// </summary>
    class LimitedStream : Stream
    {
        private FileStream baseStream;
        private string fileName;
        private long maximumSize;
        private long accumulatedSize;

        public override bool CanRead => baseStream != null && baseStream.CanRead;

        public override bool CanSeek => baseStream != null && baseStream.CanSeek;

        public override bool CanWrite => baseStream != null && baseStream.CanWrite;

        public override long Length => baseStream != null ? baseStream.Length : 0;

        public override long Position {
            get
            {
                EnsureNotClosed();
                return baseStream.Position;
            }
            set
            {
                EnsureNotClosed();
                baseStream.Position = value;
            }
        }

        public static LimitedStream CreateFile(string fileName, long maximumSize)
        {
            var baseStream = File.Create(fileName);
            return new LimitedStream(baseStream, fileName, maximumSize);
        }
        private LimitedStream(FileStream baseStream, string fileName, long maximumSize)
        {
            this.baseStream = baseStream;
            this.fileName = fileName;
            this.maximumSize = maximumSize;
        }


        private void EnsureNotClosed()
        {
            if (baseStream == null)
                throw new IOException("Base stream has been closed");
            
        }

        public override void Flush()
        {
            if(baseStream != null)
                baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            EnsureNotClosed();
            return baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            EnsureNotClosed();
            return baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            EnsureNotClosed();
            baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            EnsureNotClosed();
            if (accumulatedSize > maximumSize)
            {
                baseStream.Dispose();
                baseStream = null;
                File.Delete(fileName);
                throw new RemoteOpException("File output size has exceeded the maximum size");
            }
            else
            {
                accumulatedSize += count;
                baseStream.Write(buffer, offset, count);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(baseStream != null)
                baseStream.Dispose();
        }
    }
}
