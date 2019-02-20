using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace parking
{
    public class MasterRDImprot
    {
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="baud">波特率</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_init_com")]
        internal static extern int rf_init_com(ushort port, ushort baud);

        /// <summary>
        /// 关闭已打开的串口
        /// </summary>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_ClosePort")]
        internal static extern int rf_ClosePort();

        /// <summary>
        /// 读卡序列号,指定卡
        /// </summary>
        /// <param name="icdev">设备号,默认0</param>
        /// <param name="pData">接受返回的数据</param>
        /// <param name="retLen">返回接受到数据长度</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_s70_select")]
        internal static extern int rf_s70_select(byte icdev, byte[] pData, ref byte retLen);

        /// <summary>
        /// 使用指定秘钥读取指定块的内容
        /// </summary>
        /// <param name="icdev">设备号,默认0</param>
        /// <param name="ReadMode">读取模式,0: 读0 - 31块; 1:读32-38块; 2:读2块</param>
        /// <param name="address">读取块地址(s70取值 1-32)</param>
        /// <param name="KeyMode">秘钥模式, A密钥 =0x60 ,B密钥 =0x61</param>
        /// <param name="Key">6字节密钥</param>
        /// <param name="pData">返回读取块的内容</param>
        /// <param name="retLen">返回读取块内容的长度</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_s70_read")]
        internal static extern int rf_s70_read(byte icdev, byte ReadMode, byte address, byte KeyMode, byte[] Key, byte[] pData, ref ulong retLen);

        /// <summary>
        /// 使用指定秘钥写入指定块内容
        /// </summary>
        /// <param name="icdev">设备号:默认0</param>
        /// <param name="WriteMode">读取模式,0: 读0 - 31块; 1:读32-38块; 2:读2块(所有??)</param>
        /// <param name="address">读取块地址(s70取值 1-32)</param>
        /// <param name="KeyMode">秘钥模式, A密钥 =0x60 ,B密钥 =0x61</param>
        /// <param name="Key">6字节密钥</param>
        /// <param name="pData">要写入的内容(1块内容为16字节)</param>
        /// <param name="retLen">写入内容的长度</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_s70_write")]
        internal static extern int rf_s70_write(byte icdev, byte WriteMode, byte address, byte KeyMode, byte[] Key, byte[] pData, ulong retLen);

        /// <summary>
        /// 向读卡器写入密钥组
        /// </summary>
        /// <param name="icdev">设备号</param>
        /// <param name="block">密钥组索引号(取值0-31)</param>
        /// <param name="Key">密钥数据(6个字节)</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_M1_WriteKeyToEE2")]
        internal static extern int rf_M1_WriteKeyToEE2(byte icdev, byte block, byte[] Key);
        /// <summary>
        /// 对指定块核对秘钥
        /// </summary>
        /// <param name="icdev">设备号,默认0</param>
        /// <param name="KeyMode">密钥模式, A密钥 =0x60 ,B密钥 =0x61</param>
        /// <param name="block">指定块</param>
        /// <param name="secnr">密钥序号(密钥组索引 取值0-31)</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_M1_authentication1")]
        internal static extern int rf_M1_authentication1(byte icdev, byte KeyMode, byte block, byte secnr);

        /// <summary>
        /// 使用密钥组方式来读卡,执行此操作之前必须先核对秘钥
        /// </summary>
        /// <param name="icdev">设备号</param>
        /// <param name="block">块号</param>
        /// <param name="pData">返回读取的内容</param>
        /// <param name="pLen">返回读取内容的长度(字节)</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_M1_read")]
        internal static extern int rf_M1_read(byte icdev, byte block, byte[] pData, ref ulong pLen);

        /// <summary>
        /// 使用密码组模式写入数据,执行子操作必须先执行核对秘钥操作
        /// </summary>
        /// <param name="icdev">设备号</param>
        /// <param name="block">块号</param>
        /// <param name="pData">写入的数据(16字节)</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_M1_write")]
        internal static extern int rf_M1_write(byte icdev, byte block, byte[] pData);

        /// <summary>
        /// 读取M1卡序号,
        /// </summary>
        /// <param name="icdev">设备好 默认0</param>
        /// <param name="pData">返回的序号数据</param>
        /// <param name="pLen">数据长度</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_select1")]
        internal static extern int rf_select1(byte icdev, byte[] pData, ref ulong pLen);
        /// <summary>
        /// 核对M1卡密钥
        /// </summary>
        /// <param name="icdev">设备号,默认0</param>
        /// <param name="KeyMode">密钥模式, A密钥 =0x60 ,B密钥 =0x61 </param>
        /// <param name="block">块号</param>
        /// <param name="key">密钥字节</param>
        /// <returns>成功返回0</returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_M1_authentication2")]
        internal static extern int rf_M1_authentication2(byte icdev, byte KeyMode, byte block, byte[] key);

        /// <summary>
        /// 打开指定LED 等
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="color">0:所有灯亮,1:红灯亮,2:绿灯亮,3:所有灯亮</param>
        /// <returns></returns>
        [DllImport("MasterRD.dll", EntryPoint = "rf_light")]
        internal static extern int rf_light(byte icdev, byte color);

        [DllImport("MasterRD.dll", EntryPoint = "rf_beep")]
        internal static extern int rf_beep(byte icdev, int msec);
    }
}
