using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestApp
{
    class CharByteMap
    {
        public byte width;
        public byte height;
        byte[] bytes;
        public CharByteMap(byte width, byte height, byte[] bytes)
        {
            this.bytes = bytes;
            this.width = width;
            this.height = height;
        }

        public CharByteMap(FileStream fs, char c)
        {
            byte[] pointer = new byte[4];
            fs.Position = 4 * (int)c;
            fs.Read(pointer, 0, 4);

            fs.Position = BitConverter.ToInt32(pointer, 0);
            width = (byte)fs.ReadByte();
            height = (byte)fs.ReadByte();

            bytes = new byte[(width * height) / 8 + ((width * height) > 0 ? 1 : 0)];

            fs.Read(bytes, 0, bytes.Length);

        }

        public byte[] Compress()
        {
            byte[] _array = new byte[bytes.Length / 8 + (bytes.Length % 8 > 0 ? 1 : 0)];
            byte _bytes=0;
            byte _bit=0;
            int _index = 0;
            foreach (byte b in bytes)
            {
                byte _this = (byte)(b == 0xFF ? 1 : 0);

                _bytes = (byte)((_bytes << 1) | _this);
                _bit++;
                
                if (_bit == 8)
                {
                    _array[_index++] = _bytes;
                    _bytes = _bit = 0;
                }
            }

            if(_bit != 0)
                _array[_index++] = _bytes;

            bytes = _array;
            return bytes;
        }

        public void PrintToByteMap(byte[] bm, int w, int x, int y)
        {
            for (int _y = 0; _y < height; _y++)
            {
                for (int _x = 0; _x < width; _x++)
                {
                    int addr = (_y * width + _x);

                    if (((bytes[addr / 8] >> (8 - (addr % 8) - 1)) & 1) == 1)
                    {

                        int target_addr = x + _x + (y + _y) * w;

                        if (target_addr >= bm.Length || target_addr < 0)
                            continue;//Out of Array

                        bm[target_addr] = 255;
                    }
                }
            }
        }
    }
}
