using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Framework.Utility;

namespace Framework.DBFFactory
{
    public class DBFHelper
    {

        #region Persian String From DBF File
        public static string GetBBSStr(string str)
        {

            //int[] ic;
            string[] chars = CustomCache.Get("PersianChars") as string[];
            //var ia = Ints(out ic);
            if (chars == null)
            {
                PersianChars(out chars);
            }
            bool flag = false;
            string strRes = "";
            try
            {
                //str = Conversions.ToString(str.Trim());
                //str = str.Trim();

                //str = Rev(str);
               
                long num2 = str.Length;
                for (long i = num2; i > 0; i --)
                {
                    string mid = Mid(str, (int) i, 1);
                    var strTemp = Utility.Extention.SafeString(Asc(mid));
                    if (strTemp.SafeInt() < 0x80)
                    {
                        strTemp = mid;
                    }
                    else
                    {
                        strTemp = chars[strTemp.SafeInt()];
                        flag = true;
                    }
                    strRes += strTemp;
                }
            }
            catch (Exception)
            {
                throw;
            }
            if(flag)
            {
                return NumRev(strRes);
            }
            return strRes;
        }

        private static void PersianChars(out string[] codes)
        {
            codes = new string[10000];
            codes[141] = "آ";
            codes[142] = "ئـ";
            codes[143] = "ء";
            codes[144] = "ا";
            codes[145] = "ـا";
            codes[146] = "ب";
            codes[147] = "بـ";
            codes[148] = "پ";
            codes[149] = "پـ";
            codes[150] = "ت";
            codes[151] = "تـ";
            codes[152] = "ث";
            codes[153] = "ثـ";
            codes[154] = "ج";
            codes[155] = "جـ";
            codes[156] = "چ";
            codes[157] = "چـ";
            codes[158] = "ح";
            codes[159] = "حـ";
            codes[160] = "خ";
            codes[161] = "خـ";
            codes[162] = "د";
            codes[163] = "ذ";
            codes[164] = "ر";
            codes[165] = "ز";
            codes[166] = "ژ";
            codes[167] = "س";
            codes[168] = "سـ";
            codes[169] = "ش";
            codes[170] = "شـ";
            codes[171] = "ص";
            codes[172] = "صـ";
            codes[173] = "ض";
            codes[174] = "ضـ";
            codes[175] = "ط";
            codes[224] = "ظ";
            codes[225] = "ع";
            codes[226] = "ـع";
            codes[227] = "ـعـ";
            codes[228] = "عـ";
            codes[229] = "غ";
            codes[230] = "ـغ";
            codes[231] = "ـغـ";
            codes[232] = "غـ";
            codes[233] = "ف";
            codes[234] = "فـ";
            codes[235] = "ق";
            codes[236] = "قـ";
            codes[237] = "ک";
            codes[238] = "کـ";
            codes[239] = "گ";
            codes[240] = "گـ";
            codes[241] = "ل";
            codes[242] = "لا";
            codes[243] = "لـ";
            codes[244] = "م";
            codes[245] = "مـ";
            codes[246] = "ن";
            codes[247] = "نـ";
            codes[248] = "و";
            codes[249] = "ه";
            codes[250] = "ـهـ";
            codes[251] = "هـ";
            codes[252] = "ی";
            codes[253] = "ی";
            codes[254] = "یـ";
            CustomCache.Insert("PersianChars", codes);
        }

        private static int[] Ints(out int[] ic)
        {
            int[] ia = new int[100000];
            ia[0] = 1000000000;

            ic = new int[100000];
            ic[0] = 1000000000;
            ic[230] = -1;

            ia[32] = 160;
            ic[32] = -1;
            ia[141] = 194;
            ia[142] = 198;
            ia[143] = 193;
            ia[144] = 199;
            ia[145] = 199;
            ia[146] = 200;
            ic[146] = -1;
            ia[147] = 200;
            ia[148] = 129;
            ic[148] = -1;
            ia[149] = 129;
            ia[150] = 202;
            ic[150] = -1;
            ia[151] = 202;
            ia[152] = 203;
            ic[152] = -1;
            ia[153] = 203;
            ia[154] = 204;
            ic[154] = -1;
            ia[155] = 204;
            ia[156] = 141;
            ic[156] = -1;
            ia[157] = 141;
            ia[158] = 205;
            ic[158] = -1;
            ia[159] = 205;
            ia[160] = 206;
            ic[160] = -1;
            ia[161] = 206;
            ia[162] = 207;
            ia[163] = 208;
            ia[164] = 209;
            ia[165] = 210;
            ia[166] = 142;
            ia[167] = 211;
            ic[167] = -1;
            ia[168] = 211;
            ia[169] = 212;
            ic[169] = -1;
            ia[170] = 212;
            ia[171] = 213;
            ic[171] = -1;
            ia[172] = 213;
            ia[173] = 214;
            ic[173] = -1;
            ia[174] = 214;
            ia[175] = 216;
            ia[224] = 217;
            ia[225] = 218;
            ic[225] = -1;
            ia[226] = 218;
            ic[226] = -1;
            ia[227] = 218;
            ia[228] = 218;
            ia[229] = 219;
            ic[229] = -1;
            ia[230] = 219;
            ic[230] = -1;
            ia[231] = 219;
            ia[232] = 219;
            ia[233] = 221;
            ic[233] = -1;
            ia[234] = 221;
            ia[235] = 222;
            ic[235] = -1;
            ia[236] = 222;
            ia[237] = 223;
            ic[237] = -1;
            ia[238] = 223;
            ia[239] = 144;
            ic[239] = -1;
            ia[240] = 144;
            ia[241] = 225;
            ic[241] = -1;
            ia[243] = 225;
            ia[244] = 227;
            ic[244] = -1;
            ia[245] = 227;
            ia[246] = 228;
            ic[246] = -1;
            ia[247] = 247;
            ia[248] = 230;
            ia[249] = 229;
            ic[249] = -1;
            ia[250] = 229;
            ia[251] = 229;
            ia[252] = 237;
            ic[252] = -1;
            ia[253] = 237;
            ic[253] = -1;
            ia[254] = 237;

            ia[128] = (int)('0');
            ia[129] = (int)('1');
            ia[130] = (int)('2');
            ia[131] = (int)('3');
            ia[132] = (int)('4');
            ia[133] = (int)('5');
            ia[134] = (int)('6');
            ia[135] = (int)('7');
            ia[136] = (int)('8');
            ia[137] = (int)('9');

            ia[(int)('(')] = (int)(')');
            ia[(int)(')')] = (int)('(');
            return ia;
        }

        public static string GetTckStr(string str)
        {
            short[] numArray = new short[0x105];
            bool[] flagArray = new bool[0x105];
            numArray[0x20] = 0xfd;
            flagArray[0x20] = true;
            numArray[160] = 0xfd;
            flagArray[160] = true;
            numArray[0x80] = 160;
            numArray[0x81] = 160;
            numArray[130] = 0x2a;
            numArray[0x83] = 0xba;
            numArray[0x84] = 0x3f;
            numArray[0x85] = 0xed;
            numArray[0x86] = 0xc2;
            numArray[0x87] = 0xc7;
            numArray[0x88] = 0xd1;
            numArray[0x89] = 0xc7;
            numArray[0x8a] = 0xc7;
            numArray[0x8b] = 0xc7;
            numArray[140] = 0xc1;
            numArray[0x8d] = 0xc7;
            numArray[0x8e] = 0xc7;
            numArray[0x8f] = 0xdf;
            numArray[0x90] = 0xc4;
            numArray[0x91] = 0xc6;
            numArray[0x92] = 200;
            flagArray[0x92] = true;
            numArray[0x93] = 200;
            numArray[0x94] = 0x81;
            flagArray[0x94] = true;
            numArray[0x95] = 0x81;
            numArray[150] = 0xca;
            flagArray[150] = true;
            numArray[0x97] = 0xca;
            flagArray[0x97] = true;
            numArray[0x98] = 0xcb;
            flagArray[0x98] = true;
            numArray[0x99] = 0xcb;
            numArray[0x9a] = 0xcc;
            flagArray[0x9a] = true;
            numArray[0x9b] = 0xcc;
            numArray[0x9c] = 0x8d;
            flagArray[0x9c] = true;
            numArray[0x9d] = 0x8d;
            numArray[0x9e] = 0xd7;
            flagArray[0x9e] = true;
            numArray[0x9f] = 0xcd;
            numArray[160] = 0xcd;
            flagArray[160] = true;
            numArray[0xa1] = 0xce;
            numArray[0xa2] = 0xce;
            numArray[0xa3] = 0xcf;
            numArray[0xa4] = 0xd0;
            numArray[0xa5] = 0xd1;
            numArray[0xa6] = 210;
            numArray[0xa7] = 0x8e;
            flagArray[0xa7] = true;
            numArray[0xa8] = 0xd3;
            numArray[0xa9] = 0xd3;
            flagArray[0xa9] = true;
            numArray[170] = 0xd4;
            numArray[0xab] = 0xd4;
            flagArray[0xab] = true;
            numArray[0xac] = 0xd5;
            numArray[0xad] = 0xd5;
            flagArray[0xad] = true;
            numArray[0xae] = 0x9e;
            numArray[0xaf] = 0x9e;
            numArray[0xb5] = 0xd6;
            numArray[0xb6] = 0xd6;
            numArray[0xb7] = 0xd8;
            numArray[0xb8] = 0xd8;
            numArray[0xb8] = 0xd8;
            numArray[0xb9] = 0xb9;
            numArray[0xba] = 0xb9;
            numArray[0xbb] = 0xb9;
            numArray[0xbc] = 0xb9;
            numArray[0xbd] = 0xb9;
            numArray[190] = 0xd9;
            numArray[0xc6] = 0xd9;
            numArray[0xc7] = 0xda;
            numArray[200] = 0xfd;
            numArray[0xc9] = 0xfd;
            numArray[0xca] = 0xfd;
            numArray[0xcb] = 0xfd;
            numArray[0xcc] = 0xfd;
            numArray[0xcd] = 0xfd;
            numArray[0xce] = 0xfd;
            numArray[0xcf] = 0xfd;
            numArray[0xd0] = 0xda;
            numArray[0xd1] = 0xda;
            numArray[210] = 0xda;
            numArray[0xd3] = 0xdb;
            numArray[0xd4] = 0xdb;
            numArray[0xd5] = 0xdb;
            numArray[0xd6] = 0xdb;
            numArray[0xd7] = 0xdd;
            numArray[0xd8] = 0xdd;
            numArray[0xd9] = 0xfd;
            numArray[0xda] = 0xfd;
            numArray[0xdb] = 0xfd;
            numArray[220] = 0xfd;
            numArray[0xdd] = 0xde;
            numArray[0xde] = 0xde;
            numArray[0xdf] = 0xfd;
            numArray[0xe0] = 0xdf;
            numArray[0xe1] = 0xdf;
            flagArray[0xe1] = true;
            numArray[0xe2] = 0x90;
            flagArray[0xe2] = true;
            numArray[0xe3] = 0x90;
            numArray[0xe4] = 0xe1;
            numArray[0xe5] = 0xe1;
            flagArray[0xe5] = true;
            numArray[230] = 0xe3;
            flagArray[230] = true;
            numArray[0xe7] = 0xe3;
            numArray[0xe8] = 0xe4;
            numArray[0xe9] = 0xe4;
            flagArray[0xe9] = true;
            numArray[0xea] = 230;
            numArray[0xeb] = 0xe5;
            flagArray[0xeb] = true;
            numArray[0xec] = 0xe5;
            numArray[0xed] = 0xe5;
            flagArray[0xed] = true;
            numArray[0xee] = 0xe5;
            numArray[0xef] = 0xed;
            flagArray[0xef] = true;
            numArray[0xf1] = 0xed;
            flagArray[0xf1] = true;
            numArray[0xf2] = 0xed;
            numArray[0xf3] = 160;
            numArray[0xf4] = 0x30;
            flagArray[0xf4] = true;
            numArray[0xf5] = 0x31;
            numArray[0xf6] = 50;
            flagArray[0xf6] = true;
            numArray[0xf7] = 0x33;
            numArray[0xf8] = 0x34;
            numArray[0xf9] = 0x35;
            flagArray[0xf9] = true;
            numArray[250] = 0x36;
            numArray[0xfb] = 0x37;
            numArray[0xfc] = 0x38;
            flagArray[0xfc] = true;
            numArray[0xfd] = 0x39;
            flagArray[0xfd] = true;
            numArray[0xfe] = 160;
            numArray[0xff] = 160;
            numArray[40] = 0x29;
            numArray[0x29] = 40;
            str = Rev(str);
            string strOut = "";
            short num2 = (short)str.Length;
            for (short i = 1; i <= num2; i = (short)(i + 1))
            {
                string strTemp = Conversions.ToString(Asc(Mid(str, i, 1)));
                string strTempTwo = Conversions.ToString((int)numArray[Conversions.ToInteger(strTemp)]);
                if (Convert.ToDouble(strTempTwo) == 0.0)
                {
                    strOut = strOut + Mid(str, i, 1);
                }
                else
                {
                    strOut = strOut + Conversions.ToString(Chr(Conversions.ToInteger(strTempTwo)));
                    if (flagArray[Conversions.ToInteger(strTemp)])
                    {
                        strOut = strOut + "  ";
                    }
                }
            }
            return NumRev(strOut);
        }

        private static string Rev(string s)
        {
            string str2 = "";
            int num2 = s.Length;
            for (int i = 1; i <= num2; i++)
            {
                str2 = Mid(s, i, 1) + str2;
            }
            return str2;
        }

        private static string Mid(string str, int start, int Length)
        {
            //if (start <= 0)
            //{
            //    //throw new BussinessException("InvalidString");
            //}
            //if (Length < 0)
            //{
            //    //throw new BussinessException("InvalidString");
            //}
            if ((Length == 0) || (str == null))
            {
                return "";
            }
            int length = str.Length;
            if (start > length)
            {
                return "";
            }
            if ((start + Length) > length)
            {
                return str.Substring(start - 1);
            }
            return str.Substring(start - 1, Length);
        }

        private static int Asc(char String)
        {
            int num;
            int num2 = Convert.ToInt32(String);
            if (num2 < 0x80)
            {
                return num2;
            }
            try
            {
                byte[] buffer;
                Encoding fileIoEncoding = Encoding.Default;
                var chars = new[] { String };
                if (fileIoEncoding.IsSingleByte)
                {
                    buffer = new byte[1];
                    int num3 = fileIoEncoding.GetBytes(chars, 0, 1, buffer, 0);
                    return buffer[0];
                }
                buffer = new byte[2];
                if (fileIoEncoding.GetBytes(chars, 0, 1, buffer, 0) == 1)
                {
                    return buffer[0];
                }
                if (BitConverter.IsLittleEndian)
                {
                    byte num4 = buffer[0];
                    buffer[0] = buffer[1];
                    buffer[1] = num4;
                }
                num = BitConverter.ToInt16(buffer, 0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        private static int Asc(string str)
        {
            char ch = str[0];
            return Asc(ch);
        }

        private static char Chr(int charCode)
        {
            char ch;
            //if ((charCode < -32768) || (charCode > 0xffff))
            //{
            //    //t/hrow new BussinessException("InvalidString");
            //}
            if ((charCode >= 0) && (charCode <= 0x7f))
            {
                return Convert.ToChar(charCode);
            }
            try
            {
                int num;
                Encoding encoding = Encoding.GetEncoding(1256);
                //if (encoding.IsSingleByte && ((charCode < 0) || (charCode > 0xff)))
                //{
                //    //throw new BussinessException("InvalidString");
                //}
                char[] chars = new char[2];
                byte[] bytes = new byte[2];
                Decoder decoder = encoding.GetDecoder();
                if ((charCode >= 0) && (charCode <= 0xff))
                {
                    bytes[0] = (byte)(charCode & 0xff);
                    num = decoder.GetChars(bytes, 0, 1, chars, 0);
                }
                else
                {
                    bytes[0] = (byte)((charCode & 0xff00) >> 8);
                    bytes[1] = (byte)(charCode & 0xff);
                    num = decoder.GetChars(bytes, 0, 2, chars, 0);
                }
                ch = chars[0];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return ch;
        }

        private static string NumRev(string s)
        {
            bool flag = true;
            long num3 = 1L;
            long num2 = 1L;
            long num = 0L;
            string str2 = "";
            long num4 = s.Length;
            num = 1L;
            while (num <= num4)
            {
                str2 = Mid(s, (int)num, 1);
                if (((Asc(str2) >= 0x30) & (Asc(str2) <= 0x39)) & flag)
                {
                    flag = false;
                    num3 = num;
                }
                if (((Asc(str2) < 0x30) | (Asc(str2) > 0x39)) & !flag)
                {
                    flag = true;
                    num2 = num;
                    MidStmtStr(ref s, (int)num3, (int)(num2 - num3), Rev(Mid(s, (int)num3, (int)(num2 - num3))));
                }
                num += 1L;
            }
            if (!flag)
            {
                num2 = num;
                MidStmtStr(ref s, (int)num3, (int)(num2 - num3), Rev(Mid(s, (int)num3, (int)(num2 - num3))));
            }
            return s;
        }

        private static void MidStmtStr(ref string sDest, int startPosition, int maxInsertLength, string sInsert)
        {
            int length = 0;
            int num3 = 0;
            if (sDest != null)
            {
                length = sDest.Length;
            }
            if (sInsert != null)
            {
                num3 = sInsert.Length;
            }
            startPosition--;
            if ((startPosition < 0) || (startPosition >= length))
            {
                // throw new BussinessException("s");
            }
            if (maxInsertLength < 0)
            {
                // throw new BussinessException("s");
            }
            if (num3 > maxInsertLength)
            {
                num3 = maxInsertLength;
            }
            if (num3 > (length - startPosition))
            {
                num3 = length - startPosition;
            }
            if (num3 != 0)
            {
                StringBuilder builder = new StringBuilder(length);
                if (startPosition > 0)
                {
                    builder.Append(sDest, 0, startPosition);
                }
                builder.Append(sInsert, 0, num3);
                int count = length - (startPosition + num3);
                if (count > 0)
                {
                    builder.Append(sDest, startPosition + num3, count);
                }
                sDest = builder.ToString();
            }
        }


        #endregion

        ////////////////////////////////Parse DBF File////////////////////////////
        // This is the file header for a DBF.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct DBFHeader
        {
            private readonly byte version;
            private readonly byte updateYear;
            private readonly byte updateMonth;
            private readonly byte updateDay;
            public readonly Int32 numRecords;
            public readonly Int16 headerLen;
            public readonly Int16 recordLen;
            private readonly Int16 reserved1;
            private readonly byte incompleteTrans;
            private readonly byte encryptionFlag;
            private readonly Int32 reserved2;
            private readonly Int64 reserved3;
            private readonly byte MDX;
            private readonly byte language;
            private readonly Int16 reserved4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct FieldDescriptor
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public readonly string fieldName;
            public readonly char fieldType;
            private readonly Int32 address;
            public readonly byte fieldLen;
            private readonly byte count;
            private readonly Int16 reserved1;
            private readonly byte workArea;
            private readonly Int16 reserved2;
            private readonly byte flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            private readonly byte[] reserved3;
            private readonly byte indexFlag;
        }

        // Read an entire standard DBF file into a DataTable
        public static DataTable ReadDBF(string dbfFile)
        {
            //long start = DateTime.Now.Ticks;
            DataTable dt = new DataTable();
            BinaryReader recReader;
            string number;
            string year;
            string month;
            string day;
            long lDate;
            long lTime;
            DataRow row;
            int fieldIndex;

            // If there isn't even a file, just return an empty DataTable
            //BRule.Assert(File.Exists(dbfFile),Resource.GetString("FileNotExists")); 
            if ((false == File.Exists(dbfFile)))
            {
                return dt;
            }

            BinaryReader br = null;
            try
            {
                // Read the header into a buffer
                br = new BinaryReader(File.OpenRead(dbfFile));
                byte[] buffer = br.ReadBytes(Marshal.SizeOf(typeof(DBFHeader)));

                // Marshall the header into a DBFHeader structure
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                DBFHeader header = (DBFHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DBFHeader));
                handle.Free();


                ArrayList fields = new ArrayList();
                while ((13 != br.PeekChar()))
                {
                    buffer = br.ReadBytes(Marshal.SizeOf(typeof(FieldDescriptor)));
                    handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    fields.Add((FieldDescriptor)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FieldDescriptor)));
                    handle.Free();
                }


                ((Stream)br.BaseStream).Seek(header.headerLen + 1, SeekOrigin.Begin);
                buffer = br.ReadBytes(header.recordLen);
                recReader = new BinaryReader(new MemoryStream(buffer));


                DataColumn col = null;
                foreach (FieldDescriptor field in fields)
                {
                    number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                    switch (field.fieldType)
                    {
                        case 'N':
                            if (number.IndexOf(".") > -1)
                            {
                                col = new DataColumn(field.fieldName, typeof(decimal));
                            }
                            else
                            {
                                col = new DataColumn(field.fieldName, typeof(int));
                            }
                            break;
                        case 'C':
                            col = new DataColumn(field.fieldName, typeof(string));
                            break;
                        case 'T':

                            //col = new DataColumn(field.fieldName, typeof(string));
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'D':
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'L':
                            col = new DataColumn(field.fieldName, typeof(bool));
                            break;
                        case 'F':
                            col = new DataColumn(field.fieldName, typeof(Double));
                            break;
                    }
                    dt.Columns.Add(col);
                }

                // Skip past the end of the header. 
                ((Stream)br.BaseStream).Seek(header.headerLen, SeekOrigin.Begin);
                int counter = -1;
                // Read in all the records
                while (counter < header.numRecords - 1)
                {
                    counter++;
                    buffer = br.ReadBytes(header.recordLen);
                    recReader = new BinaryReader(new MemoryStream(buffer));

                    if (recReader.ReadChar() == '*')
                    {
                        continue;
                    }

                    fieldIndex = 0;
                    row = dt.NewRow();
                    foreach (FieldDescriptor field in fields)
                    {
                        switch (field.fieldType)
                        {
                            case 'N':  // Number
                                number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                if (IsNumber(number))
                                {
                                    if (number.IndexOf(".") > -1)
                                    {
                                        row[fieldIndex] = decimal.Parse(number);
                                    }
                                    else
                                    {
                                        row[fieldIndex] = int.Parse(number);
                                    }
                                }
                                else
                                {
                                    row[fieldIndex] = 0;
                                }

                                break;

                            case 'C': // String
                                row[fieldIndex] = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                break;

                            case 'D': // Date (YYYYMMDD)
                                year = Encoding.Default.GetString(recReader.ReadBytes(4));
                                month = Encoding.Default.GetString(recReader.ReadBytes(2));
                                day = Encoding.Default.GetString(recReader.ReadBytes(2));
                                row[fieldIndex] = System.DBNull.Value;
                                try
                                {
                                    if (IsNumber(year) && IsNumber(month) && IsNumber(day))
                                    {
                                        if ((Int32.Parse(year) > 1900))
                                        {
                                            row[fieldIndex] = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                                        }
                                    }
                                }
                                catch
                                { }

                                break;

                            case 'T': // Timestamp, 8 bytes - two integers, first for date, second for time
                                lDate = recReader.ReadInt32();
                                lTime = recReader.ReadInt32() * 10000L;
                                row[fieldIndex] = JulianToDateTime(lDate).AddTicks(lTime);
                                break;

                            case 'L': // Boolean (Y/N)
                                if ('Y' == recReader.ReadByte())
                                {
                                    row[fieldIndex] = true;
                                }
                                else
                                {
                                    row[fieldIndex] = false;
                                }

                                break;

                            case 'F':
                                number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                if (IsNumber(number))
                                {
                                    row[fieldIndex] = double.Parse(number);
                                }
                                else
                                {
                                    row[fieldIndex] = 0.0F;
                                }
                                break;
                        }
                        fieldIndex++;
                    }

                    recReader.Close();
                    dt.Rows.Add(row);
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (null != br)
                {
                    br.Close();
                }
            }

            return dt;
        }
        public static DataTable ReadDBF(Stream dbfFileStream)
        {
            //long start = DateTime.Now.Ticks;
            DataTable dt = new DataTable();
            BinaryReader recReader;
            string number;
            string year;
            string month;
            string day;
            long lDate;
            long lTime;
            DataRow row;
            int fieldIndex;


            BinaryReader br = null;
            try
            {
                // Read the header into a buffer
                br = new BinaryReader(dbfFileStream);
                byte[] buffer = br.ReadBytes(Marshal.SizeOf(typeof(DBFHeader)));

                // Marshall the header into a DBFHeader structure
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                DBFHeader header = (DBFHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DBFHeader));
                handle.Free();


                ArrayList fields = new ArrayList();
                while ((13 != br.PeekChar()))
                {
                    buffer = br.ReadBytes(Marshal.SizeOf(typeof(FieldDescriptor)));
                    handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    fields.Add((FieldDescriptor)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FieldDescriptor)));
                    handle.Free();
                }


                ((Stream)br.BaseStream).Seek(header.headerLen + 1, SeekOrigin.Begin);
                buffer = br.ReadBytes(header.recordLen);
                recReader = new BinaryReader(new MemoryStream(buffer));


                DataColumn col = null;
                foreach (FieldDescriptor field in fields)
                {
                    number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                    switch (field.fieldType)
                    {
                        case 'N':
                            if (number.IndexOf(".") > -1)
                            {
                                col = new DataColumn(field.fieldName, typeof(decimal));
                            }
                            else
                            {
                                col = new DataColumn(field.fieldName, typeof(int));
                            }
                            break;
                        case 'C':
                            col = new DataColumn(field.fieldName, typeof(string));
                            break;
                        case 'T':

                            //col = new DataColumn(field.fieldName, typeof(string));
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'D':
                            col = new DataColumn(field.fieldName, typeof(DateTime));
                            break;
                        case 'L':
                            col = new DataColumn(field.fieldName, typeof(bool));
                            break;
                        case 'F':
                            col = new DataColumn(field.fieldName, typeof(Double));
                            break;
                    }
                    dt.Columns.Add(col);
                }

                // Skip past the end of the header. 
                ((Stream)br.BaseStream).Seek(header.headerLen, SeekOrigin.Begin);
                int counter = -1;
                // Read in all the records
                while (counter < header.numRecords - 1)
                {
                    counter++;
                    buffer = br.ReadBytes(header.recordLen);
                    recReader = new BinaryReader(new MemoryStream(buffer));

                    if (recReader.ReadChar() == '*')
                    {
                        continue;
                    }

                    fieldIndex = 0;
                    row = dt.NewRow();
                    foreach (FieldDescriptor field in fields)
                    {
                        switch (field.fieldType)
                        {
                            case 'N':  // Number
                                number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                if (IsNumber(number))
                                {
                                    if (number.IndexOf(".") > -1)
                                    {
                                        row[fieldIndex] = decimal.Parse(number);
                                    }
                                    else
                                    {
                                        row[fieldIndex] = int.Parse(number);
                                    }
                                }
                                else
                                {
                                    row[fieldIndex] = 0;
                                }

                                break;

                            case 'C': // String
                                row[fieldIndex] = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                break;

                            case 'D': // Date (YYYYMMDD)
                                year = Encoding.Default.GetString(recReader.ReadBytes(4));
                                month = Encoding.Default.GetString(recReader.ReadBytes(2));
                                day = Encoding.Default.GetString(recReader.ReadBytes(2));
                                row[fieldIndex] = System.DBNull.Value;
                                try
                                {
                                    if (IsNumber(year) && IsNumber(month) && IsNumber(day))
                                    {
                                        if ((Int32.Parse(year) > 1900))
                                        {
                                            row[fieldIndex] = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                                        }
                                    }
                                }
                                catch
                                { }

                                break;

                            case 'T': // Timestamp, 8 bytes - two integers, first for date, second for time
                                lDate = recReader.ReadInt32();
                                lTime = recReader.ReadInt32() * 10000L;
                                row[fieldIndex] = JulianToDateTime(lDate).AddTicks(lTime);
                                break;

                            case 'L': // Boolean (Y/N)
                                if ('Y' == recReader.ReadByte())
                                {
                                    row[fieldIndex] = true;
                                }
                                else
                                {
                                    row[fieldIndex] = false;
                                }

                                break;

                            case 'F':
                                number = Encoding.Default.GetString(recReader.ReadBytes(field.fieldLen));
                                if (IsNumber(number))
                                {
                                    row[fieldIndex] = double.Parse(number);
                                }
                                else
                                {
                                    row[fieldIndex] = 0.0F;
                                }
                                break;
                        }
                        fieldIndex++;
                    }

                    recReader.Close();
                    dt.Rows.Add(row);
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (null != br)
                {
                    br.Close();
                }
            }

            return dt;
        }

        public static string ConvertDBFToXML(string dbfFile)
        {
            DataSet dataSet = new DataSet("Trades");
            DataTable dt = ReadDBF(dbfFile);
            dt.TableName = "Trade";
            dataSet.Tables.Add(dt);
            return dataSet.GetXml();
        }

        public static string ConvertDBFToXML(Stream dbfFileStream)
        {
            DataSet dataSet = new DataSet("Trades");
            DataTable dt = ReadDBF(dbfFileStream);
            dt.TableName = "Trade";
            dataSet.Tables.Add(dt);
            return dataSet.GetXml();
        }

        private static bool IsNumber(string numberString)
        {
            char[] numbers = numberString.ToCharArray();
            int numberCount = 0;
            int pointCount = 0;

            foreach (char number in numbers)
            {
                if ((number >= 48 && number <= 57))
                {
                    numberCount += 1;
                }
                else if (number == 46)
                {
                    pointCount += 1;
                }
                else if (number == 32)
                {
                }
                else
                {
                    return false;
                }
            }

            return (numberCount > 0 && pointCount < 2);
        }

        /// <summary>
        /// Convert a Julian Date to a .NET DateTime structure
        /// Implemented from pseudo code at http://en.wikipedia.org/wiki/Julian_day
        /// </summary>
        /// <param name="lJDN">Julian Date to convert (days since 01/01/4713 BC)</param>
        /// <returns>DateTime</returns>
        private static DateTime JulianToDateTime(long lJDN)
        {
            double p = Convert.ToDouble(lJDN);
            double s1 = p + 68569;
            double n = Math.Floor(4 * s1 / 146097);
            double s2 = s1 - Math.Floor((146097 * n + 3) / 4);
            double i = Math.Floor(4000 * (s2 + 1) / 1461001);
            double s3 = s2 - Math.Floor(1461 * i / 4) + 31;
            double q = Math.Floor(80 * s3 / 2447);
            double d = s3 - Math.Floor(2447 * q / 80);
            double s4 = Math.Floor(q / 11);
            double m = q + 2 - 12 * s4;
            double j = 100 * (n - 49) + i + s4;
            return new DateTime(Convert.ToInt32(j), Convert.ToInt32(m), Convert.ToInt32(d));
        }
    }
}
