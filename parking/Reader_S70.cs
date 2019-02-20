using System;
using System.Collections.Generic;
using System.Text;

namespace parking
{

    public enum ReadWriteTypeModeEnum
    {
        Read0To31 = 0,
        Read32TO38 = 1,
        ReadAll = 2
    }

    public enum KeyModeEnum
    {
        KeyA = 0x60,
        KeyB = 0x61
    }

    public enum UseKeyModeEnum
    {
        Key = 0,
        KeyGroup = 1
    }

    // Led    
    public enum LightColor
    {
        CloseLED = 0x00,
        RedLED = 0x01,
        GreenLED = 0x02, 
        AllLED = 0x03
    }

    public class Reader_S70
    {

        public byte[] GetWriteData(string dataStr)
        {
            if (dataStr.Length % 2 != 0)
            {
                throw new Exception("loi1");
            }
            try
            {
                List<byte> rList = new List<byte>();
                for (int i = 0; i < dataStr.Length; i += 2)
                {
                    byte d = Convert.ToByte(dataStr.Substring(i, 2), 16);
                    rList.Add(d);
                }
                return rList.ToArray();
            }
            catch
            {
                throw new Exception("loi2");
            }

        }

        public byte[] GetKeyData(string keyStr)
        {
            byte[] r = GetWriteData(keyStr);
            if (r.Length != 6)
            {
                throw new Exception("loi3");
            }
            return r;
        }

        public byte[] GetBlockData(string blockStr)
        {
            byte[] r = GetWriteData(blockStr);
            if (r.Length != 16)
            {
                throw new Exception("loi4");
            }
            return r;
        }
        
        public string GetStringByData(byte[] data)
        {
            if (data == null) return null;
            StringBuilder sbText = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sbText.Append(data[i].ToString("X2"));
            }
            return sbText.ToString();
        }
        
        public bool OpenCom(ushort comNo, ushort baud)
        {
            try
            {
                int r = MasterRDImprot.rf_init_com(comNo, baud);
                return r == 0 ? true : false;
            }
            catch
            {
                throw new Exception("loi5");
            }
        }
        
        public bool CloseCom()
        {
            try
            {
                int r = MasterRDImprot.rf_ClosePort();
                return r == 0 ? true : false;
            }
            catch
            {
                throw new Exception("loi6");
            }
        }
        
        public byte[] SelectCard()
        {
            byte[] pData = new byte[10];
            byte pdataLen = 0;
            int r = MasterRDImprot.rf_s70_select(0, pData, ref pdataLen);
            if (r == 0)
            {
                byte[] rData = new byte[pdataLen];
                for (int i = 0; i < pdataLen; i++)
                {
                    rData[i] = pData[i];
                }
                return rData;
            }
            return null;
        }
        
        public bool DownloadKeyToReader(byte blockNo, string keystr)
        {
            byte[] key = GetKeyData(keystr);
            int r = MasterRDImprot.rf_M1_WriteKeyToEE2(0, blockNo, key);
            if (r == 0)
            {
                return true;
            }
            return false;
        }
        
        public bool CheckReaderKey(byte keyModel, byte blockNo, byte keyGroupIndex)
        {
            int r = MasterRDImprot.rf_M1_authentication1(0, keyModel, blockNo, keyGroupIndex);
            return r == 0 ? true : false;
        }
        
        public byte[] ReadData(KeyModeEnum km, byte blokNo, string keyStr, out byte[] cardSerialData)
        {
            // byte readModel = (byte)rm;
            byte keyModel = (byte)km;
            byte[] key = GetKeyData(keyStr);
            byte[] pData = new byte[100];
            ulong pdataLen = 0;
            //Reading card serial number
            cardSerialData = SelectCard();
            if (cardSerialData == null) return null;
            int r;
            //check key
            r = MasterRDImprot.rf_M1_authentication2(0, keyModel, blokNo, key);

            r = MasterRDImprot.rf_M1_read(0, blokNo, pData, ref pdataLen);
            if (r == 0)
            {
                byte[] rData = new byte[pdataLen];
                for (int i = 0; i < (int)pdataLen; i++)
                {
                    rData[i] = pData[i];
                }
                return rData;
            }
            return null;
        }

        //Use a key group to read the card
        public byte[] ReadData(ReadWriteTypeModeEnum rm, KeyModeEnum km, byte blockNo, byte keyGroupIndex, out byte[] cardSerialData)
        {
            //byte readModel = (byte)rm;
            byte keyModel = (byte)km;
            cardSerialData = SelectCard();
            if (cardSerialData == null) return null;
            if (CheckReaderKey(keyModel, blockNo, keyGroupIndex))
            {
                byte[] pData = new byte[100];
                ulong pDataLen = 0;
                int r = MasterRDImprot.rf_M1_read(0, blockNo, pData, ref pDataLen);
                if (r == 0)
                {
                    byte[] resultData = new byte[pDataLen];
                    for (int i = 0; i < (int)pDataLen; i++)
                    {
                        resultData[i] = pData[i];
                    }
                    return resultData;
                }
            }
            return null;
        }

        //Use key to write data
        public bool WriteData(KeyModeEnum km, byte blockNo, string keyStr, string pDataStr)
        {
            //byte readModel = (byte)rm;
            byte keyModel = (byte)km;
            byte[] key = GetKeyData(keyStr);
            byte[] pData = GetBlockData(pDataStr);
            ulong pLen = (ulong)pData.Length;
            SelectCard();
            int r;
            //Check key
            r = MasterRDImprot.rf_M1_authentication2(0, keyModel, blockNo, key);

            r = MasterRDImprot.rf_M1_write(0, blockNo, pData);
            return r == 0 ? true : false;
        }

        //Use key groups to write data
        public bool WriteData(ReadWriteTypeModeEnum rm, KeyModeEnum km, byte blockNo, byte keyGroupIndex, string pDataStr)
        {
            byte readModel = (byte)rm;
            byte keyModel = (byte)km;
            SelectCard();
            if (CheckReaderKey(keyModel, blockNo, keyGroupIndex))
            {
                byte[] pData = GetWriteData(pDataStr);
                int r = MasterRDImprot.rf_M1_write(0, blockNo, pData);
                return r == 0 ? true : false;
            }
            return false;
        }


        public bool OpenLed(LightColor lc)
        {
            byte color = (byte)lc;
            int r = MasterRDImprot.rf_light(0, color);
            return r == 0 ? true : false;
        }
        public bool Beep(int msec)
        {
            int r = MasterRDImprot.rf_beep(0, msec);
            return r == 0 ? true : false;
        }
    }
}