using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCMasterBus2
{
    class Blob
    {
        public int size, start;

        public Blob(int blob_start, int blob_size)
        {
            start = blob_start;
            size = blob_size;
        }

        public void add(int blob_size)
        {
            this.size += blob_size;
        }
    }

    class BlobGroup
    {
        public int start, length;

        public BlobGroup(int pix_start, int pix_length)
        {
            start = pix_start;
            length = pix_length;
        }
    }
}
