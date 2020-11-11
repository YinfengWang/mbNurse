using System; 
using System.Runtime.InteropServices; 
using System.Collections; 
using Microsoft.Win32; 


namespace HISPlus
{
    /// <summary>
    /// 输液法控制
    /// </summary>
    public class IME
    {
        #region API导入
        public static readonly uint KLF_ACTIVATE                = 1; 

        public static readonly int  IME_CMODE_FULLSHAPE         = 0x8;
        public static readonly int  IME_CHOTKEY_SHAPE_TOGGLE    = 0x11;

        [DllImport("user32")] 
　　　　public static extern uint ActivateKeyboardLayout(uint hkl, uint Flags); 
        
        [DllImport("user32")] 
        public static extern uint LoadKeyboardLayout(string pwszKLID,uint Flags); 
        
        [DllImport("user32")] 
        public static extern uint GetKeyboardLayoutList(int nBuff, uint[] List);

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        #endregion


        #region 变量
        private static Hashtable _imes;
        #endregion


        public IME()
        { 
        }

        
        /// <summary>
        /// 设定当前Ime, 使用方法Ime.SetImeName("中文 (简体) - 拼音加加"); 
        /// </summary>
        /// <param name="ImeName"></param>
        public static void SetIme(string imeName) 
        { 
            if (_imes == null) 
            GetImes(); 

            uint id = Convert.ToUInt32(_imes[imeName]); 
            SetIme(id); 
        } 


        public static void SetIme(uint imeId) 
        {
            if (imeId > 0)
            {
                ActivateKeyboardLayout(imeId, KLF_ACTIVATE);
            }
        } 


        /// <summary>
        /// 获得所有的Ime列表 
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetImes() 
        { 
            if (_imes==null) 
            {
                _imes = new Hashtable(); 
            }
            else 
            {
                return _imes; 
            }

            uint[]  kbList          = new uint[64]; 
            uint    totalKbLayout   = GetKeyboardLayoutList(64, kbList); 

            for (int i = 0; i < totalKbLayout; i++) 
            { 
                string      regKey  = String.Format("System\\CurrentControlSet\\Control\\Keyboard Layouts\\{0:X8}", kbList[i]); 
                RegistryKey rk      = Registry.LocalMachine.OpenSubKey(regKey);

                if (rk == null)
                {
                    continue;
                }

                string imeName=(string)rk.GetValue("layout text");
                if (imeName == null)
                {
                    continue;
                }

                _imes.Add(imeName, kbList[i]); 
            } 
            
            return _imes; 
        } 


        /// <summary>
        /// 输入法全角半角转换
        /// </summary>
        /// <param name="h"></param>
        public static void ChangeControlIme(IntPtr h)
        {
            IntPtr hIme = ImmGetContext(h);

            // 如果输入法处于打开状态
            if (ImmGetOpenStatus(hIme))  
            {
                //检索输入法信息
                int     iMode       = 0;
                int     iSentence   = 0;                
                bool    bSuccess    = ImmGetConversionStatus(hIme, ref iMode, ref iSentence);  

                if (bSuccess)
                {
                    // 如果是全角
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)
                    {
                        // 转换成半角
                        ImmSimulateHotKey(h, IME_CHOTKEY_SHAPE_TOGGLE);
                    }
                }
            }
        }
    }    
}
