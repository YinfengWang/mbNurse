using System; 
using System.Runtime.InteropServices; 
using System.Collections; 
using Microsoft.Win32; 


namespace HISPlus
{
    /// <summary>
    /// ��Һ������
    /// </summary>
    public class IME
    {
        #region API����
        public static readonly uint KLF_ACTIVATE                = 1; 

        public static readonly int  IME_CMODE_FULLSHAPE         = 0x8;
        public static readonly int  IME_CHOTKEY_SHAPE_TOGGLE    = 0x11;

        [DllImport("user32")] 
��������public static extern uint ActivateKeyboardLayout(uint hkl, uint Flags); 
        
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


        #region ����
        private static Hashtable _imes;
        #endregion


        public IME()
        { 
        }

        
        /// <summary>
        /// �趨��ǰIme, ʹ�÷���Ime.SetImeName("���� (����) - ƴ���Ӽ�"); 
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
        /// ������е�Ime�б� 
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
        /// ���뷨ȫ�ǰ��ת��
        /// </summary>
        /// <param name="h"></param>
        public static void ChangeControlIme(IntPtr h)
        {
            IntPtr hIme = ImmGetContext(h);

            // ������뷨���ڴ�״̬
            if (ImmGetOpenStatus(hIme))  
            {
                //�������뷨��Ϣ
                int     iMode       = 0;
                int     iSentence   = 0;                
                bool    bSuccess    = ImmGetConversionStatus(hIme, ref iMode, ref iSentence);  

                if (bSuccess)
                {
                    // �����ȫ��
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)
                    {
                        // ת���ɰ��
                        ImmSimulateHotKey(h, IME_CHOTKEY_SHAPE_TOGGLE);
                    }
                }
            }
        }
    }    
}
